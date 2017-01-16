<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.Test.FrmTest" %>

<%@ Register Src="~/UserControls/Filtros/Componentes/UcFiltroCanalApertura.ascx" TagPrefix="uc1" TagName="UcFiltroCanalApertura" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaOrganizacion.ascx" TagPrefix="uc1" TagName="UcConsultaOrganizacion" %>






<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../BootStrap/js/locales/jquery.sumoselect.min.js"></script>
    <script src="../BootStrap/js/bootstrap.min.js"></script>
    <script src="../BootStrap/js/bootstrap.js"></script>
    
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
    <script type="text/javascript">
        function UploadComplete(sender, args) {
            __doPostBack('Refresh', '');
        }
        function MostrarPopup(modalName) {
            debugger;
            $(modalName).modal({ backdrop: 'static', keyboard: false });
            $(modalName).modal({ show: true });
            return true;
        };
        function CierraPopup(modalName) {
            $(modalName).modal('hide');
            return true;
        };
        function HightSearch(content, serachText) {
            debugger;
            var src_str = $("#" + content).html();
            var term = serachText;
            term = term.replace(/(\s+)/, "(<[^>]+>)*$1(<[^>]+>)*");
            var pattern = new RegExp("(" + term + ")", "gi");

            //src_str = src_str.replace(pattern, '<span style="background-color:Yellow" >' + term + '</span>');
            src_str = src_str.replace(pattern, "<mark>$1</mark>");
            src_str = src_str.replace(/(<mark>[^<>]*)((<[^>]+>)+)([^<>]*<\/mark>)/, "$1</mark>$2<mark>$4");

            $("#" + content).html(src_str);
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
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.js" />
                <asp:ScriptReference Path="~/BootStrap/js/super-panel.js" />
            </Scripts>
        </asp:ScriptManager>
        <uc1:UcConsultaOrganizacion runat="server" id="UcConsultaOrganizacion" />
    </form>




</body>
</html>














        <%--<asp:DropDownList runat="server" ID="ddlLista" CssClass="DropSelect"></asp:DropDownList>
        <ajax:DropDownExtender ID="DropDownExtender2" runat="server"  TargetControlID="TextBox1" DropDownControlID="divDataDropdown"></ajax:DropDownExtender>
        <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="None" CssClass="DropSelect"/>
        <div id="divDataDropdown" style="overflow-y: scroll; height: 200px; background-color: white" runat="server">
            <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <Columns>            
                    <asp:CommandField HeaderText="Select Data" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>--%>
        <%--<uc1:UcFiltroCatalogos runat="server" id="UcFiltroCatalogos" />--%>
        <%--<uc1:UcCargaCatalgo runat="server" id="ucCargaCatalgo" />--%>
        <%--<asp:ListBox runat="server" ID="lstBoxTest" SelectionMode="Multiple "/>
        
        <asp:Button Text="Values" Visible="True" ID="btnGetSelectedValues" OnClick="btnGetSelectedValues_Click" runat="server" />
        <uc1:AltaInformacionConsulta runat="server" id="ucAltaInformacionConsulta" />--%>