Public Class EditHospital
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oCurrentUser As hlc_User
        Dim oHospital As hlc_Hospital
        Dim oUtility As New hlc_Utility
        Dim sHospitalID As String
        Dim sTemp As String

        If Session("User") Is Nothing Then
            Response.Redirect("~\Logout.aspx")
        End If

        oCurrentUser = Session("User")

        '-- HospitalID is passed in (for edit) or is blank for a new Hospital
        '
        sHospitalID = Request.QueryString("ID")

        Select Case oCurrentUser.UserRole

            Case "Admin"

                If sHospitalID <> "" Then

                    '-- Admin (not HLC Member) can delete 
                    '
                    btnDelete.Visible = True

                    sTemp = "WARNING: Deleting this Hospital will physically remove its data from " & _
                            "the database.   " & _
                            "Are you sure you want to DELETE this Hospital?"

                    btnDelete.Attributes.Add("onclick", "return confirm('" & Server.HtmlEncode(sTemp) & "');")

                End If

        End Select

        If sHospitalID = "" Then
            '-- We are adding a new Hospital, so do not display the list of Doctors
            pnlDoctors.Visible = False
        Else
            pnlDoctors.Visible = True
            sdsDataSource.ConnectionString = oUtility.GetConnectionString()
            sdsDataSource.SelectCommand = "exec hlc_BrowseDoctorsForHospital " + sHospitalID
            sdsDataSource.SelectCommandType = SqlDataSourceCommandType.Text
        End If

        If Not IsPostBack() Then

            btnUpdate.Attributes.Add("onclick", "this.value='Processing...';")


            '-- Create a new instance of the data class and (if edit) load the values from the
            '   data tables
            '
            oHospital = New hlc_Hospital

            If sHospitalID = "" Then

                '-- ADD NEW USER
                hidMode.Value = "Add"
                lblHeading.Text = "Add a New Hospital"
                btnDelete.Visible = False

            Else

                '-- EDIT EXISTING USER

                hidMode.Value = "Edit"
                hidID.Value = sHospitalID
                lblHeading.Text = "Edit a Hospital"

                oHospital.GetHospitalByID(sHospitalID)

            End If

            '-- Display the values in the input fields
            '
            With oHospital
                If .ErrorMessage <> "" Then

                    '-- Display error that occurred when trying to load this
                    '   User's existing data
                    pnlError.Visible = True
                    lblErrorMsg.Text = .ErrorMessage
                    btnUpdate.Enabled = False

                Else

                    txtName.Text = .HospitalName
                    txtCity.Text = .City
                    txtState.Text = .State

                    txtName.Focus()

                End If

            End With

        End If


    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        ' If the current user just updated their own profile, reset the Session("User") reference
        Dim sErrorMsg As String = ""
        Dim sEmailPattern As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim oCurrentUser As hlc_User
        Dim oHospital As hlc_Hospital

        oCurrentUser = Session("User")
        oHospital = Nothing

        '-- Check for required fields and flag those that are missing
        '
        If txtName.Text = "" Then
            sErrorMsg += "<br>- Please enter the name of this Hospital."
            txtName.BackColor = Drawing.Color.LightSalmon
        End If

        If sErrorMsg = "" Then

            '-- No errors were noted, so attempt to update the record.
            '
            oHospital = New hlc_Hospital

            With oHospital
                .ID = hidID.Value
                .HospitalName = txtName.Text
                .City = txtCity.Text
                .State = txtState.Text

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

            '-- No errors occurred.  Go back to the browse page
            '
            If oCurrentUser.UserRole = "Admin" Then
                Response.Redirect("~/Browse/BrowseHospitals.aspx")
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


    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        ' Delete this HLC Hospital
        '
        ' NOTE:  The OnClick() function for this button asks the user to confirm the deletion.  This 
        '        button Click event only gets called if the user answered 'Yes'.
        '
        Dim oCurrentUser As hlc_User
        Dim oHospital As hlc_Hospital
        Dim sErrorMsg As String = ""

        oCurrentUser = Session("User")

        oHospital = New hlc_Hospital

        With oHospital
            .ID = hidID.Value
            .HospitalName = txtName.Text
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
            lblMessage.Text = "This Hospital has been successfully deleted."
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