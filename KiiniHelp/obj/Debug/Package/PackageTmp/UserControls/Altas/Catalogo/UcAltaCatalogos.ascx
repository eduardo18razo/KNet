<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaCatalogos.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.Catalogo.UcAltaCatalogos" %>
<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:UpdatePanel runat="server" ID="updateAltaCatalogo">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfEsAlta" />
        <asp:HiddenField runat="server" ID="hfIdCatalogo" />
        <section class="module no-border" style="border: none">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="module-inner">
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtNombreCatalogo" placeholder="Nombre del Catálogo" MaxLength="50" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtDescripcionCatalogo" placeholder="Nombre del Catálogo" TextMode="MultiLine" Rows="3" MaxLength="250" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <asp:RadioButton runat="server" Text="Agregar campos manualmente" AutoPostBack="True" GroupName="TipoCatalogo" ID="rbtnManual" OnCheckedChanged="rbtnTipoCatalogo_OnCheckedChanged"/>
                        </div>

                    
                        <div class="form-group">
                            <asp:Label runat="server" Text="Opción de campo" CssClass="col-lg-4 col-md-4 col-sm-4"></asp:Label>
                            <div class="col-lg-8 col-md-8 col-sm-8">
                                <asp:Repeater runat="server" ID="rptRegistros">
                                    <ItemTemplate>
                                        <div class="row margin-top-5">
                                            <div class="col-lg-10 col-md-10 col-sm-10">
                                                <asp:TextBox runat="server" ID="txtRegistro" Text='<%# Container.DataItem.ToString() %>' CssClass="form-control" />
                                            </div>
                                            <asp:LinkButton runat="server" Text="Borrar" ID="btnBorrarRegistro" CommandArgument='<%# Container.ItemIndex %>' OnClick="btnBorrarRegistro_OnClick"></asp:LinkButton>
                                        </div>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div class="row margin-top-5">
                                            <div class="col-lg-10 col-md-10 col-sm-10">
                                                <asp:TextBox runat="server" ID="txtRegistroNew" CssClass="form-control" />
                                            </div>
                                            <asp:LinkButton runat="server" CssClass="fa fa-plus-circle" ID="btnAgregarRegistro" OnClick="btnAgregarRegistro_OnClick" CommandArgument='<%# Container.ItemIndex %>' />

                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="module-inner">
                        <div class="form-group">
                            <asp:RadioButton runat="server" Text="Agregar campos desde archivo" AutoPostBack="True" GroupName="TipoCatalogo" ID="rbtnArchivo" OnCheckedChanged="rbtnTipoCatalogo_OnCheckedChanged"/>
                        </div>

                        <div class="form-group">
                            <asp:HiddenField runat="server" ID="hfFileName" />
                            <ajax:AsyncFileUpload ID="afuArchivo" runat="server" Enabled="False" UploaderStyle="Traditional" OnUploadedComplete="afuArchivo_OnUploadedComplete" PersistFile="True" />
                        </div>
                        <div class="form-group">
                            <asp:LinkButton runat="server" Text="Obtener páginas" ID="btnLeerArchivo" OnClick="btnLeerArchivo_OnClick"></asp:LinkButton>
                        </div>

                        <div class="form-group">
                            <asp:RadioButtonList runat="server" ID="rbtnHojas" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </ContentTemplate>
</asp:UpdatePanel>
