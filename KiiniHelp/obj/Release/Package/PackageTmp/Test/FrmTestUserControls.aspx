<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTestUserControls.aspx.cs" Inherits="KiiniHelp.Test.FrmTestUserControls" %>

<%@ Register Src="~/UserControls/Consultas/UcConsultaOrganizacion.ascx" TagPrefix="uc1" TagName="UcConsultaOrganizacion" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaUbicaciones.ascx" TagPrefix="uc1" TagName="UcConsultaUbicaciones" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaUsuarios.ascx" TagPrefix="uc1" TagName="UcConsultaUsuarios" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaGrupos.ascx" TagPrefix="uc1" TagName="UcConsultaGrupos" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaArboles.ascx" TagPrefix="uc1" TagName="UcConsultaArboles" %>
<%@ Register Src="~/UserControls/Altas/AltaSla.ascx" TagPrefix="uc1" TagName="AltaSla" %>






<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../BootStrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../BootStrap/css/CheckBoxStyle.css" rel="stylesheet" />
    <link href="../BootStrap/css/Calendar.css" rel="stylesheet" />
    <link href="../BootStrap/css/DropDown.css" rel="stylesheet" />
    <link href="../BootStrap/css/divs.css" rel="stylesheet" />
    <link href="../BootStrap/css/FileInput.css" rel="stylesheet" />
    <link href="../BootStrap/css/stylemainmenu.css" rel="stylesheet" />
    <%--<link href="BootStrap/css/jquery.treegrid.css" rel="stylesheet" />--%>
    <link href="../BootStrap/css/Headers.css" rel="stylesheet" />
    <%--<link href="BootStrap/css/super-panel.css" rel="stylesheet" />--%>
    <link href="../BootStrap/css/accordion-menu.css" rel="stylesheet" />
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        html, body {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }

        #full, #form1 {
            height: 100%;
        }

        #contextMenu {
            position: absolute;
            display: none;
            background: gray;
        }

        .borderless td, .borderless th {
            border: none;
        }
    </style>
    <script type="text/javascript">
        function MostrarPopup(modalName) {
            debugger;
            $(modalName).modal({ backdrop: 'static', keyboard: false });
            $(modalName).modal('show');
            return true;
        };
        function CierraPopup(modalName) {
            $(modalName).modal('hide');
            return true;
        };
        function OpenWindow(url) {
            window.open(url, "test", 'type=fullWindow, fullscreen, height=700,width=760');
        };
        function Subirscroll(modalName) {
            debugger;

            //$(modalName).scrollTop(0);
            $(modalName).animate({ scrollTop: 0 }, 'slow');

            return false;
        };


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/vmenuModule.js" />
                <asp:ScriptReference Path="~/BootStrap/js/accordion-menu.js" />
                <asp:ScriptReference Path="~/BootStrap/js/super-panel.js" />
            </Scripts>
        </asp:ScriptManager>
        <div id="full">
            <uc1:AltaSla runat="server" ID="AltaSla" />
        </div>
    </form>
</body>
</html>
