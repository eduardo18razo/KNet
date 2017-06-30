<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaHorario.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaHorario" %>
<%@ Register Src="~/UserControls/Altas/UcAltaHorario.ascx" TagPrefix="uc1" TagName="UcAltaHorario" %>


<div style="height: 100%;">
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfId" />
            <br>
            <h3 class="h6">
                <asp:HyperLink runat="server" NavigateUrl="~/Users/DashBoard.aspx">Home</asp:HyperLink>
                / Formularios </h3>
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
                    <div class="col-lg-4 col-md-3">
                        <div class="module-inner">
                            <asp:LinkButton runat="server" CssClass="btn btn-success fa fa-plus" Text="Crear nuevo horario" OnClick="btnNew_OnClick" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-8 col-md-9">
                        <div class="module-inner">
                            CONSULTA HORARIOS:<br />
                            <div class="search-box form-inline margin-bottom-lg">
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control help_search_form" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" placeholder="Busca con una palabra clave..." />
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-single-icon fa fa-search" OnClick="btnBuscar_OnClick" />
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
                                                    <th>
                                                        <asp:Label runat="server">Nombre</asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server">Creación</asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server">Últ. Edición</asp:Label></th>
                                                    <th>
                                                        <asp:Label runat="server">Activo</asp:Label></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("Descripcion")%></td>
                                            <td><%# Eval("FechaAlta")%></td>
                                            <td><%# Eval("FechaModificacion")%></td>
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
                                                        <asp:LinkButton runat="server" Text="Editar" CommandArgument='<%# Eval("Id")%>' OnClick="btnEditar_OnClick"></asp:LinkButton>
                                                        | 
                                                        <asp:LinkButton runat="server" Text="Clonar" CommandArgument='<%# Eval("Id")%>' OnClick="btnClonar_OnClick"></asp:LinkButton>
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
            <asp:PostBackTrigger ControlID="btnDownload" />
        </Triggers>
    </asp:UpdatePanel>
    <%--MODAL ALTA--%>
    <div class="modal fade" id="modalAltaHorario" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel ID="upAltaHorario" runat="server">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <uc1:UcAltaHorario runat="server" id="ucAltaHorario" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
