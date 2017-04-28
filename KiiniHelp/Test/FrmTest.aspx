<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmTest.aspx.cs" Inherits="KiiniHelp.Test.FrmTest" %>

<%@ Register Src="~/UserControls/Altas/UcAltaNivelArbol.ascx" TagPrefix="uc1" TagName="UcAltaNivelArbol" %>
<%@ Register Src="~/UserControls/Altas/Ubicaciones/UcAltaUbicaciones.ascx" TagPrefix="uc1" TagName="UcAltaUbicaciones" %>
<%@ Register Src="~/UserControls/Altas/AltaGrupoUsuario.ascx" TagPrefix="uc1" TagName="AltaGrupoUsuario" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../BootStrap/js/locales/jquery.sumoselect.min.js"></script>
    <script src="../BootStrap/js/bootstrap.min.js"></script>
    <script src="../BootStrap/js/bootstrap.js"></script>

    <link rel='stylesheet' href="../assets/css/font.css" />
    <link rel="stylesheet" href="../assets/css/font-awesome.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.css" />
    <link rel="stylesheet" href="../assets/css/styles.css" />
    <link rel="stylesheet" href="../assets/css/menuStyle.css" />
    <link rel="stylesheet" href="../assets/css/divs.css" />
    <link href="../assets/css/checkBox.css" rel="stylesheet" />
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $(<%=lstBoxTest.ClientID%>).SumoSelect({ placeholder: 'SELECCIONE', selectAll: true, csvDispCount: 1 });
        });
    </script>--%>
    <script type="text/javascript">
        function UploadComplete(sender, args) {
            __doPostBack('Refresh', '');
        }
        function MostrarPopup(modalName) {
            debugger;
            $(modalName).modal({ backdrop: 'static', keyboard: false });
            $(modalName).modal({ show: true });
            return true;
        };
        function CierraPopup(modalName) {
            $(modalName).modal('hide');
            return true;
        };
        function HightSearch(content, serachText) {
            debugger;
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
                    <asp:ScriptReference Path="~/assets/js/validation.js" />
                </Scripts>
            </asp:ScriptManager>

            <%--<asp:CheckBox runat="server" Text="prueba" />
        <div class="wrapper">
            <input type="checkbox" name="toggle" id="toggle">
            <label for="toggle"></label>
        </div>--%>

            <%--<asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Rows="10" Columns="50" onclick="storeCur(this);" onkeyup="storeCur(this);" onselect="storeCur(this);"></asp:TextBox>

        <asp:ListBox ID="lstParameter" runat="server" DataTextField="Name" DataValueField="Type" onmousedown="d = new drag(this)"
            onmouseup="d.drop(this.form.txtMsg)" onmouseout="if (typeof d != 'undefined') d.setIndex()"></asp:ListBox>--%>
            <%--<asp:UpdatePanel ID="upGeneral" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="btnAbrirModal" OnClick="btnAbrirModal_OnClick" Text="Abrir Modal" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal fade" id="editNivel" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
                    <asp:UpdatePanel ID="upCampus" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <uc1:UcAltaNivelArbol runat="server" id="ucAltaNivelArbol" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
            <%--<asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button OnClientClick="getSelectedHora();" runat="server" Text="muestra horas" />
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <table class="table table-bordered" id="our_table">
                            <tbody>
                                <tr id="1">
                                    <td class="header">Lun</td>
                                    <td class="tdHorario" id="lun0"></td>
                                    <td class="tdHorario" id="lun1"></td>
                                    <td class="tdHorario" id="lun2"></td>
                                    <td class="tdHorario" id="lun3"></td>
                                    <td class="tdHorario" id="lun4"></td>
                                    <td class="tdHorario" id="lun5"></td>
                                    <td class="tdHorario" id="lun6"></td>
                                    <td class="tdHorario" id="lun7"></td>
                                    <td class="tdHorario" id="lun8"></td>
                                    <td class="tdHorario" id="lun9"></td>
                                    <td class="tdHorario" id="lun10"></td>
                                    <td class="tdHorario" id="lun11"></td>
                                    <td class="tdHorario" id="lun12"></td>
                                    <td class="tdHorario" id="lun13"></td>
                                    <td class="tdHorario" id="lun14"></td>
                                    <td class="tdHorario" id="lun15"></td>
                                    <td class="tdHorario" id="lun16"></td>
                                    <td class="tdHorario" id="lun17"></td>
                                    <td class="tdHorario" id="lun18"></td>
                                    <td class="tdHorario" id="lun19"></td>
                                    <td class="tdHorario" id="lun20"></td>
                                    <td class="tdHorario" id="lun21"></td>
                                    <td class="tdHorario" id="lun22"></td>
                                    <td class="tdHorario" id="lun23"></td>
                                </tr>
                                <tr id="2">
                                    <td class="header">Mar</td>
                                    <td class="tdHorario" id="mar0"></td>
                                    <td class="tdHorario" id="mar1"></td>
                                    <td class="tdHorario" id="mar2"></td>
                                    <td class="tdHorario" id="mar3"></td>
                                    <td class="tdHorario" id="mar4"></td>
                                    <td class="tdHorario" id="mar5"></td>
                                    <td class="tdHorario" id="mar6"></td>
                                    <td class="tdHorario" id="mar7"></td>
                                    <td class="tdHorario" id="mar8"></td>
                                    <td class="tdHorario" id="mar9"></td>
                                    <td class="tdHorario" id="mar10"></td>
                                    <td class="tdHorario" id="mar11"></td>
                                    <td class="tdHorario" id="mar12"></td>
                                    <td class="tdHorario" id="mar13"></td>
                                    <td class="tdHorario" id="mar14"></td>
                                    <td class="tdHorario" id="mar15"></td>
                                    <td class="tdHorario" id="mar16"></td>
                                    <td class="tdHorario" id="mar17"></td>
                                    <td class="tdHorario" id="mar18"></td>
                                    <td class="tdHorario" id="mar19"></td>
                                    <td class="tdHorario" id="mar20"></td>
                                    <td class="tdHorario" id="mar21"></td>
                                    <td class="tdHorario" id="mar22"></td>
                                    <td class="tdHorario" id="mar23"></td>
                                </tr>
                                <tr id="3">
                                    <td class="tdHorario" class="header">Mie</td>
                                    <td class="tdHorario" id="mie0"></td>
                                    <td class="tdHorario" id="mie1"></td>
                                    <td class="tdHorario" id="mie2"></td>
                                    <td class="tdHorario" id="mie3"></td>
                                    <td class="tdHorario" id="mie4"></td>
                                    <td class="tdHorario" id="mie5"></td>
                                    <td class="tdHorario" id="mie6"></td>
                                    <td class="tdHorario" id="mie7"></td>
                                    <td class="tdHorario" id="mie8"></td>
                                    <td class="tdHorario" id="mie9"></td>
                                    <td class="tdHorario" id="mie10"></td>
                                    <td class="tdHorario" id="mie11"></td>
                                    <td class="tdHorario" id="mie12"></td>
                                    <td class="tdHorario" id="mie13"></td>
                                    <td class="tdHorario" id="mie14"></td>
                                    <td class="tdHorario" id="mie15"></td>
                                    <td class="tdHorario" id="mie16"></td>
                                    <td class="tdHorario" id="mie17"></td>
                                    <td class="tdHorario" id="mie18"></td>
                                    <td class="tdHorario" id="mie19"></td>
                                    <td class="tdHorario" id="mie20"></td>
                                    <td class="tdHorario" id="mie21"></td>
                                    <td class="tdHorario" id="mie22"></td>
                                    <td class="tdHorario" id="mie23"></td>
                                </tr>
                                <tr id="4">
                                    <td class="header">Jue</td>
                                    <td class="tdHorario" id="jue0"></td>
                                    <td class="tdHorario" id="jue1"></td>
                                    <td class="tdHorario" id="jue2"></td>
                                    <td class="tdHorario" id="jue3"></td>
                                    <td class="tdHorario" id="jue4"></td>
                                    <td class="tdHorario" id="jue5"></td>
                                    <td class="tdHorario" id="jue6"></td>
                                    <td class="tdHorario" id="jue7"></td>
                                    <td class="tdHorario" id="jue8"></td>
                                    <td class="tdHorario" id="jue9"></td>
                                    <td class="tdHorario" id="jue10"></td>
                                    <td class="tdHorario" id="jue11"></td>
                                    <td class="tdHorario" id="jue12"></td>
                                    <td class="tdHorario" id="jue13"></td>
                                    <td class="tdHorario" id="jue14"></td>
                                    <td class="tdHorario" id="jue15"></td>
                                    <td class="tdHorario" id="jue16"></td>
                                    <td class="tdHorario" id="jue17"></td>
                                    <td class="tdHorario" id="jue18"></td>
                                    <td class="tdHorario" id="jue19"></td>
                                    <td class="tdHorario" id="jue20"></td>
                                    <td class="tdHorario" id="jue21"></td>
                                    <td class="tdHorario" id="jue22"></td>
                                    <td class="tdHorario" id="jue23"></td>
                                </tr>
                                <tr id="5">
                                    <td class="header">Vie</td>
                                    <td class="tdHorario" id="vie0"></td>
                                    <td class="tdHorario" id="vie1"></td>
                                    <td class="tdHorario" id="vie2"></td>
                                    <td class="tdHorario" id="vie3"></td>
                                    <td class="tdHorario" id="vie4"></td>
                                    <td class="tdHorario" id="vie5"></td>
                                    <td class="tdHorario" id="vie6"></td>
                                    <td class="tdHorario" id="vie7"></td>
                                    <td class="tdHorario" id="vie8"></td>
                                    <td class="tdHorario" id="vie9"></td>
                                    <td class="tdHorario" id="vie10"></td>
                                    <td class="tdHorario" id="vie11"></td>
                                    <td class="tdHorario" id="vie12"></td>
                                    <td class="tdHorario" id="vie13"></td>
                                    <td class="tdHorario" id="vie14"></td>
                                    <td class="tdHorario" id="vie15"></td>
                                    <td class="tdHorario" id="vie16"></td>
                                    <td class="tdHorario" id="vie17"></td>
                                    <td class="tdHorario" id="vie18"></td>
                                    <td class="tdHorario" id="vie19"></td>
                                    <td class="tdHorario" id="vie20"></td>
                                    <td class="tdHorario" id="vie21"></td>
                                    <td class="tdHorario" id="vie22"></td>
                                    <td class="tdHorario" id="vie23"></td>
                                </tr>
                                <tr id="6">
                                    <td class="header">Sab</td>
                                    <td class="tdHorario" id="sab0"></td>
                                    <td class="tdHorario" id="sab1"></td>
                                    <td class="tdHorario" id="sab2"></td>
                                    <td class="tdHorario" id="sab3"></td>
                                    <td class="tdHorario" id="sab4"></td>
                                    <td class="tdHorario" id="sab5"></td>
                                    <td class="tdHorario" id="sab6"></td>
                                    <td class="tdHorario" id="sab7"></td>
                                    <td class="tdHorario" id="sab8"></td>
                                    <td class="tdHorario" id="sab9"></td>
                                    <td class="tdHorario" id="sab10"></td>
                                    <td class="tdHorario" id="sab11"></td>
                                    <td class="tdHorario" id="sab12"></td>
                                    <td class="tdHorario" id="sab13"></td>
                                    <td class="tdHorario" id="sab14"></td>
                                    <td class="tdHorario" id="sab15"></td>
                                    <td class="tdHorario" id="sab16"></td>
                                    <td class="tdHorario" id="sab17"></td>
                                    <td class="tdHorario" id="sab18"></td>
                                    <td class="tdHorario" id="sab19"></td>
                                    <td class="tdHorario" id="sab20"></td>
                                    <td class="tdHorario" id="sab21"></td>
                                    <td class="tdHorario" id="sab22"></td>
                                    <td class="tdHorario" id="sab23"></td>
                                </tr>
                                <tr id="0">
                                    <td class="header">Dom</td>
                                    <td class="tdHorario" id="dom0"></td>
                                    <td class="tdHorario" id="dom1"></td>
                                    <td class="tdHorario" id="dom2"></td>
                                    <td class="tdHorario" id="dom3"></td>
                                    <td class="tdHorario" id="dom4"></td>
                                    <td class="tdHorario" id="dom5"></td>
                                    <td class="tdHorario" id="dom6"></td>
                                    <td class="tdHorario" id="dom7"></td>
                                    <td class="tdHorario" id="dom8"></td>
                                    <td class="tdHorario" id="dom9"></td>
                                    <td class="tdHorario" id="dom10"></td>
                                    <td class="tdHorario" id="dom11"></td>
                                    <td class="tdHorario" id="dom12"></td>
                                    <td class="tdHorario" id="dom13"></td>
                                    <td class="tdHorario" id="dom14"></td>
                                    <td class="tdHorario" id="dom15"></td>
                                    <td class="tdHorario" id="dom16"></td>
                                    <td class="tdHorario" id="dom17"></td>
                                    <td class="tdHorario" id="dom18"></td>
                                    <td class="tdHorario" id="dom19"></td>
                                    <td class="tdHorario" id="dom20"></td>
                                    <td class="tdHorario" id="dom21"></td>
                                    <td class="tdHorario" id="dom22"></td>
                                    <td class="tdHorario" id="dom23"></td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr class="footerHorario">
                                    <td class="transparente"></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">00:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">01:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">02:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">03:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">04:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">05:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">06:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">07:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">08:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">09:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">10:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">11:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">12:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">13:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">14:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">15:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">16:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">17:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">18:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">19:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">20:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">21:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">22:00</span></td>
                                    <td class="tdHorario no-border"><span class="footerLabel">23:00</span></td>
                                </tr>
                            </tfoot>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
            <uc1:AltaGrupoUsuario runat="server" id="AltaGrupoUsuario" />
        </form>
    </div>
</body>
</html>
