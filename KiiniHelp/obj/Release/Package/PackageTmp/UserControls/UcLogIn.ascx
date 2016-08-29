<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcLogIn.ascx.cs" Inherits="KiiniHelp.UserControls.UcLogIn" %>
<%--<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
        <header class="" id="pnlAlerta" runat="server" visible="False">
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
                        <%# Eval("Detalle")  %>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </header>
        <div class="panel panel-primary">
            <div class="panel-heading text-primary text-center">
                <h3>Login panel</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="txtUsuario">Usuario</label>
                        <div class="col-sm-10">
                            <asp:TextBox class="form-control" ID="txtUsuario" placeholder="UserName" runat="server" Style="text-transform: none"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" for="txtpwd">Contraseña</label>
                        <div class="col-sm-10">
                            <asp:TextBox class="form-control" ID="txtpwd" placeholder="Enter Password" runat="server" TextMode="Password" Style="text-transform: none"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-2 control-label"></div>
                        <div class="col-sm-10">
                            <asp:LinkButton class="btn btn-primary" ID="lnkBtnRecuperar" runat="server" Text="Recuperar contraseña" OnClick="lnkBtnRecuperar_OnClick"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:Label CssClass="label label-danger" ID="lblmsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <div class="panel-footer" style="text-align: center">
                    <asp:Button CssClass="btn btn-success" ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Acceder"></asp:Button>
                    <asp:Button CssClass="btn btn-danger" ID="btnCacelar" OnClick="btnCacelar_OnClick" runat="server" Text="Cancelar"></asp:Button>
                </div>
            </div>
        </div>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>
