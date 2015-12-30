Public Class BrowseDoctors
    Inherits System.Web.UI.Page
    Private _AlternateRow As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sHeading As String = ""
        Dim sHospitalID As String = ""
        Dim sSpecialtyID As String = ""
        Dim sHospitalName As String = ""
        Dim sSpecialtyName As String = ""
        Dim sSQL As String = ""

        sHospitalID = Request.QueryString("H")
        sSpecialtyID = Request.QueryString("S")
        sHospitalName = Request.QueryString("HN")
        sSpecialtyName = Request.QueryString("SN")

        If sHospitalID = "" And sSpecialtyID = "" Then

            lblHeading.Text = "All Doctors"

            sSQL = "exec hlc_GetDoctorsByAlpha 'A'"

            PopulateGrid(sSQL, "There are no Doctors on file with a last name starting with 'A'")

        Else

            '-- Display page header to let user know the selections chosen
            '
            sHeading = "Doctors"
            If sHospitalID <> "-1" Then
                '-- A specific hospital was chosen
                sHeading += " at " + sHospitalName
            End If
            If sSpecialtyID <> "-1" Then
                sHeading += " with " + sSpecialtyName
            End If

            lblHeading.Text = sHeading

            pnlAlphabet.Visible = False

            sSQL = "exec hlc_GetDoctorsByHospitalIDSpecialtyID " + sHospitalID + "," + sSpecialtyID

            PopulateGrid(sSQL, "No matching Doctors were found in the database for the Hospital and Specialty you selected.")

        End If

    End Sub
    Private Sub GetDoctorsByAlpha(ByVal sLetter As String)
        Dim sSQL As String = ""

        lblHeading.Text = "All Doctors"

        sSQL = "exec hlc_GetDoctorsByAlpha " + sLetter

        PopulateGrid(sSQL, "There are no Doctors on file with a last name starting with '" & sLetter & "'")

    End Sub
    Private Sub PopulateGrid(ByVal sSQL As String, ByVal sNoResults As String)
        Dim hlcUtility As New hlc_Utility
        Dim oDataTable As New DataTable
        Dim oDataRow As DataRow
        Dim sTemp As String = ""
        Dim sNameAndPractice As String = ""
        Dim sPhones As String = ""
        Dim sAttitude As String = ""
        Dim sSpecialties As String = ""
        Dim sHospitals As String = ""

        ResultsTable.Rows.Clear()

        TableHeading()

        oDataTable = hlcUtility.GetDataTableViaSQL(sSQL)

        If oDataTable Is Nothing Then

            If hlcUtility.ErrorMessage <> "" Then
                lblNoResults.Text = hlcUtility.ErrorMessage
                lblNoResults.Visible = True
                ResultsTable.Visible = False
            Else
                lblNoResults.Text = sNoResults
                lblNoResults.Visible = True
                ResultsTable.Visible = False
            End If

        Else

            lblNoResults.Visible = False
            ResultsTable.Visible = True

            For Each oDataRow In oDataTable.Rows

                '-- Doctor name and Practice address
                sNameAndPractice = "<a href=" + Chr(34) + "../View/ViewDoctor.aspx?ID=" + oDataRow.Item("DoctorID").ToString + Chr(34) + ">" + _
                    oDataRow.Item("FirstName").ToString + " " + oDataRow.Item("LastName").ToString + "</a><br>"

                sNameAndPractice += _
                oDataRow.Item("PracticeName").ToString + "<br>" + _
                oDataRow.Item("Address1").ToString + "<br>" + _
                oDataRow.Item("City").ToString + " " + oDataRow.Item("State").ToString + " " + oDataRow.Item("Zip").ToString

                '-- Phone Numbers
                sPhones = ""
                If oDataRow.Item("OfficePhone1").ToString <> "" Then sPhones += "(O): " + oDataRow.Item("OfficePhone1").ToString + "<br>"
                If oDataRow.Item("Pager").ToString <> "" Then sPhones += "(P): " + oDataRow.Item("Pager").ToString + "<br>"
                If oDataRow.Item("MobilePhone").ToString <> "" Then sPhones += "(M): " + oDataRow.Item("MobilePhone").ToString + "<br>"
                If oDataRow.Item("HomePhone").ToString <> "" Then sPhones += "(H): " + oDataRow.Item("HomePhone").ToString + "<br>"

                '-- Attitude: Build up a more readable output of Doctor's attitude.  Note: This is the
                '   same code as the hlc_Doctor.AttitudeText
                '
                Select Case oDataRow.Item("Attitude")
                    Case hlc_Doctor.AttitudeCode.Cooperative
                        sAttitude = "<b>Cooperative</b><br>"
                    Case hlc_Doctor.AttitudeCode.Favorable
                        sAttitude = "<b>Favorable</b><br>"
                    Case hlc_Doctor.AttitudeCode.Limitations
                        sAttitude = "<b>Favorable with Limitations</b><br>"
                    Case hlc_Doctor.AttitudeCode.NotFavorable
                        sAttitude = "<b>Not Favorable</b><br>"
                    Case hlc_Doctor.AttitudeCode.Unknown
                        sAttitude = "<em>Unknown</em><br>"
                End Select

                '-- ADULT
                If oDataRow.Item("FavAdultEmergency") And oDataRow.Item("FavAdultNonEmergency") Then
                    sAttitude += "&bull;&nbsp;Favorable for Adult Emergency and Non-Emergency<br>"
                End If
                If oDataRow.Item("FavAdultEmergency") And Not oDataRow.Item("FavAdultNonEmergency") Then
                    sAttitude += "&bull;&nbsp;Favorable Adult Emergency only<br>"
                End If
                If Not oDataRow.Item("FavAdultEmergency") And oDataRow.Item("FavAdultNonEmergency") Then
                    sAttitude += "&bull;&nbsp;Favorable Adult Non-Emergency only<br>"
                End If
                '-- CHILD
                If oDataRow.Item("FavChildEmergency") And oDataRow.Item("FavChildNonEmergency") Then
                    sAttitude += "&bull;&nbsp;Favorable for Child Emergency and Non-Emergency<br>"
                End If
                If oDataRow.Item("FavChildEmergency") And Not oDataRow.Item("FavChildNonEmergency") Then
                    sAttitude += "&bull;&nbsp;Favorable Child Emergency only<br>"
                End If
                If Not oDataRow.Item("FavChildEmergency") And oDataRow.Item("FavChildNonEmergency") Then
                    sAttitude += "&bull;&nbsp;Favorable Child Non-Emergency only<br>"
                End If
                If oDataRow.Item("NotFavAdult") And oDataRow.Item("NotFavChild") Then
                    sAttitude += "&bull;&nbsp;NOT Favorable for Adult OR Child Emergency<br>"
                Else
                    If oDataRow.Item("NotFavAdult") Then
                        sAttitude += "&bull;&nbsp;NOT Favorable for Adult Emergency<br>"
                    End If
                    If oDataRow.Item("NotFavChild") Then
                        sAttitude += "&bull;&nbsp;NOT Favorable for Child Emergency<br>"
                    End If
                End If
                If oDataRow.Item("ConsultAdultEmergency") And oDataRow.Item("ConsultChildEmergency") Then
                    sAttitude += "&bull;&nbsp;Will consult for Adult and Child<br>"
                Else
                    If oDataRow.Item("ConsultAdultEmergency") Then
                        sAttitude += "&bull;&nbsp;Will consult for Adult<br>"
                    End If
                    If oDataRow.Item("ConsultChildEmergency") Then
                        sAttitude += "&bull;&nbsp;Will consult for Child<br>"
                    End If
                End If
                If oDataRow.Item("AcceptsMedicaid") Then
                    sAttitude += "&bull;&nbsp;Accepts Medicaid"
                End If

                '-- Specialties: Put line breaks at the commas
                sSpecialties = Replace(oDataRow.Item("Specialties"), ",", "<br>")
                sHospitals = Replace(oDataRow.Item("Hospitals"), ",", "<br>")

                AppendRow(sNameAndPractice, sPhones, sSpecialties, sHospitals, sAttitude)

            Next

        End If

    End Sub
    Private Sub TableHeading()
        Dim oTableRow As HtmlTableRow
        Dim oTableCell As HtmlTableCell

        oTableRow = New HtmlTableRow

        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.BgColor = "#404040"
        oTableCell.Width = "180px"
        oTableCell.InnerHtml = "<font color=white><b>Name & Practice</b></font>"
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.BgColor = "#404040"
        oTableCell.Width = "110px"
        oTableCell.InnerHtml = "<font color=white><b>Phone Numbers</b></font>"
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.BgColor = "#404040"
        oTableCell.Width = "120px"
        oTableCell.InnerHtml = "<font color=white><b>Specialties</b></font>"
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.BgColor = "#404040"
        oTableCell.Width = "120px"
        oTableCell.InnerHtml = "<font color=white><b>Hospitals</b></font>"
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.VAlign = "top"
        oTableCell.BgColor = "#404040"
        oTableCell.Width = "180px"
        oTableCell.InnerHtml = "<font color=white><b>Attitude</b></font>"
        oTableRow.Cells.Add(oTableCell)

        ResultsTable.Rows.Add(oTableRow)

    End Sub
    Private Sub AppendRow(ByVal sNameAndPractice As String, ByVal sPhones As String, _
                          ByVal sSpecialties As String, ByVal sHospitals As String, _
                          ByVal sAttitude As String)
        Dim oTableRow As HtmlTableRow
        Dim oTableCell As HtmlTableCell
        Dim sTemp As String = ""

        oTableRow = New HtmlTableRow

        _AlternateRow = Not _AlternateRow
        If _AlternateRow Then
            oTableRow.BgColor = "Cornsilk" ' "#cccccc"
        End If

        oTableCell = New HtmlTableCell
        oTableCell.Attributes("Style") = "border-bottom: solid 1px #c0c0c0;"
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = sNameAndPractice
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.Attributes("Style") = "border-bottom: solid 1px #c0c0c0;"
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = sPhones
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.Attributes("Style") = "border-bottom: solid 1px #c0c0c0;"
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = sSpecialties
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.Attributes("Style") = "border-bottom: solid 1px #c0c0c0;"
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = sHospitals
        oTableRow.Cells.Add(oTableCell)

        oTableCell = New HtmlTableCell
        oTableCell.Attributes("Style") = "border-bottom: solid 1px #c0c0c0;"
        oTableCell.VAlign = "top"
        oTableCell.InnerHtml = sAttitude
        oTableRow.Cells.Add(oTableCell)

        ResultsTable.Rows.Add(oTableRow)
    End Sub


    Protected Sub btnA_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnA.Click
        GetDoctorsByAlpha("A")
    End Sub


    Protected Sub btnB_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnB.Click
        GetDoctorsByAlpha("B")
    End Sub

    Protected Sub btnC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnC.Click
        GetDoctorsByAlpha("C")
    End Sub

    Protected Sub btnD_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnD.Click
        GetDoctorsByAlpha("D")
    End Sub

    Protected Sub btnE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnE.Click
        GetDoctorsByAlpha("E")
    End Sub

    Protected Sub btnF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnF.Click
        GetDoctorsByAlpha("F")
    End Sub

    Protected Sub btnG_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnG.Click
        GetDoctorsByAlpha("G")
    End Sub

    Protected Sub btnH_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnH.Click
        GetDoctorsByAlpha("H")
    End Sub

    Protected Sub btnI_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnI.Click
        GetDoctorsByAlpha("I")
    End Sub

    Protected Sub btnJ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnJ.Click
        GetDoctorsByAlpha("J")
    End Sub

    Protected Sub btnK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnK.Click
        GetDoctorsByAlpha("K")
    End Sub

    Protected Sub btnL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnL.Click
        GetDoctorsByAlpha("L")
    End Sub

    Protected Sub btnM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnM.Click
        GetDoctorsByAlpha("M")
    End Sub

    Protected Sub btnN_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnN.Click
        GetDoctorsByAlpha("N")
    End Sub

    Protected Sub btnO_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnO.Click
        GetDoctorsByAlpha("O")
    End Sub

    Protected Sub btnP_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnP.Click
        GetDoctorsByAlpha("P")
    End Sub

    Protected Sub btnQ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnQ.Click
        GetDoctorsByAlpha("Q")
    End Sub

    Protected Sub btnR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnR.Click
        GetDoctorsByAlpha("R")
    End Sub

    Protected Sub btnS_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnS.Click
        GetDoctorsByAlpha("S")
    End Sub

    Protected Sub btnT_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnT.Click
        GetDoctorsByAlpha("T")
    End Sub

    Protected Sub btnU_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnU.Click
        GetDoctorsByAlpha("U")
    End Sub

    Protected Sub btnV_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnV.Click
        GetDoctorsByAlpha("V")
    End Sub

    Protected Sub btnW_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnW.Click
        GetDoctorsByAlpha("W")
    End Sub

    Protected Sub btnXYZ_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnXYZ.Click
        GetDoctorsByAlpha("X")
    End Sub
End Class