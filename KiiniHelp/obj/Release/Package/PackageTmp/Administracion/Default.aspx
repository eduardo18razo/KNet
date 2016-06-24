<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp.Administracion.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:UpdatePanel ID="upAreas" runat="server">
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
            <div class="panel panel-primary" runat="server" ID="divAreas">
                <div class="panel-heading">
                    <h4>Menu de servicios</h4>
                </div>
                <div class="panel-body">
                    <asp:Repeater runat="server" ID="rptAreas" OnItemDataBound="rptAreas_OnItemDataBound">
                        <ItemTemplate>
                            <asp:Button ID="btnArea" runat="server" CssClass="btn btn-lg btn-primary" Text='<%# Eval("Descripcion") %>' CommandArgument='<%# Eval("Id") %>' OnClick="btnArea_OnClick" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
