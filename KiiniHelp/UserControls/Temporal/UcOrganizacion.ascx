<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcOrganizacion.ascx.cs" Inherits="KiiniHelp.UserControls.Temporal.UcOrganizacion" %>
<%@ Register Src="~/UserControls/Genericos/UcCatalogos.ascx" TagPrefix="uc1" TagName="UcCatalogos" %>

<asp:UpdatePanel ID="upGeneral" runat="server">
    <ContentTemplate>
        <asp:UpdatePanel ID="upOrganizacion" runat="server" >
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="alert alert-warning" id="panelAlertaOrganizacion" runat="server" visible="false">
                            <div>
                                <div style="float: left">
                                    <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                                </div>
                                <div style="float: left">
                                    <h3>Error</h3>
                                </div>
                                <div class="clearfix clear-fix"></div>
                            </div>
                            <asp:Repeater runat="server" ID="rptErrorOrganizacion">
                                <ItemTemplate>
                                    <ul>
                                        <li><%# Container.DataItem %></li>
                                    </ul>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                Organizacion
                            </div>
                            <div class="panel-body">
                                <asp:HiddenField runat="server" ID="hfIdTipoUsuario" />
                                <div class="form-group">
                                    <div class="col-sm-offset-1">
                                        <asp:Label for="ddlHolding" class="col-sm-3 control-label" runat="server">Holding</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlHolding" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlHolding_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-sm-offset-1">
                                        <asp:Label for="ddlCompañia" class="col-sm-3 control-label" runat="server">Compañia</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlCompañia" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlCompañia_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                        <asp:Button runat="server" Text="add" ID="btnCompania" CommandName="Compañia" CommandArgument="9" OnClick="OnClick" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-sm-offset-1">
                                        <asp:Label for="ddlDirecion" class="col-sm-3 control-label" runat="server">Direccion</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlDireccion" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlDirecion_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                        <asp:Button runat="server" Text="add" OnClick="OnClick" ID="btnDireccion" CommandName="Direccion" CommandArgument="10" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-sm-offset-1">
                                        <asp:Label for="ddlSubDireccion" class="col-sm-3 control-label" runat="server">Sub Direccion</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlSubDireccion" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlSubDireccion_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                        <asp:Button runat="server" Text="add" OnClick="OnClick" ID="btnSubDireccion" CommandName="Sub Direccion" CommandArgument="11" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-sm-offset-1">
                                        <asp:Label for="ddlGerencia" class="col-sm-3 control-label" runat="server">Gerencia</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlGerencia" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlGerencia_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                        <asp:Button runat="server" Text="add" OnClick="OnClick" ID="btnGerencia" CommandName="Gerencia" CommandArgument="12" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-sm-offset-1">
                                        <asp:Label for="ddlSubGerencia" class="col-sm-3 control-label" runat="server">Sub Gerencia</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlSubGerencia" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlSubGerencia_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                        <asp:Button runat="server" Text="add" OnClick="OnClick" ID="btnSubGerencia" CommandName="Sub Gerencia" CommandArgument="13" />
                                    </div>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-sm-offset-1">
                                        <asp:Label for="ddlJefatura" class="col-sm-3 control-label" runat="server">Jefatura</asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlJefatura" Width="450px" CssClass="DropSelect" OnSelectedIndexChanged="ddlJefatura_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                        <asp:Button runat="server" Text="add" OnClick="OnClick" ID="btnJefatura" CommandName="Jefatura" CommandArgument="14" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel-footer" style="text-align: center">
                                <asp:Button ID="btnCerrarOrganizacion" runat="server" CssClass="btn btn-success btn-lg" Text="Aceptar" OnClick="btnCerrarOrganizacion_OnClick" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-lg" Text="Cerrar" data-dismiss="modal" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCerrarOrganizacion" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="modal fade" id="modalManagerCatalogo" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
            <uc1:UcCatalogos runat="server" ID="modalCataogoAlta" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
