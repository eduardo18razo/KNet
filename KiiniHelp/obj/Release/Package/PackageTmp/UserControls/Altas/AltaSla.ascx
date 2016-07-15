<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaSla.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaSla" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script>
    function SumDays(tipo) {
        debugger;
        var total = document.getElementById("<%= this.FindControl("txtTiempo").ClientID %>");
        var tiempoDias = 0;
        var tiempoHoras = 0;
        var tiempoMinutos = 0;
        var tiempoSegundos = 0;
        $("#tblHeader tbody tr").each(function (index) {
            $(this).children("td").each(function (index2) {
                var element;
                switch (index2) {
                    case 0:
                    case 1:
                    case 2:
                        element = $(this).find("input[id*=txtDias]");
                        if (element != null)
                            tiempoDias += element.val();
                    case 3:
                        element = $(this).find("input[id*=txtDias]");
                        if (element != null)
                            tiempoDias += element.val();
                    case 4:
                        element = $(this).find("input[id*=txtDias]");
                        if (element != null)
                            tiempoDias += element.val();
                    case 5:
                        element = $(this).find("input[id*=txtDias]");
                        if (element != null)
                            tiempoDias += element.val();
                }
            });
        });
        $("#txtTiempo").val(tiempoDias);
    };
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
                            <asp:DropDownList runat="server" CssClass="DropSelect" ID="ddlGrupo" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" />
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
                            <asp:CheckBox runat="server" Text="Detallado" ID="chkEstimado" AutoPostBack="True" OnCheckedChanged="chkEstimado_OnCheckedChanged" />
                        </div>
                    </div>

                    <div runat="server" id="divDetalle" class="form-group col-sm-12">
                        <asp:Repeater runat="server" ID="rptSubRoles">
                            <HeaderTemplate>
                                <table class="table table-responsive" id="tblHeader">
                                    <thead>
                                        <tr align="center">
                                            <td>
                                                <asp:Label runat="server">Sub Rol</asp:Label></td>
                                            <td>
                                                <asp:Label runat="server">Dias</asp:Label></td>
                                            <td>
                                                <asp:Label runat="server">Horas</asp:Label></td>
                                            <td>
                                                <asp:Label runat="server">Minutos</asp:Label></td>
                                            <td>
                                                <asp:Label runat="server">Segundos</asp:Label></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center" id='<%# Eval("Id")%>'>
                                    <td style="display: none">
                                        <asp:Label runat="server" Text='<%#Eval("SubRol.Id") %>' Visible="False" ID="lblIdSubRol" /></td>
                                    <td>
                                        <asp:Label runat="server" Text='<%#Eval("SubRol.Descripcion") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" onkeypress="SumDays(1);" CssClass="form-control" ID="txtDias" /></td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtHoras" name="txtHoras" /></td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMinutos" /></td>
                                    <td>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSegundos" /></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                <%--<tfoot>
                                 <tr align="center">
                                     <td>
                                        <asp:Label runat="server" Text="Total" /></td>
                                    <td><asp:TextBox runat="server" CssClass="form-control" Enabled="False" ID="txtFotDias" /></td>
                                    <td><asp:TextBox runat="server" CssClass="form-control" Enabled="False" ID="txtFotHoras" /></td>
                                    <td><asp:TextBox runat="server" CssClass="form-control" Enabled="False" ID="txtFotMinutos" /></td>
                                    <td><asp:TextBox runat="server" CssClass="form-control" Enabled="False" ID="txtFotSegundos" /></td>
                                 </tr>
                                </tfoot>--%>
                            </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" Text="Tiempo horas" CssClass="col-sm-2 form-label" />
                        <div class="col-sm-2">
                            <asp:TextBox runat="server" ID="txtTiempo" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderTiempo" runat="server" TargetControlID="txtTiempo" Mask="99:99" MaskType="Time" InputDirection="LeftToRight" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button runat="server" CssClass="btn btn-success btn-lg" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-lg" Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-lg" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
