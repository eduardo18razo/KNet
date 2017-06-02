<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaCatalogo.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.UcAltaCatalogo" %>
<asp:UpdatePanel runat="server" ID="updateAltaCatalogo">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfEsAlta" />
        <asp:HiddenField runat="server" ID="hfIdCatalogo" />
        <section class="module">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="module-inner">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Catálogo" CssClass="col-lg-3 col-md-3 col-sm-3"></asp:Label>
                            <div class="col-lg-8 col-md-8 col-sm-8">
                                <asp:TextBox runat="server" ID="txtDescripcionCatalogo" CssClass="form-control" onkeydown="return (event.keyCode!=13);" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="module-inner">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Opción de campo" CssClass="col-lg-3 col-md-3 col-sm-3"></asp:Label>
                            <div class="col-lg-8 col-md-8 col-sm-8">
                                <asp:TextBox runat="server" ID="txtOpcionCampo" CssClass="form-control" onkeydown="return (event.keyCode!=13);" />
                            </div>
                            <asp:LinkButton runat="server" Text="Borrar" ID="btnBorrarRegistro"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="module-inner">
                        <p class="text-right margin-top-40">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_OnClick" />
                            <asp:Button runat="server" CssClass="btn btn-danger" Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_OnClick" />
                            <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
                        </p>
                    </div>
                </div>
            </div>
        </section>
    </ContentTemplate>
</asp:UpdatePanel>
