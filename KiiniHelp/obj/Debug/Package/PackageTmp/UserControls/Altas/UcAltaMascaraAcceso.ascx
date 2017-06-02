<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaMascaraAcceso.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.UcAltaMascaraAcceso" %>
<%@ Register Src="~/UserControls/Altas/UcAltaCatalogo.ascx" TagPrefix="uc1" TagName="UcAltaCatalogo" %>


<h3 class="h6">
    <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Home</asp:HyperLink>
    / Editor de contenido / Nuevo artículo </h3>
<hr />
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:UpdatePanel ID="upControlesMascara" runat="server">
            <ContentTemplate>
                <section class="module">
                    <div class="row">
                        <div class="col-lg-8 col-md-8">
                            <div class="module-inner">
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Título del Formulario" MaxLength="30" />
                                <asp:CheckBox runat="server" ID="chkClaveRegistro" CssClass="form-control" Text="Clave de Registro" Visible="False" Checked="True" />
                            </div>
                            <hr />
                        </div>
                        <div class="col-lg-4 col-md-4">
                            <div class="module-inner">
                                <asp:Button runat="server" CssClass="btn btn-default col-lg-3 col-md-3" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
                                <asp:Button runat="server" CssClass="btn btn-default col-lg-4 col-md-4 margin-left-5" Text="Previsualizar" ID="btnPreview" />
                                <asp:Button runat="server" CssClass="btn btn-success col-lg-3 col-md-3 margin-left-5" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_OnClick" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-8 col-md-8">
                            <div class="module-inner">
                                <asp:Repeater runat="server" ID="rptControles" OnItemDataBound="rptControles_OnItemDataBound">
                                    <HeaderTemplate>
                                        <div class="row" style="border-bottom: 1px solid">
                                            <asp:Label runat="server" Text="Titulo" CssClass="col-lg-5 col-md-5 col-sm-5"></asp:Label>
                                            <asp:Label runat="server" Text="Tipo" CssClass="col-lg-4 col-md-4 col-sm-4"></asp:Label>
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="row" style="border-bottom: 1px solid">
                                            <div class="form-inline">
                                                <asp:Label runat="server" Text='<%# Eval("Descripcion") %>' CssClass="col-lg-5 col-md-5 col-sm-5"></asp:Label>
                                                <asp:Label runat="server" Text='<%# Eval("TipoCampoMascara.Descripcion") %>' CssClass="col-lg-4 col-md-4 col-sm-4"></asp:Label>
                                            </div>

                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" Text="Editar" ID="btnEditarCampo" OnClick="btnEditarCampo_OnClick" />|<asp:LinkButton runat="server" Text="Clonar" ID="btnEliminarCampo" OnClick="btnEliminarCampo_OnClick" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="btnSubir" OnClick="btnSubir_OnClick" CssClass="fa fa-angle-up" />
                                                    <asp:LinkButton runat="server" ID="btnBajar" OnClick="btnBajar_OnClick" CssClass="fa fa-angle-down" />
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4">
                            <div class="module-inner bg-grey">
                                <asp:Repeater runat="server" ID="rptTiposControles">
                                    <ItemTemplate>
                                        <div class="row">
                                            <asp:Image runat="server" ImageUrl='<%# "~/assets/images/controls/" + Eval("Image") %>' CssClass="col-lg-2 col-md-2 col-sm-2" Style="height: 22px; width: 38px" />
                                            <asp:Label runat="server" Text='<%# Eval("Descripcion") %>' CssClass="col-lg-8 col-md-8 col-sm-8"></asp:Label>
                                            <asp:LinkButton runat="server" Text="Agregar" ID="btnAgregarControl" CommandArgument='<%#Eval("Id") %>' CssClass="col-lg-2 col-md-2 col-sm-2" OnClick="btnAgregarControl_OnClick"></asp:LinkButton>
                                            <br />
                                            <hr />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="modal fade" id="modalAgregarCampoMascara" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upAgregarCampo" runat="server">
        <ContentTemplate>
            <div class="modal-dialog modal-lg">
                <div class="modal-content">

                    <asp:HiddenField runat="server" ID="hfAltaCampo" Value="true" />
                    <asp:HiddenField runat="server" ID="hfCampoEditado" Value="0" />
                    <asp:HiddenField runat="server" ID="hfTipoCampo" />
                    <div class="modal-header text-center">
                        <asp:LinkButton class="close" ID="btnClose" OnClick="btnCancelar_OnClick" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
                        <asp:Image runat="server" ID="imgTitleImage" />
                        <asp:Label runat="server" ID="lblTitleAgregarCampo" /><br />
                        <hr />
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="form-group margin-top">
                                <asp:Label runat="server" ID="lblDescripcion" /><br />
                                <asp:TextBox runat="server" ID="txtDescripcionCampo" CssClass="form-control" placeholder="Título del campo"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <asp:CheckBox runat="server" ID="chkRequerido" AutoPostBack="False" Text="Campo obligatorio" />
                            </div>
                            <div class="form-group" runat="server" id="divMascara" visible="False">
                                <asp:Label runat="server" Text="Formato del campo" />
                                <asp:TextBox runat="server" ID="txtMascara" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-inline margin-top">
                                <div class="form-group" runat="server" id="divLongitudMinima" visible="False">
                                    <asp:Label runat="server" Text="Longitud mínima" />
                                    <asp:TextBox runat="server" ID="txtLongitudMinima" type="number" min="1" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-inline margin-top">
                                <div class="form-group" runat="server" id="divLongitudMaxima" visible="False">
                                    <asp:Label runat="server" Text="Longitud máxima" />
                                    <asp:TextBox runat="server" ID="txtLongitudMaxima" type="number" min="1" CssClass="form-control" />
                                </div>

                            </div>
                            <div class="form-group" runat="server" id="divValorMaximo" visible="False">
                                <asp:Label runat="server" Text="Valor Maximo" />
                                <asp:TextBox runat="server" ID="txtValorMaximo" type="number" min="1" CssClass="form-control" />
                            </div>
                            <div class="form-group" runat="server" id="divMoneda" visible="False">
                                <asp:Label runat="server" Text="Simbolo de moneda" />
                                <asp:TextBox runat="server" ID="txtSimboloMoneda" Text="MXN" CssClass="form-control" />
                            </div>
                            <div class="form-group" runat="server" id="divCatalgo" visible="False">
                                CATÁLOGOS<br/>
                                <asp:DropDownList runat="server" ID="ddlCatalogosCampo" AutoPostBack="True" CssClass="form-control" />
                                <asp:Button runat="server" Text="Seleccionar" CssClass="btn btn-primary"/>
                                <br/>
                                Agregar campos manualmente:<br/>
                                <uc1:UcAltaCatalogo runat="server" id="UcAltaCatalogo" />
                            </div>

                        </div>
                        <div class="row">
                            <p class="text-right margin-top-40">
                                <asp:Button ID="btnAgregarCampo" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregarCampo_OnClick" />
                                <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnLimpiarCampo" OnClick="btnLimpiarCampo_OnClick" Visible="False" />
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

