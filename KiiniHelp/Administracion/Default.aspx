<%@ Page Title="" Language="C#" MasterPageFile="~/Administracion/Administracion.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp.Administracion.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentAdministracion" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentAdministracion" runat="server">
    <asp:UpdatePanel ID="upAreas" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-header">
                </div>
                <div class="panel-body">
                    <asp:Repeater runat="server" ID="rptAreas">
                        <ItemTemplate>
                            <asp:Button ID="btnArea" runat="server" CssClass="btn btn-lg btn-primary" Text='<%# Eval("Descripcion") %>' CommandArgument='<%# Eval("Id") %>' OnClick="btnArea_OnClick" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="panel-footer">
                    <asp:Button runat="server" CssClass="btn btn-lg btn-danger" ID="btnCerraGrupos" Text="Cerrar" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
