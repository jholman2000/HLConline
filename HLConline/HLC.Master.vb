Public Class HLC
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim hlcCurrentUser As hlc_User
        Dim hlcUtility As New hlc_Utility
        Dim oTable As DataTable
        Dim oRow As DataRow

        Page.Title = "Hospital Liaison Committee"

        '-- If user is not logged in then redirect to Logon page
        '
        If Session("User") Is Nothing Then
            Response.Redirect("~/Signon.aspx")
        End If

        If Not IsPostBack Then

            hlcCurrentUser = Session("User")

            lblUserName.Text = " Welcome " & hlcCurrentUser.FullName

            '-- Check user's UserRole and remove some menu items, etc based on who they are
            '
            Select Case hlcCurrentUser.UserRole
                Case "HLC Member"
                    mnuMain.Items.Remove(mnuMain.FindItem("Admin"))
                    mnuMain.FindItem("Practices").ChildItems.Remove(mnuMain.FindItem("Practices/AddPractice"))
            End Select

            '-- Populate the dropdown lists with Hospitals and Specialties using the tables
            '   previously stored in Session vars.  If the vars do not exist yet then read
            '   them in.
            '
            If Session("Hospitals") Is Nothing Then
                Session("Hospitals") = hlcUtility.GetDataTableViaSQL("select HospitalName, ID from hlc_Hospital order by HospitalName")
            End If
            If Session("Specialties") Is Nothing Then
                Session("Specialties") = hlcUtility.GetDataTableViaSQL("select SpecialtyName, ID from hlc_Specialty order by SpecialtyName")
            End If

            oTable = Session("Hospitals")
            With ddlHospital
                .Items.Clear()
                .Items.Add(New ListItem("(All Hospitals)", -1))
                For Each oRow In oTable.Rows
                    .Items.Add(New ListItem(oRow.Item("HospitalName"), oRow.Item("ID")))
                Next
            End With

            oTable = Session("Specialties")
            With ddlSpecialty
                .Items.Clear()
                .Items.Add(New ListItem("Choose a Specialty", -1))
                For Each oRow In oTable.Rows
                    .Items.Add(New ListItem(oRow.Item("SpecialtyName"), oRow.Item("ID")))
                Next
            End With

            '-- Retrieve the last selection for the dropdowns from a Session variable
            If Not Session("HospitalID") Is Nothing Then
                ddlHospital.SelectedIndex = ddlHospital.Items.IndexOf(ddlHospital.Items.FindByValue(Session("HospitalID")))
            End If
            If Not Session("SpecialtyID") Is Nothing Then
                ddlSpecialty.SelectedIndex = ddlSpecialty.Items.IndexOf(ddlSpecialty.Items.FindByValue(Session("SpecialtyID")))
            End If

        End If

    End Sub

    Protected Sub btnFindDoctor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFindDoctor.Click
        Dim sHospitalID As String = ""
        Dim sSpecialtyID As String = ""
        Dim sHospitalName As String = ""
        Dim sSpecialtyName As String = ""

        '-- User must make a selection from both dropdowns
        '
        sSpecialtyID = ddlSpecialty.SelectedValue
        sHospitalID = ddlHospital.SelectedValue
        Session("SpecialtyID") = sSpecialtyID
        Session("HospitalID") = sHospitalID

        If sSpecialtyID = "-1" Then

            lblFindDoctorError.Text = "You must select a Specialty."
            lblFindDoctorError.Visible = True

        Else

            '-- Grab the user choices and load the results page
            '
            'sSpecialtyID = ddlSpecialty.SelectedValue
            sHospitalName = ddlHospital.SelectedItem.Text
            sSpecialtyName = ddlSpecialty.SelectedItem.Text

            Response.Redirect("~/Browse/BrowseDoctors.aspx?H=" + sHospitalID + "&S=" + sSpecialtyID + _
                              "&HN=" + Server.UrlEncode(sHospitalName) + _
                              "&SN=" + Server.UrlEncode(sSpecialtyName))
        End If
    End Sub

End Class