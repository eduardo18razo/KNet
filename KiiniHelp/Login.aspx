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
    <style type="text/css">
        #inferior {
            color: #FFF;
            background: transparent;
            position: absolute; /*El div será ubicado con relación a la pantalla*/
            left: 0px; /*A la derecha deje un espacio de 0px*/
            right: 0px; /*A la izquierda deje un espacio de 0px*/
            bottom: 0px; /*Abajo deje un espacio de 0px*/
            height: 50px; /*alto del div*/
            z-index: 0;
            height: 150px;
            padding-right: 0px;
        }
    </style>
</head>
<body style="background: url('images/backgroud.jpg'); background-size: 100% auto;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 1.0;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 25%; left: 35%; border: 10px;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="inferior">
            <img src="Images/logoKiininet.jpg" height="150" style="float: right; opacity: .7" />
        </div>
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
                <div class="panel panel-primary" style="width: 550px; position: fixed; top: 50%; left: 50%; margin-top: -200px; height: 300px; margin-left: -300px;">
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
                            <a class="btn btn-danger" href="Default.aspx">Cancelar</a>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>
</html>
