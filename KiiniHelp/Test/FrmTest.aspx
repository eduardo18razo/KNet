<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.Test.FrmTest" %>

<%@ Register Src="~/UserControls/Altas/ArbolesAcceso/UcAltaConsulta.ascx" TagPrefix="uc1" TagName="UcAltaConsulta" %>
<%@ Register Src="~/UserControls/Altas/ArbolesAcceso/UcAltaServicio.ascx" TagPrefix="uc1" TagName="UcAltaServicio" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../assets/tmp/jquery.sumoselect.min.js"></script>

    <link rel='stylesheet' href="../assets/css/font.css" />
    <link rel="stylesheet" href="../assets/css/font-awesome.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.css" />
    <link rel="stylesheet" href="../assets/css/styles.css" />
    <link rel="stylesheet" href="../assets/css/menuStyle.css" />
    <link rel="stylesheet" href="../assets/css/divs.css" />
    <link rel="stylesheet" href="../assets/tmp/sumoselect.css" />
    <link href="../assets/css/checkBox.css" rel="stylesheet" />
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $(<%=lstBoxTest.ClientID%>).SumoSelect({ placeholder: 'SELECCIONE', selectAll: true, csvDispCount: 1 });
        });
    </script>--%>
    <script type="text/javascript">
        function sum() {
            debugger;
            var totaldias = 0, totalhoras = 0, totalminutos = 0, totalsegundos = 0;
            $("#tblHeader > tbody > tr").each(function(indexRow) {
                var control;
                $(this).children("td").each(function(indexColumn) {
                    switch (indexColumn) {
                    case 2:
                        control = $(this).find("input[id*=txtDias]");
                        if (control != null) {
                            totaldias = totaldias + parseInt(control.val() === "" || control.val() === undefined ? 0 : control.val());
                        }
                        break;
                    case 3:
                        control = $(this).find("input[id*=txtHoras]");
                        if (control != null) {
                            totalhoras = totalhoras + parseInt(control.val() === "" || control.val() === undefined ? 0 : control.val());
                        }
                        break;
                    case 4:
                        control = $(this).find("input[id*=txtMinutos]");
                        if (control != null) {
                            totalminutos = totalminutos + parseInt(control.val() === "" || control.val() === undefined ? 0 : control.val());
                        }
                        break;
                    case 5:
                        control = $(this).find("input[id*=txtSegundos]");
                        if (control != null) {
                            totalsegundos = totalsegundos + parseInt(control.val() === "" || control.val() === undefined ? 0 : control.val());
                        }
                        break;
                    }

                });
            });
            $("#tblHeader > tfoot > tr").each(function (indexRow) {
                var control;
                $(this).children("td").each(function (indexColumn) {
                    switch (indexColumn) {
                        case 2:
                            control = $(this).find("input[id*=txtTotalDias]");
                            if (control != null) {
                                control.val(totaldias);
                            }
                            break;
                        case 3:
                            control = $(this).find("input[id*=txtTotalHoras]");
                            if (control != null) {
                                control.val(totalhoras);
                            }
                            break;
                        case 4:
                            control = $(this).find("input[id*=txtTotalMinutos]");
                            if (control != null) {
                                control.val(totalminutos);
                            }
                            break;
                        case 5:
                            control = $(this).find("input[id*=txtTotalSegundos]");
                            if (control != null) {
                                control.val(totalsegundos);
                            }
                            break;
                    }
                });
            });
        }

        function UploadComplete(sender, args) {
                __doPostBack('Refresh', '');
            }
            function MostrarPopup(modalName) {
                $(modalName).modal({ backdrop: 'static', keyboard: false });
                $(modalName).modal({ show: true });
                return true;
            };
            function CierraPopup(modalName) {
                $(modalName).modal('hide');
                return true;
            };
            function HightSearch(content, serachText) {
                var src_str = $("#" + content).html();
                var term = serachText;
                term = term.replace(/(\s+)/, "(<[^>]+>)*$1(<[^>]+>)*");
                var pattern = new RegExp("(" + term + ")", "gi");

                //src_str = src_str.replace(pattern, '<span style="background-color:Yellow" >' + term + '</span>');
                src_str = src_str.replace(pattern, "<mark>$1</mark>");
                src_str = src_str.replace(/(<mark>[^<>]*)((<[^>]+>)+)([^<>]*<\/mark>)/, "$1</mark>$2<mark>$4");

                $("#" + content).html(src_str);
            }

            var d;
            function drag(objSource) {
                this.select = objSource;
            }

            function dragPrototypeDrop(objDest) {
                if (!this.dragStart) return;
                this.dest = objDest;

                var o = this.option.cloneNode(true);
                this.dest.appendChild(o);
                this.select.removeChild(this.option);
            }

            function dragPrototypeSetIndex() {
                var i = this.select.selectedIndex;

                //i returns -1 if no option is "truly" selected
                window.status = "selectedIndex = " + i;
                if (i == -1) return;

                this.option = this.select.options[i];
                this.dragStart = true;
            }
            $(function () {
                var isMouseDown = false,
                    isHighlighted;
                $("#our_table tbody td")
                    .mousedown(function () {
                        isMouseDown = true;
                        $(this).toggleClass("highlighted");
                        isHighlighted = $(this).hasClass("highlighted");
                        return false; // prevent text selection
                    })
                    .mouseover(function () {
                        if (isMouseDown) {
                            $(this).toggleClass("highlighted", isHighlighted);
                        }
                    })
                    .bind("selectstart", function () {
                        return false;
                    })

                $(document)
                    .mouseup(function () {
                        isMouseDown = false;
                    });
            });

            function getSelectedHora() {
                var lunes = [];
                var martes = [];
                var miercoles = [];
                var jueves = [];
                var viernes = [];
                var sabado = [];
                var domingo = [];
                $("#our_table tbody td.highlighted").each(function () {
                    if ($(this).hasClass('highlighted')) {
                        var id = $(this).attr("id");
                        var dia = id.substring(0, 3);
                        var hora = id.substring(3);
                        switch (dia) {
                            case "lun":
                                lunes.push(parseInt(hora));
                                break;
                            case "mar":
                                martes.push(parseInt(hora));
                                break;
                            case "mie":
                                miercoles.push(parseInt(hora));
                                break;
                            case "jue":
                                jueves.push(parseInt(hora));
                                break;
                            case "vie":
                                viernes.push(parseInt(hora));
                                break;
                            case "sab":
                                sabado.push(parseInt(hora));
                                break;
                            case "dom":
                                domingo.push(parseInt(hora));
                                break;
                            default:
                        }

                    }
                });
                lunes.sort(function (a, b) { return a - b });
                martes.sort(function (a, b) { return a - b });
                miercoles.sort(function (a, b) { return a - b });
                jueves.sort(function (a, b) { return a - b });
                viernes.sort(function (a, b) { return a - b });
                sabado.sort(function (a, b) { return a - b });
                domingo.sort(function (a, b) { return a - b });
                if (lunes.length > 0)
                    alert("dia: Lunes" + "\nHora minima: " + lunes[0] + "\nHoraMaxima: " + (parseInt(lunes[lunes.length - 1]) + 1));
                if (martes.length > 0)
                    alert("dia: Martes" + "\nHora minima: " + martes[0] + "\nHoraMaxima: " + (parseInt(martes[martes.length - 1]) + 1));
                if (miercoles.length > 0)
                    alert("dia: Miercoles" + "\nHora minima: " + miercoles[0] + "\nHoraMaxima: " + (parseInt(miercoles[miercoles.length - 1]) + 1));
                if (jueves.length > 0)
                    alert("dia: Jueves" + "\nHora minima: " + jueves[0] + "\nHoraMaxima: " + (parseInt(jueves[jueves.length - 1]) + 1));
                if (viernes.length > 0)
                    alert("dia: Viernes" + "\nHora minima: " + viernes[0] + "\nHoraMaxima: " + (parseInt(viernes[viernes.length - 1]) + 1));
                if (sabado.length > 0)
                    alert("dia: Sabado" + "\nHora minima: " + sabado[0] + "\nHoraMaxima: " + (parseInt(sabado[sabado.length - 1]) + 1));
                if (domingo.length > 0)
                    alert("dia: Domingo" + "\nHora minima: " + domingo[0] + "\nHoraMaxima: " + (parseInt(domingo[domingo.length - 1]) + 1));
            }
            function SuccsessAlert(title, msg) {
                $.notify({
                    // options
                    icon: 'glyphicon glyphicon-ok',
                    title: title,
                    message: msg,
                    target: '_blank'
                }, {
                    // settings
                    element: 'body',
                    position: null,
                    type: "success",
                    allow_dismiss: true,
                    newest_on_top: false,
                    showProgressbar: false,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    offset: 20,
                    spacing: 10,
                    z_index: 1031,
                    delay: 5000,
                    timer: 1000,
                    url_target: '_blank',
                    mouse_over: null,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    onShow: null,
                    onShown: null,
                    onClose: null,
                    onClosed: null,
                    icon_type: 'class',
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });
            };
            function ErrorAlert(title, msg) {
                $.notify({
                    // options
                    icon: 'glyphicon glyphicon-warning-sign',
                    title: title,
                    message: msg,
                    target: '_blank'
                }, {
                    // settings
                    element: 'body',
                    position: null,
                    type: "danger",
                    allow_dismiss: false,
                    newest_on_top: false,
                    showProgressbar: false,
                    placement: {
                        from: "top",
                        align: "right"
                    },
                    offset: 20,
                    spacing: 10,
                    z_index: 99999,
                    delay: 5000,
                    timer: 1000,
                    url_target: '_blank',
                    mouse_over: null,
                    animate: {
                        enter: 'animated fadeInDown',
                        exit: 'animated fadeOutUp'
                    },
                    onShow: null,
                    onShown: null,
                    onClose: null,
                    onClosed: null,
                    icon_type: 'class',
                    template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert" style="z-index=9999999">' +
                        '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                        '<span data-notify="icon"></span> ' +
                        '<span data-notify="title">{1}</span> ' +
                        '<span data-notify="message">{2}</span>' +
                        '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                        '</div>' +
                        '<a href="{3}" target="{4}" data-notify="url"></a>' +
                        '</div>'
                });
            };
    </script>
    <style type="text/css">
        body {
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            color: #444;
            font-size: 13px;
        }

        p, div, ul, li {
            padding: 0px;
            margin: 0px;
        }

        .tdHorario {
            width: 20px;
            text-align: center;
            vertical-align: middle;
            background-color: #fff;
            border: 1px solid #ccc;
        }

        .no-border {
            border: none;
        }

        .highlighted {
            background-color: #40babd;
        }

        .transparente {
            background: transparent;
            border: none;
        }

        .header {
            background: transparent;
            border: none;
        }

        .footerHorario {
            background: transparent;
            height: 40px;
            width: 1px;
            border: none;
        }

        .footerLabel {
            transform: rotate(-90deg);
            background: transparent;
            border: none;
            width: 1px;
            position: absolute;
            margin-top: 8px;
        }
    </style>
