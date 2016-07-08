<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmOperacionTickets.aspx.cs" Inherits="KiiniHelp.Operacion.FrmOperacionTickets" %>

<%@ Register Src="~/UserControls/Detalles/UcDetalleUsuario.ascx" TagPrefix="uc1" TagName="UcDetalleUsuario" %>
<%@ Register Src="~/UserControls/Operacion/UcCambiarEstatusTicket.ascx" TagPrefix="uc1" TagName="UcCambiarEstatusTicket" %>
<%@ Register Src="~/UserControls/Operacion/UcCambiarEstatusAsignacion.ascx" TagPrefix="uc1" TagName="UcCambiarEstatusAsignacion" %>
<%@ Register Src="~/UserControls/Detalles/UcDetalleTicket.ascx" TagPrefix="uc1" TagName="UcDetalleTicket" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <header class="modal-header" id="pnlAlertaGeneral" runat="server" visible="false">
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
                    <asp:Repeater runat="server" ID="rptErrorGeneral">
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
                    <h3>Operacion de tickets</h3>
                </div>
                <div class="panel-body">
                    <asp:Repeater runat="server" ID="rptTickets" OnItemDataBound="rptTickets_OnItemDataBound" OnItemCommand="rptTickets_OnItemCommand">
                        <HeaderTemplate>
                            <div class="row">
                                <asp:LinkButton CssClass="col-xs-1" runat="server" Text="Fecha y Hora" CommandName="DateTime" />
                                <div class="col-xs-1 form-group">
                                    <asp:Label runat="server" Text="Numero Ticket" />
                                    <%--<asp:TextBox CssClass="form-control" runat="server" ID="txtFilerNumeroTicket" onkeypress="return ValidaCampo(this,2)" OnTextChanged="txtFilerNumeroTicket_OnTextChanged" AutoPostBack="True"--%>
                                </div>
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Nombre Usuario" />
                                <div class="col-xs-1 form-group">
                                    <asp:Label runat="server" Text="Tipificación" />
                                    <%--<asp:DropDownList runat="server" CssClass="dropdown" ID="ddlTipificacion" AppendDataBoundItems="True" />--%>
                                </div>
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Grupo Asignado" />
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Usuario Asignado" />
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Nivel Usuario Asignado" />
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Estatus Ticket" />
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Estatus Asignacion" />
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Accion Posible Estatus" />
                                <asp:Label CssClass="col-xs-1" runat="server" Text="Accion Posible Asignación" />
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="row">
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("IdTicket") %>' Visible="False" />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("IdUsuario") %>' Visible="False" />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("IdUsuarioAsignado") %>' Visible="False" />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EstatusTicket.Id") %>' Visible="False" />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EstatusAsignacion.Id") %>' Visible="False" ID="lblEstatusAsignacionActual"/>
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("IdGrupoAsignado") %>' Visible="False" ID="lblIdGrupoAsignado"/>
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EsPropietario") %>' Visible="False" ID="lblEsPropietario"/>
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("FechaHora") %>' />
                                <asp:LinkButton runat="server" CssClass="col-xs-1" Text='<%#Eval("NumeroTicket") %>' ID="lbntIdticket" OnClick="lbntIdticket_OnClick" CommandArgument='<%#Eval("IdTicket") %>'/>
                                <asp:LinkButton runat="server" CssClass="col-xs-1" Text='<%#Eval("NombreUsuario") %>' ID="btnUsuario" OnClick="btnUsuario_OnClick" CommandArgument='<%#Eval("IdUsuario") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("Tipificacion") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("GrupoAsignado") %>' />
                                <asp:LinkButton runat="server" CssClass="col-xs-1" Text='<%#Eval("UsuarioAsignado") %>' ID="btnUsuarioAsignado" OnClick="btnUsuario_OnClick" CommandArgument='<%#Eval("IdUsuario") %>'/>
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("NivelUsuarioAsignado") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EstatusTicket.Descripcion") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EstatusAsignacion.Descripcion") %>' />
                                <asp:LinkButton runat="server" CssClass='<%# (bool) Eval("EsPropietario") ? int.Parse(Eval("EstatusTicket.Id").ToString()) == 5 || int.Parse(Eval("EstatusTicket.Id").ToString()) == 6 || int.Parse(Eval("EstatusTicket.Id").ToString()) == 2 ? "col-xs-1 btn btn-sm btn-primary disabled" : "col-xs-1 btn btn-sm btn-primary" : "col-xs-1 btn btn-sm btn-primary disabled" %>' Text="Cambiar estatus" ID="btnCambiarEstatus" CommandArgument='<%#Eval("IdTicket") %>' OnClick="btnCambiarEstatus_OnClick" Enabled='<%#Eval("EsPropietario") %>' />
                                <asp:LinkButton runat="server" CssClass='<%# (bool) Eval("EsPropietario") ? int.Parse(Eval("EstatusTicket.Id").ToString()) == 5 || int.Parse(Eval("EstatusTicket.Id").ToString()) == 6 || int.Parse(Eval("EstatusTicket.Id").ToString()) == 2 ? "col-xs-1 btn btn-sm btn-primary disabled" : "col-xs-1 btn btn-sm btn-primary" : (!(bool) Eval("EsPropietario") && int.Parse(Eval("EstatusAsignacion.Id").ToString()) == 1 && int.Parse(Eval("IdUsuarioAsignado").ToString()) == 0) ? "col-xs-1 btn btn-sm btn-primary " : "col-xs-1 btn btn-sm btn-primary disabled" %>' Text="Cambiar Asignacion" ID="btnAsignar" CommandArgument='<%#Eval("IdTicket") %>' OnClick="btnAsignar_OnClick" />
                                <%--<asp:LinkButton runat="server" CssClass="col-xs-1 btn btn-sm btn-primary" Text="Auto Asignar" ID="btnautoAsignar" CommandArgument='<%#Eval("IdTicket") %>' OnClick="btnautoAsignar_OnClick" Visible='<%# !(bool)Eval("EsPropietario") %>' />--%>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="panel-footer">
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <div class="modal fade" id="modalDetalleTicket" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <uc1:UcDetalleTicket runat="server" ID="UcDetalleTicket" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade" id="modalAsignacionCambio" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <uc1:UcCambiarEstatusAsignacion runat="server" ID="UcCambiarEstatusAsignacion" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade" id="modalEstatusCambio" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <uc1:UcCambiarEstatusTicket runat="server" ID="UcCambiarEstatusTicket" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade" id="modalDetalleUsuario" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-body">
                            <uc1:UcDetalleUsuario runat="server" ID="UcDetalleUsuario" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>
