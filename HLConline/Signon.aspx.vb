Public Class Signon
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            '-- Retrieve this user's logon ID from a cookie on their machine
            '   (if one is there)
            '
            If Not IsNothing(Request.Cookies("HLC_Logon")) Then
                '-- There IS a cookie, so retrieve it and also check the
                '   Remember box
                txtUserID.Text = Request.Cookies("HLC_Logon").Value
                chkRememberMe.Checked = True
                txtPassword.Focus()
            Else
                txtUserID.Focus()
            End If

            '-- If we were redirected here from SignOff.aspx, alert the user.
            '
            If Request.QueryString("A") = "LO" Then
                lblErrorMessage.Text = "You were successfully logged out of the system."
                lblErrorMessage.Visible = True
            End If


        End If
    End Sub

    Protected Sub btnLogon_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogon.Click
        '--

        If txtUserID.Text = "" Or txtPassword.Text = "" Then

            lblErrorMessage.Text = "Please enter your User ID and password."
            lblErrorMessage.Visible = True

        Else
            Dim hlcUser As New hlc_User
            Dim UserCookie As HttpCookie

            If hlcUser.Logon(txtUserID.Text, txtPassword.Text) Then

                If Not hlcUser.IsActive Then
                    '-- The hlc_User class is set to return True if the user
                    '   logged in correctly but has been flagged as inactive.
                    '
                    lblErrorMessage.Text = "Sorry, you are not authorized to use this site."
                    lblErrorMessage.Visible = True

                Else

                    '-- Log in is OK and user is Active. Store this user in a Session variable 
                    '   so we can refer to their info within the site.
                    '
                    Session("User") = hlcUser

                    '-- IF the user has requested, then save the user's logon ID in a 
                    '   persistent(cookie)
                    '
                    If chkRememberMe.Checked Then
                        UserCookie = New HttpCookie("HLC_Logon")
                        UserCookie.Value = hlcUser.UserID
                        UserCookie.Expires = #12/31/2011#
                        Response.AppendCookie(UserCookie)
                    End If

                    '-- Now that user has logged in, determine the next form to show
                    '
                    If hlcUser.MustChangePassword Then

                        '-- User has been flagged as required to change their password
                        '   on the next login (usually when this is the first time
                        '   logging in or if the Admin reset their password).
                        Response.Redirect("~/Admin/ChangePassword.aspx?M=Mandatory&U=" + hlcUser.UserID)

                    Else

                        '-- Direct user to the correct startup page based on their UserRole
                        '
                        Select Case hlcUser.UserRole
                            Case "Admin"
                                Response.Redirect("~/Browse/BrowseDoctors.aspx")
                            Case "HLC Member"
                                Response.Redirect("~/Browse/BrowseDoctors.aspx")
                        End Select

                    End If

                End If

            Else

                '-- Logon failed for some reason.  The reason should be in the
                '   ErrorMessage property
                '
                lblErrorMessage.Text = hlcUser.ErrorMessage
                lblErrorMessage.Visible = True

            End If


        End If
    End Sub

End Class