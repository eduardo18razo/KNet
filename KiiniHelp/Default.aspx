<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="BootStrap/css/super-panel.css" rel="stylesheet" />
    <link href="BootStrap/css/accordion-menu.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="accordion">
        <ul>
            <li>
                <div>Consultas</div>
                <ul>
                    <li>
                        <div>Nivel1</div>
                        <ul>
                            <li>
                                <div>Nivel2</div>
                                <ul>
                                    <li>
                                        <div>Nivel3</div>
                                        <ul>
                                            <li>
                                                <div>Nivel4</div>
                                                <ul>
                                                    <li>
                                                        <div>Nivel5</div>
                                                        <ul>
                                                            <li>
                                                                <div>Nivel6</div>
                                                                <ul>
                                                                    <li>
                                                                        <a href="www.google.com">Google</a>
                                                                    </li>
                                                                </ul>
                                                            </li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </li>
                </ul>
            </li>
        </ul>
    </div>

</asp:Content>
