<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Identificar.aspx.cs" Inherits="KiiniHelp.Identificar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="BootStrap/css/bootstrap.css" rel="stylesheet" />
    <link href="BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="BootStrap/css/CheckBoxStyle.css" rel="stylesheet" />
    <link href="BootStrap/css/Calendar.css" rel="stylesheet" />
    <link href="BootStrap/css/DropDown.css" rel="stylesheet" />
    <link href="BootStrap/css/divs.css" rel="stylesheet" />
    <link href="BootStrap/css/FileInput.css" rel="stylesheet" />
    <link href="BootStrap/css/Headers.css" rel="stylesheet" />
    <link href="BootStrap/css/stylemainmenu.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <header class="" id="panelAlertaGeneral" runat="server" visible="False">
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
                <div class="panel panel-primary" style="width: 750px; margin: 0 auto">
                    <div class="panel-heading">
                        Recuperar cuenta
                    </div>

                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Correo electrónico, teléfono, nombre de usuario" CssClass="col-sm-4" />
                                <div class="col-sm-5">
                                    <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" Style="text-transform: none" />
                                </div>
                                <asp:Button runat="server" ID="btnBuscar" Text="Buscar" CssClass="btn btn-success" OnClick="btnBuscar_OnClick" />
                            </div>
                            <div class="form-horizontal">
                                <asp:RadioButtonList runat="server" ID="rbtnLstUsuarios" OnSelectedIndexChanged="rbtnLstUsuarios_OnSelectedIndexChanged" AutoPostBack="True" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer">
                        <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger" OnClick="btnCancelar_OnClick" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
