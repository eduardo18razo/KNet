<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaGrupoUsuario.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaGrupoUsuario" %>
<asp:UpdatePanel ID="upGrupoUsuario" runat="server">
    <ContentTemplate>
        <header class="alert alert-danger" id="panelAlerta" runat="server" visible="false">
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
        </header>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <asp:Label runat="server" Text="Alta Grupo de Usuario" ID="lblTitle"></asp:Label>
            </div>
            <div class="panel-body">
                <div>
                    <asp:HiddenField runat="server" ID="hfIdGrupo" />
                    <asp:HiddenField runat="server" ID="hfIdTipoUsuario" />
                    <asp:HiddenField runat="server" ID="hfIdTipoSubGrupo" />
                    <div>
                        <div class="form-group">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Descripcion</label>
                                <asp:TextBox runat="server" ID="txtDescripcionGrupoUsuario" placeholder="DESCRIPCION" class="form-control" onkeydown = "return (event.keyCode!=13);"/>
                            </div>
                            <div class="form-group">
                                <asp:CheckBox runat="server" ID="chkHabilitado" Checked="True" Visible="False" />
                            </div> 
                            <div class="panel-body" runat="server" ID="divSubRoles" Visible="False">
                                <asp:HiddenField runat="server" ID="hfOperacion" />
                                <div>
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:CheckBoxList runat="server" ID="chklbxSubRoles" Checked="True" Visible="True" OnSelectedIndexChanged="chklbxSubRoles_OnSelectedIndexChanged" AutoPostBack="True"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardar_OnClick" />
                <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-danger" Text="Limpiar" OnClick="btnLimpiar_OnClick" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
