<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.TestUsControl.FrmTest" %>

<%@ Register Src="~/UserControls/Genericos/UcInformacionConsulta.ascx" TagPrefix="uc1" TagName="UcInformacionConsulta" %>
<%@ Register Src="~/UserControls/Temporal/UcMascaraCaptura.ascx" TagPrefix="uc1" TagName="UcMascaraCaptura" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <uc1:UcMascaraCaptura runat="server" ID="UcMascaraCaptura" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
