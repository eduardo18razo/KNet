<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaEncuesta.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.Encuestas.UcAltaEncuesta" %>
<asp:UpdatePanel ID="upEncuesta" runat="server">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfAlta" />
        <asp:HiddenField runat="server" ID="hfIdGrupo" />
        <asp:HiddenField runat="server" ID="hfIdEncuesta" />
        <asp:HiddenField runat="server" ID="hfModalPadre" />
        <div class="modal-header">
            <asp:LinkButton class="close" ID="btnClose" OnClick="btnClose_OnClick" runat="server" Text='&times;' />
            <h2 class="modal-title" id="modal-new-ticket-label">
                <asp:Label runat="server" ID="lblBrandingModal" Text="ENCUESTA" /></h2>
        
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="module-inner">
                        <div class="form-group">
                            <asp:DropDownList runat="server" ID="ddlTipoEncuesta" CssClass="form-control" />
                            <hr class="bordercolor" />
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtTitulo" placeholder="Titúlo de la encuesta" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <b>CONFIGURACIÓN PARA EL CLIENTE</b>
                        </div>
                        <div class="form-group">
                            Assigna a esta encuesta que será visible para los clientes. Pueden agregar una breve descripción o instrucciones para facilitar su llenado.
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtTituloCliente" placeholder="Titúlo de la encuesta" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtDescripcion" placeholder="Descripcion" MaxLength="250" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <b>PREGUNTAS</b>
                        </div>
                        <div class="form-group">
                            Agrega las preguntas y su ponderación. Recuerda que la ponderación debe sumar 100.
                        </div>
                        <div class="form-group">
                            <asp:Repeater runat="server" ID="rptPreguntas">
                                <HeaderTemplate>
                                    <div class="row">
                                        <div class="col-xs-6 col-sm-5">Pregunta</div>
                                        <div class="col-xs-6 col-sm-1">Ponderacion</div>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="margen-arriba">
                                            <div class="col-xs-6 col-md-5">
                                                <asp:TextBox runat="server" ID="txtPregunta" CssClass="form-control" />
                                            </div>
                                            <div class="col-xs-5 col-md-2">
                                                <asp:TextBox runat="server" ID="txtPonderacion" CssClass="form-control" />
                                            </div>
                                            <asp:LinkButton runat="server" ID="btnSubir" CssClass="fa fa-angle-double-up" CommandArgument='<%# Eval("Id") %>' CommandName='<%# Container.ItemIndex %>' />
                                            <asp:LinkButton runat="server" ID="btnBajar" CssClass="fa fa-angle-double-down" CommandArgument='<%# Eval("Id") %>' CommandName='<%# Container.ItemIndex %>'/>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div class="row margin-top-5">
                                            <div class="col-xs-6 col-md-5">
                                                <asp:LinkButton runat="server" ID="btnagregarPregunta" CssClass="fa fa-plus-circle" OnClick="btnAddPregunta_OnClick" CommandArgument='<%# Container.ItemIndex %>'></asp:LinkButton>
                                            </div>
                                            <div class="col-xs-5 col-md-2">
                                                <asp:TextBox runat="server" ID="txtTotal" ReadOnly="True" CssClass="form-control" />
                                            </div>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnGuardarEncuesta" runat="server" CssClass="btn btn-lg btn-success" Text="Guardar" OnClick="btnGuardarEncuesta_OnClick" />
                            <asp:Button ID="btnLimpiarEncuesta" runat="server" CssClass="btn btn-lg btn-danger" Text="Limpiar" OnClick="btnLimpiarEncuesta_OnClick" />
                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-lg btn-danger" Text="Cancelar" OnClick="btnCancelar_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
