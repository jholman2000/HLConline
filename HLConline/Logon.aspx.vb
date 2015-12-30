Public Class Logon
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim sEmail = txtEmail.Text.ToString()
        Dim sPwd = txtPwd.Text.ToString()

        lblError.Visible = False

        If sEmail = "" Or sPwd = "" Then
            lblError.Text = "Email address and password are required"
            lblError.Visible = True
        Else
            Dim oUser As New hlc_User
            If Not oUser.Logon(sEmail, sPwd) Then
                lblError.Text = "Invalid Email address/Password. Try again."
                lblError.Visible = True

            End If
        End If
    End Sub

End Class