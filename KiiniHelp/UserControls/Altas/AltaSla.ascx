<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaSla.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaSla" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <header id="panelAlert" runat="server" visible="False">
            <div class="alert alert-danger" >
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
                        <asp:Label runat="server" Text="Descripcion" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-10">
                            <asp:CheckBox runat="server" Text="Detallado" ID="chkEstimado" AutoPostBack="True" OnCheckedChanged="chkEstimado_OnCheckedChanged" Visible="False" />
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" Text="Tiempo" CssClass="col-sm-2 form-label" />
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtTiempo" CssClass="form-control"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderTiempo" runat="server"  TargetControlID="txtTiempo" Mask="99:99" MaskType="Time" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button runat="server" CssClass="btn btn-success btn-lg" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger btn-lg" Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
