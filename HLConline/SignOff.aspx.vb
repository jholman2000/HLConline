Public Class SignOff
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        '-- To log this user out, simply abandon the Session (wiping out Session("TAGUser") and others)
        Session("User") = Nothing
        Session.Abandon()
        Server.Transfer("Signon.aspx?A=LO")
    End Sub

End Class