<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.Test.FrmTest" %>

<%@ Register Src="~/UserControls/Filtros/Componentes/UcFiltroFechasGrafico.ascx" TagPrefix="uc1" TagName="UcFiltroFechasGrafico" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Pruebas</title>
    <link href="~/BootStrap/css/bootstrap.css" rel="stylesheet" />
    <link href="~/BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/BootStrap/css/CheckBoxStyle.css" rel="stylesheet" />
    <link href="~/BootStrap/css/Calendar.css" rel="stylesheet" />
    <link href="~/BootStrap/css/DropDown.css" rel="stylesheet" />
    <link href="~/BootStrap/css/divs.css" rel="stylesheet" />
    <link href="~/BootStrap/css/FileInput.css" rel="stylesheet" />
    <link href="~/BootStrap/css/Headers.css" rel="stylesheet" />
    <link href="~/BootStrap/css/stylemainmenu.css" rel="stylesheet" />
    <link href="../BootStrap/css/Calendar/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../BootStrap/css/Calendar/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function MostrarPopup(modalName) {
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
        function UpScroll(modalName) {
            $('#' + modalName).animate({ scrollTop: 0 }, 'slow');
            return false;
        };
        function SetCalendar(id) {
            debugger;
            if (id != undefined)
                if (id.id != undefined) {
                    $('#' + id.id).datepicker({
                        keyboardNavigation: true,
                        forceParse: false,
                        locale: 'es',
                        language: "es",
                        autoclose: true,
                        todayHighlight: true
                    });
                    $('#' + id.id).datepicker('show');
                } else {
                    $('#' + id).datepicker({
                        keyboardNavigation: true,
                        forceParse: false,
                        locale: 'es',
                        language: "es",
                        autoclose: true,
                        todayHighlight: true
                    });
                    $('#' + id).datepicker('show');
                }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/super-panel.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap-datepicker.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/locales/bootstrap-datepicker.es.min.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:TextBox runat="server" type="month" ID="TextBox1"></asp:TextBox>
        <uc1:UcFiltroFechasGrafico runat="server" ID="UcFiltroFechasGrafico" />
        <asp:TextBox runat="server" type="week" ID="txtWeek"></asp:TextBox>
        <asp:Label runat="server" ID="lblFormatWeek"></asp:Label>
        <br/>
        <asp:TextBox runat="server" type="month" ID="txtMonth"></asp:TextBox>
        <asp:Label runat="server" ID="lblFormatMonth"></asp:Label>
        <asp:Button runat="server" OnClick="OnClick" Text="ver"/>
        <asp:Chart ID="ChartStack" runat="server" Width="800px" Height="600px" Visible="True">
            <Titles>
                <asp:Title ShadowOffset="3" Name="Items" />
            </Titles>
            <Legends>
                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" Title="Titulo" />
            </Legends>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
            </ChartAreas>
        </asp:Chart>
        
        <asp:Chart ID="chartColumn" runat="server" Width="800px" Height="600px">
            <Titles>
                <asp:Title ShadowOffset="3" Name="Items" />
            </Titles>
            <Legends>
                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" Title="Titulo" />
            </Legends>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BorderWidth="0"/>
            </ChartAreas>
        </asp:Chart>


        <asp:Chart ID="Chart1" runat="server" Height="531px" Width="920px">
            <Titles>
                <asp:Title ShadowOffset="3" Name="Items" />
            </Titles>
            <Legends>
                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" Title="Titulo" />
            </Legends>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="Gainsboro" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                    <AxisY2 IsLabelAutoFit="False" Interval="25">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                    </AxisY2>
                    <AxisY LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisY>
                    <AxisX LineColor="64, 64, 64, 64">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>

    </form>
</body>
</html>
