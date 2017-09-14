<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaMisTickets.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaMisTickets" %>
<%@ Import Namespace="KinniNet.Business.Utils" %>
<%@ Register Src="~/UserControls/Operacion/UcCambiarEstatusTicket.ascx" TagPrefix="uc1" TagName="UcCambiarEstatusTicket" %>

<div style="height: 100%;">
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Home</asp:HyperLink>
                / Cátegorias </h3>
            <hr />
            <section class="module">
                <div class="row">
                    <div class="col-lg-8 col-md-9">
                        <div class="module-inner">
                            <div class="module-heading">
                                <h3 class="module-title">
                                    <asp:Label runat="server" ID="lblBranding" Text="Mis Tickets"></asp:Label></h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="module-inner">
                            <asp:LinkButton runat="server" CssClass="btn btn-success fa fa-plus" Text="Crear Nuevo Ticket" OnClick="btnNew_OnClick" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            <br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <label class="sr-only" for="txtFiltro">Buscar</label>
                                <div class="form-group col-lg-12 col-md-12 col-sm-12">
                                    <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control help_search_form" Style="width: 95%" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" placeholder="Busca con una palabra clave..." />
                                    <asp:LinkButton runat="server" class="btn btn-primary btn-single-icon fa fa-search" OnClick="btnBuscar_OnClick"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            ... o consulta por estatus<br />
                            <div class="form-horizontal margin-bottom-lg">
                                <div class="form-group col-lg-12">
                                    <asp:DropDownList runat="server" ID="ddlEstatus" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlEstatus_OnSelectedIndexChanged" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section class="module module-headings">
                <div class="module-inner">
                    <div class="module-content collapse in" id="content-1">
                        <div class="module-content-inner no-padding-bottom">
                            <div class="table-responsive">
                                <asp:Repeater runat="server" ID="rptResultados">
                                    <HeaderTemplate>
                                        <table class="table table-striped display" id="tblResults">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:Label runat="server">Ticket</asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server">Asunto</asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server">Solicitado</asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server">Estatus</asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("IdTicket")%></td>
                                            <td><%# Eval("Tipificacion")%></td>
                                            <td><%# Eval("FechaHora" , "{0:dd/MMM/yyyy}")%></td>
                                            <td class="text-center">
                                                <div style='<%# "margin: auto; width: 50%; background-color: " + Eval("Estatusticket.Color") %>' class="text-center">
                                                    <asp:Label runat="server" Text='<%# Eval("Estatusticket.Descripcion")%>'></asp:Label>
                                                </div>

                                            </td>
                                            <td>
                                                <asp:Button runat="server" Text="Estatus" ID="btnCambiaEstatus" CssClass="btn btn-primary" OnClick="btnCambiaEstatus_OnClick" CommandArgument='<%# Eval("IdTicket")%>' CommandName='<%# Eval("EstatusTicket.Id") %>'
                                                    Visible='<%# int.Parse(Eval("Estatusticket.Id").ToString()) == (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                            </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <div class="modal fade" id="modalEstatusCambio" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <uc1:UcCambiarEstatusTicket runat="server" ID="ucCambiarEstatusTicket" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</div>
