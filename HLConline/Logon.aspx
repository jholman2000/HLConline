<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Logon.aspx.vb" Inherits="HLConline.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital Liaison Committee</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="Content/HLC.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:Gray;">
<div>
    <form id="form1" runat="server">
     <div>
        <div class="login-div">
            <div class="login-logo">
              <i class="fa fa-user-md fa-4x"></i><br />
              Hospital Liaison Committee
              <br />&nbsp;
            </div>            
            <div class="input-group" style="margin-bottom:5px;">
                <span class="input-group-addon"><i class="fa fa-envelope-o fa-fw"></i></span>
                <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email address"></asp:TextBox>
            </div>
            <div class="input-group" style="margin-bottom:15px;">
                <span class="input-group-addon"><i class="fa fa-key fa-fw"></i></span>
                <asp:TextBox ID="txtPwd" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
            </div>
            <div class="checkbox" style="margin-bottom:15px;">
                <label>
                    <asp:CheckBox ID="chkRemember" runat="server" />Remember me
                </label>
            </div>
            <div style="width:100%">
                <asp:Label ID="lblError" runat="server" Text="Error" class="alert alert-danger btn-sm btn-block" style="margin-bottom:0px;text-align:center;vertical-align:center;40px;" Visible="false"></asp:Label>
            </div>
            <div style="margin-top:15px;">
                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary btn-block" Text="Sign In" />
            </div>
            <br />&nbsp;
        </div> 
    </div>
    </form>   
    </div>  
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="Scripts/jquery-2.1.4.min.js" type="text/javascript"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
