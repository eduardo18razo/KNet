<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.Test.FrmTest" %>

<%@ Register Src="~/UserControls/Consultas/UcConcultaCatalogos.ascx" TagPrefix="uc1" TagName="UcConcultaCatalogos" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaCatalogo.ascx" TagPrefix="uc1" TagName="UcConsultaCatalogo" %>
<%@ Register Src="~/UserControls/Altas/AltaInformacionConsulta.ascx" TagPrefix="uc1" TagName="AltaInformacionConsulta" %>






<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../BootStrap/js/locales/jquery.sumoselect.min.js"></script>
    <link href="~/BootStrap/css/sumoselect.css" rel="stylesheet" />
    <link href="~/BootStrap/css/bootstrap.css" rel="stylesheet" />
    <link href="~/BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/BootStrap/css/CheckBoxStyle.css" rel="stylesheet" />
    <link href="~/BootStrap/css/Calendar.css" rel="stylesheet" />
    <link href="~/BootStrap/css/DropDown.css" rel="stylesheet" />
    <link href="~/BootStrap/css/divs.css" rel="stylesheet" />
    <link href="~/BootStrap/css/FileInput.css" rel="stylesheet" />
    <link href="~/BootStrap/css/Headers.css" rel="stylesheet" />
    <link href="~/BootStrap/css/stylemainmenu.css" rel="stylesheet" />
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $(<%=lstBoxTest.ClientID%>).SumoSelect({ placeholder: 'SELECCIONE', selectAll: true, csvDispCount: 1 });
        });
    </script>--%>
    <script type ="text/javascript" >
        function UploadComplete(sender, args) {
            __doPostBack('Refresh', '');
        }
   </script>
    <style type="text/css">
        body {
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            color: #444;
            font-size: 13px;
        }

        p, div, ul, li {
            padding: 0px;
            margin: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/super-panel.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:ListBox runat="server" ID="lstBoxTest" SelectionMode="Multiple "/>
        
        <asp:Button Text="Values" Visible="True" ID="btnGetSelectedValues" OnClick="btnGetSelectedValues_Click" runat="server" />
        <uc1:AltaInformacionConsulta runat="server" id="ucAltaInformacionConsulta" />
    </form>
    

    

</body>
</html>
