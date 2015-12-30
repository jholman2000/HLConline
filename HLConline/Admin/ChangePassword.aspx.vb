Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If Request.QueryString("M") = "Mandatory" Then
                lblMandatory.Visible = True
                txtCurrent.Focus()
            End If

        End If

    End Sub

    Protected Sub btnAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAction.Click
        Dim sErrorMsg As String = ""
        Dim sPassword As String = ""
        Dim oCurrentUser As hlc_User
        Dim sUserID As String = ""

        '-- This .aspx page is only used for updating the currently logged in user's password,
        '   so grab the current user object from the Session var
        '
        oCurrentUser = Session("TAGUser")

        If oCurrentUser Is Nothing Then
            '-- UserID must be passed in via querystring
            sUserID = Request.QueryString("U")
            oCurrentUser = New hlc_User
            oCurrentUser.GetUserByUserID(sUserID)
        End If

        '-- Check for required fields and flag those that are missing
        '
        If txtCurrent.Text = "" Then
            sErrorMsg += "<br>- Please enter your current password."
            txtCurrent.BackColor = Drawing.Color.LightSalmon
        Else
            If txtCurrent.Text <> oCurrentUser.Password Then
                sErrorMsg += "<br>- The current password you entered is incorrect.  Please try again."
                txtCurrent.BackColor = Drawing.Color.LightSalmon
            End If
        End If
        If txtNew.Text = "" Or txtRetype.Text = "" Then
            sErrorMsg += "<br>- Please type your new password and then type it again to confirm."
            txtNew.BackColor = Drawing.Color.LightSalmon
            txtRetype.BackColor = Drawing.Color.LightSalmon
        Else
            If txtNew.Text <> txtRetype.Text Then
                sErrorMsg += "<br>- The two new passwords you entered do not match.  Please check and try again."
                txtNew.BackColor = Drawing.Color.LightSalmon
                txtRetype.BackColor = Drawing.Color.LightSalmon
            End If
        End If

        If sErrorMsg = "" Then

            '-- Above edits passed.  Now check that the password is in the required format.
            sPassword = txtNew.Text

            If sPassword.Length < 6 Then
                sErrorMsg += "<br>- Your password MUST be at least 6 characters long.  Please choose a different password and retry."
                txtNew.BackColor = Drawing.Color.LightSalmon
            Else

                '-- All is OK.  Update the user's new password and update the Session object
                '
                oCurrentUser.Password = sPassword
                oCurrentUser.MustChangePassword = False

                If Not oCurrentUser.Update(oCurrentUser.UserID) Then

                    sErrorMsg = "<br>- " + oCurrentUser.ErrorMessage

                Else

                    '-- Update the Session var
                    Session("User") = oCurrentUser

                    '-- Direct user to the correct startup page based on their UserRole
                    '
                    Select Case oCurrentUser.UserRole
                        Case "Admin"
                            Response.Redirect("~/Browse/BrowseDoctors.aspx")
                        Case "HLC Member"
                            Response.Redirect("~/Browse/BrowseDoctors.aspx")
                    End Select

                End If

            End If
        End If

        '-- If an error occurred somewhere above, display it to the user.
        If sErrorMsg <> "" Then

            pnlError.Visible = False
            pnlError.Visible = True
            lblErrorMsg.Text = sErrorMsg
            pnlError.Height = 40 + (CountLines(sErrorMsg) * 20)

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