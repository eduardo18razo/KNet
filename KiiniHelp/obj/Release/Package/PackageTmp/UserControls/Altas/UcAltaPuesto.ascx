<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaPuesto.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.UcAltaPuesto" %>
<asp:UpdatePanel runat="server" ID="updateAltaAreas">
    <ContentTemplate>
        <header id="panelAlerta" runat="server" visible="false">
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
        <div class="panel panel-primary">
            <div class="panel-heading">
                Agregar Puesto
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <asp:HiddenField runat="server" ID="hfEsAlta" />
                    <asp:HiddenField runat="server" ID="hfIdPuesto" />
                    <div class="form-group" runat="server" Visible="False">
                        <asp:Label runat="server" Text="Tipo Usuario" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="DropSelect" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Descripción" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" ID="txtDescripcionPuesto" CssClass="form-control obligatorio" onkeydown="return (event.keyCode!=13);" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button runat="server" CssClass="btn btn-success" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger" Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
