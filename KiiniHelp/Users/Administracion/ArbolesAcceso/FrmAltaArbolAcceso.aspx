<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmAltaArbolAcceso.aspx.cs" Inherits="KiiniHelp.Users.Administracion.ArbolesAcceso.FrmAltaArbolAcceso" %>

<%@ Register Src="~/UserControls/Altas/AltaInformacionConsulta.ascx" TagPrefix="uc1" TagName="UcAltaInformacionConsulta" %>
<%@ Register Src="~/UserControls/Altas/AltaSla.ascx" TagPrefix="uc" TagName="UcSla" %>
<%@ Register Src="~/UserControls/Altas/AltaMascaraAcceso.ascx" TagPrefix="uc" TagName="UcAltaMascaraAcceso" %>
<%@ Register Src="~/UserControls/Altas/AltaEncuesta.ascx" TagPrefix="uc" TagName="UcEncuesta" %>
<%@ Register Src="~/UserControls/Seleccion/AsociarGrupoUsuario.ascx" TagPrefix="uc" TagName="AsociarGrupoUsuario" %>
<%@ Register Src="~/UserControls/Altas/AltaArea.ascx" TagPrefix="uc" TagName="AltaArea" %>
<%@ Register Src="~/UserControls/Altas/AltaTiempoEstimado.ascx" TagPrefix="uc" TagName="AltaTiempoEstimado" %>
<%@ Register Src="~/UserControls/Seleccion/UcImpactoUrgencia.ascx" TagPrefix="uc" TagName="UcImpactoUrgencia" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <header>
                        <div class="alert alert-danger" id="panelAlert" runat="server" visible="False">
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
                            <asp:Repeater runat="server" ID="rptHeaderError">
                                <ItemTemplate>
                                    <%# Container.DataItem %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </header>

                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Agregar Opcion
                        </div>
                        <div class="panel-body">
                            <div class="well">
                                <asp:UpdatePanel ID="upSeleccionArea" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-horizontal verical-center center-content-div">
                                            <asp:Label ID="Label2" runat="server" Text="Area" class="col-sm-s control-label" />
                                            <asp:DropDownList runat="server" ID="ddlArea" CssClass="DropSelect" OnSelectedIndexChanged="ddlArea_OnSelectedIndexChanged" AutoPostBack="true" />
                                            <asp:Button runat="server" ID="btnAddArea" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="btnAddArea_OnClick" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <%--ARBOL DE ACCESO--%>
                            <div class="well center-block">
                                <asp:UpdatePanel ID="upArbolAcceso" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                Datos generales
                                            </div>
                                            <div class="panel-body">
                                                <div class="panel-body">
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlTipoUsuario" class="col-sm-3 control-label">Tipo de Usuario Autorizado</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlTipoUsuario" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" AutoPostBack="true" />
                                                            <%----%>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlTipoArbol" class="col-sm-3 control-label">Tipo de Servicio</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlTipoArbol" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlTipoArbol_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel1" class="col-sm-3 control-label">SubMenu/Opcion 1</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel1" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel1_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" ID="btnAddMenu1" Text="Agregar SubMenu" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelSubMenu" CommandName="Menu" CommandArgument="1" Enabled="False" />
                                                            <asp:Button runat="server" ID="btnAddOpti1" Text="Agregar Opcion" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelOpcion" CommandName="Opcion" CommandArgument="1" Enabled="False" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel2" class="col-sm-3 control-label">SubMenu/Opcion 2</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel2" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel2_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" ID="btnAddMenu2" Text="Agregar SubMenu" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelSubMenu" CommandName="Menu" CommandArgument="2" Enabled="False" />
                                                            <asp:Button runat="server" ID="btnAddOpti2" Text="Agregar Opcion" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelOpcion" CommandName="Opcion" CommandArgument="2" Enabled="False" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel3" class="col-sm-3 control-label">SubMenu/Opcion 3</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel3" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel3_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" ID="btnAddMenu3" Text="Agregar SubMenu" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelSubMenu" CommandName="Menu" CommandArgument="3" Enabled="False" />
                                                            <asp:Button runat="server" ID="btnAddOpti3" Text="Agregar Opcion" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelOpcion" CommandName="Opcion" CommandArgument="3" Enabled="False" />

                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel4" class="col-sm-3 control-label">SubMenu/Opcion 4</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel4" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel4_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" ID="btnAddMenu4" Text="Agregar SubMenu" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelSubMenu" CommandName="Menu" CommandArgument="4" Enabled="False" />
                                                            <asp:Button runat="server" ID="btnAddOpti4" Text="Agregar Opcion" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelOpcion" CommandName="Opcion" CommandArgument="4" Enabled="False" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel5" class="col-sm-3 control-label">SubMenu/Opcion 5</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel5" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel5_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" ID="btnAddMenu5" Text="Agregar SubMenu" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelSubMenu" CommandName="Menu" CommandArgument="5" Enabled="False" />
                                                            <asp:Button runat="server" ID="btnAddOpti5" Text="Agregar Opcion" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelOpcion" CommandName="Opcion" CommandArgument="5" Enabled="False" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel6" class="col-sm-3 control-label">SubMenu/Opcion 6</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel6" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel6_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" ID="btnAddMenu6" Text="Agregar SubMenu" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelSubMenu" CommandName="Menu" CommandArgument="6" Enabled="False" />
                                                            <asp:Button runat="server" ID="btnAddOpti6" Text="Agregar Opcion" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelOpcion" CommandName="Opcion" CommandArgument="6" Enabled="False" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel7" class="col-sm-3 control-label">SubMenu/Opcion 7</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel7" Width="450px" CssClass="DropSelect" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" ID="btnAddOpti7" Text="Agregar Opcion" CssClass="btn btn-primary btn-xs" OnClick="OnClickNivelOpcion" CommandName="Opcion" CommandArgument="7" Enabled="False" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <div class="clearfix clear-fix"></div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--MODAL CATALOGO NIVELES--%>
            <div class="modal fade" id="editNivel" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upNivel" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header" id="panelAlertaNivel" runat="server" visible="false">
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
                                        <asp:Repeater runat="server" ID="rptErrorNivel">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <ul>
                                                        <li><%# Container.DataItem %></li>
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                </div>
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <h4 class="modal-title">
                                            <asp:Label runat="server" ID="lblTitleCatalogo"></asp:Label>
                                        </h4>
                                    </div>
                                    <div class="panel-body">
                                        <asp:HiddenField runat="server" ID="hfCatalogo" />
                                        <div>
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Tipo de Usuario</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" ID="ddlTipoUsuarioNivel" CssClass="DropSelect" Width="100%" Enabled="False" />

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Descripcion</label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox runat="server" ID="txtDescripcionNivel" placeholder="DESCRIPCION" class="form-control" onkeydown="return (event.keyCode!=13);" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ID="chkNivelHabilitado" Text="Habilitado" Checked="True" Visible="False" />
                                                    <asp:CheckBox runat="server" ID="chkNivelTerminal" CssClass="col-sm-3" Text="Es Nodo terminal" Checked="False" Visible="False" AutoPostBack="True" OnCheckedChanged="chkNivelTerminal_OnCheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div>
                                            <div class="panel panel-primary center-content-div" runat="server" id="divDatos" visible="False">
                                                <div class="panel-heading">
                                                    Información
                                                </div>
                                                <div class="panel-body">
                                                    <asp:Button class="btn btn-primary " runat="server" Text="Consulta" ID="btnModalConsultas" OnClick="btnModalConsultas_OnClick"/>
                                                    <asp:Button class="btn btn-primary " runat="server" Text="Ticket" ID="btnModalTicket" OnClick="btnModalTicket_OnClick"/>
                                                    <asp:Button class="btn btn-primary disabled" runat="server" Text="Grupos" ID="btnModalGrupos" OnClick="btnModalGrupos_OnClick"/>
                                                    <asp:Button class="btn btn-primary disabled" runat="server" Text="SLA" ID="btnModalSla" OnClick="btnModalSla_OnClick"/>
                                                    <asp:Button class="btn btn-primary disabled" runat="server" Text="Impacto/Urgencia" ID="btnModalImpactoUrgencia" OnClick="btnModalImpactoUrgencia_OnClick"/>
                                                    <asp:Button class="btn btn-primary disabled" runat="server" Text="Tiempo Informe" ID="btnModalInforme" OnClick="btnModalInforme_OnClick"/>
                                                    <asp:Button class="btn btn-primary disabled" runat="server" Text="Encuesta" ID="btnModalEncuesta" OnClick="btnModalEncuesta_OnClick"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer" style="text-align: center">
                                    <asp:Button ID="btnGuardarNivel" runat="server" CssClass="btn btn-lg btn-success" Text="Guardar" OnClick="btnGuardarNivel_OnClick" />
                                    <asp:Button ID="btnLimpiarNivel" runat="server" CssClass="btn btn-lg btn-danger" Text="Limpiar" OnClick="btnLimpiarNivel_OnClick" />
                                    <asp:Button ID="btnCancelarNivel" runat="server" CssClass="btn btn-lg btn-danger" Text="Cancelar" OnClick="btnCancelarNivel_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--INFORMACION DE CONSULTA--%>
            <div class="modal fade" id="modalConsultas" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upConsultas" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header" id="panelAlertaInfoConsulta" runat="server" visible="false">
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
                                        <asp:Repeater runat="server" ID="rptErrorInfoConsulta">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <ul>
                                                        <li><%# Container.DataItem %></li>
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <div class="col-sm-3 control-label">
                                            Tipo Información
                                        </div>
                                        <div class="col-sm-3 control-label">
                                            Información
                                        </div>
                                        <div class="clear-fix clearfix"></div>
                                    </div>
                                    <div class="panel-body">
                                        <asp:Repeater runat="server" ID="rptInformacion" OnItemDataBound="rptInformacion_OnItemDataBound">
                                            <ItemTemplate>
                                                <div class="row ">
                                                    <asp:Label runat="server" ID="lblIndex" Text='<%# Container.ItemIndex %>' Visible="False"></asp:Label>
                                                    <asp:Label runat="server" Text='<%# Eval("TipoInfConsulta.Id") %>' Visible="False" ID="lblIdTipoInformacion"></asp:Label>
                                                    <div class="col-sm-3 control-label" style="width: 180px">
                                                        <asp:CheckBox runat="server" Text='<%# Eval("TipoInfConsulta.Descripcion") %>' Checked="False" ID="chkInfoConsulta" OnCheckedChanged="chkInfoConsulta_OnCheckedChanged" AutoPostBack="True" />
                                                    </div>
                                                    <div runat="server" class="col-sm-9 control-label margen-arriba">
                                                        <div runat="server" visible='<%# Convert.ToBoolean(Eval("TipoInfConsulta.EsBaseDatos")) %>'>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList runat="server" ID="ddlPropietario" CssClass="DropSelect" Width="100%" Enabled="False" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:Button runat="server" Text="Agregar" ID="btnAgregarPropietario" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaInfCons" Enabled="False" />
                                                            </div>
                                                        </div>
                                                        <div runat="server" visible='<%# Convert.ToBoolean(Eval("TipoInfConsulta.EsDirectorio")) %>'>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList runat="server" ID="ddlDocumento" CssClass="DropSelect" Width="100%" Enabled="False" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:Button runat="server" Text="Agregar" ID="btnAgregarDocumento" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaInfCons" Enabled="False" />
                                                            </div>
                                                        </div>
                                                        <div runat="server" visible='<%# Convert.ToBoolean(Eval("TipoInfConsulta.EsUrl")) %>'>
                                                            <div class="col-sm-4">
                                                                <asp:DropDownList runat="server" ID="ddlUrl" CssClass="DropSelect" Width="100%" Enabled="False" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:Button runat="server" Text="Agregar" ID="btnAgregarUrl" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaInfCons" Enabled="False" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCerrarConsultas" Text="Cerrar" OnClick="btnCerrarConsultas_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--TICKET--%>
            <div class="modal fade" id="modalTicket" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upTocket" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" id="panelAlertaTicket" runat="server" visible="false">
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
                                        <asp:Repeater runat="server" ID="rptErrorTicket">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <ul>
                                                        <li><%# Container.DataItem %></li>
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <%--TICKET--%>
                                <div class="panel panel-primary" runat="server" id="div1" visible="True">
                                    <div class="panel-heading">
                                        Formulario de Cliente
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-horizontal">
                                            <%--Formulario de Cliente--%>
                                            <div class="form-group">
                                                <div class="form-inline" style="width: 100%">
                                                    <label class="col-sm-3 control-label" style="width: 180px">Formulario de Cliente</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" ID="ddlMascaraAcceso" class="form-control" Style="width: 100%" />
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaMascara" data-keyboard="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCerrarTicket" Text="Cerrar" OnClick="btnCerrarTicket_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--GRUPOS--%>
            <div class="modal fade" id="modalGruposNodo" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upGrupos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <uc:AsociarGrupoUsuario runat="server" ID="AsociarGrupoUsuario" />
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerraGrupos" Text="Cerrar" OnClick="btnCerraGrupos_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--SLA--%>
            <div class="modal fade" id="modalSla" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc:UcSla runat="server" ID="UcSla" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            <%--IMPACTO URGENCIA--%>
            <div class="modal fade" id="modalImpacto" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc:UcImpactoUrgencia runat="server" ID="UcImpactoUrgencia" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--TIEMPO INFORME--%>
            <div class="modal fade" id="modalTiempoInforme" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc:AltaTiempoEstimado runat="server" ID="ucAltaTiempoEstimado" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--ENCUESTA--%>
            <div class="modal fade" id="modalEncuesta" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header" id="Div2" runat="server" visible="false">
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
                                        <asp:Repeater runat="server" ID="Repeater1">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <ul>
                                                        <li><%# Container.DataItem %></li>
                                                    </ul>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="panel panel-primary" runat="server" id="div3" visible="True">
                                    <div class="panel-heading">
                                        Encuesta
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-horizontal">

                                            <%--ENCUESTA--%>
                                            <div class="form-group">
                                                <div class="form-inline" style="width: 100%">
                                                    <label class="col-sm-3 control-label" style="width: 180px">Encuesta</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" ID="ddlEncuesta" class="form-control" Style="width: 100%" />
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaEncuesta" data-keyboard="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCerrarEncuesta" Text="Cerrar" OnClick="btnCerrarEncuesta_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            
            <%-- ALTAS --%>
            <div class="modal fade" id="modalAltaInfCons" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upModalAltaInfCons" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc1:UcAltaInformacionConsulta runat="server" ID="UcAltaInformacionConsulta" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal fade" id="modalAltaMascara" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg" style="width: 1300px; height: 500px">
                            <div class="modal-content" style="height: 100%">
                                <div class="modal-body">
                                    <uc:UcAltaMascaraAcceso runat="server" ID="UcAltaMascaraAcceso" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal fade" id="modalAltaEncuesta" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc:UcEncuesta runat="server" ID="UcEncuesta" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--AREA--%>
    <div class="modal fade" id="modalAreas" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel ID="upModalAltaAreas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <uc:AltaArea runat="server" ID="AltaAreas" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
