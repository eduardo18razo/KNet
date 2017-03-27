<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp.Default1" %>

<%@ Register Src="~/UserControls/UcLogCopia.ascx" TagPrefix="uc1" TagName="UcLogCopia" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Kiinisupport</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel='stylesheet' href="assets/css/font.css" />
    <link rel="stylesheet" href="assets/css/font-awesome.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.css" />
    <link rel="stylesheet" href="assets/css/styles.css" />
    <script src="assets/js/jquery.js"></script>

    <script type="text/javascript">

        function DontCloseMenu(event) {
            event.stopPropagation();
        };
    </script>
</head>

<body class="layout_no_leftnav" data-trigger="">
    <form runat="server">
        <!--JS-->
        <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/assets/js/jquery.js" />
                <asp:ScriptReference Path="~/assets/js/bootstrap.js" />
                <asp:ScriptReference Path="assets/js/imagesloaded.js" />
                <asp:ScriptReference Path="assets/js/masonry.js" />
                <asp:ScriptReference Path="assets/js/main.js" />
            </Scripts>
        </asp:ScriptManager>
        <!--INICIA HEADER-->
        <header class="header">
            <div class="branding ">
                <h1 class="logo text-center">
                    <asp:HyperLink NavigateUrl="~/Default.aspx" runat="server"> <asp:Image class="logo-icon" ImageUrl="~/assets/images/logo-icon.svg" alt="icon"  runat="server"/> <span class="nav-label"> <span class="h3"><strong>Bancremex</strong></span></span> </asp:HyperLink>
                </h1>
            </div>
            <div class="topbar bg_w_header">
                <!--INICIA MENU COLAPSABLE-->
                <!--TERMINA MENU COLAPSABLE-->
                <!--INICIA BUSCADOR-->
                <div class="search-container">
                    <div id="main-search">
                        <i id="main-search-toggle" class="fa fa-search icon"></i>
                        <div id="main_search_input_wrapper" class="main_search_input_wrapper">
                            <asp:TextBox type="text" ID="main_search_input" class="main_search_input form-control" placeholder="Buscar por palabra clave..." runat="server" />
                            <span id="clear-search" aria-hidden="true" class="fs1 icon icon_close_alt2 clear-search"></span>
                        </div>
                    </div>
                    <!--TERMINA BUSCADOR-->
                </div>
                <!--INICIA HERRAMIENTAS-->
                <div class="navbar-tools">
                    <div class="utilities-container">
                        <div class="utilities">
                            <!--INICIA TICKET-->
                            <div class="item item-notifications">
                                <div class="dropdown-toggle" data-toggle="dropdown" aria-expanded="true" role="button">
                                    <span class="sr-only">Tickets</span> <span class="pe-icon fa fa-ticket icon" data-toggle="tooltip" data-placement="bottom" title="Tickets"></span>
                                </div>
                                <ul class="dropdown-menu wdropdown-ticket" role="menu" aria-labelledby="dropdownMenu-user">
                                    <li><span class="arrow"></span><a role="menuitem" data-toggle="modal" data-target="#modal-new-ticket">
                                        <span class="pe-icon pe-7s-plus icon"></span>Nuevo ticket
                                    </a></li>
                                    <li>
                                        <asp:HyperLink role="menuitem" NavigateUrl="~/Publico/Consultas/FrmConsultaTicket.aspx" runat="server">
                                            <span class="pe-icon pe-7s-look icon"></span>Consultar ticket
                                        </asp:HyperLink>
                                    </li>
                                </ul>
                            </div>
                            <!--TERMINA TICKET-->

                            <!--INICIA REGISTRATE-->
                            <div class="item item-notifications">
                                <a href="signup.html">
                                    <span class="sr-only">Regístrate</span>
                                    <span class="pe-icon fa fa-book icon" data-toggle="tooltip" data-placement="bottom" title="Regístrate"></span>
                                </a>
                            </div>
                            <!--TERMINA REGISTRATE-->
                            <!--INICIA LOGIN-->
                            <div class="item item-messages dropdown">
                                <div class="dropdown-toggle" id="dropdownMenu-messages" data-toggle="dropdown" aria-expanded="true" role="button">
                                    <span class="sr-only">Ingresa</span> <span class="pe-icon fa fa-sign-in icon" data-toggle="tooltip" data-placement="bottom" title="Ingresa"></span>
                                </div>
                                <div class="dropdown-menu wdropdown-login" role="menu" aria-labelledby="dropdownMenu-messages">
                                    <span class="arrow"></span>
                                    <div>
                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                            <br />
                                            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <uc1:UcLogCopia runat="server" ID="UcLogCopia" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="social-btns col-md-12 col-sm-12 col-xs-12 col-md-offset-1 col-sm-offset-0 col-sm-offset-0">
                                            <div class="divider"><span>O ingresa con</span></div>
                                            <ul class="list-unstyled social-login">
                                                <li>
                                                    <button class="social-btn facebook-btn btn" type="button"><i class="fa fa-facebook"></i>Facebook</button>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--TERMINA LOGIN-->
                        </div>
                    </div>
                    <!--TERMINA HERRAMIENTAS-->

                    <!--INICIA PERFIL-->
                    <div class="user-container dropdown">
                        <div class="dropdown-toggle" id="dropdownMenu-user" data-toggle="dropdown" aria-expanded="true" role="button">
                            <img src="assets/images/profiles/profile-1.png" alt="" />
                            <i class="fa fa-caret-down"></i>
                        </div>
                        <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu-user">
                            <li><span class="arrow"></span><a role="menuitem" href="#"><span class="pe-icon pe-7s-user icon"></span>Mi perfil</a></li>
                            <li><a role="menuitem" href="signup.html"><span class="pe-icon pe-7s-paper-plane icon"></span>Registrarse</a></li>
                            <li><a role="menuitem" href="login.html"><span class="fa fa-sign-in icon"></span>Ingresar</a></li>
                            <li>
                                <asp:HyperLink role="menuitem" NavigateUrl="~/Default.aspx" runat="server"><span class="fa fa-sign-out icon"></span>Salir</asp:HyperLink></li>
                        </ul>
                    </div>
                    <!--TERMINA PERFIL-->
                </div>
            </div>
        </header>
        <!--TERMINA HEADER-->
        <div id="content-wrapper" class="content-wrapper">
            <div class="container-fluid">
                <div class="row">
                    <br />
                    <br />
                    <br />
                    <!--INICIA CARRUSEL-->
                    <div class="module-content collapse in" id="content-1">
                        <div class="module-content-inner no-padding-bottom">
                            <div class="carousel slide" data-ride="carousel" id="carouselPresenter">
                                <!-- Indicators -->
                                <ol class="carousel-indicators">
                                    <li data-target="#carouselPresenter" data-slide-to="0" class="active"></li>
                                    <li data-target="#carouselPresenter" data-slide-to="1"></li>
                                    <li data-target="#carouselPresenter" data-slide-to="2"></li>
                                </ol>
                                <!-- Wrapper for slides -->
                                <div class="carousel-inner">
                                    <div class="item active">
                                        <img src="assets/images/carousels/slide-1.jpg" alt="" />
                                    </div>
                                    <div class="item">
                                        <img src="assets/images/carousels/slide-2.jpg" alt="" />
                                    </div>
                                    <div class="item">
                                        <img src="assets/images/carousels/slide-3.jpg" alt="" />
                                    </div>
                                </div>
                                <!-- Controls -->
                                <a class="left carousel-control" data-slide="prev" href="#carouselPresenter">
                                    <span class="glyphicon glyphicon-chevron-left"></span><span class="sr-only">Previous</span>
                                </a><a class="right carousel-control" data-slide="next" href="#carouselPresenter"><span class="glyphicon glyphicon-chevron-right"></span><span class="sr-only">Next</span></a>
                            </div>
                        </div>
                    </div>
                    <!--TERMINA CARRUSEL-->
                </div>
                <br />
                <!--INICIA TITULO-->
                <h2 class="title text-left">Para ofrecerte un mejor servicio indicanos que tipo de usuario eres.</h2>
                <hr />
                <!--TERMINA TITULO-->
                <hr />
                <!--INICIA USUARIOS-->
                <div id="masonry" class="row">
                    <div class=" col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <section class="module ">
                            <div class="module-inner">
                                <div class="module-content collapse in" id="content-2">
                                    <div class="module-content-inner">
                                        <asp:LinkButton runat="server" ID="lnkBtnEmpleado" OnClick="lnkBtnEmpleado_OnClick">
                                            <img class="img-responsive margin-left" src="assets/images/users_icon/usuario_2.jpg" alt="Iconos de usuarios" /></asp:LinkButton>
                                        <h4 style="text-align: center">EMPLEADO</h4>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class=" col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <section class="module ">
                            <div class="module-inner">
                                <div class="module-content collapse in" id="content-3">
                                    <div class="module-content-inner">
                                        <asp:LinkButton runat="server" ID="lnkBtnCliente" OnClick="lnkBtnCliente_OnClick">
                                            <img class="img-responsive  margin-left" src="assets/images/users_icon/usuario_1.jpg" alt="Iconos de usuarios" /></asp:LinkButton>
                                        <h4 style="text-align: center">CLIENTE</h4>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <section class="module">
                            <div class="module-inner">
                                <div class="module-content collapse in" id="content-4">
                                    <div class="module-content-inner">
                                        <asp:LinkButton runat="server" ID="lnkBtnProveedor" OnClick="lnkBtnProveedor_OnClick">
                                            <img class="img-responsive  margin-left" src="assets/images/users_icon/usuario_3.jpg" alt="Iconos de usuarios" /></asp:LinkButton>
                                        <h4 style="text-align: center">PROVEEDOR</h4>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
                <!--TERMINA USUARIOS-->
                <hr>
                <!--INICIA BUSCADOR-->
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <section class="module">
                            <div class="module-inner">
                                <div class="module-content">
                                    <div class="module-content-inner">
                                        <div class="help-section">
                                            <div>
                                                <h3 class="text-center title">¿Podemos ayudarte?</h3>
                                                <div role="form" class="search-box form-inline text-center">
                                                    <label class="sr-only" for="help_search_form">Buscar</label>
                                                    <div class="form-group">
                                                        <asp:TextBox ID="help_search_form" name="search-form" type="text" class="form-control help-search-form" placeholder="Busca con una palabra clave..." runat="server"></asp:TextBox>
                                                        <asp:LinkButton type="submit" CssClass="btn btn-primary btn-single-icon" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="row text-center"></div>
                                                <div class="row text-center"></div>
                                                <div class="row text-center"></div>
                                            </div>
                                            <div class="help-lead text-center">
                                                <h4 class="subtitle">¿Aún necesitas ayuda?</h4>
                                                <a class="btn btn-primary" data-toggle="modal" data-target="#modal-new-ticket"><i class="fa fa-play-circle"></i>Generar un ticket </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
            <!--TERMINA BUSCADOR-->
        </div>

        <!--INICIA MODAL NUEVO TICKET-->
        <div class="modal fade" id="modal-new-ticket" tabindex="-1" role="dialog" aria-labelledby="modal-new-ticket-label">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="modal-new-ticket-label">
                            <img img-responsive src="assets/images/icons/new_ticket.png" alt="" /><br />
                            Crear Ticket Nuevo</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            <div class="form-group">
                                <label class="col-sm-12 control-label">¿Cuál es tu solicitud?</label>
                                <div class="col-sm-8">
                                    <label class="radio-inline">
                                        <asp:RadioButton name="inlineRadioOptions" value="option1" runat="server" Text="" />
                                    </label>
                                    <label class="radio-inline">
                                        <asp:RadioButton name="inlineRadioOptions" value="option2" runat="server" Text="Problema" />

                                    </label>
                                </div>
                            </div>
                            <br />
                            <br />
                            <hr />
                            <div class="form-group">
                                <label class="col-sm-12 control-label">Área de atención</label>
                                <div class="col-sm-10">
                                    <select class="form-control">
                                        <option>Ninguna</option>
                                        <option>Administración</option>
                                        <option>Servicio al Cliente</option>
                                        <option>Cuentas Especiales</option>
                                        <option>Gerencia</option>
                                    </select>
                                </div>
                            </div>
                            <br />
                            <br />
                            <br />
                            <hr />
                            <div class="form-group">
                                <label class="sr-only">Ticket</label>
                                <input type="text" class="form-control" placeholder="Nombre" />
                            </div>
                            <div class="form-group">
                                <label class="sr-only">Ticket</label>
                                <input type="text" class="form-control" placeholder="Correo electrónico" />
                            </div>
                            <div class="form-group">
                                <label class="sr-only">Ticket</label>
                                <input type="text" class="form-control" placeholder="Asunto" />
                            </div>
                            <div class="form-group">
                                <label class="sr-only">Description</label>
                                <textarea class="form-control" rows="2" placeholder="Escribe un comentario..."></textarea>
                            </div>
                            <hr>
                            <div class="form-group">
                                <label for="exampleInputFile">Agregar un archivo</label>
                                <input type="file" id="exampleInputFile" />
                            </div>
                            <hr>
                            <div class="checkbox remember">
                                <label>
                                    <input type="checkbox" />
                                    No soy un Robot
                                </label>
                            </div>
                            <button type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#myModal" data-dismiss="modal">Crear ticket </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--TERMINA MODAL NUEVO TICKET-->
        <!--INICIA MODAL RESPUESTA NUEVO TICKET-->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title" id="myModalLabel">
                            <img img-responsive margin-left src="assets/images/icons/ok.png" alt="" /><br>
                            Tu ticket se creo con éxito</h3>
                    </div>
                    <div class="modal-body">
                        <hr />
                        <p class="h4">
                            <strong>Tu no. de ticket: 01</strong><br>
                        </p>
                        <p class="h4"><strong>Clave de registro: 234D45</strong></p>
                        <hr />
                        En breve recibirás un correo con los datos de tu ticket para que puedas dar seguimiento.
                    </div>
                    <div class="modal-footer"></div>
                </div>
            </div>
        </div>
        <!--INICIA MODAL RESPUESTA NUEVO TICKET-->
        <!--TERMINA MODALES-->
        <!--INICIA FOOTER-->
        <footer id="footer" class="site-footer">
            <div class="copyright">Kiinisuppor &copy; 2017 - <a href="http://www.kiininet.com" target="_blank">Powered by Kiinenet</a></div>
        </footer>
        <!--TERMINA FOOTER-->
        <%--<script type="text/javascript">
            //$("#frmdefault").submit(function (event) {
            //    event.preventDefault();

            //});
            $('.dropdown-menu').click(function (e) {
                e.stopPropagation();
            });
            $('.dropdownMenu-messages').click(function (e) {
                e.stopPropagation();
            });
            function OpenDropLogin() {

                $('.wdropdown-login').toggle();

            };
        </script>--%>
    </form>
</body>
</html>
