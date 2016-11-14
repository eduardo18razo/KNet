<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.Test.FrmTest" %>

<%@ Register Src="~/UserControls/Consultas/UcConcultaCatalogos.ascx" TagPrefix="uc1" TagName="UcConcultaCatalogos" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaCatalogo.ascx" TagPrefix="uc1" TagName="UcConsultaCatalogo" %>





<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../BootStrap/js/locales/jquery.sumoselect.min.js"></script>
    <link href="~/BootStrap/css/sumoselect.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(<%=lstBoxTest.ClientID%>).SumoSelect({ placeholder: 'SELECCIONE', selectAll: true, csvDispCount: 1 });
        });
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
    <form id="form1" runat="server">
        <asp:ListBox runat="server" ID="lstBoxTest" SelectionMode="Multiple "/>
        
        <asp:Button Text="Values" Visible="True" ID="btnGetSelectedValues" OnClick="btnGetSelectedValues_Click" runat="server" />
    </form>

</body>
</html>
