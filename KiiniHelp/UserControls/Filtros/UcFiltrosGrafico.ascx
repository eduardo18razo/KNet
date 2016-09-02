<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFiltrosGrafico.ascx.cs" Inherits="KiiniHelp.UserControls.Filtros.UcFiltrosGrafico" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroGrupo" Src="~/UserControls/Filtros/UcFiltroGrupo.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroOrganizacion" Src="~/UserControls/Filtros/UcFiltroOrganizacion.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroUbicacion" Src="~/UserControls/Filtros/UcFiltroUbicacion.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroServicioIncidente" Src="~/UserControls/Filtros/UcFiltroServicioIncidente.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroTipificacion" Src="~/UserControls/Filtros/UcFiltroTipificacion.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroPrioridad" Src="~/UserControls/Filtros/UcFiltroPrioridad.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroEstatus" Src="~/UserControls/Filtros/UcFiltroEstatus.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroSla" Src="~/UserControls/Filtros/UcFiltroSla.ascx" %>
<%@ Register TagPrefix="userControls" TagName="UcFiltroFechas" Src="~/UserControls/Filtros/UcFiltroFechas.ascx" %>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfTicket" />
        <asp:HiddenField runat="server" ID="hfConsulta" />
        <asp:HiddenField runat="server" ID="hfEncuesta" />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <header class="modal-header" id="panelAlerta" runat="server" visible="false">
                    <div class="alert alert-danger">
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
                        <asp:Repeater runat="server" ID="rptError">
                            <ItemTemplate>
                                <ul>
                                    <li><%# Container.DataItem %></li>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </header>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Filtros
                    </div>
                    <div class="panel-body text-center">
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroEstatus" Text="Estatus" OnClick="btnFiltroEstatus_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroTipoGrafico" Text="Tipo Grafico" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroStack" Text="Stack" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroObservaciones" Text="Observaciones" />
                    </div>
                    <div class="panel-footer">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="modalFiltroEstatus" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroEstatus" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <userControls:UcFiltroEstatus runat="server" ID="ucFiltroEstatus" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>


