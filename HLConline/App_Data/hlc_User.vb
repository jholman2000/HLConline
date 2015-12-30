Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class hlc_User

    Public ErrorMessage As String

    Public UserID As String = ""
    Public FirstName As String = ""
    Public LastName As String = ""
    Public UserRole As String = "HLC Member"
    Public Password As String = "hlcuser"
    Public EmailAddress As String = ""
    Public Address As String = ""
    Public City As String = ""
    Public State As String = ""
    Public Zip As String = ""
    Public HomePhone As String = ""
    Public CellPhone As String = ""
    Public DateLastOn As Date = Date.MinValue
    Public IsActive As Boolean = True
    Public MustChangePassword As Boolean = True


    Public Function Logon(ByVal sUserID As String, ByVal sPassword As String) As Boolean
        '
        ' Log a user onto the HLC site and, if logged on successfully, update
        ' the user's DateLastOn field
        '
        Dim hlcUtility As New hlc_Utility

        If GetUserByUserID(sUserID) Then

            If Password = sPassword Then

                '-- Password matched; logon is successful. 
                hlcUtility.ExecuteSQL("update hlc_User set DateLastOn = getDate() where UserID = '" & sUserID & "'")

                Return True

            Else

                '-- Password didn't match
                ErrorMessage = "Invalid password. Please check and try again."
                Return False

            End If

        Else

            '-- User not found in hlcUser Table or some other error occurred
            'ErrorMessage = "Invalid logon ID. Please check and try again."
            ErrorMessage = "Unable to log on with this ID and password.<br>" + ErrorMessage
            Return False

        End If
    End Function

    Public Function GetUserByUserID(ByVal sUserID As String) As Boolean
        ' 
        '   Retrieve the data for a single User from the hlc_User table
        '
        Dim hlcUtility As New hlc_Utility
        Dim hlcDataRow As DataRow

        ErrorMessage = ""

        hlcDataRow = hlcUtility.GetDataRowViaSQL("select hlc_User.* from hlc_User where UserID = '" & Trim(sUserID) & "'")

        If hlcDataRow Is Nothing Then

            '-- No record found.  Return false to signal the record could
            '   not be retrieved and pass along the error from the utility
            '
            ErrorMessage = hlcUtility.ErrorMessage
            Return False

        Else

            '-- Transfer the data values from the data row into the public properties
            '
            UserID = hlcDataRow.Item("UserID")
            FirstName = hlcDataRow.Item("FirstName").ToString
            LastName = hlcDataRow.Item("LastName").ToString
            UserRole = hlcDataRow.Item("UserRole")
            Password = hlcDataRow.Item("Password")
            EmailAddress = hlcDataRow.Item("EmailAddress").ToString
            Address = hlcDataRow.Item("Address").ToString
            City = hlcDataRow.Item("City").ToString
            State = hlcDataRow.Item("State").ToString
            Zip = hlcDataRow.Item("Zip").ToString
            HomePhone = hlcDataRow.Item("HomePhone").ToString
            CellPhone = hlcDataRow.Item("CellPhone").ToString
            DateLastOn = IIf(IsDBNull(hlcDataRow.Item("DateLastOn")), Date.MinValue, hlcDataRow.Item("DateLastOn"))
            IsActive = hlcDataRow.Item("IsActive")
            MustChangePassword = hlcDataRow.Item("MustChangePassword")

            Return True
        End If
    End Function

    Public Function AddNew(ByVal currentUserID As String) As Boolean
        '
        ' Add a new hlc_User record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        ' AUDIT:    Updates hlc_ActivityLog to reflect this activity.  CurrentUserID is the
        '           ID of the user who added the record.
        '
        Dim oConn As New SqlConnection
        Dim oCmd As New SqlCommand
        'Dim hlcUtility As hlc_Utility
        Dim intRecsAffected As Integer

        ErrorMessage = ""
        intRecsAffected = 0

        '===================================================================
        ' MAKE SURE USERID HASN'T BEEN ASSIGNED ALREADY
        '===================================================================
        '
        Try
            oConn.ConnectionString = GetConnectionString()
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "select count(UserID) from hlc_User where UserID = '" & UserID & "'"
            oCmd.CommandType = CommandType.Text
            intRecsAffected = oCmd.ExecuteScalar()

            If intRecsAffected = 1 Then

                '-- This UserID already exists!  Flag an error and set the error message
                '
                ErrorMessage = "User ID <b>" & UserID & "</b> has already been assigned to another HLC User."
                intRecsAffected = -1

            End If

        Catch ex As Exception
            ErrorMessage = "(hlc_User.01): " + ex.Message
            intRecsAffected = -1

        Finally
            '-- Close up shop before leaving
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        End Try

        '===================================================================
        ' CALL STORED PROC TO ADD USER
        '===================================================================
        If ErrorMessage = "" Then

            Try

                oConn.ConnectionString = GetConnectionString()
                oConn.Open()

                oCmd.Connection = oConn
                oCmd.CommandText = "hlc_InsertUser"
                oCmd.CommandType = CommandType.StoredProcedure
                oCmd.Parameters.AddWithValue("@UserID", UserID)
                oCmd.Parameters.AddWithValue("@FirstName", FirstName)
                oCmd.Parameters.AddWithValue("@LastName", LastName)
                oCmd.Parameters.AddWithValue("@Password", Password)
                oCmd.Parameters.AddWithValue("@UserRole", UserRole)
                oCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
                oCmd.Parameters.AddWithValue("@Address", Address)
                oCmd.Parameters.AddWithValue("@City", City)
                oCmd.Parameters.AddWithValue("@State", State)
                oCmd.Parameters.AddWithValue("@Zip", Zip)
                oCmd.Parameters.AddWithValue("@CellPhone", CellPhone)
                oCmd.Parameters.AddWithValue("@HomePhone", HomePhone)
                oCmd.Parameters.AddWithValue("@IsActive", IsActive)
                oCmd.Parameters.AddWithValue("@MustChangePassword", MustChangePassword)

                ' ReSharper disable once RedundantAssignment
                intRecsAffected = oCmd.ExecuteNonQuery()

            Catch ex As Exception

                ErrorMessage = ex.Message
                intRecsAffected = -1
                Return False

            Finally

                '-- Close up shop before leaving
                If oConn.State = ConnectionState.Open Then
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
        ' Update an existing hlc_User record using the values in this class's Public variables.
        ' 
        ' RETURNS:  True/False  (If False, ErrorMessage property will contain the reason.)
        '
        ' AUDIT:    Updates hlc_ActivityLog to reflect this activity.  CurrentUserID is the
        '           ID of the user who updated the record.
        '
        Dim oConn As New SqlConnection
        Dim oCmd As New SqlCommand
        'Dim hlcUtility As hlc_Utility
        Dim intRecsAffected As Integer

        ErrorMessage = ""
        intRecsAffected = 0

        Try
            oConn.ConnectionString = GetConnectionString()
            oConn.Open()

            oCmd.Connection = oConn
            oCmd.CommandText = "hlc_UpdateUser"
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@UserID", UserID)
            oCmd.Parameters.AddWithValue("@FirstName", FirstName)
            oCmd.Parameters.AddWithValue("@LastName", LastName)
            oCmd.Parameters.AddWithValue("@Password", Password)
            oCmd.Parameters.AddWithValue("@UserRole", UserRole)
            oCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress)
            oCmd.Parameters.AddWithValue("@Address", Address)
            oCmd.Parameters.AddWithValue("@City", City)
            oCmd.Parameters.AddWithValue("@State", State)
            oCmd.Parameters.AddWithValue("@Zip", Zip)
            oCmd.Parameters.AddWithValue("@CellPhone", CellPhone)
            oCmd.Parameters.AddWithValue("@HomePhone", HomePhone)
            oCmd.Parameters.AddWithValue("@IsActive", IsActive)
            oCmd.Parameters.AddWithValue("@MustChangePassword", MustChangePassword)

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
            If oConn.State = ConnectionState.Open Then
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
        Dim oConn As New SqlConnection
        Dim oCmd As New SqlCommand
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
            oCmd.CommandText = "hlc_DeleteUser"
            oCmd.CommandType = CommandType.StoredProcedure
            oCmd.Parameters.AddWithValue("@UserID", UserID)

            intRecsAffected = oCmd.ExecuteNonQuery()
            'Return True

        Catch ex As Exception

            ErrorMessage = ex.Message
            intRecsAffected = -1
            Return False

        Finally

            '-- Close up shop before leaving
            If oConn.State = ConnectionState.Open Then
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Function GetConnectionString() As String
        Dim hlcUtility As New hlc_Utility
        Return hlcUtility.GetConnectionString()
    End Function

End Class
