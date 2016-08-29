<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaTicketGrafica.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaTicketGrafica" %>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
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
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroGrupo" Text="Grupo" OnClick="btnFiltroGrupo_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroOrganizacion" Text="Organización" OnClick="btnFiltroOrganizacion_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroUbicacion" Text="Ubicación" OnClick="btnFiltroUbicacion_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroServicioIncidente" Text="Servicio/Incidente" OnClick="btnFiltroServicioIncidente_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroTipificacion" Text="Tipificación" OnClick="btnFiltroTipificacion_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroPrioridad" Text="Prioridad" OnClick="btnFiltroPrioridad_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroEstatus" Text="Estatus" OnClick="btnFiltroEstatus_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroSla" Text="SLA" OnClick="btnFiltroSla_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroFechas" Text="Fechas" OnClick="btnFiltroFechas_OnClick" />
                    </div>
                    <div class="panel-footer">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="modalFiltroGrupo" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroGpo" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<div class="modal fade" id="modalFiltroOrganizacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroOrganizacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<div class="modal fade" id="modalFiltroUbicacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroUbicacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="modal fade" id="modalFiltroServicioIncidente" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroServicioIncidente" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="modal fade" id="modalFiltroTipificacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroTipificacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="modal fade" id="modalFiltroPrioridad" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroPrioridad" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="modal fade" id="modalFiltroEstatus" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroEstatus" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="modal fade" id="modalFiltroSla" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroSla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<div class="modal fade" id="modalFiltroFechas" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upFiltroFechas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
