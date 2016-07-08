<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcConsultaUsuarios.ascx.cs" Inherits="KiiniHelp.UserControls.Consultas.UcConsultaUsuarios" %>
<%@ Register TagPrefix="uc1" TagName="UcDetalleUsuario" Src="~/UserControls/Detalles/UcDetalleUsuario.ascx" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <asp:Label runat="server" Text="NOMBRE COMPLETO" CssClass="col-sm-5 " />
                    <asp:Label runat="server" Text="TIPO DE USUARIO" CssClass="col-sm-2 " />
                    <asp:Label runat="server" Text="DIRECTORIO ACTIVO" CssClass="col-sm-2 " />
                    <asp:Label runat="server" Text="ACTIVO" CssClass="col-sm-1 " />
                </div>
            </div>
            <div class="panel-body">
                <asp:Repeater runat="server" ID="rptUsuarios">
                    <ItemTemplate>
                        <div class="row">
                            <asp:LinkButton runat="server" CssClass="col-sm-5" Text='<%#Eval("NombreCompleto") %>' ID="btnUsuario" OnClick="btnUsuario_OnClick" CommandArgument='<%#Eval("Id") %>' />
                            <asp:Label runat="server" Text='<%# Eval("TipoUsuario.Descripcion") %>' CssClass="col-sm-2" />
                            <asp:Label runat="server" Text='<%# (bool) Eval("DirectorioActivo") ? "SI" : "NO" %>' CssClass="col-sm-2" />
                            <asp:Label runat="server" Text='<%# (bool) Eval("Habilitado") ? "ACTIVO" : "INACTIVO" %>' CssClass="col-sm-2" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div class="modal fade" id="modalDetalleUsuario" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <uc1:UcDetalleUsuario runat="server" ID="UcDetalleUsuario1" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
