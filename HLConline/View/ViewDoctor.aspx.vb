Public Class ViewDoctor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim hlcCurrentUser As hlc_User
        Dim hlcDoctor As hlc_Doctor
        Dim sID As String = ""
        Dim sCRLF As String = "<br>"        ' Carriage return/linefeed (for HTML)
        Dim sTemp As String = ""

        If Not IsPostBack Then

            hlcCurrentUser = Session("User")
            sID = Request.QueryString("ID")
            hlcDoctor = New hlc_Doctor

            If Not hlcDoctor.GetDoctorByID(sID) Then

                '-- Unable to retrieve Doctor information
                '
                lblAddress.Text = hlcDoctor.ErrorMessage
                lblAddress.ForeColor = Drawing.Color.DarkRed
                lblAddress.Font.Bold = True

            Else

                '-- Display the Doctor information in the appropriate areas
                '
                With hlcDoctor

                    '-- Name & address
                    '
                    lblName.Text = .FirstName + " " + .LastName
                    lblName.Font.Bold = True
                    sTemp = ""
                    If .Practice.PracticeName <> "" Then sTemp += .Practice.PracticeName + sCRLF
                    If .Practice.Address1 <> "" Then sTemp += .Practice.Address1 + sCRLF
                    If .Practice.Address2 <> "" Then sTemp += .Practice.Address2 + sCRLF
                    If .Practice.Address3 <> "" Then sTemp += .Practice.Address3 + sCRLF
                    If .Practice.City <> "" Then sTemp += .Practice.City + " " + .Practice.State + " " + .Practice.Zip + sCRLF
                    lblAddress.Text = sTemp

                    '-- Phone numbers
                    lblOfficePhone1.Text = .Practice.OfficePhone1
                    If .Practice.OfficePhone2 <> "" Then
                        lblOfficePhone2Label.Visible = True
                        lblOfficePhone2.Text = .Practice.OfficePhone2
                    End If
                    lblFax.Text = .Practice.Fax
                    lblMobilePhone.Text = .MobilePhone
                    lblHomePhone.Text = .HomePhone
                    lblPager.Text = .Pager
                    lblEmailAddress.Text = .EmailAddress
                    If .Practice.WebsiteURL <> "" Then
                        lblWebsiteURL.Text = .Practice.WebsiteURL
                    Else
                        lblWebsiteURLLabel.Visible = False
                        lblWebsiteURL.Visible = False
                    End If

                    lblStatus.Text = .StatusText

                    chkIsBSMP.Checked = .IsBSMP
                    chkIsHRP.Checked = .IsHRP
                    If .PeerReview <> "" Then
                        lblPeerReviewLabel.Visible = True
                        lblPeerReview.Visible = True
                        lblPeerReview.Text = .PeerReview
                    End If

                    '-- Attitude
                    '
                    Select Case .Attitude
                        Case hlc_Doctor.AttitudeCode.Cooperative
                            imgAttitude.ImageUrl = "~/images/Cooperative.jpg"
                        Case hlc_Doctor.AttitudeCode.Favorable
                            imgAttitude.ImageUrl = "~/images/Favorable.jpg"
                        Case hlc_Doctor.AttitudeCode.Limitations
                            imgAttitude.ImageUrl = "~/images/Limitations.jpg"
                        Case hlc_Doctor.AttitudeCode.NotFavorable
                            imgAttitude.ImageUrl = "~/images/NotFavorable.jpg"
                        Case Else
                            imgAttitude.ImageUrl = "~/images/Unknown.jpg"
                    End Select
                    If .AttitudeText <> "" Then
                        lblAttitudeText.Text = .AttitudeText
                    End If

                    If .HospitalsText <> "" Then
                        lblHospital.Text = .HospitalsText
                    End If

                    If .SpecialtiesText <> "" Then
                        lblSpecialty.Text = .SpecialtiesText
                    End If

                    .GetNotes()
                    Dim hlcDoctorNote As hlc_DoctorNote
                    Dim sNoteHeader As String = ""
                    Dim sNote As String = ""
                    If Not .DoctorNotes Is Nothing Then

                        NotesTable.Rows.Clear()
                        For Each hlcDoctorNote In .DoctorNotes
                            sNoteHeader = "On " + hlcDoctorNote.DateEntered.ToString() + _
                                          " " + hlcDoctorNote.FullName + " wrote:"
                            AppendRow(sNoteHeader, hlcDoctorNote.Notes)
                        Next
                    End If
                End With
            End If
        End If
    End Sub
    Private Sub AppendRow(ByVal NoteHeader As String, ByVal Note As String)
        Dim oTableRow As HtmlTableRow
        Dim oTableCell As HtmlTableCell
        Dim sTemp As String = ""

        oTableRow = New HtmlTableRow
        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = "<b><em>" + NoteHeader + "</em></b>"
        oTableRow.Cells.Add(oTableCell)
        NotesTable.Rows.Add(oTableRow)

        oTableRow = New HtmlTableRow
        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = Note
        oTableRow.Cells.Add(oTableCell)
        NotesTable.Rows.Add(oTableRow)

        oTableRow = New HtmlTableRow
        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = "&nbsp;"
        oTableRow.Cells.Add(oTableCell)
        NotesTable.Rows.Add(oTableRow)

    End Sub

    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChange.Click
        Response.Redirect("~/Edit/EditDoctor.aspx?ID=" + Request.QueryString("ID"))
    End Sub

    Protected Sub btnNotes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNotes.Click
        Server.Transfer("~/Edit/EditNote.aspx?DoctorID=" + Request.QueryString("ID") + "&N=" + Server.UrlEncode(lblName.Text))
    End Sub
End Class