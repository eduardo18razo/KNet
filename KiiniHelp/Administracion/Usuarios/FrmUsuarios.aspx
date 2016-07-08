<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmUsuarios.aspx.cs" Inherits="KiiniHelp.Administracion.Usuarios.FrmUsuarios" %>

<%@ Register Src="~/UserControls/Seleccion/AsociarGrupoUsuario.ascx" TagPrefix="uc" TagName="AsociarGrupoUsuario" %>
<%@ Register Src="~/UserControls/Altas/AltaOrganizacion.ascx" TagPrefix="uc" TagName="AltaOrganizacion" %>
<%@ Register Src="~/UserControls/Altas/UcAltaUbicacion.ascx" TagPrefix="uc" TagName="UcAltaUbicacion" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Alta Usuarios</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                                <asp:Button type="button" class="btn btn-primary btn-lg " Text="Datos Generales" ID="btnModalDatosGenerales" data-toggle="modal" data-target="#modalDatosGenerales" data-backdrop="static" data-keyboard="false" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Organización" ID="btnModalOrganizacion" data-toggle="modal" data-target="#modalOrganizacion" data-backdrop="static" data-keyboard="false" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Ubicación" ID="btnModalUbicacion" data-toggle="modal" data-target="#modalUbicacion" data-backdrop="static" data-keyboard="false" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Roles" ID="btnModalRoles" data-toggle="modal" data-target="#modalRoles" data-backdrop="static" data-keyboard="false" runat="server"></asp:Button>
                                <asp:Button type="button" class="btn btn-primary btn-lg disabled" Text="Grupos" ID="btnModalGrupos" data-toggle="modal" data-target="#modalGrupos" data-backdrop="static" data-keyboard="false" runat="server"></asp:Button>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <asp:Button CssClass="btn btn-lg btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" Style="float: right; margin-left: 25px" OnClick="btnCancelar_OnClick"></asp:Button>
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
                                                    <asp:Label ID="Label4" runat="server" Text="Apellido Paterno" class="col-sm-2 control-label izquierda"/>
                                                    <asp:TextBox ID="txtAp" runat="server" CssClass="form-control" onkeypress="return ValidaCampo(this,1)" MaxLength="100"/>
                                                </div>
                                                <div class="form-inline margen-arriba">
                                                    <asp:Label ID="Label5" runat="server" Text="Apellido Materno" class="col-sm-2 control-label izquierda"/>
                                                    <asp:TextBox ID="txtAm" runat="server" CssClass="form-control" onkeypress="return ValidaCampo(this,1)" MaxLength="100"/>
                                                </div>
                                                <div class="form-inline margen-arriba">
                                                    <asp:Label ID="Label6" runat="server" Text="Nombre" class="col-sm-2 control-label izquierda"/>

                                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" onkeypress="return ValidaCampo(this,1)" MaxLength="100"/>
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
                                                                        <asp:TextBox runat="server" ID="txtNumero" Text='<%# Eval("Numero") %>' CssClass="form-control" onkeypress="return ValidaCampo(this,2)" MaxLength="10" />
                                                                    </div>
                                                                    <div class="col-xs-4 col-md-3" runat="server" visible='<%# Eval("TipoTelefono.Extension") %>'>
                                                                        <asp:TextBox runat="server" ID="txtExtension" Text='<%# Eval("Extension") %>' CssClass="form-control" onkeypress="return ValidaCampo(this,2)" MaxLength="40" />
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
                                                                        <asp:TextBox runat="server" ID="txtCorreo" Text='<%# Eval("Correo") %>' CssClass="form-control" Style="text-transform: lowercase" onkeypress="return ValidaCampo(this,13)" />
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
                                        <asp:Button ID="btnCerrarDatosGenerales" runat="server" CssClass="btn btn-danger" Text="Cancelar" data-dismiss="modal" />
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
                        <uc:AltaOrganizacion runat="server" ID="ucOrganizacion" FromModal="True" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <%--UBICACIONES--%>
            <div class="modal fade" id="modalUbicacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <asp:UpdatePanel ID="upUbicacion" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc:UcAltaUbicacion runat="server" id="UcUbicacion" FromModal="True"/>
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
                                        <asp:Button ID="btnCerrarRoles" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCerrarRoles_OnClick" data-dismiss="modal" />
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
                                <uc:AsociarGrupoUsuario runat="server" ID="AsociarGrupoUsuario" Modal="#modalGrupos" />
                                <div class="modal-footer">
                                    <asp:Button runat="server" CssClass="btn btn-danger" ID="btnCerrarGrupos" Text="Cerrar" OnClick="btnCerrarGrupos_OnClick" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
