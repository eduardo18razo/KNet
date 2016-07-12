<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaUbicaciones.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaUbicaciones" %>
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
    <asp:UpdatePanel runat="server" style="height: 100%" ID="upGeneral">
        <ContentTemplate>
            <div id="contextMenu" class="panel-heading">
                <asp:HiddenField runat="server" ClientIDMode="Inherit" ID="hfId" />
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Baja" ID="btnBaja" OnClick="btnBaja_OnClick" ClientIDMode="Inherit" />
                </div>
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Alta" ID="btnAlta" OnClick="btnAlta_OnClick" ClientIDMode="Static" />

                </div>
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Editar" ID="btnEditar" OnClick="btnEditar_OnClick" />
                </div>
                <div class="form-group">
                    <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" />
                </div>
            </div>
            <div class="modal-header" id="panelAlertaUbicacion" runat="server" visible="false">
                <div class="alert alert-danger" role="alert">
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
                    <asp:Repeater runat="server" ID="rptErrorUbicacion">
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
                    <h3>Consulta Ubicaciones</h3>
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
                                                <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Pais</asp:Label>
                                                <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Campus</asp:Label>
                                                <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Torre</asp:Label>
                                                <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Piso</asp:Label>
                                                <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Zona</asp:Label>
                                                <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Sub Zona</asp:Label>
                                                <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Site Rack</asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" Width="14%" ID="ddlpais" CssClass="DropSelect" OnSelectedIndexChanged="ddlpais_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" ID="ddlCampus" CssClass="DropSelect" OnSelectedIndexChanged="ddlCampus_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" ID="ddlTorre" CssClass="DropSelect" OnSelectedIndexChanged="ddlTorre_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" ID="ddlPiso" CssClass="DropSelect" OnSelectedIndexChanged="ddlPiso_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" ID="ddlZona" CssClass="DropSelect" OnSelectedIndexChanged="ddlZona_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" ID="ddlSubZona" CssClass="DropSelect" OnSelectedIndexChanged="ddlSubZona_OnSelectedIndexChanged" AutoPostBack="True" AppendDataBoundItems="True" />
                                                <asp:DropDownList runat="server" Width="14%" ID="ddlSiteRack" CssClass="DropSelect" AppendDataBoundItems="True" />
                                            </div>
                                            <div class="form-group">
                                                <asp:Button runat="server" CssClass="col-xs-1 btn btn-primary" ID="btnNew" Text="Agregar Campus" Width="14%" OnClick="btnNew_OnClick" Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary">
                            <%--ScrollBars="Horizontal" Height="100%" Style="text-align: right; min-height: 10%" Width="100%"--%>
                            <div class="panel-body">
                                <asp:Repeater runat="server" ID="rptResultados">
                                    <HeaderTemplate>
                                        <%--<panel ID="Panel1" runat="server" ScrollBars="Horizontal" Style="height: 94%">--%>
                                        <table border="1" class="table table-bordered table-hover table-responsive" id="tblHeader">
                                            <thead>
                                                <tr align="center">
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Pais</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Campus</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Torre</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Piso</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Zona</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Sub Zona</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Site Rack</asp:Label></td>
                                                    <td>
                                                        <asp:Label class="col-xs-1 control-label;padding: 0;" runat="server">Habilitado</asp:Label></td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center" id='<%# Eval("Id")%>'>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" ondblclick="dbClic()" contextmenu="contextMenu"><%# Eval("Pais.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Campus.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Torre.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Piso.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("Zona.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("SubZona.Descripcion")%></td>
                                            <td style="padding: 0;" oncontextmenu="ContextMenu()" contextmenu="contextMenu"><%# Eval("SiteRack.Descripcion")%></td>
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

    <%--MODAL CATALOGOS--%>
    <div class="modal fade" id="editCatalogoUbicacion" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
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


    <%--MODAL CAMPUS--%>
    <div class="modal fade" id="editCampus" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upCampus" runat="server">
                    <ContentTemplate>
                        <div class="modal-header" id="panelAlertaCampus" runat="server" visible="false">
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
                                <asp:Repeater runat="server" ID="rptErrorCampus">
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
                                Datos generales
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Tipo de Usuario</label>
                                        <asp:DropDownList runat="server" ID="ddlTipoUsuarioCampus" CssClass="DropSelect" Enabled="False" />
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Descripcion</label>
                                        <asp:TextBox runat="server" ID="txtDescripcionCampus" placeholder="DESCRIPCION" class="form-control" onkeydown="return (event.keyCode!=13);" />
                                    </div>
                                    <div class="form-group">
                                        <asp:CheckBox runat="server" ID="CheckBox1" Checked="True" Visible="False" />
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Codigo Postal</label>
                                        <asp:TextBox runat="server" ID="txtCp" placeholder="CODIGO POSTAL" AutoPostBack="True" OnTextChanged="txtCp_OnTextChanged" class="form-control" onkeypress="return ValidaCampo(this,2)" onkeydown="return (event.keyCode!=13);" />
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Colonia</label>
                                        <asp:DropDownList runat="server" ID="ddlColonia" CssClass="DropSelect" />
                                    </div>
                                    <div class="form-group">

                                        <label class="col-sm-3 control-label">Calle</label>
                                        <asp:TextBox runat="server" ID="txtCalle" placeholder="CALLE" class="form-control" onkeydown="return (event.keyCode!=13);" />
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Numero Exterior</label>
                                        <asp:TextBox runat="server" ID="txtNoExt" placeholder="NUMERO EXTERIOR" class="form-control" onkeydown="return (event.keyCode!=13);" />
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Numero Interior</label>
                                        <asp:TextBox runat="server" ID="txtNoInt" placeholder="NUMERO " class="form-control" onkeydown="return (event.keyCode!=13);" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel-footer" style="text-align: center">
                            <asp:Button ID="btnCrearCampus" runat="server" CssClass="btn btn-success" Text="Aceptar" ValidationGroup="vsData" OnClick="btnCrearCampus_OnClick" />
                            <asp:Button ID="btnCancelarCampus" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelarCampus_OnClick" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancelarCampus" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>
