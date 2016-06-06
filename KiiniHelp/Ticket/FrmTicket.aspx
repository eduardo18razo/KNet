<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmTicket.aspx.cs" Inherits="KiiniHelp.Ticket.FrmTicket" %>

<%@ Register Src="~/UserControls/Temporal/UcMascaraCaptura.ascx" TagPrefix="uc1" TagName="UcMascaraCaptura" %>
<%@ Register Src="~/UserControls/Genericos/UcInformacionConsulta.ascx" TagPrefix="uc1" TagName="UcInformacionConsulta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <header>
                <div class="alert alert-danger" id="panelAlert" runat="server" visible="False">
                    <div>
                        <div style="float: left">
                            <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                        </div>
                        <div style="float: left">
                            <h3>Error</h3>
                        </div>
                        <div class="clearfix clear-fix" />
                    </div>
                    <hr />
                    <asp:Repeater runat="server" ID="rptHeaderError">
                        <ItemTemplate>
                            <%# Container.DataItem %>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </header>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <asp:HiddenField runat="server" ID="hfIdMascara" />
                    <asp:HiddenField runat="server" ID="hfIdConsulta" />
                    <asp:HiddenField runat="server" ID="hfIdEncuesta" />
                    <asp:HiddenField runat="server" ID="hfIdSla" />

                </div>

                <div class="panel-body">
                    <uc1:UcInformacionConsulta runat="server" ID="UcInformacionConsulta" />
                    <uc1:UcMascaraCaptura runat="server" ID="UcMascaraCaptura" />

                </div>
                <div class="panel-footer">
                    <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_OnClick" Text="Guardar" CssClass="btn btn-lg btn-success" />
                    <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-lg btn-danger" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
