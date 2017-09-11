<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaCatalogo.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaCatalogo" %>
<%@ Register Src="~/UserControls/Operacion/UcRegistroCatalogo.ascx" TagPrefix="uc1" TagName="UcRegistroCatalogo" %>



<div style="height: 100%;">

    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Home</asp:HyperLink>
                / REGISTROS CATALOGOS </h3>
            <hr />

            <section class="module">
                <div class="row">
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
                                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnNew" Text="Agregar Registro"  OnClick="btnNew_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="module-inner">
                            CONSULTA REGISTROS CATÁLOGOS:<br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <label class="sr-only" for="txtFiltro">Catálogo</label>
                                <div class="form-group ">
                                    <asp:DropDownList runat="server" ID="ddlCatalogos" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCatalogos_OnSelectedIndexChanged" />
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
                                                        <asp:Label runat="server">Descripción</asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server">Habilitado</asp:Label></td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Descripcion") %></td>
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
                                                        <asp:Button runat="server" CssClass="btn btn-sm btn-primary" Text="Editar" CommandArgument='<%# Eval("Id")%>' OnClick="btnEditar_OnClick"/>
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
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--MODAL ALTA--%>
    <div class="modal fade" id="modalAltaRegistro" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel ID="upAltaAltaRegistro" runat="server">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <uc1:UcRegistroCatalogo runat="server" id="ucRegistroCatalogo" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
