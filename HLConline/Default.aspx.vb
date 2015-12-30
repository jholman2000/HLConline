Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Server.Transfer("Signon.aspx")
    End Sub

End Class