<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcDetalleGrupoUsuario.ascx.cs" Inherits="KiiniHelp.UserControls.Detalles.UcDetalleGrupoUsuario" %>
<%@ Import Namespace="KiiniNet.Entities.Cat.Usuario" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="panel panel-primary">
            <div >
                <asp:Repeater runat="server" ID="rptUserGroups" OnItemDataBound="rptUserGroups_OnItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <asp:Label runat="server" Text='<%#Eval("GrupoUsuario.Descripcion") %>' CssClass="form-control"></asp:Label>
                            <div class="panel panel-primary" style="margin-left: 20px" runat="server" visible='<%# Eval("SubGrupoUsuario") != null %>'>
                                <div class="panel-heading">
                                    SubRol
                                </div>
                                <div class="panel-body">
                                    <asp:Label runat="server" Text='<%# Eval("SubGrupoUsuario") != null ? Eval("SubGrupoUsuario.SubRol.Descripcion") : "" %>'></asp:Label>
                                </div>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