</head>
<body class="preload" style="background: #fff">
    <div id="full">
        <form id="form1" runat="server" enctype="multipart/form-data">
            <asp:ScriptManager ID="scripMain" runat="server" EnablePageMethods="true">
                <Scripts>
                    <asp:ScriptReference Path="~/assets/js/jquery.js" />
                    <asp:ScriptReference Path="~/assets/js/bootstrap.js" />
                    <asp:ScriptReference Path="~/assets/js/imagesloaded.js" />
                    <asp:ScriptReference Path="~/assets/js/masonry.js" />
                    <asp:ScriptReference Path="~/assets/js/main.js" />
                    <asp:ScriptReference Path="~/assets/js/modernizr.custom.js" />
                    <asp:ScriptReference Path="~/assets/js/pmenu.js" />
                    <asp:ScriptReference Path="~/assets/js/bootstrap-notify.js" />
                    <asp:ScriptReference Path="~/assets/js/bootstrap-notify.min.js" />
                    <asp:ScriptReference Path="~/assets/tmp/chosen.jquery.js" />
                    <asp:ScriptReference Path="~/assets/tmp/jquery.sumoselect.min.js" />
                    <asp:ScriptReference Path="~/assets/js/validation.js" />
                </Scripts>
            </asp:ScriptManager>
            <script type="text/javascript">
                $(function () {
                    $('[id*=lstFruits]').multiselect({
                        includeSelectAllOption: true,
                        enableFiltering: false,
                        enableCaseInsensitiveFiltering: true,
                    });
                });
                var prm = Sys.WebForms.PageRequestManager.getInstance();

                prm.add_endRequest(function () {
                    $('[id*=lstGrupoEspecialConsulta]').multiselect({
                        includeSelectAllOption: false,
                        enableFiltering: false,
                    });
                });

            </script>
            <%--<link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
                rel="stylesheet" type="text/css" />--%>
            <%--<link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
                rel="stylesheet" type="text/css" />--%>
            <%--<script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
                type="text/javascript"></script>--%>

            <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
                <asp:ListItem Text="Mango" Value="1" />
                <asp:ListItem Text="Apple" Value="2" />
                <asp:ListItem Text="Banana" Value="3" />
                <asp:ListItem Text="Guava" Value="4" />
                <asp:ListItem Text="Orange" Value="5" />
            </asp:ListBox>
            <%--<uc1:UcAltaConsulta runat="server" id="UcAltaConsulta" />--%>
            <uc1:UcAltaServicio runat="server" id="UcAltaServicio" />
            <%--<div class="span12">
                <div class="pe-block pe-view-layout-block pe-view-layout-block-26 pe-view-layout-class-form">
                    <form action="../wp-content/themes/oneup/uploadFisica.php" enctype="multipart/form-data" method="POST">
                        <div class="bay form-horizontal">
                            <div class="control-group">
                                <span class="control-label" for="sender_name">NOMBRE</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_name" name="sender_name" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_rfc">RFC</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_rfc" name="sender_rfc" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_estadocivil">ESTADO CIVIL</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_estadocivil" name="sender_estadocivil" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_calleno">CALLE Y NUMERO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_calleno" name="sender_calleno" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_coliniapersona">COLONIA</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_coliniapersona" name="sender_coliniapersona" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_delegacionpersona">DELEGACION</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_delegacionpersona" name="sender_delegacionpersona" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonopersonal">TELEFONO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonopersonal" name="sender_telefonopersonal" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_celularpersonal">CELULAR</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_celularpersonal" name="sender_celularpersonal" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_emailpersonal">CORREO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_emailpersonal" name="sender_emailpersonal" type="email" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_profesion">PROFESIÓN</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_profesion" name="sender_profesion" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_ingreso">INGRESO MENSUAL COMPROBABLE</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_ingreso" name="sender_ingreso" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_empresa">EMPRESA DONDE TRABAJA</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_empresa" name="sender_empresa" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_antiguedad">ANTIGUEDAD</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_antiguedad" name="sender_antiguedad" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_puesto">PUESTO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_puesto" name="sender_puesto" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonotrabajo">TELEFONO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonotrabajo" name="sender_telefonotrabajo" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_horariotrabajo">HORARIO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_horariotrabajo" name="sender_horariotrabajo" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_callenotrabajo">DOMICILIO ACTUAL DEL TRABAJO CALLE Y NÚMERO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_callenotrabajo" name="sender_callenotrabajo" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_coloniatrabajo">COLONIA</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_coloniatrabajo" name="sender_coloniatrabajo" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_delegaciontrabajo">DELEGACIÓN O MUNICIPIO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_delegaciontrabajo" name="sender_delegaciontrabajo" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_giroempresa">GIRO DE LA EMPRESA</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_giroempresa" name="sender_giroempresa" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_paginaInternet">PÁGINA DE INTERNET</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_paginaInternet" name="sender_paginaInternet" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_jefeinmediato">JEFE INMEDIATO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_jefeinmediato" name="sender_jefeinmediato" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_puestojefe">PUESTO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_puestojefe" name="sender_puestojefe" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_correojefe">CORREO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_correojefe" name="sender_correojefe" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>

                            <div class="control-group">
                                <span class="control-label span3" for="my_ife">Identificación oficial</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="my_ife" name="my_ife" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="my_saldo">Comprobante de Ingresos</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="my_saldo" name="my_saldo" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="my_carta">Carta Constancia de percepciones y antigüedad de la Empresa en que Trabaja</span>

                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="my_carta" name="my_carta" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombrearrendador">NOMBRE DE SU ACTUAL ARRENDADOR</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombrearrendador" name="sender_nombrearrendador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonoarrendador">TELÉFONO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonoarrendador" name="sender_telefonoarrendador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_domicilioinmueblearrendado">DOMICILIO DEL INMUEBLE ARRENDADO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_domicilioinmueblearrendado" name="sender_domicilioinmueblearrendado" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_montorentaactual">MONTO DE RENTA ACTUAL $</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_montorentaactual" name="sender_montorentaactual" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_motivocambioinmueble">¿POR QUÉ DESEA CAMBIARSE DEL INMUEBLE?</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_motivocambioinmueble" name="sender_motivocambioinmueble" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombrereferencia1">NOMBRE DE TRES FAMILIARES (QUE NO VIVAN CON USTED).</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombrereferencia1">1. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombrereferencia1" name="sender_nombrereferencia1" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonoreferencia1">TELEFONO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonoreferencia1" name="sender_telefonoreferencia1" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombrereferencia2">2. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombrereferencia2" name="sender_nombrereferencia2" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonoreferencia2">TELEFONO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonoreferencia2" name="sender_telefonoreferencia2" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombrereferencia3">3. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombrereferencia3" name="sender_nombrereferencia3" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonoreferencia3">TELEFONO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonoreferencia3" name="sender_telefonoreferencia3" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_horariotrabajo">NOMBRE DE TRES CONOCIDOS (QUE NO SEAN FAMILIARES).</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombreconocido1">1. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombreconocido1" name="sender_nombreconocido1" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonoconocido1">TELEFONO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonoconocido1" name="sender_telefonoconocido1" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombreconocido2">2. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombreconocido2" name="sender_nombreconocido2" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonoconocido2">TELEFONO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonoconocido2" name="sender_telefonoconocido2" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombreconocido3">3. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombreconocido3" name="sender_nombreconocido3" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telefonoconocido3">TELEFONO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telefonoconocido3" name="sender_telefonoconocido3" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>


                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupa1">PERSONAS QUE OCUPARAN EL INMUEBLE.</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupa1">1. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupa1" name="sender_personaocupa1" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupaparentesco1">PARENTESCO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupaparentesco1" name="sender_personaocupaparentesco1" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupa2">2. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupa2" name="sender_personaocupa2" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupaparentesco2">PARENTESCO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupaparentesco2" name="sender_personaocupaparentesco2" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupa3">3. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupa3" name="sender_personaocupa3" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupaparentesco3">PARENTESCO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupaparentesco3" name="sender_personaocupaparentesco3" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupa4">4. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupa4" name="sender_personaocupa4" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupaparentesco4">PARENTESCO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupaparentesco4" name="sender_personaocupaparentesco4" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupa5">5. -</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupa5" name="sender_personaocupa5" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_personaocupaparentesco5">PARENTESCO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_personaocupaparentesco5" name="sender_personaocupaparentesco5" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="ddlcomprobante">REQUIERE DE COMPROBANTE  FISCAL DEL ARRENDAMIENTO</span>
                                <div class="controls">
                                    <select id="ddlcomprobante" name="ddlcomprobante">
                                        <option value="volvo">SELECCIONE</option>
                                        <option value="saab">SI</option>
                                        <option value="opel">NO</option>
                                    </select>
                                    <span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_afianzado">HA ESTADO USTED AFIANZADO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_afianzado" name="sender_afianzado" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_afianzadora">¿CON QUÉ AFIANZADORA? </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_afianzadora" name="sender_afianzadora" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>

                            <div class="control-group">
                                <span class="control-label" for="sender_nombrefiador">FIADOR</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombrefiador">FIADOR (PERSONA FÍSICA).</span>
                            </div>

                            <div class="control-group">
                                <span class="control-label" for="sender_nombrefiador">NOMBRE DE SU OBLIGADO SOLIDIARIO (FIADOR)</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombrefiador" name="sender_nombrefiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_parentescofiador">¿QUÉ PARENTESCO TIENE CON EL FIADOR?</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_parentescofiador" name="sender_parentescofiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_estadocivilfiador">ESTADO CIVIL DEL FIADOR</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_estadocivilfiador" name="sender_estadocivilfiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_callenofiador">DOMICILIO PARTICULAR  CALLE Y NÚMERO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_callenofiador" name="sender_callenofiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_coloniafiador">COLONIA </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_coloniafiador" name="sender_coloniafiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_delegacionfiador">DELEGACIÓN ó MUNICIPIO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_delegacionfiador" name="sender_delegacionfiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_telfiador">TELEFONO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_telfiador" name="sender_horariotrabajo" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_celfiador">CELULAR</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_celfiador" name="sender_celfiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_correofiador">CORREO ELECTRÓNICO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_correofiador" name="sender_correofiador" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_correofiador">DOMICILIO DEL INMUEBLE EN GARANTIA</span>
                            </div>
                            
                            <div class="control-group">
                                <span class="control-label" for="sender_callenogarantia">CALLE Y NÚMERO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_callenogarantia" name="sender_callenogarantia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_coloniagarantia">COLONIA </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_coloniagarantia" name="sender_coloniagarantia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_delegaciongarantia">DELEGACIÓN ó MUNICIPIO </span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_delegaciongarantia" name="sender_delegaciongarantia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_profesiongarantia">PROFESIÓN, OCUPACIÓN U OFICIO</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_profesiongarantia" name="sender_profesiongarantia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_empresagarantia">EMPRESA DONDE TRABAJA</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_empresagarantia" name="sender_empresagarantia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_nombreconyugegarantia">NOMBRE DEL CÓNYUGE:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_nombreconyugegarantia" name="sender_nombreconyugegarantia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            
                            <div class="control-group">
                                <span class="control-label span3" for="ifefiador">Identificación oficial</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="ifefiador" name="ifefiador" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="domiciliofiador">Comprobante de Domicilio</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="domiciliofiador" name="domiciliofiador" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="escriturasfiador">Copia Simple de Escrituras del Inmueble en garantía con Datos del Registro Público de la Propiedad  (FOLIO REAL).</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="escriturasfiador" name="escriturasfiador" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="pagopredialfiador">Copia del Último Pago Predial</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="pagopredialfiador" name="pagopredialfiador" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="actamatrimonio">Copia del Acta de Matrimonio</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="actamatrimonio" name="actamatrimonio" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralrazonsocial">FIADOR (PERSONA MORAL).</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralrazonsocial">RAZÓN SOCIAL:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoralrazonsocial" name="sender_fiadormoralrazonsocial" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralrfc">R.F.C.:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoralrfc" name="sender_fiadormoralrfc" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralcalleno">DOMICILIO CALLE Y NÚMERO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoralcalleno" name="sender_fiadormoralcalleno" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralcolonia">COLONIA:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoralcolonia" name="sender_fiadormoralcolonia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoraldelegacion">DELEGACIÓN ó MUNICIPIO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoraldelegacion" name="sender_fiadormoraldelegacion" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoraltel">TELEFONO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoraltel" name="sender_fiadormoraltel" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralgiroempresa">GIRO DE LA EMPRESA:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoralgiroempresa" name="sender_fiadormoralgiroempresa" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralpagina">PÁGINA DE INTERNET:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoralpagina" name="sender_fiadormoralpagina" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fiadormoralnombrerepresentantelegal">NOMBRE DEL REPRESENTANTE LEGAL:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fiadormoralnombrerepresentantelegal" name="sender_fiadormoralnombrerepresentantelegal" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_fmPuesto">PUESTO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_fmPuesto" name="sender_fmPuesto" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_actacons">ACTA CONSTITUTIVA No.:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_actacons" name="sender_actacons" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_poderrepno">PODER REP. LEGAL No.:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_poderrepno" name="sender_poderrepno" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_relcalleno">DOMICILIO PARTICULAR DEL REP. LEGAL DOMICILIO CALLE Y NÚMERO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_relcalleno" name="sender_relcalleno" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_relcolonia">COLONIA:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_relcolonia" name="sender_relcolonia" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            <div class="control-group">
                                <span class="control-label" for="sender_reldelegacion">DELEGACIÓN ó MUNICIPIO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_reldelegacion" name="sender_reldelegacion" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            
                            <div class="control-group">
                                <span class="control-label" for="sender_reltelefono">TELÉFONO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_reltelefono" name="sender_reltelefono" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            
                            <div class="control-group">
                                <span class="control-label" for="sender_relcelular">CELULAR:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_relcelular" name="sender_relcelular" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                            
                            <div class="control-group">
                                <span class="control-label" for="sender_relcorreo">CORREO ELECTRÓNICO:</span>
                                <div class="controls">
                                    <input required class="required span9" id="sender_relcorreo" name="sender_relcorreo" type="text" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                                </div>
                            </div>
                           <div class="control-group">
                                <span class="control-label span3" for="reldactaconstitutiva">Copia Simple de Acta Constitutiva de la Empresa</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="reldactaconstitutiva" name="reldactaconstitutiva" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="reldrfc">Copia de R.F.C.</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="reldrfc" name="reldrfc" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="reldpodernotarial">Copia Simple del Poder Notarial del Representante Legal de la Empresa.</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="reldpodernotarial" name="reldpodernotarial" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="reldife">Identificación Oficial del Representante Legal.</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="reldife" name="reldife" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="reldcomprobantedomicilio">Comprobante de Domicilio de la Empresa.</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="reldcomprobantedomicilio" name="reldcomprobantedomicilio" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="reldescrituras">Copia Simple de Escrituras del Inmueble en garantía con Datos del Registro Público de la Propiedad.</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="reldescrituras" name="reldescrituras" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <div class="control-group">
                                <span class="control-label span3" for="reldpagopredial">Copia del Último Pago Predial</span>
                            </div>
                            <div class="controls" style="margin-left: 0">
                                <input required class="required span2" id="reldpagopredial" name="reldpagopredial" type="file" style="margin-left: 15px; display: inline-block; width: 90%" /><span class="help-inline" style="color: #1e73be; vertical-align: top; font-size: 13px; font-weight: 700">*</span>
                            </div>
                            <input name="button" type="submit" value="ENVIAR" class="contour-btn red" />
                        </div>
                    </form>
                </div>
            </div>--%>
        </form>
    </div>
</body>
</html>
