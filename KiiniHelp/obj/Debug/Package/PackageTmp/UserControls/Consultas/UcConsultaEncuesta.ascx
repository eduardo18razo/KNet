<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaEncuesta.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaEncuesta" %>
<%@ Register Src="~/UserControls/Altas/Encuestas/UcAltaEncuesta.ascx" TagPrefix="uc1" TagName="UcAltaEncuesta" %>






<div style="height: 100%;">
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Home</asp:HyperLink>
                / Catálogo </h3>
            <hr />
            <section class="module">
                <div class="row">
                    <div class="col-lg-8 col-md-9">
                        <div class="module-inner">
                            <div class="module-heading">
                                <h3 class="module-title">
                                    <asp:Label runat="server" ID="lblBranding"></asp:Label></h3>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="module-inner">
                            <asp:LinkButton runat="server" CssClass="btn btn-success fa fa-plus" Text="Crear nueco catálogo" OnClick="btnNew_OnClick" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            CONSULTA ENCUESTA:<br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <label class="sr-only" for="txtFiltro">Buscar</label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control help_search_form" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" placeholder="Busca con una palabra clave..." />
                                    <asp:LinkButton runat="server" class="btn btn-primary btn-single-icon fa fa-search" OnClick="btnBuscar_OnClick"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-3">
                        <div class="module-inner">
                            <div class="form-group">
                                <asp:LinkButton runat="server" CssClass="btn btn-primary fa fa-download" Text="  Descargar reporte" ID="btnDownload" OnClick="btnDownload_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            <section class="module module-headings">
                <div class="module-inner">
                    <div class="module-content collapse in" id="content-1">
                        <div class="module-content-inner no-padding-bottom">
                            <div class="table-responsive">
                                <asp:Repeater runat="server" ID="rptResultados">
                                    <HeaderTemplate>
                                        <table class="table table-striped display" id="tblResults">
                                            <thead>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server">Titúlos</asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server">Tipo</asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server">Creación</asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server">Últ. edición</asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server">Activo</asp:Label></td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Titulo")%></td>
                                            <td><%# Eval("TipoEncuesta.Descripcion")%></td>
                                            <td><%# Eval("FechaAlta", "{0:d}")%></td>
                                            <td><%# Eval("FechaModificacion", "{0:d}")%></td>
                                            <td id="colHabilitado">
                                                <ul class="list list-unstyled" id="hiddenEnabled">
                                                    <li>
                                                        <asp:CheckBox runat="server" AutoPostBack="true" Checked='<%# (bool) Eval("Habilitado") %>' CssClass="chkIphone" Width="30px" data-id='<%# Eval("Id")%>' Text='<%# (bool) Eval("Habilitado") ? "SI" : "NO"%>' OnCheckedChanged="OnCheckedChanged" />
                                                    </li>
                                                </ul>
                                            </td>
                                            <td id="colEditar">
                                                <ul class="list list-unstyled hidden" id="hiddenEdit">
                                                    <li>
                                                        <asp:LinkButton runat="server" Text="Editar" CommandArgument='<%# Eval("Id")%>' ID="btnEditar" OnClick="btnEditar_OnClick"></asp:LinkButton>
                                                        | 
                                                        <asp:LinkButton runat="server" Text="Clonar" CommandArgument='<%# Eval("Id")%>' ID="btnClonar" OnClick="btnClonar_OnClick"></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                            </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDownload"/>
        </Triggers>
    </asp:UpdatePanel>
    <%--MODAL ALTA--%>
    <div class="modal fade" id="modalAltaEncuesta" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel ID="upAltaEncuesta" runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <uc1:UcAltaEncuesta runat="server" id="ucAltaEncuesta" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

