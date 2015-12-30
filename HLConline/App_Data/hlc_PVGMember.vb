Imports Microsoft.VisualBasic

Public Class hlc_PVGMember
    Public ErrorMessage As String

    Public ID As Integer = 0
    Public FirstName As String = ""
    Public LastName As String = ""
    Public Address As String = ""
    Public City As String = ""
    Public State As String = ""
    Public Zip As String = ""
    Public EmailAddress As String = ""
    Public HomePhone As String = ""
    Public MobilePhone As String = ""
    Public Notes As String = ""
    Public Congregation As String = ""
    Public Hospitals As New Collection    ' Of hlc_PVGMemberHospital

    Public Function GetPVGMemberByID(ByVal nID As Integer) As Boolean
        ' 
        '   Retrieve the data for a single User from the hlc_User table
        '
        Dim hlcUtility As New hlc_Utility
        Dim hlcDataRow As System.Data.DataRow

        ErrorMessage = ""

        hlcDataRow = hlcUtility.GetDataRowViaSQL("select * from hlc_PVGMember where ID = " + nID.ToString)

        If hlcDataRow Is Nothing Then

            '-- No record found.  Return false to signal the record could
            '   not be retrieved and pass along the error from the utility
            '
            ErrorMessage = hlcUtility.ErrorMessage
            Return False

        Else

            '-- Transfer the data values from the data row into the public properties
            '
            ID = hlcDataRow.Item("ID")
            FirstName = hlcDataRow.Item("FirstName").ToString
            LastName = hlcDataRow.Item("LastName").ToString
            Address = hlcDataRow.Item("Address").ToString
            City = hlcDataRow.Item("City").ToString
            State = hlcDataRow.Item("State").ToString
            Zip = hlcDataRow.Item("Zip").ToString
            EmailAddress = hlcDataRow.Item("EmailAddress").ToString
            HomePhone = hlcDataRow.Item("HomePhone").ToString
            MobilePhone = hlcDataRow.Item("MobilePhone").ToString
            Congregation = hlcDataRow.Item("Congregation").ToString
            Notes = hlcDataRow.Item("Notes").ToString

            GetHospitals()

            Return True
        End If
    End Function

    Public Function GetHospitals() As Boolean
        '-- Read a list of Hospitals for this Doctor into the Hospitals collection
        '   (Assumes the ID has already been set)
        '
        Dim hlcUtility As New hlc_Utility
        Dim oTable As System.Data.DataTable
        Dim oDataRow As System.Data.DataRow
        Dim hlcPVGHospital As hlc_PVGMemberHospital

        oTable = hlcUtility.GetDataTableViaSQL("exec hlc_GetPVGMemberHospitals " + ID.ToString)

        If Not oTable Is Nothing Then

            Hospitals = New Collection
            For Each oDataRow In oTable.Rows
                hlcPVGHospital = New hlc_PVGMemberHospital
                With hlcPVGHospital
                    .ID = oDataRow.Item("ID")
                    .PVGMemberID = oDataRow.Item("PVGMemberID")
                    .HospitalID = oDataRow.Item("HospitalID")
                    .HospitalName = oDataRow.Item("HospitalName").ToString
                    .City = oDataRow.Item("City").ToString
                    .State = oDataRow.Item("State").ToString
                    .Notes = oDataRow.Item("Notes").ToString
                    .CommitteeID = oDataRow.Item("CommitteeID").ToString
                    .DayOfWeek = oDataRow.Item("DayOfWeek").ToString
                End With
                Hospitals.Add(hlcPVGHospital)
            Next

        End If

        Return True

    End Function

    Public Function GetHospitals(ByVal nID As Integer) As Boolean
        ID = nID
        GetHospitals = GetHospitals()
    End Function

    Public Function AddNew(ByVal CurrentUserID As String, ByVal HospIDs As String, ByVal DaysOfWeek As String) As Boolean
        '
        ' Add a new hlc_User record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        Dim intRecsAffected As Integer
        Dim aHospIDs() As String
        Dim aDaysOfWeek() As String

        ErrorMessage = ""
        intRecsAffected = 0

        '===================================================================
        ' CALL STORED PROC TO ADD RECORD
        '===================================================================
        If ErrorMessage = "" Then

            Try

                oConn.ConnectionString = GetConnectionString()
                oConn.Open()

                oCmd.Connection = oConn
                oCmd.CommandText = "hlc_InsertPVGMember"
                oCmd.CommandType = Data.CommandType.StoredProcedure
                oCmd.Parameters.AddWithValue("@FirstName", FirstName)
                oCmd.Parameters.AddWithValue("@LastName", LastName)
                oCmd.Parameters.AddWithValue("@Address", Address)
                oCmd.Parameters.AddWithValue("@City", City)
                oCmd.Parameters.AddWithValue("@State", State)
                oCmd.Parameters.AddWithValue("@Zip", Zip)
                oCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
                oCmd.Parameters.AddWithValue("@MobilePhone", MobilePhone)
                oCmd.Parameters.AddWithValue("@HomePhone", HomePhone)
                oCmd.Parameters.AddWithValue("@Congregation", Congregation)
                oCmd.Parameters.AddWithValue("@Notes", Notes)

                aHospIDs = Split(HospIDs, "|")
                aDaysOfWeek = Split(DaysOfWeek, "|")
                oCmd.Parameters.AddWithValue("@HospID1", aHospIDs(0))
                oCmd.Parameters.AddWithValue("@DayOfWeek1", aDaysOfWeek(0))
                oCmd.Parameters.AddWithValue("@HospID2", aHospIDs(1))
                oCmd.Parameters.AddWithValue("@DayOfWeek2", aDaysOfWeek(1))
                oCmd.Parameters.AddWithValue("@HospID3", aHospIDs(2))
                oCmd.Parameters.AddWithValue("@DayOfWeek3", aDaysOfWeek(2))

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

    Public Function Update(ByVal CurrentUserID As String, ByVal HospIDs As String, ByVal DaysOfWeek As String) As Boolean
        '
        ' Update an existing hlc_User record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCmd As New Data.SqlClient.SqlCommand
        Dim intRecsAffected As Integer
        Dim aHospIDs() As String
        Dim aDaysOfWeek() As String

        ErrorMessage = ""
        intRecsAffected = 0

        Try
            oConn.ConnectionString = GetConnectionString()
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "hlc_UpdatePVGMember"
            oCmd.CommandType = Data.CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@ID", ID)
            oCmd.Parameters.AddWithValue("@FirstName", FirstName)
            oCmd.Parameters.AddWithValue("@LastName", LastName)
            oCmd.Parameters.AddWithValue("@Address", Address)
            oCmd.Parameters.AddWithValue("@City", City)
            oCmd.Parameters.AddWithValue("@State", State)
            oCmd.Parameters.AddWithValue("@Zip", Zip)
            oCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
            oCmd.Parameters.AddWithValue("@MobilePhone", MobilePhone)
            oCmd.Parameters.AddWithValue("@HomePhone", HomePhone)
            oCmd.Parameters.AddWithValue("@Congregation", Congregation)
            oCmd.Parameters.AddWithValue("@Notes", Notes)

            aHospIDs = Split(HospIDs, "|")
            aDaysOfWeek = Split(DaysOfWeek, "|")
            oCmd.Parameters.AddWithValue("@HospID1", aHospIDs(0))
            oCmd.Parameters.AddWithValue("@DayOfWeek1", aDaysOfWeek(0))
            oCmd.Parameters.AddWithValue("@HospID2", aHospIDs(1))
            oCmd.Parameters.AddWithValue("@DayOfWeek2", aDaysOfWeek(1))
            oCmd.Parameters.AddWithValue("@HospID3", aHospIDs(2))
            oCmd.Parameters.AddWithValue("@DayOfWeek3", aDaysOfWeek(2))

            intRecsAffected = oCmd.ExecuteNonQuery()

            If intRecsAffected >= 1 Then

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
            oConn.ConnectionString = GetConnectionString()
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "hlc_DeletePVGMember"
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

    Public ReadOnly Property FullName() As String
        Get
            FullName = FirstName + " " + LastName
        End Get
    End Property

    Private Function GetConnectionString() As String
        Dim hlcUtility As New hlc_Utility
        Return hlcUtility.GetConnectionString()
    End Function

End Class
