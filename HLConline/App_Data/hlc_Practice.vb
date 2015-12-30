Imports Microsoft.VisualBasic

Public Class hlc_Practice
    Public ErrorMessage As String = ""

    Public ID As Integer = 0
    Public PracticeName As String = ""
    Public Address1 As String = ""
    Public Address2 As String = ""
    Public Address3 As String = ""
    Public City As String = ""
    Public State As String = ""
    Public Zip As String = ""
    Public OfficePhone1 As String = ""
    Public OfficePhone2 As String = ""
    Public Fax As String = ""
    Public WebsiteURL As String = ""
    Public OfficeManager As String = ""

    Public Function GetPracticeByID(ByVal nID As Integer) As Boolean
        ' 
        '
        Dim hlcUtility As New hlc_Utility
        Dim hlcDataRow As System.Data.DataRow

        ErrorMessage = ""

        hlcDataRow = hlcUtility.GetDataRowViaSQL("SELECT * FROM hlc_Practice WHERE hlc_Practice.ID = " + nID.ToString)

        If hlcDataRow Is Nothing Then

            '-- No record found.  Return false to signal the record could
            '   not be retrieved
            '
            ErrorMessage = "Could not retrieve the information for Practice ID " + nID.ToString + " " + _
                    hlcUtility.ErrorMessage
            Return False

        Else

            '-- Transfer the data values from the data row into the public properties
            '
            'ID = hlcDataRow.Item("ID")
            PracticeName = hlcDataRow.Item("PracticeName").ToString
            Address1 = hlcDataRow.Item("Address1").ToString
            Address2 = hlcDataRow.Item("Address2").ToString
            Address3 = hlcDataRow.Item("Address3").ToString
            City = hlcDataRow.Item("City").ToString
            State = hlcDataRow.Item("State")
            Zip = hlcDataRow.Item("Zip").ToString
            OfficePhone1 = hlcDataRow.Item("OfficePhone1").ToString
            OfficePhone2 = hlcDataRow.Item("OfficePhone2").ToString
            Fax = hlcDataRow.Item("Fax").ToString
            WebsiteURL = hlcDataRow.Item("WebsiteURL").ToString
            OfficeManager = hlcDataRow.Item("OfficeManager").ToString

            Return True
        End If
    End Function

    Public Function AddNew(ByVal CurrentUserID As String) As Boolean
        '
        ' Add a new hlc_Practice record using the values in this class's Public variables.
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
                oCmd.CommandText = "hlc_InsertPractice"
                oCmd.CommandType = Data.CommandType.StoredProcedure
                oCmd.Parameters.AddWithValue("@PracticeName", PracticeName)
                oCmd.Parameters.AddWithValue("@Address1", Address1)
                oCmd.Parameters.AddWithValue("@Address2", Address2)
                oCmd.Parameters.AddWithValue("@Address3", Address3)
                oCmd.Parameters.AddWithValue("@City", City)
                oCmd.Parameters.AddWithValue("@State", State)
                oCmd.Parameters.AddWithValue("@Zip", Zip)
                oCmd.Parameters.AddWithValue("@OfficePhone1", OfficePhone1)
                oCmd.Parameters.AddWithValue("@OfficePhone2", OfficePhone2)
                oCmd.Parameters.AddWithValue("@Fax", Fax)
                oCmd.Parameters.AddWithValue("@WebsiteURL", WebsiteURL)
                oCmd.Parameters.AddWithValue("@OfficeManager", OfficeManager)

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

        '===================================================================
        ' UPDATE ACTIVITY LOG
        '===================================================================
        'If ErrorMessage = "" Then

        '    hlcUtility = New hlc_Utility

        '    '-- Different Activity log msg depending on the type of user just added...
        '    If UserRole = "Dealer" Then
        '        hlcUtility.AddActivity(CurrentUserID, DealerID, _
        '           "Add", "Added " & UserName & " to " & DealerID)
        '    Else
        '        hlcUtility.AddActivity(CurrentUserID, DealerID, _
        '           "Add", "Added " & UserRole & ": " & UserName)
        '    End If

        'End If

        Return True

    End Function

    Public Function Update(ByVal CurrentUserID As String) As Boolean
        '
        ' Update an existing hlc_Practice record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        ' AUDIT:    Updates hlc_ActivityLog to reflect this activity.  CurrentUserID is the
        '           ID of the user who updated the record.
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        'Dim hlcUtility As hlc_Utility
        Dim intRecsAffected As Integer

        ErrorMessage = ""
        intRecsAffected = 0

        Try
            oConn.ConnectionString = GetConnectionString
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "hlc_UpdatePractice"
            oCmd.CommandType = Data.CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@ID", ID)
            oCmd.Parameters.AddWithValue("@PracticeName", PracticeName)
            oCmd.Parameters.AddWithValue("@Address1", Address1)
            oCmd.Parameters.AddWithValue("@Address2", Address2)
            oCmd.Parameters.AddWithValue("@Address3", Address3)
            oCmd.Parameters.AddWithValue("@City", City)
            oCmd.Parameters.AddWithValue("@State", State)
            oCmd.Parameters.AddWithValue("@Zip", Zip)
            oCmd.Parameters.AddWithValue("@OfficePhone1", OfficePhone1)
            oCmd.Parameters.AddWithValue("@OfficePhone2", OfficePhone2)
            oCmd.Parameters.AddWithValue("@Fax", Fax)
            oCmd.Parameters.AddWithValue("@WebsiteURL", WebsiteURL)
            oCmd.Parameters.AddWithValue("@OfficeManager", OfficeManager)

            intRecsAffected = oCmd.ExecuteNonQuery()

            If intRecsAffected = 1 Then
                'hlcUtility = New hlc_Utility
                'If UserID = CurrentUserID Then

                '    hlcUtility.AddActivity(CurrentUserID, DealerID, _
                '           "Update", "Updated his/her own profile or password.")
                'Else

                '    hlcUtility.AddActivity(CurrentUserID, DealerID, _
                '           "Update", "Updated info for: " & UserName)
                'End If

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
        ' Delete an existing hlc_Practice record using the UserID in the Public variable.
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
            oCmd.CommandText = "hlc_DeletePractice"
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


    Public ReadOnly Property AddressText() As String
        Get
            Dim sTemp As String = ""

            sTemp = PracticeName + "<br>" + Address1
            If Address2 <> "" Then sTemp += Address2 + "<br>"
            If Address3 <> "" Then sTemp += Address3 + "<br>"
            sTemp += Trim(City) + ", " + State + "  " + Zip + "<br>" + OfficePhone1

            AddressText = sTemp

        End Get
    End Property

    Private Function GetConnectionString() As String
        Dim hlcUtility As New hlc_Utility
        Return hlcUtility.GetConnectionString()
    End Function

End Class
