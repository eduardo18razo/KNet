﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaInformacionConsulta.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.UcAltaInformacionConsulta" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="ctrlExterno" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<script>
    function uploadComplete() {
        $get("<%=ReloadThePanel.ClientID %>").click();
    }
</script>
<asp:HiddenField runat="server" ID="hfFileName" />
<asp:HiddenField runat="server" ID="hfEsAlta" Value="true" />
<asp:HiddenField runat="server" ID="hfIdInformacionConsulta" />
<asp:HiddenField runat="server" ID="hfValueText" />
<br>
<h3 class="h6">
    <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Home</asp:HyperLink>
    / Editor de contenido / Nuevo artículo </h3>
<hr />
<asp:UpdatePanel runat="server" ID="upAltaInformacionConsulta" UpdateMode="Conditional">
    <ContentTemplate>
        <section class="module">
            <div class="row">
                <div class="col-lg-8 col-md-8">
                    <div class="module-inner">
                        <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" placeholder="Título del Artículo" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);"/>
                    </div>
                    <hr />
                </div>
                <div class="col-lg-4 col-md-4">
                    <div class="module-inner">
                        <asp:Button runat="server" CssClass="btn btn-default col-lg-3 col-md-3" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-default col-lg-4 col-md-4 margin-left-5" Text="Previsualizar" ID="btnPreview" OnClick="btnPreview_OnClick" />
                        <asp:Button runat="server" CssClass="btn btn-success col-lg-3 col-md-3 margin-left-5" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_OnClick" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 col-md-8">
                    <div class="module-inner">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ctrlExterno:HtmlEditor runat="Server" ID="txtEditor" Height="350px" ToggleMode="ToggleButton" ColorScheme="VisualStudio" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4">
                    <div class="module-inner">
                        Palabras de Búsqueda<hr />
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtBusqueda" ClientIDMode="Static" TextMode="MultiLine" Rows="5" CssClass="form-control" Style="width: 100%"></asp:TextBox>
                        </div>
                        Etiquetas<hr />
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtTags" ClientIDMode="Static" TextMode="MultiLine" Rows="5" CssClass="form-control" Style="width: 100%"></asp:TextBox>
                        </div>
                        Adjuntos<br />
                        <hr />
                        <div class="form-group">
                            <asp:Repeater runat="server" ID="rptFiles">
                                <ItemTemplate>
                                    <div class="row col-lg-12 col-md-12 col-sm-12">
                                        <span>
                                            <span class="col-lg-10 col-md-10 col-sm-10 fa fa-file-o">
                                                <asp:Label runat="server" ID="lblFile" Text='<%# Eval("NombreArchivo")%>' />
                                            </span>

                                            <asp:Label runat="server" ID="Label1" CssClass="col-lg-10 col-md-10 col-sm-10" Text='<%# Eval("Tamaño")%>' />
                                            <asp:LinkButton runat="server" CssClass="col-lg-1 col-md-1 col-sm-1 fa fa-remove" ID="btnRemoveFile" CommandArgument='<%# Eval("NombreArchivo")%>' OnClick="btnRemoveFile_OnClick" />
                                        </span>
                                        <br />
                                        <hr />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clearfix"></div>
                        </div>
                        <div class="form-group">
                            <span class="span-upload">
                                <ajax:AsyncFileUpload ID="afuArchivo" runat="server" CssClass="FileUploadClass" UploaderStyle="Traditional" OnClientUploadComplete="uploadComplete" OnUploadedComplete="afuArchivo_OnUploadedComplete" ClientIDMode="AutoID" PersistFile="True" ViewStateMode="Enabled" />
                                Cargar archivos (max 10 MB)
                                                <br />
                                <br />
                                <asp:Button ID="ReloadThePanel" runat="server" Text="hidden" OnClick="ReloadThePanel_OnClick" Style="visibility: hidden" />
                            </span>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <script>
            $(function () {
                $('#txtBusqueda').tagsInput({ width: 'auto', defaultText: 'Agregar', delimiter: '|' });
                $('#txtTags').tagsInput({ width: 'auto', defaultText: 'Agregar', delimiter: '|' });
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#txtBusqueda').tagsInput({ width: 'auto', defaultText: 'Agregar', delimiter: '|' });
                $('#txtTags').tagsInput({ width: 'auto', defaultText: 'Agregar', delimiter: '|' });
                });
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
