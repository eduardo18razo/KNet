<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaOrganizacion.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaOrganizacion" %>
<%@ Register Src="~/UserControls/Altas/Organizaciones/UcAltaOrganizaciones.ascx" TagPrefix="uc1" TagName="UcAltaOrganizaciones" %>

<div style="height: 100%;">
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfModalName" />
            <asp:HiddenField runat="server" ID="hfIdSeleccion" />
            <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Inicio</asp:HyperLink>
                / Configuración / Organización </h3>
            <hr />
            <section class="module">
                <div class="row">
                    <div class="col-lg-8 col-md-9">
                        <div class="module-inner">
                            <div class="module-heading">
                                <h3 class="module-title">
                                    <asp:Label runat="server" ID="lblOrganización" Text="Organización"/></h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="module-inner">
                            <asp:LinkButton CssClass="btn btn-success" ID="btnNew" OnClick="btnNew_OnClick" runat="server"><i class="fa fa-plus"></i>Crear Nueva Organización</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            Consulta Organizaciones:<br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <label class="sr-only" for="txtFiltro">Buscar</label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtFiltroDecripcion" CssClass="form-control help_search_form" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" placeholder="Busca con una palabra clave..." />
                                    <asp:LinkButton runat="server" class="btn btn-primary btn-single-icon fa fa-search" OnClick="btnBuscar_OnClick" />
                                </div>
                            </div>
                            <br />
                            ... o consulta por Tipo de Usuario<br />
                            <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" />
                        </div>
                    </div>
                </div>
            </section>
            <!--/MÓDULO-->
            <br />
            <asp:Label runat="server" Text="Organizaciones" ID="lblTitleOrganizacion" /></h3>
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
                                        <asp:Repeater runat="server" ID="rptResultados" OnItemCreated="rptResultados_OnItemCreated">
                                            <HeaderTemplate>
                                                <table class="table table-striped display" id="tblResults">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:Label runat="server" ID="Label1">Tipo Usuario</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblHolding">Nivel 1</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblCompania">Nivel 2</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblDireccion">Nivel 3</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblSubDireccion">Nivel 4</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblGerencia">Nivel 5</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblSubGerencia">Nivel 6</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server" ID="lblJefatura">Nivel 7</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server">Activo</asp:Label></th>
                                                            <th>
                                                                <asp:Label runat="server"></asp:Label></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <button type="button" class="btn btn-default-alt btn-square-usuario" style='<%# "Border: none !important; Background: " + Eval("TipoUsuario.Color") + " !important" %>'>
                                                            <%# Eval("TipoUsuario.Abreviacion") %></button></td>
                                                    <td><%# Eval("Holding.Descripcion")%></td>
                                                    <td><%# Eval("Compania.Descripcion")%></td>
                                                    <td><%# Eval("Direccion.Descripcion")%></td>
                                                    <td><%# Eval("SubDireccion.Descripcion")%></td>
                                                    <td><%# Eval("Gerencia.Descripcion")%></td>
                                                    <td><%# Eval("SubGerencia.Descripcion")%></td>
                                                    <td><%# Eval("Jefatura.Descripcion")%></td>
                                                    <td id="colHabilitado">
                                                        <ul class="list list-unstyled" id="hiddenEnabled">
                                                            <li>
                                                                <asp:CheckBox runat="server" AutoPostBack="true" Checked='<%# (bool) Eval("Habilitado") %>' Visible='<%# int.Parse(Eval("IdNivelOrganizacion").ToString()) != 1 %>' CssClass="chkIphone" Width="30px" data-id='<%# Eval("Id")%>' Text='<%# (bool) Eval("Habilitado") ? "SI" : "NO"%>' OnCheckedChanged="OnCheckedChanged" />
                                                            </li>
                                                        </ul>
                                                    </td>
                                                    <td id="colEditar">
                                                        <ul class="list list-unstyled hidden" id="hiddenEdit">
                                                            <li>
                                                                <asp:Button runat="server" CssClass="btn btn-sm btn-primary" Text="Editar" CommandArgument='<%# Eval("Id")%>' Visible='<%# (bool) Eval("Habilitado") %>' OnClick="btnEditar_OnClick" />
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
            <div id="contextMenuOrganizacion" class="panel-heading contextMenu">
                <asp:HiddenField runat="server" ClientIDMode="Inherit" ID="hfModal" />
            </div>
            <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>

        </ContentTemplate>
    </asp:UpdatePanel>

    <%--MODAL CATALOGOS--%>
    <div class="modal fade" id="editCatalogoOrganizacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel ID="upCatlogos" runat="server">
            <ContentTemplate>
                <uc1:UcAltaOrganizaciones runat="server" id="ucAltaOrganizaciones" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
