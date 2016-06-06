<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.TestUsControl.FrmTest" %>

<%@ Register Src="~/UserControls/Genericos/UcInformacionConsulta.ascx" TagPrefix="uc1" TagName="UcInformacionConsulta" %>
<%@ Register Src="~/UserControls/Temporal/UcMascaraCaptura.ascx" TagPrefix="uc1" TagName="UcMascaraCaptura" %>
<%@ Register Src="~/UserControls/Altas/AltaMascaraAcceso.ascx" TagPrefix="uc1" TagName="AltaMascaraAcceso" %>
<%@ Register Src="~/UserControls/Altas/AltaEncuesta.ascx" TagPrefix="uc1" TagName="AltaEncuesta" %>




<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<script type="text/javascript">
        function CierraPopup(modalName) {
            debugger;
            $(modalName).modal('hide');
            return true;
        }
       function NavegaFrame(url)
        {
           debugger;
           $("#Iframe1").attr('src', url);
            MostrarPopup("modalMuestraInformacion");
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <uc1:AltaMascaraAcceso runat="server" ID="AltaMascaraAcceso" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
