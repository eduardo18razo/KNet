<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcDetalleGrupoUsuario.ascx.cs" Inherits="KiiniHelp.UserControls.Detalles.UcDetalleGrupoUsuario" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <asp:Repeater runat="server" ID="rptRoles" OnItemDataBound="rptRoles_OnItemDataBound">
            <ItemTemplate>
                <div class="row col-lg-12 col-md-12">
                    <div runat="server" id="divRolesGrupos">
                        <span><%# Eval("DescripcionRol") %></span>
                        <div class="row col-lg-12 col-md-12">
                            <asp:Repeater runat="server" ID="rptGrupos">
                                <ItemTemplate>
                                    <div class="row col-lg-4 col-md-4" style="padding: 5px">
                                        <span class="tag label label-info">
                                            <div class="row col-lg-4 col-md-4" style="padding: 5px">
                                                <span><%# Eval("DescripcionGrupo") %></span>
                                                <asp:Repeater runat="server" ID="rptSubGrupos">
                                                    <HeaderTemplate>
                                                        <br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("Descripcion") %>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </span>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <hr />
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%--<div class="panel panel-primary">
            <div class="panel-body">
                <asp:Repeater runat="server" ID="rptUserGroups" OnItemDataBound="rptUserGroups_OnItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <asp:Label runat="server" Text='<%#Eval("GrupoUsuario.TipoGrupo.Descripcion") + " - " + Eval("GrupoUsuario.Descripcion") %>' CssClass="form-control"></asp:Label>
                            <div class="panel panel-primary" style="margin-left: 20px" runat="server" visible='<%# Eval("SubGrupoUsuario") != null %>'>
                                <div class="panel-heading">
                                    SubRol
                                </div>
                                <div class="panel-body">
                                    <asp:Label runat="server" Text='<%# Eval("SubGrupoUsuario") != null ? Eval("SubGrupoUsuario.SubRol.Descripcion") : "" %>'></asp:Label>
                                    <asp:Label runat="server" Text='<%# Eval("SubGrupoUsuario") != null ? Eval("SubGrupoUsuario.SubRol.Descripcion") : "" %>'></asp:Label>
                                </div>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>--%>
    </ContentTemplate>
</asp:UpdatePanel>
