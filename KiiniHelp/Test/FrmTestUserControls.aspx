<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTestUserControls.aspx.cs" Inherits="KiiniHelp.Test.FrmTestUserControls" %>

<%@ Register Src="~/UserControls/Altas/AltaOrganizacion.ascx" TagPrefix="uc1" TagName="AltaOrganizacion" %>
<%@ Register Src="~/UserControls/Detalles/UcDetalleUsuario.ascx" TagPrefix="uc1" TagName="UcDetalleUsuario" %>
<%@ Register Src="~/UserControls/Detalles/UcDetalleUbicacion.ascx" TagPrefix="uc1" TagName="UcDetalleUbicacion" %>
<%@ Register Src="~/UserControls/Detalles/UcDetalleOrganizacion.ascx" TagPrefix="uc1" TagName="UcDetalleOrganizacion" %>
<%@ Register Src="~/UserControls/Detalles/UcDetalleGrupoUsuario.ascx" TagPrefix="uc1" TagName="UcDetalleGrupoUsuario" %>






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
    <link href="../BootStrap/css/super-panel.css" rel="stylesheet" />
    <link href="../BootStrap/css/accordion-menu.css" rel="stylesheet" />
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }
    </style>
    <script type="text/javascript">
        function MostrarPopup(modalName) {
            debugger;
            $(modalName).modal({ backdrop: 'static', keyboard: false });
            $(modalName).modal('show');
            return true;
        }
        function CierraPopup(modalName) {
            debugger;
            $(modalName).modal('hide');
            return true;
        }
        function OpenWindow(url) {
            debugger;
            window.open(url, "test", 'type=fullWindow, fullscreen, height=700,width=760');
        }
        function UpScroll() {
            document.body.scrollTop = 0;
        }


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
       <%-- <nav id="myNavmenu" class="navmenu navmenu-default navmenu-fixed-left offcanvas" role="navigation">
            <a class="navmenu-brand" href="#">Brand</a>
            <ul class="nav navmenu-nav">
                <li class="active"><a href="#">Home</a></li>
                <li><a href="#">Link</a></li>
                <li><a href="#">Link</a></li>
            </ul>
        </nav>--%>
        

        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upPrincipal">
            <ContentTemplate>
                <asp:Button type="button" class="btn btn-primary btn-lg" Text="Grupos" ID="btnOnModal" runat="server" OnClick="btnOnModal_OnClick"/>
                <uc1:UcDetalleGrupoUsuario runat="server" id="UcDetalleGrupoUsuario" />
                <%--<uc1:UcDetalleOrganizacion runat="server" id="UcDetalleOrganizacion" />
                <uc1:UcDetalleUbicacion runat="server" id="UcDetalleUbicacion" />--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="modal fade" id="modalDetalleUsuario" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-dialog modal-lg">

                        <div class="modal-content">
                            <div class="modal-body">
                                <uc1:UcDetalleUsuario runat="server" ID="UcDetalleUsuario1" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
