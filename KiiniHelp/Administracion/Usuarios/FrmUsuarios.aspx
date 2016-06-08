<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/Administracion.master" AutoEventWireup="true" CodeBehind="FrmUsuarios.aspx.cs" Inherits="KiiniHelp.Administracion.Usuarios.FrmUsuarios" %>

<%@ Register Src="~/UserControls/Seleccion/AsociarGrupoUsuario.ascx" TagPrefix="uc" TagName="AsociarGrupoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentAdministracion" runat="server">
    <title>Alta Usuarios</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentAdministracion" runat="server">
    <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <header class="" id="panelAlertaGeneral" runat="server" visible="False">
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
                                    <%# Eval("Detalle")  %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </header>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h4>Alta de Usuarios</h4>
                        </div>
                        <div class="panel-body">
                            <div class="well">
                                <div class="form-inline verical-center center-content-div">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo Usuario" class="col-sm-s control-label"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="DropSelect" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" AutoPostBack="true" />
                                </div>
                            </div>
                            <div class="well center-content-div" runat="server" id="divDatos" visible="False">
                                <asp:Button type="button" class="btn btn-primary btn-lg " Text="Datos Generales" ID="btnModalDatosGenerales" data-toggle="modal" data-target="#modalDatosGenerales" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Organización" ID="btnModalOrganizacion" data-toggle="modal" data-target="#modalOrganizacion" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Ubicación" ID="btnModalUbicacion" data-toggle="modal" data-target="#modalUbicacion" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Roles" ID="btnModalRoles" data-toggle="modal" data-target="#modalRoles" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Grupos" ID="btnModalGrupos" data-toggle="modal" data-target="#modalGrupos" runat="server"></asp:Button>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:Button CssClass="btn btn-lg btn-success" ID="btnGuardar" runat="server" Text="Guardar" Style="float: right" OnClick="btnGuardar_OnClick"></asp:Button>
                            <div class="clearfix clear-fix"></div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--DATOS GENERALES--%>
            <div class="modal fade" id="modalDatosGenerales" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="upDatosGenerales" runat="server">
                            <ContentTemplate>
                                <div class="modal-header" id="panelAlertaModalDg" runat="server" visible="False">
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
                                        <asp:Repeater runat="server" ID="rptErrorDg">
                                            <ItemTemplate>
                                                <%# Container.DataItem %>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        Datos generales
                                    </div>
                                    <div class="panel-body">
                                        <%--DATOS GENERALES--%>
                                        <div class="panel-body">
                                            <div class="form-inline">
                                                <br />
                                                <div class="form-inline">
                                                    <asp:Label ID="Label4" runat="server" Text="Apellido Paterno" class="col-sm-2 control-label izquierda"></asp:Label>
                                                    <asp:TextBox ID="txtAp" runat="server" CssClass="form-control" onkeypress="return ValidaCampo(this,1)" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <div class="form-inline margen-arriba">
                                                    <asp:Label ID="Label5" runat="server" Text="Apellido Materno" class="col-sm-2 control-label izquierda"></asp:Label>
                                                    <asp:TextBox ID="txtAm" runat="server" CssClass="form-control" onkeypress="return ValidaCampo(this,1)" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <div class="form-inline margen-arriba">
                                                    <asp:Label ID="Label6" runat="server" Text="Nombre" class="col-sm-2 control-label izquierda"></asp:Label>
                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" onkeypress="return ValidaCampo(this,1)" MaxLength="100"></asp:TextBox>
                                                </div>
                                                <div class="form-inline">
                                                    <div class="form-group margen-arriba">
                                                        <asp:CheckBox runat="server" Text="Directorio Activo " ID="chkDirectoriActivo" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--TELEFONOS--%>
                                        <div class="well">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    <div class="row">
                                                        <div class="col-xs-6 col-sm-3">Telefonos</div>
                                                        <div class="col-xs-6 col-sm-3">
                                                            Numero telefono
                                                        </div>
                                                        <div class="col-xs-6 col-sm-3">
                                                            Extensiones
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel-body">
                                                    <asp:Repeater ID="rptTelefonos" runat="server">
                                                        <ItemTemplate>
                                                            <div style="border-radius: 20px; margin-bottom: 5px; height: auto">
                                                                <div class="row">
                                                                    <div class="col-xs-6 col-md-3" style="display: none">
                                                                        <asp:Label runat="server" ID="lblTipotelefono" Text='<%# Eval("TipoTelefono.Id") %>'></asp:Label>
                                                                    </div>
                                                                    <div class="col-xs-5 col-md-3">
                                                                        <asp:Label runat="server"><%# Eval("TipoTelefono.Descripcion") %></asp:Label>
                                                                    </div>
                                                                    <div class="col-xs-5 col-md-3">
                                                                        <asp:TextBox runat="server" ID="txtNumero" Text='<%# Eval("Numero") %>' CssClass="form-control" onkeypress="return ValidaCampo(this,2)" MaxLength="10"/>
                                                                    </div>
                                                                    <div class="col-xs-4 col-md-3" runat="server" Visible='<%# Eval("TipoTelefono.Extension") %>'>
                                                                        <asp:TextBox runat="server" ID="txtExtension" Text='<%# Eval("Extension") %>' CssClass="form-control" onkeypress="return ValidaCampo(this,2)" MaxLength="40"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                        <%--CORREOS--%>
                                        <div class="well">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">
                                                    Correos
                                                </div>
                                                <div class="panel-body">
                                                    <asp:Repeater ID="rptCorreos" runat="server">
                                                        <ItemTemplate>
                                                            <div style="border-radius: 20px; margin-bottom: 5px; height: auto">
                                                                <div class="row">
                                                                    <div class="col-xs-8 col-md-6">
                                                                        <asp:TextBox runat="server" ID="txtCorreo" Text='<%# Eval("Correo") %>' CssClass="form-control" Style="text-transform: lowercase" onkeypress="return ValidaCampo(this,13)"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer" style="text-align: center">
                                        <asp:Button ID="btnAceptarDatosGenerales" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnAceptarDatosGenerales_OnClick" />
                                        <asp:Button ID="btnCerrarDatosGenerales" runat="server" CssClass="btn btn-danger" Text="Cerrar" data-dismiss="modal" />
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCerrarDatosGenerales" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <%--ORGANIZACION--%>
            <div class="modal fade" id="modalOrganizacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upOrganizacion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header" id="panelAlertaOrganizacion" runat="server" visible="false">
                                    <div class="alert alert-danger" role="alert">
                                        <div>
                                            <div style="float: left">
                                                <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                                            </div>
                                            <div style="float: left">
                                                <h3>Error</h3>
                                            </div>
                                            <div class="clearfix clear-fix"></div>
                                        </div>
                                        <hr />
                                        <asp:Repeater runat="server" ID="rptErrorOrganizacion">
                                            <ItemTemplate>
                                                <ul>
                                                    <li><%# Container.DataItem %></li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        Organizacion
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlHolding" class="col-sm-3 control-label" runat="server">Holding</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlHolding" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlHolding_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlCompañia" class="col-sm-3 control-label" runat="server">Compañia</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlCompañia" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlCompañia_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnCompania" CommandName="Compañia" CommandArgument="9" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlDirecion" class="col-sm-3 control-label" runat="server">Direccion</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlDireccion" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlDirecion_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnDireccion" CommandName="Direccion" CommandArgument="10" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlSubDireccion" class="col-sm-3 control-label" runat="server">Sub Direccion</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSubDireccion" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlSubDireccion_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnSubDireccion" CommandName="Sub Direccion" CommandArgument="11" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlGerencia" class="col-sm-3 control-label" runat="server">Gerencia</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlGerencia" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlGerencia_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnGerencia" CommandName="Gerencia" CommandArgument="12" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlSubGerencia" class="col-sm-3 control-label" runat="server">Sub Gerencia</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlSubGerencia" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlSubGerencia_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnSubGerencia" CommandName="Sub Gerencia" CommandArgument="13" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlJefatura" class="col-sm-3 control-label" runat="server">Jefatura</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlJefatura" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlJefatura_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnJefatura" CommandName="Jefatura" CommandArgument="14" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer" style="text-align: center">
                                        <asp:Button ID="btnCerrarOrganizacion" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnCerrarOrganizacion_OnClick" />
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger" Text="Cerrar" data-dismiss="modal" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--UBICACIONES--%>
            <div class="modal fade" id="modalUbicacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upUbicacion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header" id="panelAlertaUbicacion" runat="server" visible="false">
                                    <div class="alert alert-danger" role="alert">
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
                                        <asp:Repeater runat="server" ID="rptErrorUbicacion">
                                            <ItemTemplate>
                                                <ul>
                                                    <li><%# Container.DataItem %></li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        Ubicacion
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlpais" class="col-sm-2 control-label" runat="server">País</asp:Label>
                                                <asp:DropDownList runat="server" ID="ddlpais" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlpais_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlCampus" class="col-sm-2 control-label" runat="server">Campus</asp:Label>
                                                <asp:DropDownList Enabled="False" runat="server" ID="ddlCampus" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlCampus_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" data-toggle="modal" data-target="#editCampus" CommandName="Campus" CommandArgument="0" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlTorre" class="col-sm-2 control-label" runat="server">Torre</asp:Label>
                                                <asp:DropDownList Enabled="False" runat="server" ID="ddlTorre" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlTorre_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddTorre" data-toggle="modal" data-target="#editCatalogoUbicacion" CommandName="Torre" CommandArgument="3" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlPiso" class="col-sm-2 control-label" runat="server">Piso</asp:Label>
                                                <asp:DropDownList Enabled="False" runat="server" ID="ddlPiso" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlPiso_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddPiso" data-toggle="modal" data-target="#editCatalogoUbicacion" CommandName="Piso" CommandArgument="4" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddllZona" class="col-sm-2 control-label" runat="server">Zona</asp:Label>
                                                <asp:DropDownList Enabled="False" runat="server" ID="ddlZona" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlZona_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddZona" data-toggle="modal" data-target="#editCatalogoUbicacion" CommandName="Zona" CommandArgument="5" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlSubZona" class="col-sm-2 control-label" runat="server">Sub Zona</asp:Label>
                                                <asp:DropDownList Enabled="False" runat="server" ID="ddlSubZona" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlSubZona_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddSubZona" data-toggle="modal" data-target="#editCatalogoUbicacion" CommandName="Sub Zona" CommandArgument="6" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-offset-1">
                                                <asp:Label for="ddlSiteRack" class="col-sm-2 control-label" runat="server">Site/Rack</asp:Label>
                                                <asp:DropDownList Enabled="False" runat="server" ID="ddlSiteRack" Width="450px" CssClass="DropSelect" AppendDataBoundItems="True" />
                                                <asp:Button runat="server" Text="Agregar" CssClass="btn btn-primary btn-xs" OnClick="OnClick" ID="btnAddSite" data-toggle="modal" data-target="#editCatalogoUbicacion" CommandName="Site Rack" CommandArgument="7" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-footer" style="text-align: center">
                                        <asp:Button ID="btnAceptarUbicaciones" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnCerrarUbicacion_OnClick" />
                                        <asp:Button ID="btnCerrarUbicacion" runat="server" CssClass="btn btn-danger" Text="Cerrar" data-dismiss="modal" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--ROLES--%>
            <div class="modal fade" id="modalRoles" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="upRoles" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-header" id="panelAlertaRoles" runat="server" visible="false">
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
                                        <asp:Repeater runat="server" ID="rptErrorRoles">
                                            <ItemTemplate>
                                                <ul>
                                                    <li><%# Container.DataItem %></li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        Asignacion de Roles                                
                                    </div>
                                    <div class="panel-body">
                                        <asp:CheckBoxList runat="server" ID="chklbxRoles" OnSelectedIndexChanged="chkKbxRoles_OnSelectedIndexChanged" AutoPostBack="True" />
                                    </div>
                                    <div class="panel-footer" style="text-align: center">
                                        <asp:Button ID="btnAceptarRoles" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnCerrarRoles_OnClick" />
                                        <asp:Button ID="btnCerrarRoles" runat="server" CssClass="btn btn-danger" Text="Cerrar" OnClick="btnCerrarRoles_OnClick" data-dismiss="modal" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <%--GRUPOS--%>
            <div class="modal fade" id="modalGrupos" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upGrupos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <uc:AsociarGrupoUsuario runat="server" ID="AsociarGrupoUsuario" />
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerrarGrupos" Text="Cerrar" OnClick="btnCerrarGrupos_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--MODAL CAMPUS--%>
            <div class="modal fade" id="editCampus" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <asp:UpdatePanel ID="upCampus" runat="server">
                            <ContentTemplate>
                                <div class="modal-header" id="panelAlertaCampus" runat="server" visible="false">
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
                                        <asp:Repeater runat="server" ID="rptErrorCampus">
                                            <ItemTemplate>
                                                <ul>
                                                    <li><%# Container.DataItem %></li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        Datos generales
                                    </div>
                                    <div class="panel-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">Tipo de Usuario</label>
                                                <asp:DropDownList runat="server" ID="ddlTipoUsuarioCampus" CssClass="DropSelect" />
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">Descripcion</label>
                                                <asp:TextBox runat="server" ID="txtDescripcionCampus" placeholder="DESCRIPCION" class="form-control" />
                                            </div>
                                            <div class="form-group">
                                                <asp:CheckBox runat="server" ID="chkHabilitado" Checked="True" Visible="False" />
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">Codigo Postal</label>
                                                <asp:TextBox runat="server" ID="txtCp" placeholder="CODIGO POSTAL" AutoPostBack="True" OnTextChanged="txtCp_OnTextChanged" class="form-control" onkeypress="return ValidaCampo(this,2)"/>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">Colonia</label>
                                                <asp:DropDownList runat="server" ID="ddlColonia" CssClass="DropSelect" />
                                            </div>
                                            <div class="form-group">

                                                <label class="col-sm-3 control-label">Calle</label>
                                                <asp:TextBox runat="server" ID="txtCalle" placeholder="CALLE" class="form-control" />
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Numero Exterior</label>
                                                <asp:TextBox runat="server" ID="txtNoExt" placeholder="NUMERO EXTERIOR" class="form-control" />
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">Numero Interior</label>
                                                <asp:TextBox runat="server" ID="txtNoInt" placeholder="NUMERO " class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer" style="text-align: center">
                                    <asp:Button ID="btnCrearCampus" runat="server" CssClass="btn btn-success" Text="Aceptar" ValidationGroup="vsData" OnClick="btnCrearCampus_OnClick" />
                                    <asp:Button ID="btnCancelarCampus" runat="server" CssClass="btn btn-danger" Text="Cancelar" data-dismiss="modal" OnClick="btnCancelarCampus_OnClick" />
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCancelarCampus" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <%--MODAL CATALOGOS--%>
            <div class="modal fade" id="editCatalogoUbicacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upCatlogos" runat="server">
                    <ContentTemplate>
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" id="panelAlertaCatalogo" runat="server" visible="false">
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
                                        <asp:Repeater runat="server" ID="rptErrorCatalogo">
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
                                        <asp:Label runat="server" ID="lblTitleCatalogo"></asp:Label>
                                    </div>
                                    <div class="panel-body">
                                        <asp:HiddenField runat="server" ID="hfCatalogo" />
                                        <div>
                                            <div class="form-group">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Tipo de Usuario</label>
                                                    <asp:DropDownList runat="server" ID="ddlTipoUsuarioCatalogo" CssClass="DropSelect" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Descripcion</label>
                                                    <asp:TextBox runat="server" ID="txtDescripcionCatalogo" placeholder="DESCRIPCION" class="form-control" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox runat="server" ID="CheckBox1" Checked="True" Visible="False" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer" style="text-align: center">
                                    <asp:Button ID="btnGuardarCatalogo" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnGuardarCatalogo_OnClick" />
                                    <asp:Button ID="btnCancelarCatalogo" runat="server" CssClass="btn btn-danger" Text="Cancelar" data-dismiss="modal" OnClick="btnCancelarCatalogo_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarCatalogo" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
