<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp.Default" %>

<%@ Register Src="~/UserControls/UcLogIn.ascx" TagPrefix="uc1" TagName="UcLogIn" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KiiniNet</title>
    <link href="BootStrap/css/bootstrap.css" rel="stylesheet" />
    <link href="BootStrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="BootStrap/css/CheckBoxStyle.css" rel="stylesheet" />
    <link href="BootStrap/css/Calendar.css" rel="stylesheet" />
    <link href="BootStrap/css/DropDown.css" rel="stylesheet" />
    <link href="BootStrap/css/divs.css" rel="stylesheet" />
    <link href="BootStrap/css/FileInput.css" rel="stylesheet" />
    <link href="BootStrap/css/Headers.css" rel="stylesheet" />
    <link href="BootStrap/css/stylemainmenu.css" rel="stylesheet" />
    <script type="text/javascript">
        function MostrarPopup(modalName) {
            $(modalName).modal({ backdrop: 'static', keyboard: false });
            $(modalName).modal('show');
            return true;
        };

        function CierraPopup(modalName) {
            $(modalName).modal('hide');
            return true;
        };
    </script>
</head>
<body style="background: white;">
    <div id="full">
        <form id="form1" runat="server">

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
            <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
                <Scripts>
                    <asp:ScriptReference Path="~/BootStrap/js/jquery-2.1.1.min.js" />
                    <asp:ScriptReference Path="~/BootStrap/js/bootstrap.min.js" />
                    <asp:ScriptReference Path="~/BootStrap/js/super-panel.js" />
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
            <div id="headerNav">
                <span data-panel="panel1" class="panel-button"></span>
                <div class="logo" id="logo">
                    <div style="float: left; :hover {background: #265B7F}">
                        <div id="headerNavInfoDiv">
                        </div>
                    </div>
                    <div style="float: left; margin-left: -19px">
                        <div class="MenuHorizontal">
                            <ul style="border: none; background: transparent">
                                <li style="border: none"><a>Servicio a clientes</a>
                                    <asp:Repeater runat="server" ID="rptClientes">
                                        <HeaderTemplate>
                                            <ul style="border: none">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li style="border: none;border-bottom: 1px solid #000000">
                                                <asp:Label runat="server" Visible="False" ID="lblId" Text='<%#Eval("Id") %>' />
                                                <asp:LinkButton runat="server" Text='<%#Eval("Descripcion") %>' CommandArgument='<%#Eval("Id") %>' ID="lbtnCteArea" OnClick="lbtnCteArea_OnClick" />
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                </li>

                                <li style="border: none"><a>Servicio a Empleados</a>
                                    <asp:Repeater runat="server" ID="rptEmpleados">
                                        <HeaderTemplate>
                                            <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label runat="server" Visible="False" ID="lblId" Text='<%#Eval("Id") %>' />
                                                <asp:LinkButton runat="server" Text='<%#Eval("Descripcion") %>' CommandArgument='<%#Eval("Id") %>' ID="lbtnEmpleadoArea" OnClick="lbtnEmpleadoArea_OnClick" />
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </li>
                                <li style="border: none"><a>Servicio a Proveedores</a>
                                    <asp:Repeater runat="server" ID="rptProveedores">
                                        <HeaderTemplate>
                                            </ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label runat="server" Visible="False" ID="lblId" Text='<%#Eval("Id") %>' />
                                                <asp:LinkButton runat="server" Text='<%#Eval("Descripcion") %>' CommandArgument='<%#Eval("Id") %>' ID="lbtnProveedorArea" OnClick="lbtnProveedorArea_OnClick" />
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </li>
                                <li style="border: none"><a>Nuestra Institución</a>
                                    <asp:Repeater runat="server" ID="Repeater3">
                                        <HeaderTemplate>
                                            <ul>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label runat="server" Visible="False" ID="lblId" Text='<%#Eval("Id") %>' />
                                                <asp:LinkButton runat="server" Text='<%#Eval("Descripcion") %>' CommandArgument='<%#Eval("Id") %>' />
                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </li>
                                <li style="border: none">
                                    <asp:LinkButton runat="server" ID="lnkConsultaticket" OnClick="lnkConsultaticket_OnClick" Text="Consultar ticket"></asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>

                </div>
                <span id="top-nav">
                    <a class="btn btn-sm btn-success" data-toggle="modal" data-target="#modalSingIn" data-backdrop="static" data-keyboard="false" style="margin-left: 10px; margin-right: 10px">Iniciar Sesión</a>
                </span>
            </div>

            <div id="panel1">
                <div class="MenuVertical">
                    <ul style="color: transparent">
                        <li>
                            <asp:Repeater runat="server" ID="rptMenu" OnItemDataBound="rptMenu_OnItemDataBound">
                                <ItemTemplate>
                                    <li>
                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") ==null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                        <ul>
                                            <asp:Repeater runat="server" ID="rptSubMenu1" OnItemDataBound="rptSubMenu1_OnItemDataBound">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") == null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                                        <ul>
                                                            <asp:Repeater runat="server" ID="rptSubMenu2" OnItemDataBound="rptSubMenu2_OnItemDataBound">
                                                                <ItemTemplate>
                                                                    <li>
                                                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") ==null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                                                        <ul>
                                                                            <asp:Repeater runat="server" ID="rptSubMenu3" OnItemDataBound="rptSubMenu3_OnItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <li>
                                                                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") ==null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                                                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                                                                        <ul>
                                                                                            <asp:Repeater runat="server" ID="rptSubMenu4" OnItemDataBound="rptSubMenu4_OnItemDataBound">
                                                                                                <ItemTemplate>
                                                                                                    <li>
                                                                                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") ==null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                                                                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                                                                                        <ul>
                                                                                                            <asp:Repeater runat="server" ID="rptSubMenu5" OnItemDataBound="rptSubMenu5_OnItemDataBound">
                                                                                                                <ItemTemplate>
                                                                                                                    <li>
                                                                                                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") ==null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                                                                                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                                                                                                        <ul>
                                                                                                                            <asp:Repeater runat="server" ID="rptSubMenu6" OnItemDataBound="rptSubMenu6_OnItemDataBound">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <li>
                                                                                                                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") ==null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                                                                                                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                                                                                                                        <ul>
                                                                                                                                            <asp:Repeater runat="server" ID="rptSubMenu7">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <li>
                                                                                                                                                        <asp:HyperLink NavigateUrl='<%# Eval("Url") %>' Visible='<%# Eval("Menu1") ==null %>' runat="server" Text='<%# Eval("Descripcion") %>' />
                                                                                                                                                        <div runat="server" visible='<%# Eval("Menu1") !=null %>' style="color: white"><%# Eval("Descripcion") %></div>
                                                                                                                                                    </li>
                                                                                                                                                </ItemTemplate>
                                                                                                                                            </asp:Repeater>
                                                                                                                                        </ul>
                                                                                                                                    </li>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:Repeater>
                                                                                                                        </ul>
                                                                                                                    </li>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:Repeater>
                                                                                                        </ul>
                                                                                                    </li>
                                                                                                </ItemTemplate>
                                                                                            </asp:Repeater>
                                                                                        </ul>
                                                                                    </li>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </ul>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ul>
                </div>
            </div>

            <%--LOGIN--%>
            <div class="modal fade" id="modalSingIn" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                <%--<asp:UpdatePanel ID="upModalSingIn" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <uc1:UcLogIn runat="server" ID="UcLogIn" />
                    </div>
                </div>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
        </form>
    </div>
</body>
</html>
