<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaArboles.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaArboles" %>
<%@ Import Namespace="KinniNet.Business.Utils" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

<style>
		a img{border: none;}
		ol li{list-style: decimal outside;}
		div#container{width: 780px;margin: 0 auto;padding: 1em 0;}
		div.side-by-side{width: 100%;margin-bottom: 1em;}
		div.side-by-side > div{float: left;width: 50%;}
		div.side-by-side > div > em{margin-bottom: 10px;display: block;}
		.clearfix:after{content: "\0020";display: block;height: 0;clear: both;overflow: hidden;visibility: hidden;}
		
	</style>

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
                            CssClass="form-control"
                        </div>
                    </div>
                </div>
            </section>
            <div class="side-by-side clearfix">

                <div>

                    <asp:DropDownList data-placeholder="Selecciona..." runat="server" ID="cboCountry" class="chzn-select" Style="width: 350px;">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="Opcion 1" Value="United States"></asp:ListItem>
                        <asp:ListItem Text="opcion 2" Value="United Kingdom"></asp:ListItem>
                        <asp:ListItem Text="opcion 3" Value="United Kingdom"></asp:ListItem>
                    </asp:DropDownList><%--<asp:Button runat="server" ID="btnSelect" Text="Get Selected" OnClick="btnSelect_Click" />--%>

                </div>
            </div>
            <%--<script src="Scripts/jquery.min.js" type="text/javascript"></script>--%>
            
            
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

            <%--<div class="panel panel-primary">
                <div class="panel-heading">
                    <asp:Label runat="server" ID="lbotest"></asp:Label>
                    <h3>Opciones de menú</h3>
                </div>
                <div class="panel-body">
                    <div class="panel panel-primary">
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingFiltros">
                                <h4 class="panel-title">
                                    <div role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFiltros" aria-expanded="true" aria-controls="collapseFiltros" style="cursor: pointer">
                                        Ocultar/Mostrar Filtros
                                    </div>
                                </h4>
                            </div>
                            <div id="collapseFiltros" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingFiltros">
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:Label Width="16%" runat="server" class="col-sm-3 control-label">Area de Atención</asp:Label>
                                                <asp:Label Width="16%" runat="server" class="col-sm-3 control-label">Tipo de Usuario Autorizado</asp:Label>
                                                <asp:Label Width="16%" runat="server" class="col-sm-3 control-label">Tipo de Servicio</asp:Label>
                                            </div>

                                            <div class="form-group">


                                                <asp:DropDownList runat="server" Width="16%" CssClass="DropSelect" ID="ddlTipoArbol" OnSelectedIndexChanged="ddlTipoArbol_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                            </div>
                                            <div class="form-group">

                                                <asp:Label Width="14%" runat="server" class="col-sm-3 control-label">SubMenu/Opcion 1</asp:Label>
                                                <asp:Label Width="14%" runat="server" class="col-sm-3 control-label">SubMenu/Opcion 2</asp:Label>
                                                <asp:Label Width="14%" runat="server" class="col-sm-3 control-label">SubMenu/Opcion 3</asp:Label>
                                                <asp:Label Width="14%" runat="server" class="col-sm-3 control-label">SubMenu/Opcion 4</asp:Label>
                                                <asp:Label Width="14%" runat="server" class="col-sm-3 control-label">SubMenu/Opcion 5</asp:Label>
                                                <asp:Label Width="14%" runat="server" class="col-sm-3 control-label">SubMenu/Opcion 6</asp:Label>
                                                <asp:Label Width="14%" runat="server" class="col-sm-3 control-label">SubMenu/Opcion 7</asp:Label>
                                            </div>
                                            <div class="form-group">

                                                <asp:DropDownList runat="server" Width="14%" CssClass="DropSelect" ID="ddlNivel1" OnSelectedIndexChanged="ddlNivel1_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" CssClass="DropSelect" ID="ddlNivel2" OnSelectedIndexChanged="ddlNivel2_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" CssClass="DropSelect" ID="ddlNivel3" OnSelectedIndexChanged="ddlNivel3_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" CssClass="DropSelect" ID="ddlNivel4" OnSelectedIndexChanged="ddlNivel4_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" CssClass="DropSelect" ID="ddlNivel5" OnSelectedIndexChanged="ddlNivel5_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" CssClass="DropSelect" ID="ddlNivel6" OnSelectedIndexChanged="ddlNivel6_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" CssClass="DropSelect" ID="ddlNivel7" OnSelectedIndexChanged="ddlNivel7_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="col-xs-1 btn btn-primary" ID="btnNew" Text="Agregar Holding" Width="14%" OnClick="btnNew_OnClick" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary">
                            <div class="panel-body">
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</div>
