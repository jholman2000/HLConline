Public Class EditUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oCurrentUser As hlc_User
        Dim oUser As hlc_User
        Dim sUserID As String
        Dim sTemp As String

        If Session("User") Is Nothing Then
            Response.Redirect("~\Logout.aspx")
        End If

        oCurrentUser = Session("User")

        '-- UserID is passed in (for edit) or is blank for a new User
        '
        sUserID = Request.QueryString("ID")
        If sUserID = "self" Then sUserID = oCurrentUser.UserID

        '-- Modify form to display/hide items based on the current users's UserRole
        '
        'If sUserID <> "" Then
        '    pnlActivityLog.Visible = True
        'End If

        Select Case oCurrentUser.UserRole

            Case "Admin"

                MultiView1.SetActiveView(vwAdmin)

                lblPassword.Visible = True
                txtPassword.Visible = True

                'sdsActivityLog.SelectCommand = "select DateActivity, Activity, Description from hlc_ActivityLog where UserID = '" & sUserID & "' order by DateActivity desc"
                'sdsActivityLog.SelectCommandType = SqlDataSourceCommandType.Text

                If oCurrentUser.UserRole = "Admin" Then

                    '-- Admin

                    If sUserID <> "" Then

                        '-- Admin (not HLC Member) can delete users
                        '
                        btnDelete.Visible = True

                        sTemp = "WARNING: Deleting this user will physically remove their data from " & _
                                "the database (including past posts/notes they have entered). If you simply " & _
                                "wish to restrict access to the site, uncheck the Active button instead.  " & _
                                "Are you sure you want to DELETE this user?"

                        btnDelete.Attributes.Add("onclick", "return confirm('" & Server.HtmlEncode(sTemp) & "');")

                    End If

                End If

            Case "HLC Member"

                MultiView1.SetActiveView(vwUser)
                lblHeading.Text = "Edit My Profile"

        End Select

        If Not IsPostBack() Then

            btnUpdate.Attributes.Add("onclick", "this.value='Processing...';")

            If oCurrentUser.UserRole = "HLC Member" Then

                '-- Dealer can only edit their own record.  
                sUserID = oCurrentUser.UserID

            End If


            '-- Create a new instance of the data class and (if edit) load the values from the
            '   data tables
            '
            oUser = New hlc_User

            If sUserID = "" Then

                '-- ADD NEW USER
                hidMode.Value = "Add"
                lblHeading.Text = "Add a New User Profile"
                txtUserID.Enabled = True
                btnDelete.Visible = False

            Else

                '-- EDIT EXISTING USER

                'If UCase(oCurrentUser.UserID) = UCase(sUserID) Then
                'lblHeading.Text = "Edit Your Profile"
                'End If

                txtUserID.Enabled = False
                hidMode.Value = "Edit"
                lblHeading.Text = "Edit a User Profile"

                oUser.GetUserByUserID(sUserID)

            End If

            '-- Display the values in the input fields
            '
            With oUser
                If .ErrorMessage <> "" Then

                    '-- Display error that occurred when trying to load this
                    '   User's existing data
                    pnlError.Visible = True
                    lblErrorMsg.Text = .ErrorMessage
                    btnUpdate.Enabled = False

                Else

                    txtUserID.Text = .UserID
                    txtFirstName.Text = .FirstName
                    txtLastName.Text = .LastName
                    txtPassword.Text = .Password
                    txtEmailAddress.Text = .EmailAddress
                    txtAddress.Text = .Address
                    txtCity.Text = .City
                    txtState.Text = .State
                    txtZip.Text = .Zip
                    txtHomePhone.Text = .HomePhone
                    txtMobilePhone.Text = .CellPhone
                    If .IsActive Then
                        chkIsActive.Checked = True
                    End If
                    If .MustChangePassword Then
                        chkMustChangePassword.Checked = True
                    End If
                    If .DateLastOn.Year > 2000 Then
                        lblDateLastOn.Text = "Last signed on " & .DateLastOn.ToString("g")
                    Else
                        lblDateLastOn.Text = "This user has never signed on."
                    End If

                    ddlUserRole.SelectedIndex = ddlUserRole.Items.IndexOf(ddlUserRole.Items.FindByText(.UserRole))

                    If txtUserID.Enabled Then
                        txtUserID.Focus()
                    Else
                        txtFirstName.Focus()
                    End If

                End If

            End With

        End If

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ' If the current user just updated their own profile, reset the Session("User") reference
        Dim sErrorMsg As String = ""
        Dim sEmailPattern As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim oCurrentUser As hlc_User
        Dim oUser As hlc_User
        'Dim sEmail As String
        'Dim hlcUtility As hlc_Utility

        oCurrentUser = Session("User")
        oUser = Nothing

        '-- Check for required fields and flag those that are missing
        '
        If txtUserID.Text = "" Then
            sErrorMsg += "<br>- Please enter a UserID at least five characters in length."
            txtUserID.BackColor = Drawing.Color.LightSalmon
        End If
        If (ddlUserRole.SelectedValue = "HLC Member" Or ddlUserRole.SelectedValue = "Admin") _
           And txtEmailAddress.Text = "" Then
            sErrorMsg += "<br>- HLC Members must have an email address on file."
            txtEmailAddress.BackColor = Drawing.Color.LightSalmon
        End If
        If Trim(txtFirstName.Text) = "" Or Trim(txtLastName.Text) = "" Then
            sErrorMsg += "<br>- Please enter this User's first and last name."
            txtFirstName.BackColor = Drawing.Color.LightSalmon
        End If
        If Len(Trim(txtPassword.Text)) < 6 Then
            sErrorMsg += "<br>- Please enter a password (at least 6 characters)."
            txtPassword.BackColor = Drawing.Color.LightSalmon
        End If
        If Trim(txtEmailAddress.Text) = "" Then
            sErrorMsg += "<br>- Please enter a valid email address."
            txtEmailAddress.BackColor = Drawing.Color.LightSalmon
        Else
            If Not Regex.IsMatch(Trim(txtEmailAddress.Text), sEmailPattern) Then
                sErrorMsg += "<br>- The email address entered is not formatted as a valid email address."
                txtEmailAddress.BackColor = Drawing.Color.LightSalmon
            End If
        End If

        If sErrorMsg = "" Then

            '-- No errors were noted, so attempt to update the record.
            '
            oUser = New hlc_User

            With oUser
                .UserID = LCase(txtUserID.Text)
                .FirstName = txtFirstName.Text
                .LastName = txtLastName.Text
                .Password = txtPassword.Text
                .UserRole = ddlUserRole.SelectedValue
                .EmailAddress = txtEmailAddress.Text
                .Address = txtAddress.Text
                .City = txtCity.Text
                .State = UCase(txtState.Text)
                .Zip = txtZip.Text
                .HomePhone = txtHomePhone.Text
                .CellPhone = txtMobilePhone.Text
                .IsActive = chkIsActive.Checked
                .MustChangePassword = chkMustChangePassword.Checked

                If hidMode.Value = "Add" Then
                    .AddNew(oCurrentUser.UserID)
                Else
                    .Update(oCurrentUser.UserID)
                End If

                '-- See if an error occurred during the AddNew/Update
                If .ErrorMessage <> "" Then
                    sErrorMsg += "<br>" & .ErrorMessage
                End If

            End With

        End If

        If sErrorMsg = "" Then

            '-- Add/Update was successful. If this was the user changing their own record,
            '   update the Session variable to reflect it
            '
            If LCase(oCurrentUser.UserID) = LCase(oUser.UserID) Then
                Session("User") = oUser
            End If

            '-- No errors occurred.  Go back to the browse page
            '
            If oCurrentUser.UserRole = "Admin" Then
                Response.Redirect("~/Browse/BrowseUsers.aspx")
            Else
                btnUpdate.Visible = True
                pnlError.Visible = False
                pnlMessage.Visible = True
                lblMessage.Text = "Profile successfully updated."
            End If
        Else

            '-- Errors occurred during the validations or the updating, so display the error 
            '   message and remain on this webform
            '
            pnlError.Visible = False
            pnlError.Visible = True
            lblErrorMsg.Text = sErrorMsg
            pnlError.Height = 40 + (CountLines(sErrorMsg) * 20)

        End If
    End Sub


    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ' Delete this HLC User
        '
        ' NOTE:  The OnClick() function for this button asks the user to confirm the deletion.  This 
        '        button Click event only gets called if the user answered 'Yes'.
        '
        Dim oCurrentUser As hlc_User
        Dim oUser As hlc_User
        Dim sErrorMsg As String = ""

        oCurrentUser = Session("User")

        oUser = New hlc_User

        With oUser
            .UserID = txtUserID.Text
            .FirstName = txtFirstName.Text
            .LastName = txtLastName.Text
            .UserRole = ddlUserRole.SelectedValue
            .Delete(oCurrentUser.UserID)    ' Parm=Who did deleting?
            If .ErrorMessage <> "" Then
                sErrorMsg += "<br>" & .ErrorMessage
            End If
        End With

        If sErrorMsg <> "" Then

            '-- Error occurred.  Display it and stay on this screen.
            pnlError.Visible = True
            lblErrorMsg.Text = sErrorMsg
            pnlError.Height = 40 + (CountLines(sErrorMsg) * 20)

        Else

            '-- Deleted successfully.  Stay on this form and display a message
            '   that the delete happened.
            '
            pnlError.Visible = False
            pnlMessage.Visible = True
            lblMessage.Text = "This HLC User has been successfully deleted."
            btnDelete.Visible = False
            btnUpdate.Visible = False

        End If

    End Sub

    Private Function CountLines(ByVal sText As String) As Integer
        Dim nNumLines As Integer = 0
        Dim nStart As Integer = 0

        Do Until nStart = -1
            nNumLines += 1
            nStart = sText.IndexOf("<br>", nStart + 1)
        Loop

        Return nNumLines

    End Function

End Class