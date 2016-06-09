<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcInformacionConsulta.ascx.cs" Inherits="KiiniHelp.UserControls.Genericos.UcInformacionConsulta" %>
<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <header class="modal-header" id="pnlAlertaGeneral" runat="server" visible="false">
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
                        <ul>
                            <li><%# Container.DataItem %></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </header>
        <div class="well">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <asp:Label runat="server" ID="lbltitleArbol" />
                </div>
                <div class="panel-body">
                    <asp:HiddenField runat="server" ID="dfIdGrupo" />
                    <asp:Repeater runat="server" ID="rptInformacionConsulta">
                        <ItemTemplate>
                            <div style="float: left; width: 25%;">
                                <asp:Button runat="server" Text='<%# Eval("TipoInfConsulta.Descripcion") %>' CommandArgument='<%# Eval("Id") %>' ID="btnInformacion" OnClick="btnInformacion_OnClick" CommandName="0" CssClass="btn btn-primary btn-lg"/>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<%--MODAL Agrega Campo--%>
<div class="modal fade" id="modalMuestraInformacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upMostrarInformacion" runat="server">
        <ContentTemplate>
            <div class="modal-dialog modal-lg" style="width: 90%;">
                <div class="modal-content" style="width: 100%;">
                    <div class="modal-header" id="panelAlertaModal" runat="server" visible="False">
                        <div class="alert alert-danger">
                            <div>
                                <div style="float: left">
                                    <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                                </div>
                                <div style="float: left; margin-left: 20px">
                                    <h3>Error</h3>
                                </div>
                                <div class="clearfix clear-fix" />
                            </div>
                            <hr />
                            <asp:Repeater runat="server" ID="rptErrorModal">
                                <ItemTemplate>
                                    <%# Eval("Detalle")  %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="panel panel-primary">
                        <asp:HiddenField runat="server" ID="hfIdArbol"/>
                        <div runat="server" ID="divPropuetario" Visible="False">
                            <asp:Label runat="server" ID="lblContenido" ></asp:Label>
                        </div>
                        <div runat="server" ID="divInfoDocto" Visible="False">
                            <iframe runat="server" ID="ifDoctos" scrolling="yes" frameborder="0" style="border:none; overflow:hidden; width:100%; height:700px;" allowTransparency="true"></iframe>
                        </div>
                    </div>
                    <div class="modal-footer" style="text-align: center">
                        <asp:Button ID="btnCerrarModalInfo" runat="server" CssClass="btn btn-lg btn-danger" Text="Cerrar" OnClick="btnCerrarModalInfo_OnClick" data-dismiss="modal" />
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</div>

