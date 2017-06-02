<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaUsuarios.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaUsuarios" %>
<%@ Import Namespace="KinniNet.Business.Utils" %>
<%@ Register TagPrefix="uc1" TagName="UcDetalleUsuario" Src="~/UserControls/Detalles/UcDetalleUsuario.ascx" %>
<div style="height: 100%;">
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx" Text="Home" />
                / Usaurios </h3>
            <hr />

            <!--MÓDULO-->
            <section class="module">
                <div class="row">
                    <div class="col-lg-8 col-md-9">
                        <div class="module-inner">
                            <div class="module-heading">
                                <h3 class="module-title"><asp:Label runat="server" ID="lblBranding" /></h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="module-inner">
                            <asp:LinkButton runat="server" CssClass="btn btn-success fa fa-plus" Text="Crear Nuevo Usuario" OnClick="btnNew_OnClick">Crear Nuevo Usuario</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            CONSULTA PUESTOS:<br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <label class="sr-only" for="txtFiltro">Buscar</label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control help_search_form" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" placeholder="Busca con una palabra clave..." />
                                    <asp:LinkButton runat="server" class="btn btn-primary btn-single-icon" OnClick="btnBuscar_OnClick"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                            </div>
                            <br/>
                            ... o consulta por Tipo de Usuario<br />
                            <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="form-control" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" AutoPostBack="true" />
                            
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            
                        </div>
                    </div>
                </div>
            </section>
            <!--/MÓDULO-->

            <div id="masonry" class="row">
                <div class="module-wrapper masonry-item col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <section class="module module-headings">
                        <div class="module-inner">
                            <div class="module-content collapse in" id="content-1">
                                <div class="module-content-inner no-padding-bottom">
                                    <div class="table-responsive">
                                        <asp:Repeater runat="server" ID="rptResultados">
                                            <HeaderTemplate>
                                                <table class="table table-striped table-hover display" id="tblResults"> style="table-layout: fixed">
                                                    <thead>
                                                        <tr>
                                                            <th>Tipo Usuario</th>
                                                            <th>Nombre Completo</th>
                                                            <th>Activo</th>
                                                            <th></th>
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
int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.Proveedor || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado || int.Parse(Eval("IdTipoUsuario").ToString()) == (int)BusinessVariables.EnumTiposUsuario.ProveedorPersonaFisica ? "btn btn-default-alt btn-square-usuario proveedor" : "btn btn-default-alt btn-square-usuario"
                                                        %>'><%# Eval("TipoUsuario.Descripcion").ToString().Substring(0,1) %></button></td>
                                                    <td style="padding: 0; text-align: left; font-size: 10px;">
                                                        <asp:LinkButton Style="padding: 0;" runat="server" Text='<%#Eval("NombreCompleto") %>' ID="LinkButton1" OnClick="btnUsuario_OnClick" CommandArgument='<%#Eval("Id") %>' /></td>
                                                    <td id="colHabilitado">
                                                        <ul class="list list-unstyled " id="hiddenEnabled">
                                                            <li>
                                                                <asp:CheckBox runat="server" AutoPostBack="true" Checked='<%# (bool) Eval("Habilitado") %>' CssClass="chkIphone" Width="30px" data-id='<%# Eval("Id")%>' Text='<%# (bool) Eval("Habilitado") ? "SI" : "NO"%>' OnCheckedChanged="OnCheckedChanged" />
                                                            </li>
                                                        </ul>
                                                    </td>
                                                    <td id="colEditar">
                                                        <ul class="list list-unstyled hidden" id="hiddenEdit">
                                                            <li>
                                                                <asp:Button runat="server" CssClass="btn btn-sm btn-primary" Text="Editar" CommandArgument='<%# Eval("Id")%>' OnClick="btnEditar_OnClick" />
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
    <%--MODAL DETALLE--%>
    <div class="modal fade" id="modalDetalleUsuario" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <uc1:UcDetalleUsuario runat="server" ID="UcDetalleUsuario1" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>


<%--MODAL Usuarios--%>
<%--<div class="modal fade" id="modalPersonaMoral" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upUser" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal-dialog modal-lg" style="width: 1250px; height: 940px; overflow: hidden">
                <div class="modal-content">
                    <uc1:UcAltaUsuarioMoral runat="server" id="ucAltaUsuarioMoral" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>--%>




