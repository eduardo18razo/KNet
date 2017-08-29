<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaUbicaciones.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaUbicaciones" %>
<%@ Register Src="~/UserControls/Altas/Ubicaciones/UcAltaUbicaciones.ascx" TagPrefix="uc1" TagName="UcAltaUbicaciones" %>



<div style="height: 100%;">
    <asp:UpdatePanel runat="server" style="height: 100%" ID="upGeneral">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfModalName" />
            <asp:HiddenField runat="server" ID="hfIdSeleccion" />
            <asp:HiddenField runat="server" ClientIDMode="Inherit" ID="hfModal" />
            <asp:HiddenField runat="server" ClientIDMode="Inherit" ID="hfId" />
                        <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Inicio</asp:HyperLink>
                / Configuración / Ubicación </h3>
            <hr />
            <section class="module">
                <div class="row">
                    <div class="col-lg-8 col-md-9">
                        <div class="module-inner">
                            <div class="module-heading">
                                <h3 class="module-title">
                                    <asp:Label runat="server" ID="lblSeccion" Text="Ubicación" /></h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="module-inner">
                            <asp:LinkButton CssClass="btn btn-success" ID="btnNuevo" OnClick="btnNew_OnClick" runat="server"><i class="fa fa-plus"></i>Crear Nueva Ubicación</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            Consulta Ubicación:<br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <label class="sr-only" for="txtFiltroDecripcion">Buscar</label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtFiltroDecripcion" CssClass="form-control help_search_form" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" placeholder="Busca con una palabra clave..." />
                                    <asp:LinkButton runat="server" ID="btnBuscar" CssClass="btn btn-primary btn-single-icon fa fa-search" OnClick="btnBuscar_OnClick"/>
                                </div>
                            </div>
                            <br />
                            ... o consulta por Tipo de Usuario<br />
                            <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" />
                        </div>
                    </div>
                </div>
            </section>
            <br>
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
                                                            <th style="width: 8%">
                                                                <asp:Label runat="server" ID="Label1">Tipo Usuario</asp:Label></th>
                                                            <th  style="width: 8%">
                                                                <asp:Label runat="server" ID="lblNivel1">Nivel 1</asp:Label></th>
                                                            <th  style="width: 10%">
                                                                <asp:Label runat="server" ID="lblNivel2">Nivel 2</asp:Label></th>
                                                            <th style="width: 10%">
                                                                <asp:Label runat="server" ID="lblNivel3">Nivel 3</asp:Label></th>
                                                            <th style="width: 10%">
                                                                <asp:Label runat="server" ID="lblNivel4">Nivel 4</asp:Label></th>
                                                            <th style="width: 10%">
                                                                <asp:Label runat="server" ID="lblNivel5">Nivel 5</asp:Label></th>
                                                            <th style="width: 10%">
                                                                <asp:Label runat="server" ID="lblNivel6">Nivel 6</asp:Label></th>
                                                            <th style="width: 10%">
                                                                <asp:Label runat="server" ID="lblNivel7">Nivel 7</asp:Label></th>
                                                            <th style="width: 19%">
                                                                <asp:Label runat="server"></asp:Label></th>
                                                        <%--</tr>--%>
                                                    </thead>
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                <button type="button" class="btn btn-default-alt btn-square-usuario" style='<%# "Border: none !important; Background: " + Eval("TipoUsuario.Color") + " !important" %>'>
                                                            <%# Eval("TipoUsuario.Abreviacion") %></button></td>
                                                    <td><%# Eval("Pais.Descripcion")%></td>
                                                    <td><%# Eval("Campus.Descripcion")%></td>
                                                    <td><%# Eval("Torre.Descripcion")%></td>
                                                    <td><%# Eval("Piso.Descripcion")%></td>
                                                    <td><%# Eval("Zona.Descripcion")%></td>
                                                    <td><%# Eval("SubZona.Descripcion")%></td>
                                                    <td><%# Eval("SiteRack.Descripcion")%></td>
                                                    <td id="colHabilitado">
                                                        <ul class="list list-unstyled" id="hiddenEnabled">
                                                            <li>
                                                                <asp:CheckBox runat="server" AutoPostBack="true" Checked='<%# (bool) Eval("Habilitado") %>'  Visible='<%# int.Parse(Eval("IdNivelUbicacion").ToString()) != 1 %>' CssClass="chkIphone" Width="30px" data-id='<%# Eval("Id")%>' Text='<%# (bool) Eval("Habilitado") ? "SI" : "NO"%>' OnCheckedChanged="OnCheckedChanged" />
                                                            </li>
                                                        </ul>
                                                    </td>
                                                    <td id="colEditar">
                                                        <ul class="list list-unstyled hidden" id="hiddenEdit">
                                                            <li>
                                                                <asp:Button runat="server" CssClass="btn btn-sm btn-primary" Text="Editar" CommandArgument='<%# Eval("Id")%>' OnClick="btnEditar_OnClick" Visible='<%# int.Parse(Eval("IdNivelUbicacion").ToString()) != 1 && (bool) Eval("Habilitado") %>'/>
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

    <%--MODAL CATALOGOS--%>
    <div class="modal fade" id="editCatalogoUbicacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true" >
        <asp:UpdatePanel ID="upCatlogos" runat="server">
            <ContentTemplate>
                <uc1:UcAltaUbicaciones runat="server" id="ucAltaUbicaciones" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
