Public Class ForgotPassword
    Inherits System.Web.UI.Page

    Protected Sub btnAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAction.Click
        '-- User has asked for their password to be sent to their email
        '   address.  Grab the info for this user and send the email
        '
        Dim oUser As New hlc_User
        Dim oUtility As New hlc_Utility

        If txtUserID.Text = "" Then
            lblMessage.Text = "Please enter your User ID."
            lblMessage.Visible = True
            lblMessage.ForeColor = Drawing.Color.Red
            lblMessage.Font.Bold = True
        Else

            If oUser.GetUserByUserID(txtUserID.Text) Then

                '-- Turn off data entry controls and let user know we've sent their
                '   password via email.
                lblUserID.Visible = False
                txtUserID.Visible = False
                btnAction.Visible = False

                If oUser.EmailAddress = "" Then

                    '-- No email has been entered for this user
                    '
                    lblMessage.Text = "We're sorry, but you do not appear to have an email address associated with your account.  Please contact the HLC site administrator to obtain your password."
                    lblMessage.Visible = True
                    lblMessage.ForeColor = Drawing.Color.Red
                    lblMessage.Font.Bold = True

                Else

                    Dim sEmail As String

                    '-- Load the email template and fill in the variables
                    '
                    sEmail = System.IO.File.ReadAllText( _
                        Server.MapPath("\") & "\HLC\App_Data" & "/ForgotPassword.htm")

                    sEmail = Replace(sEmail, "_EmailHeader", "Forgot Password")
                    sEmail = Replace(sEmail, "_EmailBody", "Your logon password is: <b>" & oUser.Password & "</b>")

                    If oUtility.SendMailAsHTML(oUser.EmailAddress, "Your HLC Password", sEmail, "noreply@nowhere.com", "Hospital Liasion Committee Site") Then

                        lblMessage.Text = "Your password has been sent to the email address on file for <b>" & txtUserID.Text & "</b>. If this email does not arrive shortly please check your Spam folder! " + _
                                          "Please <a href='..\Logon.aspx'>log on</a> again to continue."
                        lblMessage.Visible = True
                        lblMessage.ForeColor = Drawing.Color.Black
                        lblMessage.Font.Bold = True

                    Else
                        '-- Email failed to be sent for some reason
                        lblMessage.Text = "We're sorry, but we are temporarily unable to complete this request. Please try again later."
                        lblMessage.Visible = True
                        lblMessage.ForeColor = Drawing.Color.Red
                        lblMessage.Font.Bold = True

                    End If

                End If

            Else

                lblMessage.Text = "Sorry, that User ID does not exist in our files.  Please verify and try again."
                lblMessage.Visible = True
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Font.Bold = True

            End If

        End If

    End Sub
End Class