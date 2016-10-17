<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaMascaraAcceso.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaMascaraAcceso" %>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:UpdatePanel ID="upControlesMascara" runat="server">
            <ContentTemplate>
                <header id="panelAlertaGeneral" runat="server" visible="False">
                    <div class="alert alert-danger">
                        <div>
                            <div style="float: left">
                                <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                            </div>
                            <div style="float: left; margin-left: 20px">
                                <h3 style="color: #ff0000">Error</h3>
                            </div>
                            <div class="clearfix clear-fix" />
                        </div>
                        <hr />
                        <asp:Repeater runat="server" ID="rptErrorGeneral">
                            <ItemTemplate>
                                <div style="color: #ff0000">
                                    <%# Eval("Detalle")  %>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </header>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Alta de Mascara de Captura
                    </div>
                    <div class="panel-body">

                        <div class="panel">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="form-inline verical-center">
                                            <asp:Label runat="server" Text="Nombre" CssClass="col-sm-2 izquierda control-label" />
                                            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" onkeypress="return ValidaCampo(this,1)"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="form-inline verical-center">
                                            <asp:CheckBox runat="server" ID="chkClaveRegistro" CssClass="form-control" Text="Clave de Registro"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Tipo de campo" CssClass="col-sm-2 control-label izquierda"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlTipoCampo" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoCampo_OnSelectedIndexChanged" CssClass="DropSelect" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <div class="row">
                                        <div class="col-xs-4 col-md-2 center-content-div" style="width: 200px; padding-left: 0">
                                            <asp:Label runat="server" Text="Tipo de campo"></asp:Label>
                                        </div>
                                        <div class="col-xs-4 col-md-2 center-content-div" style="width: 90px; padding-left: 0">
                                            <asp:Label runat="server" Text="Obligatorio"></asp:Label>
                                        </div>
                                        <div class="col-xs-4 col-md-2 center-content-div" style="width: 200px; padding-left: 0;">
                                            <asp:Label runat="server" Text="Descripcion"></asp:Label>
                                        </div>
                                        <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                            <asp:Label runat="server" Text="Longitud Min"></asp:Label>
                                        </div>
                                        <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                            <asp:Label runat="server" Text="Longitud Max"></asp:Label>
                                        </div>
                                        <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                            <asp:Label runat="server" Text="Simbolo Moneda"></asp:Label>
                                        </div>
                                        <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                            <asp:Label runat="server" Text="Valor Maximo"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <asp:Repeater runat="server" ID="rptControles">
                                        <ItemTemplate>
                                            <div class="row">
                                                <div class="form-inline">
                                                    <div class="col-xs-4 col-md-2 center-content-div" style="width: 200px; padding-left: 0">
                                                        <%# Eval("TipoCampoMascara.Descripcion") %>
                                                    </div>

                                                    <div class="col-xs-4 col-md-2 center-content-div" style="width: 90px; padding-left: 0">
                                                        <%# (bool) Eval("Requerido") ? "SI" : "NO" %>
                                                    </div>

                                                    <div class="col-xs-4 col-md-2 " style="width: 200px; padding-left: 0;">
                                                        <%# Eval("Descripcion") %>
                                                    </div>

                                                    <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                                        <%# Eval("LongitudMinima") %>
                                                    </div>

                                                    <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                                        <%# Eval("LongitudMaxima") %>
                                                    </div>

                                                    <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                                        <%# Eval("SimboloMoneda") %>
                                                    </div>

                                                    <div class="col-xs-4 col-md-2 center-content-div" style="width: 120px; padding-left: 0;">
                                                        <%# Eval("ValorMaximo") %>
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
                        <asp:Button CssClass="btn btn-success" ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_OnClick"></asp:Button>
                        <asp:Button CssClass="btn btn-danger" ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_OnClick"></asp:Button>
                        <asp:Button CssClass="btn btn-danger" ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_OnClick"></asp:Button>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="modalAgregarCampoMascara" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
            <asp:UpdatePanel ID="upAgregarCampo" runat="server">
                <ContentTemplate>
                    <div class="modal-dialog modal-lg" >
                        <div class="modal-content" >
                            <div class="modal-header" id="panelAlertaAgregarCampo" runat="server" visible="False">
                                <div class="alert alert-danger">
                                    <div>
                                        <div style="float: left">
                                            <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                                        </div>
                                        <div style="float: left; margin-left: 20px">
                                            <h3>Error</h3>
                                        </div>
                                    </div>
                                    <hr />
                                    <asp:Repeater runat="server" ID="rptErrorModalAgregarCampo">
                                        <ItemTemplate>
                                            <%# Eval("Detalle")  %>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h4 class="modal-title">
                                        <asp:Label runat="server" ID="lblTitleAgregarCampo" />
                                    </h4>
                                </div>
                                <div class="panel panel-body">
                                    <div class="form-group">
                                        <asp:Label runat="server" Text="Descripcion" />
                                        <asp:TextBox runat="server" ID="txtDescripcionCampo" CssClass="form-control" onkeypress="return ValidaCampo(this,1)"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:CheckBox runat="server" ID="chkRequerido" AutoPostBack="False" Text="Campo obligatorio" />
                                    </div>
                                    <div class="form-group" runat="server" id="divMascara" visible="False">
                                        <asp:Label runat="server" Text="Mascara" />
                                        <asp:TextBox runat="server" ID="txtMascara" CssClass="form-control" onkeypress="return ValidaCampo(this,1)"></asp:TextBox>
                                    </div>
                                    <div class="form-group" runat="server" id="divLongitudes" visible="False">
                                        <asp:Label runat="server" Text="Longitud minima" />
                                        <asp:TextBox runat="server" ID="txtLongitudMinima" type="number" min="1" CssClass="form-control" />
                                        <asp:Label runat="server" Text="Longitud Maxima" />
                                        <asp:TextBox runat="server" ID="txtLongitudMaxima" type="number" min="1" CssClass="form-control" />
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
                                        <asp:Label runat="server" Text="Catalgo" />
                                        <asp:DropDownList runat="server" ID="ddlCatalogosCampo" AutoPostBack="True" CssClass="DropSelect" />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="text-align: center">
                                <asp:Button ID="btnGuardarCampo" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardarCampo_OnClick" />
                                <asp:Button runat="server" CssClass="btn btn-danger" Text="Limpiar" ID="btnLimpiarCampo" OnClick="btnLimpiarCampo_OnClick" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
