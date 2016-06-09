<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp.Default" %>

<%@ Register Src="~/UserControls/Altas/AltaArea.ascx" TagPrefix="uc1" TagName="AltaArea" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KiiniNet</title>
    <link href="BootStrap/css/bootstrap.css" rel="stylesheet" />
    <link href="BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="BootStrap/css/CheckBoxStyle.css" rel="stylesheet" />
    <link href="BootStrap/css/Calendar.css" rel="stylesheet" />
    <link href="BootStrap/css/DropDown.css" rel="stylesheet" />
    <link href="BootStrap/css/divs.css" rel="stylesheet" />
    <link href="BootStrap/css/FileInput.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/accordion-menu.js" />
                <asp:ScriptReference Path="~/BootStrap/js/super-panel.js" />
            </Scripts>
        </asp:ScriptManager>
        <nav class="navbar navbar-inverse navbar-static-top" style="width: 100%; height: 150px; background: transparent; border: 0" role="navigation">
            <div class="container-fluid">
                <div style="float: left">
                    <img src="Images/logoKiininet.jpg" height="150" />
                </div>
                <div style="float: right">
                    <img src="Images/logoKiininet.jpg" height="150" />
                </div>
                <div class="clearfix clear-fix"></div>
            </div>
        </nav>
        <div class="well">
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-primary" Text="Empleado Invitado" PostBackUrl="~/Public/Default.aspx" />
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-primary" Text="Cliente Invitado" PostBackUrl="~/Public/Default.aspx" />
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-primary" Text="Proveedor Invitado" PostBackUrl="~/Public/Default.aspx" />
        </div>
        <uc1:AltaArea runat="server" ID="AltaArea" />
        <div class="well" style="text-align: center"> 
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-success" Text="Ingresar" PostBackUrl="Login.aspx"/>
        </div>

    </form>
</body>
</html>
