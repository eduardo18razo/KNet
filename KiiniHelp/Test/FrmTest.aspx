<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.Test.FrmTest" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
        
        <%--<asp:Chart ID="Chart1" runat="server" Width="412px" Height="296px" BorderlineDashStyle="Solid" Palette="BrightPastel" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" BackColor="WhiteSmoke" BorderColor="26, 59, 105">
            <Legends>
                <asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                </asp:Legend>
            </Legends>
            <BorderSkin SkinStyle="Emboss"></BorderSkin>
            <Series>
                <asp:Series Name="Default" BorderColor="180, 26, 59, 105">
                </asp:Series>
            </Series>
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
        </asp:Chart>--%>

        <asp:Chart ID="Chart1" runat="server" Height="531px" Width="920px">
            <Series>
                <asp:Series Name="Default" BorderColor="180, 26, 59, 105">
                </asp:Series>
            </Series>
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
