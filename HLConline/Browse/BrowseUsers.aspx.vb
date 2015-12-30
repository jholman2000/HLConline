Public Class BrowseUsers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oUser As hlc_User
        Dim oUtility As New hlc_Utility

        If Session("User") Is Nothing Then
            Response.Redirect("~\Logout.aspx")
        End If

        oUser = Session("User")

        Select Case oUser.UserRole

            Case "Admin"
                sdsDataSource.ConnectionString = oUtility.GetConnectionString()
                sdsDataSource.SelectCommand = "hlc_BrowseUsers"
                sdsDataSource.SelectCommandType = SqlDataSourceCommandType.StoredProcedure

            Case Else

                Response.Redirect("~/NotAuthorized.aspx")

        End Select

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        '-- Mark inactive Users
        '
        If e.Row.RowType = DataControlRowType.DataRow Then
            '-- If this User is not active, mark it in gray strikethrough
            If Not DataBinder.Eval(e.Row.DataItem, "IsActive") Then
                e.Row.Font.Strikeout = True
                e.Row.ForeColor = Drawing.Color.Gray
            End If
        End If

    End Sub
End Class