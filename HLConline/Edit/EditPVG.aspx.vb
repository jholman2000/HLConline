Public Class EditPVG
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oCurrentUser As hlc_User
        Dim oPVGMember As hlc_PVGMember
        Dim oPVGHospital As hlc_PVGMemberHospital
        Dim oTable As DataTable
        Dim sID As String
        Dim sTemp As String
        Dim i As Integer

        If Session("User") Is Nothing Then
            Response.Redirect("~\Logout.aspx")
        End If

        oCurrentUser = Session("User")

        '-- ID is passed in (for edit) or is blank for a new member
        '
        sID = Request.QueryString("ID")

        Select Case oCurrentUser.UserRole

            Case "Admin"

                If sID <> "" Then

                    '-- Admin (not HLC Member) can delete users
                    '
                    btnDelete.Visible = True

                    sTemp = "Are you sure you want to DELETE this PVG Member?"

                    btnDelete.Attributes.Add("onclick", "return confirm('" & Server.HtmlEncode(sTemp) & "');")

                End If

        End Select

        If Not IsPostBack() Then

            btnUpdate.Attributes.Add("onclick", "this.value='Processing...';")

            '-- Create a new instance of the data class and (if edit) load the values from the
            '   data tables
            '
            oPVGMember = New hlc_PVGMember

            If sID = "" Then

                '-- ADD 
                hidMode.Value = "Add"
                lblHeading.Text = "Add a New PVG Member"
                btnDelete.Visible = False

            Else

                '-- EDIT 
                hidMode.Value = "Edit"
                hidID.Value = sID
                lblHeading.Text = "Edit a PVG Member"

                oPVGMember.GetPVGMemberByID(sID)

            End If

            '-- Display the values in the input fields
            '
            With oPVGMember
                If .ErrorMessage <> "" Then

                    '-- Display error that occurred when trying to load this
                    '   User's existing data
                    pnlError.Visible = True
                    lblErrorMsg.Text = .ErrorMessage
                    btnUpdate.Enabled = False

                Else

                    txtFirstName.Text = .FirstName
                    txtLastName.Text = .LastName
                    txtEmailAddress.Text = .EmailAddress
                    txtAddress.Text = .Address
                    txtCity.Text = .City
                    txtState.Text = .State
                    txtZip.Text = .Zip
                    txtHomePhone.Text = .HomePhone
                    txtMobilePhone.Text = .MobilePhone
                    txtNotes.Text = .Notes
                    txtCongregation.Text = .Congregation

                    '-- Populate the Hospitals and Specialties drop-downs using the data in the
                    '   Session vars.  These should be filled already at logon.
                    '
                    oTable = Session("Hospitals")
                    With ddlHospital1
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("HospitalName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlHospital2
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("HospitalName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlHospital3
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("HospitalName"), oRow.Item("ID")))
                        Next
                    End With
                    '-- Load the dropdown with the Hospital(s) for this PVG Member
                    If .Hospitals.Count > 0 Then
                        i = 0
                        For Each oPVGHospital In .Hospitals
                            i += 1
                            Select Case i
                                Case 1
                                    ddlHospital1.SelectedIndex = ddlHospital1.Items.IndexOf(ddlHospital1.Items.FindByValue(oPVGHospital.HospitalID))
                                    ddlDayOfWeek1.SelectedIndex = ddlDayOfWeek1.Items.IndexOf(ddlDayOfWeek1.Items.FindByValue(oPVGHospital.DayOfWeek))
                                Case 2
                                    ddlHospital2.SelectedIndex = ddlHospital2.Items.IndexOf(ddlHospital2.Items.FindByValue(oPVGHospital.HospitalID))
                                    ddlDayOfWeek2.SelectedIndex = ddlDayOfWeek2.Items.IndexOf(ddlDayOfWeek2.Items.FindByValue(oPVGHospital.DayOfWeek))
                                Case 3
                                    ddlHospital3.SelectedIndex = ddlHospital3.Items.IndexOf(ddlHospital3.Items.FindByValue(oPVGHospital.HospitalID))
                                    ddlDayOfWeek3.SelectedIndex = ddlDayOfWeek3.Items.IndexOf(ddlDayOfWeek3.Items.FindByValue(oPVGHospital.DayOfWeek))
                            End Select
                        Next
                    End If

                    txtFirstName.Focus()

                End If

            End With

        End If

    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ' If the current user just updated their own profile, reset the Session("User") reference
        Dim sErrorMsg As String = ""
        Dim sEmailPattern As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim oCurrentUser As hlc_User
        Dim oPVGMember As hlc_PVGMember
        Dim sHospIDs As String = ""
        Dim sDayofWeek As String = ""

        oCurrentUser = Session("User")
        oPVGMember = Nothing

        '-- Check for required fields and flag those that are missing
        '
        If Trim(txtFirstName.Text) = "" Or Trim(txtLastName.Text) = "" Then
            sErrorMsg += "<br>- Please enter this person's first and last name."
            txtFirstName.BackColor = Drawing.Color.LightSalmon
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
            oPVGMember = New hlc_PVGMember

            With oPVGMember
                .ID = Val(hidID.Value)
                .FirstName = txtFirstName.Text
                .LastName = txtLastName.Text
                .EmailAddress = txtEmailAddress.Text
                .Address = txtAddress.Text
                .City = txtCity.Text
                .State = UCase(txtState.Text)
                .Zip = txtZip.Text
                .HomePhone = txtHomePhone.Text
                .MobilePhone = txtMobilePhone.Text
                .Congregation = txtCongregation.Text
                .Notes = txtNotes.Text

                sHospIDs = ddlHospital1.SelectedValue.ToString + "|" + _
                           ddlHospital2.SelectedValue.ToString + "|" + _
                           ddlHospital3.SelectedValue.ToString + "|"

                sDayofWeek = ddlDayOfWeek1.SelectedValue.ToString + "|" + _
                             ddlDayOfWeek2.SelectedValue.ToString + "|" + _
                             ddlDayOfWeek3.SelectedValue.ToString

                If hidMode.Value = "Add" Then
                    .AddNew(oCurrentUser.UserID, sHospIDs, sDayofWeek)
                Else
                    .Update(oCurrentUser.UserID, sHospIDs, sDayofWeek)
                End If

                '-- See if an error occurred during the AddNew/Update
                If .ErrorMessage <> "" Then
                    sErrorMsg += "<br>" & .ErrorMessage
                End If

            End With

        End If

        If sErrorMsg = "" Then

            '-- No errors occurred.  Go back to the browse page
            '
            'If oCurrentUser.UserRole = "Admin" Then
            Response.Redirect("~/Browse/BrowsePVG.aspx")
            'Else
            'btnUpdate.Visible = True
            'pnlError.Visible = False
            'pnlMessage.Visible = True
            'lblMessage.Text = "PVG Member successfully updated."
            'End If
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
        Dim oPVGMember As hlc_PVGMember
        Dim sErrorMsg As String = ""

        oCurrentUser = Session("User")

        oPVGMember = New hlc_PVGMember

        With oPVGMember
            .ID = hidID.Value
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