Imports Microsoft.VisualBasic

Public Class hlc_Hospital

    Public ErrorMessage As String

    Public ID As Integer = 0
    Public HospitalName As String = ""
    Public City As String = ""
    Public State As String = ""
    Public CommitteeID As Integer = 0

    Public Function GetHospitalByID(ByVal sID As String) As Boolean
        ' 
        '   Retrieve the info for the hospital
        '
        Dim hlcUtility As New hlc_Utility
        Dim oDataRow As System.Data.DataRow
        Dim sSQL As String

        ErrorMessage = ""

        sSQL = "select hlc_Hospital.* from hlc_Hospital where ID = " & sID

        oDataRow = hlcUtility.GetDataRowViaSQL(sSQL)

        If oDataRow Is Nothing Then

            '-- No record found.  Return false to signal the record could
            '   not be retrieved
            '
            ErrorMessage = "Hospital information not found."
            Return False

        Else

            '-- Transfer the data values from the data row into the public properties
            '
            ID = oDataRow.Item("ID")
            HospitalName = oDataRow.Item("HospitalName").ToString
            City = oDataRow.Item("City").ToString
            State = oDataRow.Item("State").ToString
            CommitteeID = oDataRow.Item("CommitteeID")

            If ErrorMessage = "" Then
                Return True
            Else
                Return False
            End If

        End If

    End Function


    Public Function AddNew(ByVal CurrentUserID As String) As Boolean
        '
        ' Add a new hlc_Hospital record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        'Dim hlcUtility As hlc_Utility
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
                oCmd.CommandText = "hlc_InsertHospital"
                oCmd.CommandType = Data.CommandType.StoredProcedure
                oCmd.Parameters.AddWithValue("@HospitalName", HospitalName)
                oCmd.Parameters.AddWithValue("@City", City)
                oCmd.Parameters.AddWithValue("@State", State)
                oCmd.Parameters.AddWithValue("@CommitteeID", 1)

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
        ' Update an existing hlc_Hospital record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        ' AUDIT:    Updates hlc_ActivityLog to reflect this activity.  CurrentUserID is the
        '           ID of the user who updated the record.
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
            oCmd.CommandText = "hlc_UpdateHospital"
            oCmd.CommandType = Data.CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@ID", ID)
            oCmd.Parameters.AddWithValue("@HospitalName", HospitalName)
            oCmd.Parameters.AddWithValue("@City", City)
            oCmd.Parameters.AddWithValue("@State", State)
            oCmd.Parameters.AddWithValue("@CommitteeID", 1)

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
        ' Delete an existing hlc_Hospital record using the UserID in the Public variable.
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
            oCmd.CommandText = "hlc_DeleteHospital"
            oCmd.CommandType = Data.CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@ID", ID)

            intRecsAffected = oCmd.ExecuteNonQuery()
            'Return True

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

        Return True

    End Function
    Private Function GetConnectionString() As String
        Dim hlcUtility As New hlc_Utility
        Return hlcUtility.GetConnectionString()
    End Function

End Class
