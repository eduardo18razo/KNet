<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KiiniHelp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acceso</title>
    <link href="BootStrap/css/bootstrap.css" rel="stylesheet" />
    <link href="BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="BootStrap/css/CheckBoxStyle.css" rel="stylesheet" />
    <link href="BootStrap/css/Calendar.css" rel="stylesheet" />
    <link href="BootStrap/css/DropDown.css" rel="stylesheet" />
    <link href="BootStrap/css/divs.css" rel="stylesheet" />
    <link href="BootStrap/css/FileInput.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
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
                <div class="panel panel-primary" style="width: 550px; position: fixed; top: 50%; left: 50%; margin-top: -200px; height: 300px; margin-left: -300px">
                    <div class="panel-heading text-primary text-center">
                        <h3>Login panel</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtUsuario">Usuario</label>
                                <div class="col-sm-10">
                                    <asp:TextBox class="form-control" ID="txtUsuario" placeholder="UserName" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtpwd">Contraseña</label>
                                <div class="col-sm-10">
                                    <asp:TextBox class="form-control" ID="txtpwd" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>
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
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>
</html>
