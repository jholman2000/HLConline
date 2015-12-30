Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.Caching
'Imports System.Web.Caching

Public Class hlc_Utility
    Public ErrorMessage As String

    Public Function ExecuteSQL(ByVal sSQL As String) As Integer
        Dim oConn As New SqlConnection
        Dim oCmd As SqlCommand
        Dim intRecsAffected As Integer

        Try

            ErrorMessage = Nothing   '-- Initialize error message

            oConn.ConnectionString = GetConnectionString()
            oConn.Open()

            oCmd = New SqlCommand(sSQL, oConn)
            intRecsAffected = oCmd.ExecuteNonQuery()

        Catch ex As Exception

            ErrorMessage = ex.Message
            intRecsAffected = -1

        Finally

            '-- Close up shop before leaving
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        End Try

        Return intRecsAffected

    End Function

    Public Function GetDataRowViaSQL(ByVal sSQL As String) As DataRow
        Dim oConn As New SqlConnection
        Dim oDataAdapter As SqlDataAdapter
        Dim oTable As New DataTable

        ErrorMessage = ""

        Try
            oConn.ConnectionString = GetConnectionString()
            oConn.Open()
            oDataAdapter = New SqlDataAdapter(sSQL, oConn)
            oDataAdapter.Fill(oTable)
            oConn.Close()
        Catch ex As Exception
            ErrorMessage = "(GetDataRowViaSQL) " + ex.Message
            Return Nothing
        Finally
            '-- Close up shop before leaving
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        End Try

        If oTable.Rows.Count = 0 Then
            Return Nothing
        Else
            Return oTable.Rows(0)
        End If

    End Function

    Public Function GetDataTableViaSQL(ByVal sSQL As String) As DataTable
        Dim oConn As New SqlConnection
        Dim oDataAdapter As SqlDataAdapter
        Dim oTable As New DataTable

        ErrorMessage = ""

        Try
            oConn.ConnectionString = GetConnectionString()
            oConn.Open()
            oDataAdapter = New SqlDataAdapter(sSQL, oConn)
            oDataAdapter.Fill(oTable)
            oConn.Close()
        Catch ex As Exception
            ErrorMessage = "(GetDataTableViaSQL) " + ex.Message
            Return Nothing
        Finally
            '-- Close up shop before leaving
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        End Try

        If oTable.Rows.Count = 0 Then
            Return Nothing
        Else
            Return oTable
        End If

    End Function

    Public Function SendMailAsHTML(ByVal sEmailTo As String, ByVal sSubject As String, ByVal sBody As String, _
    Optional ByVal sEmailFrom As String = "noreply@nowhere.com", _
    Optional ByVal sEmailFromDisplayName As String = "HLC Mailer") As Boolean

        Dim mail As New MailMessage
        Dim client As New SmtpClient
        Dim aEmailTo() As String
        Dim i As Integer

        Try
            ErrorMessage = ""

            mail.From = New MailAddress(sEmailFrom, sEmailFromDisplayName)
            If sEmailTo.Contains(",") Then
                sEmailTo = Replace(sEmailTo, " ", "")
                aEmailTo = Split(sEmailTo, ",")
                For i = 0 To UBound(aEmailTo)
                    mail.To.Add(New MailAddress(aEmailTo(i)))
                Next
            Else
                mail.To.Add(New MailAddress(sEmailTo))
            End If

            mail.IsBodyHtml = True
            mail.Subject = sSubject
            mail.Body = sBody

            client.Host = "V020U03UQU.maximumasp.com"
            client.Send(mail)

            Return True

        Catch ex As Exception
            ErrorMessage = ex.Message
            Return False

        End Try

    End Function

    Public Function GetConnectionString() As String
        Dim connString As String = ""
        Dim environment As String
        Dim cache As ObjectCache

        cache = MemoryCache.Default
        connString = Convert.ToString(cache.Get("HLCConnection"))

        If String.IsNullOrWhiteSpace(connString) Then

            environment = ConfigurationManager.AppSettings("RunEnvironment")

            Select Case environment
                Case "PROD"
                    connString = ConfigurationManager.ConnectionStrings("PROD").ConnectionString
                Case "LOCAL"
                    connString = ConfigurationManager.ConnectionStrings("LOCAL").ConnectionString
                Case "DEV"
                    connString = ConfigurationManager.ConnectionStrings("DEV").ConnectionString
                Case Else
            End Select

            ' Save the connectionstring so we can retrieve it later
            Dim policy As New CacheItemPolicy
            policy.Priority = CacheItemPriority.NotRemovable
            cache.Add("HLCConnection", connString, policy)

        End If

        Return connString
    End Function
End Class
