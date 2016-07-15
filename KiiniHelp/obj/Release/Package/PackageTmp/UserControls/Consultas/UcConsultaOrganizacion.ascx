<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaOrganizacion.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaOrganizacion" %>
<div style="height: 100%;">
    <script>
        function dbClic(e) {
            $('#tblHeader').find('tr').dblclick(function (e) {
                alert(e.target.parentElement.id);
            });
        };

        function ContextMenu() {
            var $contextMenu = $("#contextMenu");
            $("body").on("click", function (e) {
                //debugger;
                $contextMenu.hide();
                var table = $("#tblHeader");
                table.find('tr').each(function (i, ev) {
                    $(this).css('background', "transparent");
                });
            });
            $("body").on("contextmenu", "table tr", function (e) {
                debugger;
                $contextMenu.css({
                    display: "block",
                    left: e.pageX,
                    top: e.pageY
                });
                var baja = false;
                var alta = false;
                var parent = e.target.parentElement;
                var nodos = parent.parentElement.childNodes;
                for (var fondo = 0; fondo < nodos.length; fondo++) {
                    if (nodos[fondo].nodeType === 1)
                        parent.parentElement.childNodes[fondo].removeAttribute("style");
                }

                parent.parentElement.parentElement.style.background = 'transparent';
                parent.style.background = "gray";
                var columnas = e.target.parentElement.childNodes;
                for (var z = 0; z < columnas.length; z++) {
                    if (columnas[z].id === "colHabilitado") {
                        baja = (columnas[z].textContent === 'SI');
                    }
                }
                alta = !baja;
                document.getElementById("<%= this.FindControl("btnBaja").ClientID %>").style.display = baja ? 'block' : 'none';
                document.getElementById("<%= this.FindControl("btnAlta").ClientID %>").style.display = alta ? 'block' : 'none';
                var elementId = document.getElementById("<%= this.FindControl("hfId").ClientID %>");
                elementId.value = e.target.parentElement.id;
                return false;
            });

            $contextMenu.on("click", "button", function () {
                debugger;
                $contextMenu.hide();
            });
        };
    </script>
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <div id="contextMenu" class="panel-heading">
                <asp:HiddenField runat="server" ClientIDMode="Inherit" ID="hfId" />
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Baja" ID="btnBaja" OnClick="btnBaja_OnClick" />
                </div>
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Alta" ID="btnAlta" OnClick="btnAlta_OnClick" />

                </div>
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Editar" ID="btnEditar" OnClick="btnEditar_OnClick" />
                </div>
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" />
                </div>
            </div>
            <div class="modal-header" id="panelAlertaOrganizacion" runat="server" visible="false">
                <div class="alert alert-danger" role="alert">
                    <div>
                        <div style="float: left">
                            <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                        </div>
                        <div style="float: left">
                            <h3>Error</h3>
                        </div>
                        <div class="clearfix clear-fix"></div>
                    </div>
                    <hr />
                    <asp:Repeater runat="server" ID="rptErrorOrganizacion">
                        <ItemTemplate>
                            <ul>
                                <li><%# Container.DataItem %></li>
                            </ul>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <asp:Label runat="server" ID="lbotest"></asp:Label>
                    <h3>Consulta Organizacion</h3>
                </div>
                <div class="panel-body">
                    <div class="panel panel-primary">
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingFiltros">
                                <h4 class="panel-title">
                                    <div role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseFiltros" aria-expanded="true" aria-controls="collapseFiltros" style="cursor: pointer">
                                        Filtros
                                    </div>
                                </h4>
                            </div>
                            <div id="collapseFiltros" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingFiltros">
                                <div class="panel-body">
                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <asp:Label Width="14%" for="ddlTipoUsuario" class="col-xs-1 control-label" runat="server">Tipo de Usuario</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlTipoUsuario" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" />
                                        </div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:Label Width="14%" for="ddlHolding" class="col-xs-1 control-label" runat="server">Holding</asp:Label>
                                                <asp:Label Width="14%" for="ddlCompañia" class="col-xs-1 control-label" runat="server">Compañia</asp:Label>
                                                <asp:Label Width="14%" for="ddlDirecion" class="col-xs-1 control-label" runat="server">Direccion</asp:Label>
                                                <asp:Label Width="14%" for="ddlSubDireccion" class="col-xs-1 control-label" runat="server">Sub Direccion</asp:Label>
                                                <asp:Label Width="14%" for="ddlGerencia" class="col-xs-1 control-label" runat="server">Gerencia</asp:Label>
                                                <asp:Label Width="14%" for="ddlSubGerencia" class="col-xs-1 control-label" runat="server">Sub Gerencia</asp:Label>
                                                <asp:Label Width="14%" for="ddlJefatura" class="col-xs-1 control-label" runat="server">Jefatura</asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="ddlHolding" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlHolding_OnSelectedIndexChanged" />
                                                <asp:DropDownList runat="server" ID="ddlCompañia" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCompañia_OnSelectedIndexChanged"/>
                                                <asp:DropDownList runat="server" ID="ddlDireccion" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDirecion_OnSelectedIndexChanged" />
                                                <asp:DropDownList runat="server" ID="ddlSubDireccion" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSubDireccion_OnSelectedIndexChanged"/>
                                                <asp:DropDownList runat="server" ID="ddlGerencia" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlGerencia_OnSelectedIndexChanged"/>
                                                <asp:DropDownList runat="server" ID="ddlSubGerencia" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSubGerencia_OnSelectedIndexChanged"/>
                                                <asp:DropDownList runat="server" ID="ddlJefatura" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlJefatura_OnSelectedIndexChanged"/>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="col-xs-1 btn btn-primary" ID="btnNew" Text="Agregar Holding" Width="14%" OnClick="btnNew_OnClick" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary">
                            <div class="panel-body">
                                <asp:Repeater runat="server" ID="rptResultados">
                                    <HeaderTemplate>
                                        <table border="1" class="table table-bordered table-hover table-responsive" id="tblHeader">
                                            <thead>
                                                <tr align="center">
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Holding</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Compañia</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Direccion</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Sub Direccion</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Gerencia</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Sub Gerencia</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Jefatura</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Habilitado</asp:Label></td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center" id='<%# Eval("Id")%>'>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" ondblclick="dbClic()" contextmenu="contextMenu"><%# Eval("Holding.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Compania.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Direccion.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("SubDireccion.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Gerencia.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("SubGerencia.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Jefatura.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu" id="colHabilitado"><%# (bool) Eval("Habilitado") ? "SI" : "NO"%></td>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</div>
<%--MODAL CATALOGOS--%>
<div class="modal fade" id="editCatalogoOrganizacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel ID="upCatlogos" runat="server">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" id="panelAlertaCatalogo" runat="server" visible="false">
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
                            <asp:Repeater runat="server" ID="rptErrorCatalogo">
                                <ItemTemplate>
                                    <div class="row">
                                        <ul>
                                            <li><%# Container.DataItem %></li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <asp:Label runat="server" ID="lblTitleCatalogo"></asp:Label>
                        </div>
                        <div class="panel-body">
                            <asp:HiddenField runat="server" ID="hfCatalogo" />
                            <asp:HiddenField runat="server" ID="hfAlta" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Tipo de Usuario</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList runat="server" ID="ddlTipoUsuarioCatalogo" CssClass="DropSelect" Enabled="False" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Descripcion</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ID="txtDescripcionCatalogo" placeholder="DESCRIPCION" class="form-control" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" />
                                    </div>
                                </div>
                            </div>
                            <asp:CheckBox runat="server" ID="chkHabilitado" Checked="True" Visible="False" />
                        </div>
                    </div>
                    <div class="panel-footer" style="text-align: center">
                        <asp:Button ID="btnGuardarCatalogo" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnGuardarCatalogo_OnClick" />
                        <asp:Button ID="btnCancelarCatalogo" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelarCatalogo_OnClick" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancelarCatalogo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
