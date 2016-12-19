<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaPuestos.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaPuestos" %>
<%@ Register Src="~/UserControls/Altas/UcAltaPuesto.ascx" TagPrefix="uc1" TagName="UcAltaPuesto" %>

<div style="height: 100%;">
    <script>
        function dbClic(e) {
            debugger;
            $('#tblHeader').find('tr').dblclick(function (e) {
                alert(e.target.parentElement.id);
            });
        };

        function contextMenuPuestos() {
            var $contextMenuPuestos = $("#contextMenuPuestos");
            $("body").on("click", function (e) {
                debugger;
                $contextMenuPuestos.hide();
                var table = $("#tblHeader");
                table.find('tr').each(function (i, ev) {
                    $(this).css('background', "transparent");
                });
            });
            $("body").on("contextmenu", "table tr", function (e) {
                debugger;
                $contextMenuPuestos.css({
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

            $contextMenuPuestos.on("click", "button", function () {
                debugger;
                $contextMenuPuestos.hide();
            });
        };
    </script>
    <asp:UpdatePanel runat="server" style="height: 100%">
        <ContentTemplate>
            <div id="contextMenuPuestos" class="panel-heading contextMenu">
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
            <div class="modal-header" id="panelAlertaGeneral" runat="server" visible="false">
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
                    <asp:Repeater runat="server" ID="rptErrorGeneral">
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
                    <h3>Consulta Grupos</h3>
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
                                            <asp:Label Width="14%" class="col-xs-1 control-label" runat="server">Puestos</asp:Label>
                                            <asp:DropDownList runat="server" ID="ddlpuestos" Width="14%" CssClass="col-xs-1 DropSelect" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlpuestos_OnSelectedIndexChanged" />
                                        </div>
                                        <div class="form-group">
                                        </div>
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="col-xs-1 btn btn-primary" ID="btnNew" Text="Agregar Puesto" Width="14%" OnClick="btnNew_OnClick"/>
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
                                                        <asp:Label runat="server">Descripcion</asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server">Habilitado</asp:Label></td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr align="center" id='<%# Eval("Id")%>'>
                                            <td style="padding: 0; text-align: left; font-size: 10px;" oncontextmenu="contextMenuPuestos()" ><%# Eval("Descripcion")%></td>
                                            <td style="padding: 0; font-size: 10px;" oncontextmenu="contextMenuPuestos()"  id="colHabilitado"><%# (bool) Eval("Habilitado") ? "SI" : "NO"%></td>
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
    <%--MODAL ALTA--%>
    <div class="modal fade" id="modalAltaPuesto" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel ID="upAltaPuesto" runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <uc1:UcAltaPuesto runat="server" id="ucAltaPuesto" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
