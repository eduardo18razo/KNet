<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaTiempoEstimado.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaTiempoEstimado" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=16.1.0.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<script>
    function sum() {
        debugger;
        var totaldias = 0, totalhoras = 0, totalminutos = 0, totalsegundos = 0;
        $("#tblHeader > tbody > tr").each(function (indexRow) {
            var control;
            $(this).children("td").each(function (indexColumn) {
                switch (indexColumn) {
                    case 2:
                        control = $(this).find("input[id*=txtDias]");
                        if (control != null) {
                            totaldias = totaldias + parseInt(control.val() === "" ? 0 : control.val());
                        }
                        break;
                    case 3:
                        control = $(this).find("input[id*=txtHoras]");
                        if (control != null) {
                            totalhoras = totalhoras + parseInt(control.val() === "" ? 0 : control.val());
                        }
                        break;
                    case 4:
                        control = $(this).find("input[id*=txtMinutos]");
                        if (control != null) {
                            totalminutos = totalminutos + parseInt(control.val() === "" ? 0 : control.val());
                        }
                        break;
                    case 5:
                        control = $(this).find("input[id*=txtSegundos]");
                        if (control != null) {
                            totalsegundos = totalsegundos + parseInt(control.val() === "" ? 0 : control.val());
                        }
                        break;
                }

            });
        });

        $("#tblHeader > tfoot > tr").each(function (indexRow) {
            var control;
            $(this).children("td").each(function(indexColumn) {
                switch (indexColumn) {
                    case 2:
                        control = $(this).find("input[id*=txtTotalDias]");
                        if (control != null) {
                            control.val(totaldias);
                        }
                        break;
                    case 3:
                        control = $(this).find("input[id*=txtTotalHoras]");
                        if (control != null) {
                            control.val(totalhoras);
                        }
                        break;
                    case 4:
                        control = $(this).find("input[id*=txtTotalMinutos]");
                        if (control != null) {
                            control.val(totalminutos);
                        }
                        break;
                    case 5:
                        control = $(this).find("input[id*=txtTotalSegundos]");
                        if (control != null) {
                            control.val(totalsegundos);
                        }
                        break;
                }
            });
        });
    }
</script>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <header id="panelAlert" runat="server" visible="False">
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
                <asp:Repeater runat="server" ID="rptHeaderError">
                    <ItemTemplate>
                        <%# Container.DataItem %>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </header>

        <div class="panel panel-primary">
            <div class="panel-heading">
                Service Level Agreement
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Grupo" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlGrupo" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" Text="Descripcion" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-10">
                            <asp:CheckBox runat="server" Text="Detallado" ID="chkEstimado" AutoPostBack="True" OnCheckedChanged="chkEstimado_OnCheckedChanged"/>
                        </div>
                    </div>
                    
                    <div runat="server" ID="divSimple">
                        <div class="form-group">
                        <asp:Label runat="server" Text="Tiempo Total (horas)" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtTiempoTotal" CssClass="form-control" />
                        </div>
                    </div>
                    </div>

                    <div runat="server" id="divDetalle" Visible="False">
                        <asp:Repeater runat="server" ID="rptSubRoles">
                            <HeaderTemplate>
                                <table class="table table-responsive" id="tblHeader">
                                    <thead>
                                        <tr align="center">
                                            <td><asp:Label runat="server">Sub Rol</asp:Label></td>
                                            <td><asp:Label runat="server">Dias</asp:Label></td>
                                            <td><asp:Label runat="server">Horas</asp:Label></td>
                                            <td><asp:Label runat="server">Minutos</asp:Label></td>
                                            <td><asp:Label runat="server">Segundos</asp:Label></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center" id='<%# Eval("IdSubRol")%>'>
                                    <td style="padding: 0; display: none">
                                        <asp:Label runat="server" Text='<%# Eval("IdSubRol") %>' ID="lblIdSubRol" /></td>
                                    <td><asp:Label runat="server" Text='<%# Eval("SubRol.Descripcion") %>' /></td>
                                    <td><asp:TextBox onkeyup="sum();" runat="server" Text="0" onkeypress="return ValidaCampo(this, 2)" CssClass="form-control" ID="txtDias" /></td>
                                    <td><asp:TextBox onkeyup="sum();" runat="server" Text="0" onkeypress="return ValidaCampo(this, 2)" CssClass="form-control" name="txtHoras" ID="txtHoras" /></td>
                                    <td><asp:TextBox onkeyup="sum();" runat="server" Text="0" onkeypress="return ValidaCampo(this, 2)" CssClass="form-control" name="txtMinutos" ID="txtMinutos" /></td>
                                    <td><asp:TextBox onkeyup="sum();" runat="server" Text="0" onkeypress="return ValidaCampo(this, 2)" CssClass="form-control" name="txtSegundos" ID="txtSegundos" /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                <tfoot>
                                    <tr align="center">
                                        <td style="padding: 0; display: none"></td>
                                        <td> Totales</td>
                                        <td><asp:TextBox Enabled="False" runat="server" Text="0" CssClass="form-control" ID="txtTotalDias" /></td>
                                        <td><asp:TextBox Enabled="False" runat="server" Text="0" CssClass="form-control" name="txtHoras" ID="txtTotalHoras" /></td>
                                        <td><asp:TextBox Enabled="False" runat="server" Text="0" CssClass="form-control" name="txtMinutos" ID="txtTotalMinutos" /></td>
                                        <td><asp:TextBox Enabled="False" runat="server" Text="0" CssClass="form-control" name="txtSegundos" ID="txtTotalSegundos" /></td>
                                    </tr>
                                </tfoot>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Aceptar" ID="btnGuardar" OnClick="btnGuardar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-sm" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
