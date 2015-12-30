Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim oUser As New hlc_User

        If oUser.Logon("admin", "hlcman") Then
            lblName.Text = oUser.FullName

        End If

    End Sub
End Class