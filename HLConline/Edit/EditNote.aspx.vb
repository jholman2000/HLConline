Public Class EditNote
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oCurrentUser As hlc_User
        Dim oNote As hlc_DoctorNote
        Dim sDoctorID As String
        Dim sDoctorName As String
        Dim sNoteID As String
        Dim sTemp As String

        If Session("User") Is Nothing Then
            Response.Redirect("~\Logout.aspx")
        End If

        oCurrentUser = Session("User")

        '-- DoctorID, Doctor Name and (optionally) NoteID are passed in 
        '
        sDoctorID = Request.QueryString("DoctorID")
        sDoctorName = Request.QueryString("N")
        sNoteID = Request.QueryString("ID")

        Select Case oCurrentUser.UserRole

            Case "Admin"

                If sNoteID <> "" Then

                    '-- Admin (not HLC Member) can delete 
                    '
                    btnDelete.Visible = True

                    sTemp = "Are you sure you want to DELETE this Note?"

                    btnDelete.Attributes.Add("onclick", "return confirm('" & Server.HtmlEncode(sTemp) & "');")

                End If

        End Select

        If Not IsPostBack() Then

            btnUpdate.Attributes.Add("onclick", "this.value='Processing...';")

            hidDoctorID.Value = sDoctorID
            hidDoctorName.Value = sDoctorName

            '-- Create a new instance of the data class and (if edit) load the values from the
            '   data tables
            '
            oNote = New hlc_DoctorNote

            If sNoteID = "" Then

                '-- ADD NEW USER
                hidMode.Value = "Add"
                lblHeading.Text = "Add a Note for " + sDoctorName
                btnDelete.Visible = False

            Else

                '-- EDIT EXISTING USER

                hidMode.Value = "Edit"
                hidID.Value = sNoteID
                lblHeading.Text = "Edit a Note for " + sDoctorName

                'oNote.GetNoteByID(sNoteID)

            End If

            '-- Display the values in the input fields
            '
            With oNote
                If .ErrorMessage <> "" Then

                    '-- Display error that occurred when trying to load this
                    '   User's existing data
                    pnlError.Visible = True
                    lblErrorMsg.Text = .ErrorMessage
                    btnUpdate.Enabled = False

                Else

                    txtNote.Text = .Notes

                    txtNote.Focus()

                End If

            End With
        End If
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        ' If the current user just updated their own profile, reset the Session("User") reference
        Dim sErrorMsg As String = ""
        Dim sEmailPattern As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim oCurrentUser As hlc_User
        Dim oNote As hlc_DoctorNote

        oCurrentUser = Session("User")
        oNote = Nothing

        '-- Check for required fields and flag those that are missing
        '
        If txtNote.Text = "" Then
            sErrorMsg += "<br>- You didn't enter a note!"
            txtNote.BackColor = Drawing.Color.LightSalmon
        End If

        If sErrorMsg = "" Then

            '-- No errors were noted, so attempt to update the record.
            '
            oNote = New hlc_DoctorNote

            With oNote
                .ID = Val(hidID.Value)
                .DoctorID = hidDoctorID.Value
                .UserID = oCurrentUser.UserID
                .DateEntered = Date.Now
                .Notes = txtNote.Text
                .FullName = hidDoctorName.Value

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

            '-- No errors occurred.  Go back to the View page
            '
            Response.Redirect("~/View/ViewDoctor.aspx?ID=" + hidDoctorID.Value)

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
        Dim oNote As hlc_Hospital
        Dim sErrorMsg As String = ""

        oCurrentUser = Session("User")

        oNote = New hlc_Hospital

        With oNote
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
            lblMessage.Text = "This Note has been successfully deleted."
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