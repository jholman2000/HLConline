Public Class EditDoctor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oCurrentUser As hlc_User
        Dim oDoctor As hlc_Doctor
        Dim sDoctorID As String
        Dim sTemp As String
        Dim oTable As DataTable
        Dim oDoctorHospital As hlc_DoctorHospital
        Dim oDoctorSpecialty As hlc_DoctorSpecialty
        Dim oPractice As hlc_Practice
        Dim oUtility As New hlc_Utility
        Dim i As Integer = 0

        If Session("User") Is Nothing Then
            Response.Redirect("~\Logout.aspx")
        End If

        oCurrentUser = Session("User")

        '-- DoctorID is passed in (for edit) or is blank for a new Doctor
        '
        sDoctorID = Request.QueryString("ID")

        '-- Modify form to display/hide items based on the current users's UserRole
        '
        Select Case oCurrentUser.UserRole

            Case "Admin"


                If sDoctorID <> "" Then

                    '-- Admin (not HLC Member) can delete users
                    '
                    btnDelete.Visible = True

                    sTemp = "WARNING: Deleting this Doctor will physically remove their data from " & _
                            "the database (including past posts/notes that have been entered).  " & _
                            "Are you sure you want to DELETE this Doctor?"

                    btnDelete.Attributes.Add("onclick", "return confirm('" + Server.HtmlEncode(sTemp) + "');")
                End If

        End Select

        sdsPractice.ConnectionString = oUtility.GetConnectionString()
        sdsPractice.SelectCommand = "hlc_DropDownPractice"
        sdsPractice.SelectCommandType = SqlDataSourceCommandType.StoredProcedure

        If Not IsPostBack() Then

            btnUpdate.Attributes.Add("onclick", "this.value='Processing...';")

            '-- Create a new instance of the data class and (if edit) load the values from the
            '   data tables
            '
            oDoctor = New hlc_Doctor

            If sDoctorID = "" Then

                '-- ADD NEW DOCTOR

                hidMode.Value = "Add"
                lblHeading.Text = "Add a New Doctor"
                btnDelete.Visible = False

            Else

                '-- EDIT EXISTING DOCTOR

                hidMode.Value = "Edit"
                hidID.Value = sDoctorID
                lblHeading.Text = "Edit a Doctor"

                oDoctor.GetDoctorByID(sDoctorID)

            End If

            '-- Display the values in the input fields
            '
            With oDoctor

                If .ErrorMessage <> "" Then

                    '-- Display error that occurred when trying to load this
                    '   User's existing data
                    pnlError.Visible = True
                    lblErrorMsg.Text = .ErrorMessage
                    btnUpdate.Enabled = False

                Else

                    txtFirstName.Text = .FirstName
                    txtLastName.Text = .LastName
                    txtMobilePhone.Text = .MobilePhone
                    txtPager.Text = .Pager
                    txtHomePhone.Text = .HomePhone
                    txtEmailAddress.Text = .EmailAddress

                    chkIsBSMP.Checked = .IsBSMP
                    chkIsHRP.Checked = .IsHRP
                    txtPeerReview.Text = .PeerReview

                    ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(.Status))
                    hidOriginalStatus.Value = .Status
                    hidStatusDate.Value = .StatusDate.ToString("MM/dd/yyyy")

                    '-- Admin may add Practices
                    If oCurrentUser.UserRole = "Admin" Then
                        ddlPractice.Items.Add(New ListItem("(Create a new Practice)", "-1"))
                    End If

                    ddlPractice.DataBind()
                    ddlPractice.SelectedIndex = ddlPractice.Items.IndexOf(ddlPractice.Items.FindByValue(.PracticeID))

                    '-- If this Doctor already has a Practice assigned, display its address
                    If .PracticeID <> 0 Then
                        pnlPractice.Visible = False
                        lblPracticeAddress.Visible = True
                        oPractice = New hlc_Practice
                        oPractice.GetPracticeByID(ddlPractice.SelectedValue)
                        lblPracticeAddress.Text = oPractice.AddressText
                    End If

                    '-- Populate Attitude section
                    '
                    ddlAttitudeCode.SelectedIndex = ddlAttitudeCode.Items.IndexOf(ddlAttitudeCode.Items.FindByValue(.Attitude))

                    ddlRegContacted.SelectedIndex = ddlRegContacted.Items.IndexOf(ddlRegContacted.Items.FindByValue(.RegContacted))
                    ddlSpecificallyKnown.SelectedIndex = ddlSpecificallyKnown.Items.IndexOf(ddlSpecificallyKnown.Items.FindByValue(.SpecificallyKnown))
                    ddlHelpful.SelectedIndex = ddlHelpful.Items.IndexOf(ddlHelpful.Items.FindByValue(.Helpful))
                    ddlFrequentlyTreat.SelectedIndex = ddlFrequentlyTreat.Items.IndexOf(ddlFrequentlyTreat.Items.FindByValue(.FrequentlyTreat))
                    txtTreatYears.Text = .TreatYears.ToString

                    chkAcceptsMedicaid.Checked = .AcceptsMedicaid
                    chkConsultAdultEmergency.Checked = .ConsultAdultEmergency
                    chkConsultChildEmergency.Checked = .ConsultChildEmergency
                    chkFavAdultEmergency.Checked = .FavAdultEmergency
                    chkFavAdultNonEmergency.Checked = .FavAdultNonEmergency
                    chkFavChildEmergency.Checked = .FavChildEmergency
                    chkFavChildNonEmergency.Checked = .FavChildNonEmergency
                    chkNotFavAdult.Checked = .NotFavAdult
                    chkNotFavChild.Checked = .NotFavChild

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
                    With ddlHospital4
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("HospitalName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlHospital5
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("HospitalName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlHospital6
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("HospitalName"), oRow.Item("ID")))
                        Next
                    End With
                    If .DoctorHospitals.Count > 0 Then
                        i = 0
                        For Each oDoctorHospital In .DoctorHospitals
                            i += 1
                            Select Case i
                                Case 1
                                    ddlHospital1.SelectedIndex = ddlHospital1.Items.IndexOf(ddlHospital1.Items.FindByValue(oDoctorHospital.HospitalID))
                                    txtHospitalNotes1.Text = oDoctorHospital.Notes
                                Case 2
                                    ddlHospital2.SelectedIndex = ddlHospital2.Items.IndexOf(ddlHospital2.Items.FindByValue(oDoctorHospital.HospitalID))
                                    txtHospitalNotes2.Text = oDoctorHospital.Notes
                                Case 3
                                    ddlHospital3.SelectedIndex = ddlHospital3.Items.IndexOf(ddlHospital3.Items.FindByValue(oDoctorHospital.HospitalID))
                                    txtHospitalNotes3.Text = oDoctorHospital.Notes
                                Case 4
                                    ddlHospital4.SelectedIndex = ddlHospital4.Items.IndexOf(ddlHospital4.Items.FindByValue(oDoctorHospital.HospitalID))
                                    txtHospitalNotes4.Text = oDoctorHospital.Notes
                                Case 5
                                    ddlHospital5.SelectedIndex = ddlHospital5.Items.IndexOf(ddlHospital5.Items.FindByValue(oDoctorHospital.HospitalID))
                                    txtHospitalNotes5.Text = oDoctorHospital.Notes
                                Case 6
                                    ddlHospital6.SelectedIndex = ddlHospital6.Items.IndexOf(ddlHospital6.Items.FindByValue(oDoctorHospital.HospitalID))
                                    txtHospitalNotes6.Text = oDoctorHospital.Notes
                            End Select
                        Next
                    End If

                    '-- SPECIALTIES
                    '
                    oTable = Session("Specialties")
                    With ddlSpecialty1
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("SpecialtyName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlSpecialty2
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("SpecialtyName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlSpecialty3
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("SpecialtyName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlSpecialty4
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("SpecialtyName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlSpecialty5
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("SpecialtyName"), oRow.Item("ID")))
                        Next
                    End With
                    With ddlSpecialty6
                        .Items.Clear()
                        .Items.Add(New ListItem("", 0))
                        For Each oRow In oTable.Rows
                            .Items.Add(New ListItem(oRow.Item("SpecialtyName"), oRow.Item("ID")))
                        Next
                    End With
                    If .DoctorSpecialties.Count > 0 Then
                        i = 0
                        For Each oDoctorSpecialty In .DoctorSpecialties
                            i += 1
                            Select Case i
                                Case 1
                                    ddlSpecialty1.SelectedIndex = ddlSpecialty1.Items.IndexOf(ddlSpecialty1.Items.FindByValue(oDoctorSpecialty.SpecialtyID))
                                    txtSpecialtyNotes1.Text = oDoctorSpecialty.AreaOfExpertise
                                Case 2
                                    ddlSpecialty2.SelectedIndex = ddlSpecialty2.Items.IndexOf(ddlSpecialty2.Items.FindByValue(oDoctorSpecialty.SpecialtyID))
                                    txtSpecialtyNotes2.Text = oDoctorSpecialty.AreaOfExpertise
                                Case 3
                                    ddlSpecialty3.SelectedIndex = ddlSpecialty3.Items.IndexOf(ddlSpecialty3.Items.FindByValue(oDoctorSpecialty.SpecialtyID))
                                    txtSpecialtyNotes3.Text = oDoctorSpecialty.AreaOfExpertise
                                Case 4
                                    ddlSpecialty4.SelectedIndex = ddlSpecialty4.Items.IndexOf(ddlSpecialty4.Items.FindByValue(oDoctorSpecialty.SpecialtyID))
                                    txtSpecialtyNotes4.Text = oDoctorSpecialty.AreaOfExpertise
                                Case 5
                                    ddlSpecialty5.SelectedIndex = ddlSpecialty5.Items.IndexOf(ddlSpecialty5.Items.FindByValue(oDoctorSpecialty.SpecialtyID))
                                    txtSpecialtyNotes5.Text = oDoctorSpecialty.AreaOfExpertise
                                Case 6
                                    ddlSpecialty6.SelectedIndex = ddlSpecialty6.Items.IndexOf(ddlSpecialty6.Items.FindByValue(oDoctorSpecialty.SpecialtyID))
                                    txtSpecialtyNotes6.Text = oDoctorSpecialty.AreaOfExpertise
                            End Select
                        Next

                    End If


                    txtFirstName.Focus()

                End If

            End With
        End If
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        Dim sErrorMsg As String = ""
        Dim sEmailPattern As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim oCurrentUser As hlc_User
        Dim oDoctor As hlc_Doctor
        Dim sDocHospIDs As String = ""
        Dim sDocHospNotes As String = ""
        Dim sDocSpecIDs As String = ""
        Dim sDocSpecNotes As String = ""

        'Dim sEmail As String
        'Dim hlcUtility As hlc_Utility

        oCurrentUser = Session("User")
        oDoctor = Nothing

        '-- Check for required fields and flag those that are missing
        '
        If Trim(txtFirstName.Text) = "" Or Trim(txtLastName.Text) = "" Then
            sErrorMsg += "<br>- Please enter this Doctor's first and last name."
            txtFirstName.BackColor = Drawing.Color.LightSalmon
        End If
        If Trim(txtEmailAddress.Text) <> "" Then
            If Not Regex.IsMatch(Trim(txtEmailAddress.Text), sEmailPattern) Then
                sErrorMsg += "<br>- The email address entered is not formatted as a valid email address."
                txtEmailAddress.BackColor = Drawing.Color.LightSalmon
            End If
        End If

        If sErrorMsg = "" Then

            '-- No errors were noted, so attempt to update the record.
            '
            oDoctor = New hlc_Doctor

            With oDoctor
                .ID = hidID.Value
                .FirstName = txtFirstName.Text
                .LastName = txtLastName.Text
                .PracticeID = ddlPractice.SelectedValue
                If .PracticeID = -1 Then
                    '-- User is creating a new Practice
                    .Practice.PracticeName = txtPracticeName.Text
                    .Practice.Address1 = txtPracticeAddress1.Text
                    .Practice.Address2 = txtPracticeAddress2.Text
                    .Practice.Address3 = ""
                    .Practice.City = txtPracticeCity.Text
                    .Practice.State = txtPracticeState.Text
                    .Practice.Zip = txtPracticeZip.Text
                    .Practice.OfficePhone1 = txtPracticePhone1.Text
                    .Practice.OfficePhone2 = txtPracticePhone2.Text
                    .Practice.Fax = txtPracticeFax.Text
                End If
                .HomePhone = txtHomePhone.Text
                .MobilePhone = txtMobilePhone.Text
                .Pager = txtPager.Text
                .EmailAddress = txtEmailAddress.Text
                .Status = ddlStatus.SelectedValue
                ' 12/3/2015: If status just changed then reset the date to today
                If hidOriginalStatus.Value <> ddlStatus.SelectedValue Then
                    .StatusDate = Date.Today
                Else
                    .StatusDate = CDate(hidStatusDate.Value)
                End If
                .IsBSMP = chkIsBSMP.Checked
                .IsHRP = chkIsHRP.Checked
                .PeerReview = Left(txtPeerReview.Text, 120)

                .Attitude = ddlAttitudeCode.SelectedValue
                .AcceptsMedicaid = chkAcceptsMedicaid.Checked
                .ConsultAdultEmergency = chkConsultAdultEmergency.Checked
                .ConsultChildEmergency = chkConsultChildEmergency.Checked
                .FavAdultEmergency = chkFavAdultEmergency.Checked
                .FavAdultNonEmergency = chkFavAdultNonEmergency.Checked
                .FavChildEmergency = chkFavChildEmergency.Checked
                .FavChildNonEmergency = chkFavChildNonEmergency.Checked
                .NotFavAdult = chkNotFavAdult.Checked
                .NotFavChild = chkNotFavChild.Checked
                .RegContacted = ddlRegContacted.SelectedValue
                .SpecificallyKnown = ddlSpecificallyKnown.SelectedValue
                .Helpful = ddlHelpful.SelectedValue
                .FrequentlyTreat = ddlFrequentlyTreat.SelectedValue
                .TreatYears = Val(txtTreatYears.Text)

                sDocHospIDs = ddlHospital1.SelectedValue.ToString + "|" + _
                              ddlHospital2.SelectedValue.ToString + "|" + _
                              ddlHospital3.SelectedValue.ToString + "|" + _
                              ddlHospital4.SelectedValue.ToString + "|" + _
                              ddlHospital5.SelectedValue.ToString + "|" + _
                              ddlHospital6.SelectedValue.ToString
                sDocHospNotes = Replace(txtHospitalNotes1.Text, "|", "/") + "|" + _
                                Replace(txtHospitalNotes2.Text, "|", "/") + "|" + _
                                Replace(txtHospitalNotes3.Text, "|", "/") + "|" + _
                                Replace(txtHospitalNotes4.Text, "|", "/") + "|" + _
                                Replace(txtHospitalNotes5.Text, "|", "/") + "|" + _
                                Replace(txtHospitalNotes6.Text, "|", "/")
                sDocSpecIDs = ddlSpecialty1.SelectedValue.ToString + "|" + _
                              ddlSpecialty2.SelectedValue.ToString + "|" + _
                              ddlSpecialty3.SelectedValue.ToString + "|" + _
                              ddlSpecialty4.SelectedValue.ToString + "|" + _
                              ddlSpecialty5.SelectedValue.ToString + "|" + _
                              ddlSpecialty6.SelectedValue.ToString
                sDocSpecNotes = Replace(txtSpecialtyNotes1.Text, "|", "/") + "|" + _
                                Replace(txtSpecialtyNotes2.Text, "|", "/") + "|" + _
                                Replace(txtSpecialtyNotes3.Text, "|", "/") + "|" + _
                                Replace(txtSpecialtyNotes4.Text, "|", "/") + "|" + _
                                Replace(txtSpecialtyNotes5.Text, "|", "/") + "|" + _
                                Replace(txtSpecialtyNotes6.Text, "|", "/")

                If hidMode.Value = "Add" Then
                    .AddNew(oCurrentUser.UserID, sDocHospIDs, sDocHospNotes, sDocSpecIDs, sDocSpecNotes)
                    hidID.Value = .ID.ToString
                Else
                    .Update(oCurrentUser.UserID, sDocHospIDs, sDocHospNotes, sDocSpecIDs, sDocSpecNotes)
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
            Response.Redirect("~/View/ViewDoctor.aspx?ID=" + hidID.Value)
            'If oCurrentUser.UserRole = "Admin" Then
            '    Response.Redirect("~/Browse/BrowseDoctors.aspx")
            'Else
            '    btnUpdate.Visible = True
            '    pnlError.Visible = False
            '    pnlMessage.Visible = True
            '    lblMessage.Text = "Doctor successfully updated."
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

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        ' Delete this HLC Doctor
        '
        ' NOTE:  The OnClick() function for this button asks the user to confirm the deletion.  This 
        '        button Click event only gets called if the user answered 'Yes'.
        '
        Dim oCurrentUser As hlc_User
        Dim oDoctor As hlc_Doctor
        Dim sErrorMsg As String = ""

        oCurrentUser = Session("User")

        oDoctor = New hlc_Doctor

        With oDoctor
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
            lblMessage.Text = "This Doctor has been successfully deleted."
            btnDelete.Visible = False
            btnUpdate.Visible = False

        End If

    End Sub

    Protected Sub ddlPractice_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPractice.SelectedIndexChanged
        Dim oPractice As hlc_Practice

        Select Case ddlPractice.SelectedValue
            Case "0"
                '-- No selection made
                pnlPractice.Visible = False
                lblPracticeAddress.Visible = False

            Case "-1"
                pnlPractice.Visible = True
                lblPracticeAddress.Visible = False

            Case Else
                pnlPractice.Visible = False
                lblPracticeAddress.Visible = True
                oPractice = New hlc_Practice
                oPractice.GetPracticeByID(ddlPractice.SelectedValue)
                lblPracticeAddress.Text = oPractice.AddressText

        End Select
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