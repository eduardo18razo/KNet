<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmAltaArbolAcceso.aspx.cs" Inherits="KiiniHelp.Administracion.ArbolesAecceso.FrmAltaArbolAcceso" %>

<%@ Register Src="~/UserControls/Altas/AltaInformacionConsulta.ascx" TagPrefix="uc1" TagName="UcAltaInformacionConsulta" %>
<%@ Register Src="~/UserControls/Altas/AltaSla.ascx" TagPrefix="uc" TagName="UcSla" %>
<%@ Register Src="~/UserControls/Altas/AltaMascaraAcceso.ascx" TagPrefix="uc" TagName="UcAltaMascaraAcceso" %>
<%@ Register Src="~/UserControls/Altas/AltaEncuesta.ascx" TagPrefix="uc" TagName="UcEncuesta" %>
<%@ Register Src="~/UserControls/Seleccion/AsociarGrupoUsuario.ascx" TagPrefix="uc" TagName="AsociarGrupoUsuario" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Alta de Arboles de Acceso</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                            Alta de Arbol de Acceso
                        </div>
                        <div class="panel-body">
                            <div class="well">
                                <div class="form-horizontal verical-center center-content-div">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo Usuario" class="col-sm-s control-label"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="DropSelect" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" AutoPostBack="true" />
                                </div>
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
                                                            <asp:Label runat="server" for="ddlTipoArbol" class="col-sm-3 control-label">Tipo de Arbol</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlTipoArbol" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlTipoArbol_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel1" class="col-sm-3 control-label">Nivel 1</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel1" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel1_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddNivel1" data-toggle="modal" data-target="#editNivel" CommandName="Nivel 1" CommandArgument="1" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel2" class="col-sm-3 control-label">Nivel 2</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel2" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel2_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddNivel2" data-toggle="modal" data-target="#editNivel" CommandName="Nivel 2" CommandArgument="2" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel3" class="col-sm-3 control-label">Nivel 3</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel3" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel3_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddNivel3" data-toggle="modal" data-target="#editNivel" CommandName="Nivel 3" CommandArgument="3" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel4" class="col-sm-3 control-label">Nivel 4</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel4" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel4_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddNivel4" data-toggle="modal" data-target="#editNivel" CommandName="Nivel 4" CommandArgument="4" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel5" class="col-sm-3 control-label">Nivel 5</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel5" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel5_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddNivel5" data-toggle="modal" data-target="#editNivel" CommandName="Nivel 5" CommandArgument="5" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel6" class="col-sm-3 control-label">Nivel 6</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel6" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlNivel6_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddNivel6" data-toggle="modal" data-target="#editNivel" CommandName="Nivel 6" CommandArgument="6" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="col-sm-offset-1">
                                                            <asp:Label runat="server" for="ddlNivel7" class="col-sm-3 control-label">Nivel 7</asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlNivel7" Width="450px" CssClass="DropSelect" AutoPostBack="True" AppendDataBoundItems="True" />
                                                            <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddNivel7" data-toggle="modal" data-target="#editNivel" CommandName="Nivel 7" CommandArgument="7" />
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
                                                        <asp:TextBox runat="server" ID="txtDescripcionNivel" placeholder="DESCRIPCION" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ID="chkNivelHabilitado" Text="Habilitado" Checked="True" Visible="False" />
                                                    <asp:CheckBox runat="server" ID="chkNivelTerminal" CssClass="col-sm-3" Text="Es Nodo terminal" Checked="False" Visible="True" AutoPostBack="True" OnCheckedChanged="chkNivelTerminal_OnCheckedChanged" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div >
                                            <div class="panel panel-primary center-content-div">
                                                <div class="panel-heading">
                                                    Información
                                                </div>
                                                <div class="panel-body">
                                                    <asp:Button type="button" class="btn btn-primary btn-lg " Text="Consulta" ID="btnModalConsultas" data-toggle="modal" data-target="#modalConsultas" runat="server" Visible="False"></asp:Button>
                                                    <asp:Button type="button" class="btn btn-primary btn-lg " Text="Ticket" ID="btnMocalTicket" data-toggle="modal" data-target="#modalTicket" runat="server" Visible="False"></asp:Button>
                                                    <asp:Button type="button" class="btn btn-primary btn-lg " Text="Grupos" ID="btnModalGrupos" data-toggle="modal" data-target="#modalGruposNodo" runat="server"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer" style="text-align: center">
                                        <asp:Button ID="btnGuardarNivel" runat="server" CssClass="btn btn-lg btn-success" Text="Guardar" OnClick="btnGuardarNivel_OnClick" />
                                        <asp:Button ID="btnLimpiarNivel" runat="server" CssClass="btn btn-lg btn-danger" Text="Limpiar" OnClick="btnLimpiarNivel_OnClick" />
                                    </div>
                                </div>
                                <div class="modal-footer">
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
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerrarConsultas" Text="Cerrar" OnClick="btnCerrarConsultas_OnClick" />
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
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <%--TICKET--%>
                                <div class="panel panel-primary" runat="server" id="div1" visible="True">
                                    <div class="panel-heading">
                                        Ticket
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-horizontal">
                                            <%--MASCARA DE CAPTURA--%>
                                            <div class="form-group">
                                                <div class="form-inline" style="width: 100%">
                                                    <label class="col-sm-3 control-label" style="width: 180px">Mascara de acceso</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" ID="ddlMascaraAcceso" class="form-control" Style="width: 100%" />
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaMascara" />
                                                    </div>
                                                </div>
                                            </div>
                                            <%--SLA--%>
                                            <div class="form-group">
                                                <div class="form-inline margen-arriba" style="width: 100%">
                                                    <label class="col-sm-3 control-label" style="width: 180px">SLA</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" ID="ddlSla" class="form-control" Style="width: 100%" />
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaSla" />
                                                    </div>
                                                </div>
                                            </div>
                                            <%--ENCUESTA--%>
                                            <div class="form-group">
                                                <div class="form-inline" style="width: 100%">
                                                    <label class="col-sm-3 control-label" style="width: 180px">Encuesta</label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList runat="server" ID="ddlEncuesta" class="form-control" Style="width: 100%" />
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" data-toggle="modal" data-target="#modalAltaEncuesta" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerrarTicket" Text="Cerrar" OnClick="btnCerrarTicket_OnClick" />
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

            <div class="modal fade" id="modalAltaInfCons" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upModalAltaInfCons" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc1:UcAltaInformacionConsulta runat="server" ID="UcAltaInformacionConsulta" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerraraltaInformacion" Text="Cerrar" OnClick="btnCerraraltaInformacion_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal fade" id="modalAltaMascara" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg" style="width: 1300px">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc:UcAltaMascaraAcceso runat="server" ID="UcAltaMascaraAcceso" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerrarModalAltaMascara" Text="Cerrar" OnClick="btnCerrarModalAltaMascara_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="modal fade" id="modalAltaSla" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upModalAltaSla" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <uc:UcSla runat="server" ID="UcSla" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerrarAltaSla" Text="Cerrar" OnClick="btnCerrarAltaSla_OnClick" />
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
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerrarEncuesta" Text="Cerrar" OnClick="btnCerrarEncuesta_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
