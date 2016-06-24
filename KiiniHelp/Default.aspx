<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp.Default" %>

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
    <style type="text/css">

        #inferior {
            color: #FFF;
            background: transparent;
            position: absolute; /*El div será ubicado con relación a la pantalla*/
            left: 0px; /*A la derecha deje un espacio de 0px*/
            right: 0px; /*A la izquierda deje un espacio de 0px*/
            bottom: 0px; /*Abajo deje un espacio de 0px*/
            height: 50px; /*alto del div*/
            z-index: 0;
            height: 150px;
            padding-right: 0px
        }
    </style>
</head>
<body style="background: url('images/backgroud.jpg'); background-size: 100% auto;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/accordion-menu.js" />
                <asp:ScriptReference Path="~/BootStrap/js/super-panel.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 1.0;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 25%; left: 35%; border: 10px;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="inferior">
            <img src="Images/logoKiininet.jpg" height="150" style="float: right; opacity: .7"/>
        </div>
        <nav class="navbar navbar-inverse navbar-static-top" style="width: 100%; height: 150px; background: transparent; border: 0" role="navigation">
            <div class="container-fluid">

                <div class="clearfix clear-fix"></div>
            </div>
        </nav>
        <div class="well" style="background: transparent; border: 0;text-align: center">
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-success" Text="Ingresar" PostBackUrl="Login.aspx" Width="295px" /><br/><br/>
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-primary" Text="Empleado no registrado" PostBackUrl="~/Public/Default.aspx" Width="295px"/><br/><br/>
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-primary" Text="Aun no eres nuestro cliente" PostBackUrl="~/Public/Default.aspx" Width="295px"/><br/><br/>
            <asp:LinkButton runat="server" CssClass="btn btn-lg btn-primary" Text="Quisieras ser nuestro Proveedor" PostBackUrl="~/Public/Default.aspx" Width="295px"/><br/><br/>
        </div>
    </form>
</body>
</html>
