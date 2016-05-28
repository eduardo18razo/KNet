<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.TestUsControl.FrmTest" %>

<%@ Register Src="~/UserControls/Seleccion/AsociarGrupoUsuario.ascx" TagPrefix="uc1" TagName="AsociarGrupoUsuario" %>
<%@ Register Src="~/UserControls/Altas/AltaInformacionConsulta.ascx" TagPrefix="uc1" TagName="AltaInformacionConsulta" %>





<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function CierraPopup(modalName) {
            debugger;
            $(modalName).modal('hide');
            return true;
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <uc1:AltaInformacionConsulta runat="server" ID="AltaInformacionConsulta" />
          <%--  <asp:FileUpload ID="uploadFile" runat="server" Width="300px" ></asp:FileUpload>
            <asp:Button runat="server" ID="btnUpload" Width="120px" Text="SubirArchivo" OnClick="btnUpload_OnClick"/>--%>
            <%--<uc1:AsociarGrupoUsuario runat="server" id="AsociarGrupoUsuario" />--%>
            <%--<asp:Button runat="server" OnClick="OnClick" ID="btn"/>--%>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload"/>--%>
        </Triggers>
    </asp:UpdatePanel>
    <%--<div class="modal fade" id="modalTest" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <uc1:UcEncuesta runat="server" id="UcEncuesta1" />
    </div>--%>
</asp:Content>
