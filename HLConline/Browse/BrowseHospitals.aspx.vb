Public Class BrowseHospitals
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oUtility As New hlc_Utility

        If Session("User") Is Nothing Then
            Response.Redirect("~\LogOut.aspx")
        End If

        sdsDataSource.ConnectionString = oUtility.GetConnectionString()
        sdsDataSource.SelectCommand = "hlc_BrowseHospitals"
        sdsDataSource.SelectCommandType = SqlDataSourceCommandType.StoredProcedure

    End Sub
End Class