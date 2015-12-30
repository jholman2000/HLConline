Imports Microsoft.VisualBasic

Public Class hlc_DoctorNote

    Public ErrorMessage As String = ""
    Public ID As Integer
    Public DoctorID As Integer
    Public UserID As String = ""
    Public FullName As String = ""
    Public DateEntered As Date = Today
    Public Notes As String = ""
    Public FileName As String = ""


    Public Function AddNew(ByVal CurrentUserID As String) As Boolean
        '
        ' Add a new hlc_User record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        Dim intRecsAffected As Integer

        ErrorMessage = ""
        intRecsAffected = 0

        '===================================================================
        ' CALL STORED PROC TO ADD RECORD
        '===================================================================
        If ErrorMessage = "" Then

            Try

                oConn.ConnectionString = GetConnectionString
                oConn.Open()

                oCmd.Connection = oConn
                oCmd.CommandText = "hlc_InsertDoctorNote"
                oCmd.CommandType = Data.CommandType.StoredProcedure
                oCmd.Parameters.AddWithValue("@DoctorID", DoctorID)
                oCmd.Parameters.AddWithValue("@UserID", UserID)
                oCmd.Parameters.AddWithValue("@DateEntered", DateEntered)
                oCmd.Parameters.AddWithValue("@Notes", Notes)
                oCmd.Parameters.AddWithValue("@FileName", FileName)

                intRecsAffected = oCmd.ExecuteNonQuery()

            Catch ex As Exception

                ErrorMessage = ex.Message
                intRecsAffected = -1
                Return False

            Finally

                '-- Close up shop before leaving
                If oConn.State = Data.ConnectionState.Open Then
                    oConn.Close()
                End If

            End Try

        End If

        Return True

    End Function

    Public Function Update(ByVal CurrentUserID As String) As Boolean
        '
        ' Update an existing hlc_User record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        Dim intRecsAffected As Integer

        ErrorMessage = ""
        intRecsAffected = 0

        Try
            oConn.ConnectionString = GetConnectionString
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "hlc_UpdateDoctorNote"
            oCmd.CommandType = Data.CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@ID", ID)
            oCmd.Parameters.AddWithValue("@DoctorID", DoctorID)
            oCmd.Parameters.AddWithValue("@UserID", UserID)
            oCmd.Parameters.AddWithValue("@DateEntered", DateEntered)
            oCmd.Parameters.AddWithValue("@Notes", Notes)
            oCmd.Parameters.AddWithValue("@FileName", FileName)

            intRecsAffected = oCmd.ExecuteNonQuery()

            If intRecsAffected = 1 Then

                Return True

            Else

                Return False

            End If

        Catch ex As Exception

            ErrorMessage = ex.Message
            intRecsAffected = -1
            Return False

        Finally

            '-- Close up shop before leaving
            If oConn.State = Data.ConnectionState.Open Then
                oConn.Close()
            End If

        End Try

    End Function

    Public Function Delete(ByVal CurrentUserID As String) As Boolean
        '
        ' Delete an existing hlc_User record using the UserID in the Public variable.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        ' AUDIT:    Updates hlc_ActivityLog to reflect this activity.  CurrentUserID is the
        '           ID of the user who deleted the record.
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        'Dim hlcUtility As hlc_Utility
        Dim intRecsAffected As Integer

        ErrorMessage = ""
        intRecsAffected = 0

        '===================================================================
        ' CALL STORED PROC TO DELETE THIS USER
        '===================================================================
        Try
            oConn.ConnectionString = GetConnectionString
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "hlc_DeleteDoctorNote"
            oCmd.CommandType = Data.CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@ID", ID)

            intRecsAffected = oCmd.ExecuteNonQuery()
            Return True

        Catch ex As Exception

            ErrorMessage = ex.Message
            intRecsAffected = -1
            Return False

        Finally

            '-- Close up shop before leaving
            If oConn.State = Data.ConnectionState.Open Then
                oConn.Close()
            End If

        End Try

        '===================================================================
        ' UPDATE ACTIVITY LOG
        '===================================================================
        'If ErrorMessage = "" Then

        '    hlcUtility = New hlc_Utility
        '    If UserRole = "Dealer" Then
        '        hlcUtility.AddActivity(CurrentUserID, DealerID, _
        '               "Delete", "Deleted " & UserName & " from " & DealerID)
        '    Else
        '        hlcUtility.AddActivity(CurrentUserID, DealerID, _
        '           "Delete", "Deleted " & UserRole & ": " & UserName)
        '    End If

        'End If

        Return True

    End Function
    Private Function GetConnectionString() As String
        Dim hlcUtility As New hlc_Utility
        Return hlcUtility.GetConnectionString()
    End Function

End Class
