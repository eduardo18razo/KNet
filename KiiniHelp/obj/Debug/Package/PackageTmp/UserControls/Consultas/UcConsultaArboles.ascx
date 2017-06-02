<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaArboles.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaArboles" %>
<%@ Import Namespace="KinniNet.Business.Utils" %>
<%@ Register Src="~/UserControls/Altas/ArbolesAcceso/UcAltaAbrolAcceso.ascx" TagPrefix="uc1" TagName="UcAltaAbrolAcceso" %>

<div style="height: 100%;">
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Home</asp:HyperLink>
                / Configuración de Menú </h3>
            <hr />

            <section class="module">
                <div class="row">
                    <div class="col-lg-8 col-md-9">
                        <div class="module-inner">
                            <div class="module-heading">
                                <h3 class="module-title">
                                    <asp:Label runat="server" ID="lblBranding"></asp:Label></h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="module-inner">
                            <asp:LinkButton runat="server" CssClass="btn btn-success fa fa-plus" Text="Crear Nueva Area" OnClick="btnNew_OnClick" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            CONSULTA OPCIONES DE MENÚ:<br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <label class="sr-only" for="txtFiltro">Buscar</label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control help_search_form" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" placeholder="Busca con una palabra clave..." />
                                    <asp:LinkButton runat="server" class="btn btn-primary btn-single-icon fa fa-search" OnClick="btnBuscar_OnClick"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12">
                        <div class="module-inner">
                            ... o consulta por Categoría y Tipo de Usuario<br />

                            <asp:DropDownList runat="server" ID="ddlArea" CssClass="form-control" Style="width: 30%; display: inline-block" OnSelectedIndexChanged="ddlArea_OnSelectedIndexChanged" AutoPostBack="true" autocompletemode="SuggestAppend" casesensitive="false" />
                            <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="form-control" Style="width: 30%; display: inline-block; margin-left: 25px" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" AutoPostBack="true" />
                        </div>
                    </div>
                </div>
            </section>

            <div id="masonry" class="row">
                <div class="module-wrapper masonry-item col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <section class="module module-headings">
                        <div class="module-inner">
                            <div class="module-heading">
                                <ul class="actions list-inline">
                                    <li><a class="collapse-module" data-toggle="collapse" href="#content-1" aria-expanded="false" aria-controls="content-1"><span aria-hidden="true" class="icon arrow_carrot-up"></span></a></li>
                                </ul>
                            </div>
                            <div class="module-content collapse in" id="content-1">
                                <div class="module-content-inner no-padding-bottom">
                                    <div class="table-responsive">
                                        <asp:Repeater runat="server" ID="rptResultados">
                                            <HeaderTemplate>
                                                <table class="table table-striped display" id="tblResults">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:Label runat="server">Tipo Usuario</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server">Titulo</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server">Categoría</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server">Tipificación</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server">Tipo</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server">Nivel</asp:Label></th>

                                                            <th>
                                                                <asp:Label runat="server">Activo</asp:Label></th>
                                                            <%--<th>Editar</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr id='<%# Eval("Id")%>'>
                                                    <td>
                                                        <button type="button" class='<%# 
int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.Empleado || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.EmpleadoPersonaFisica ? "btn btn-default-alt btn-square-usuario empleado" : 
int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.Cliente || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.ClienteInvitado || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.ClientaPersonaFisica ? "btn btn-default-alt btn-square-usuario cliente" : 
int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.Proveedor || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.ProveedorPersonaFisica ? "btn btn-default-alt btn-square-usuario proveedor" : "btn btn-default-alt btn-square-usuario" %>'>
                                                            <%# Eval("TipoUsuario.Descripcion").ToString().Substring(0,1) %></button></td>
                                                    <td><%# Eval("Tipificacion")%></td>
                                                    <td><%# Eval("Area.Descripcion")%></td>
                                                    <td><%# Eval("TipoArbolAcceso.Descripcion")%></td>
                                                    <td><%# (bool)Eval("EsTerminal") ? "OPCIÓN" : "SECCIÓN"%></td>
                                                    <td><%# Eval("Nivel")%></td>



                                                    <td id="colHabilitado">
                                                        <ul class="list list-unstyled hidden" id="hiddenEnabled">
                                                            <li>
                                                                <asp:CheckBox runat="server" AutoPostBack="true" Checked='<%# (bool) Eval("Habilitado") %>' CssClass="chkIphone" Width="30px" data-id='<%# Eval("Id")%>' Text='<%# (bool) Eval("Habilitado") ? "SI" : "NO"%>' OnCheckedChanged="OnCheckedChanged" />
                                                            </li>
                                                        </ul>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                            </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="modalAtaOpcion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true" style="margin-top: 60px">
        <asp:UpdatePanel ID="upAltaArea" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <<div class="modal-dialog" style="height: 250px;">
                    <div class="modal-content">
                        <uc1:UcAltaAbrolAcceso runat="server" id="UcAltaAbrolAcceso" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</div>
