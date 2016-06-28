<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmOperacionTickets.aspx.cs" Inherits="KiiniHelp.Operacion.FrmOperacionTickets" %>

<%@ Register Src="~/UserControls/Detalles/UcDetalleUsuario.ascx" TagPrefix="uc1" TagName="UcDetalleUsuario" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EstatusAsignacion.Id") %>' Visible="False" />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("FechaHora") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("NumeroTicket") %>' />
                                <asp:LinkButton runat="server" CssClass="col-xs-1" Text='<%#Eval("NombreUsuario") %>' ID="btnUsuario" OnClick="btnUsuario_OnClick" CommandArgument='<%#Eval("IdUsuario") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("Tipificacion") %>' />

                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("GrupoAsignado") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("UsuarioAsignado") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("NivelUsuarioAsignado") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EstatusTicket.Descripcion") %>' />
                                <asp:Label runat="server" CssClass="col-xs-1" Text='<%#Eval("EstatusAsignacion.Descripcion") %>' />
                                <%--<asp:DropDownList CssClass="col-xs-1 dropdown" runat="server" ID="ddlEstatus" Visible="False"/>--%>
                                <asp:LinkButton runat="server" CssClass="col-xs-1" Text="Cambiar Estatus" ID="btnCambiarEstatus" CommandArgument='<%#Eval("IdTicket") %>' OnClick="btnCambiarEstatus_OnClick"/>
                                <%--<asp:DropDownList CssClass="col-xs-1 dropdown" runat="server" ID="ddlAsignacion" Visible="False" />--%>
                                <asp:LinkButton runat="server" CssClass="col-xs-1" Text="Asignar" ID="btnAsignar" CommandArgument='<%#Eval("IdTicket") %>' OnClick="btnAsignar_OnClick"/>
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
    <div class="modal fade" id="modalDetalleUsuario" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">

                    <div class="modal-content">
                        <div class="modal-body">
                            <uc1:UcDetalleUsuario runat="server" ID="UcDetalleUsuario1" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <div class="modal fade" id="modalCambiarEstatus" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-body">
                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <div class="modal fade" id="modalAsignar" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-body">
                            
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
