<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcTicketDetalle.ascx.cs" Inherits="KiiniHelp.UserControls.Detalles.UcTicketDetalle" %>
<%@ Register Src="~/UserControls/Detalles/UcDetalleMascaraCaptura.ascx" TagPrefix="uc1" TagName="UcDetalleMascaraCaptura" %>

<style>
        .form-control select {
            content: '<>';
        }

        .wrapperResponse {
            border: none;
            display: inline-block;
        }

        .wrapperbutton {
            position: absolute;
            bottom: 30px;
            right: 50px;
        }

        .selectConver {
            cursor: pointer;
        }

            .selectConver [type=radio] {
                display: none;
            }

                .selectConver [type=radio] + label {
                    margin-left: 15px;
                    padding: 6px 12px;
                    cursor: pointer;
                }

                .selectConver [type=radio]:checked + label {
                    padding: 6px 12px;
                    border-bottom: 2px solid #3aa7aa;
                }
    </style>

<div style="height: 100%;">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hfTicketActivo" />
            <asp:HiddenField runat="server" ID="hfEstatusAsignacionActual" />
            <asp:HiddenField runat="server" ID="hfEsPropietario" />

            <section class="module">
                <div class="row">
                    <div class="col-lg-9 col-md-9 col-sm-9">
                        <div class="module-inner">
                            <div class="row bordered">
                                <div class="col-lg-1 col-md-1 col-sm-1">
                                    <span class="fa fa-envelope fa-30x padding-10 border-rounded margin-top-5" style=""></span>
                                </div>
                                <div class="col-lg-7 col-md-7 col-sm-7">
                                    <div class="row">
                                        <h1 class="module-title" style="font-size: 25px">
                                            <asp:Label runat="server" ID="lblNoticket" />:<asp:Label runat="server" ID="lblTituloTicket"></asp:Label></h1>
                                        <br />
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblNombreCorreo" /><br />
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblFechaAlta"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1 text-center">
                                    <div class="row">
                                        <asp:Label runat="server" Text="Prioridad" />
                                    </div>
                                    <div class="row">
                                        <asp:Image runat="server" ID="imgPrioridad" Width="50%" />
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblDescripcionPrioridad" />
                                    </div>
                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1 text-center">
                                    <div class="row">
                                        <asp:Label runat="server" Text="SLA" />
                                    </div>
                                    <div class="row">
                                        <asp:Image runat="server" ID="imgSLA" Width="50%" />
                                    </div>
                                    <div class="row">
                                        <asp:Label runat="server" ID="lblTiempoRestanteSLa" />
                                    </div>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2 text-center">
                                    <div class="row">
                                        <asp:Label runat="server" Text="Estatus" />
                                    </div>
                                    <div class="row">
                                        <div class="text-center" style="margin: auto; width: 75%;" runat="server" id="divEstatus">
                                            <asp:Label runat="server" CssClass="btn" ID="lblEstatus" Text="Abierto" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row bordered">
                                <div class="row">
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        Asignación
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        Asignado a
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        Tipo
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        Tipificación
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        <asp:DropDownList runat="server" class="form-control" AutoPostBack="True" />
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        <asp:DropDownList runat="server" class="form-control" AutoPostBack="True" />
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        <asp:DropDownList runat="server" class="form-control" AutoPostBack="True" />
                                    </div>
                                    <div class="col-lg-2 col-md-3 col-sm-3">
                                        <asp:DropDownList runat="server" class="form-control" AutoPostBack="True" />
                                    </div>
                                </div>
                            </div>
                            <div class="row margin-top-5 bordered">
                                <div class="col-lg-1 col-md-1 col-sm-1">
                                    <asp:Image runat="server" ImageUrl="~/assets/images/profiles/profile-1.png" />
                                </div>
                                <div class="col-lg-10 col-md-10 col-sm-10">

                                    <div class="row">
                                        <asp:RadioButton runat="server" GroupName="tipoConversacion" Text="Respuesta pública" CssClass="selectConver" Checked="True" />
                                        <asp:RadioButton runat="server" GroupName="tipoConversacion" Text="Interna" CssClass="selectConver" />
                                    </div>


                                    <div class="row">
                                        <div class="wrapperResponse col-lg-12">
                                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="15" CssClass="form-control" MaxLength="999" />
                                            <asp:Button ID="btnSendPublic" runat="server" Text="Enviar" CssClass="btn btn-primary wrapperbutton" />
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="row margin-top-5">
                                <div class="col-lg-1">
                                    <asp:DropDownList runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-lg-1">|</div>
                                <asp:RadioButton runat="server" GroupName="tipoConversacionPreview" Text="Todas" CssClass="selectConver" Checked="True" />
                                <asp:RadioButton runat="server" GroupName="tipoConversacionPreview" Text="Pública" CssClass="selectConver" />
                                <asp:RadioButton runat="server" GroupName="tipoConversacionPreview" Text="Interna" CssClass="selectConver" />
                            </div>
                            <div class="row margin-top-5">
                                <asp:Repeater runat="server" ID="rptConversaciones">
                                    <ItemTemplate>
                                        <div class="row" style="border-top: 1px solid #f3f3f7; border-bottom: 1px solid #f3f3f7">
                                            <div class="col-lg-1 col-md-1 col-sm-1">
                                                <asp:Image runat="server" ImageUrl="~/assets/images/profiles/profile-1.png" />
                                            </div>
                                            <div class="col-lg-10 col-md-10 col-sm-10">
                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                    <h1>
                                                        <asp:Label runat="server" ID="lblNombre" Text="Gabriela Vega"></asp:Label></h1>
                                                </div>
                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                    <asp:Label runat="server" ID="lblFecha" Text="Ayer 14/04/2017, 10:54 PM"></asp:Label>
                                                </div>

                                                <div class="col-lg-12 col-md-12 col-sm-12">
                                                    <asp:Label runat="server" ID="lblMensaje" Text="Buenas tarde, \n Necesito ayuda con mi password."></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="row margin-top-5">
                                <uc1:UcDetalleMascaraCaptura runat="server" id="UcDetalleMascaraCaptura" />
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-3">
                        <div class="module-inner">
                            <div class="row bordered">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <asp:Image runat="server" ImageUrl="~/assets/images/profiles/profile-square-1.png" Width="75px" />
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-9 borderright">
                                    <asp:Label runat="server" ID="lblNombreDetalle" Text="gabriela Vega" Style="font-size: 24px; font-weight: 700"></asp:Label><br />
                                    <span class="btn btn-default-alt btn-square-usuario empleado">
                                        <asp:Label runat="server" ID="lblTipoUsuarioDetalle"></asp:Label></span>
                                    <asp:Image runat="server" ImageUrl="~/assets/images/icons/vip.png" Width="25px" ID="imgVip" />
                                </div>
                            </div>
                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Image runat="server" ImageUrl="~/assets/images/icons/ojo.png" Width="25px" />
                                    <asp:Label runat="server" Text="6 de abril 2017" ID="lblFechaUltimaconexion"></asp:Label>
                                </div>
                            </div>
                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <span class="col-lg-2 col-md-2 col-sm-2 pe-icon pe-7s-ticket icon" style="font-size: 24px"></span>
                                    <div class="col-lg-10 col-md-10 col-sm-10">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTicketUsuario" />
                                    </div>
                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="Puesto" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Label runat="server" ID="lblPuesto" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>
                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="correo electornico principal" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Label runat="server" ID="lblCorreoPrincipal" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="telefono principal" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Label runat="server" ID="lblTelefonoPrincipal" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="Facebook" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Label runat="server" ID="lblFacebook" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="twitter" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Label runat="server" ID="lbltwitter" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="Organización" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Label runat="server" ID="lblOrganizacion" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="Ubicación" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Label runat="server" ID="lblUbicacion" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" Text="Etiquetas" CssClass="col-lg-12 col-md-12 col-sm-12"></asp:Label>
                                    <asp:Repeater runat="server" ID="rptEtiquetas">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblEtiquetas" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>

                            <div class="row margin-top-5">
                                <div class="col-lg-5 col-md-5 col-sm-5">
                                    <asp:Label runat="server" Text="Creado" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                                <div class="col-lg-7 col-md-7 col-sm-7">
                                    <asp:Label runat="server" ID="lblFechaAltaDetalle" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>
                            <div class="row margin-top-5">
                                <div class="col-lg-5 col-md-5 col-sm-5">
                                    <asp:Label runat="server" Text="UltimaActualizacion" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                                <div class="col-lg-7 col-md-7 col-sm-7">
                                    <asp:Label runat="server" ID="lblfechaUltimaActualizacion" CssClass="col-lg-12 col-md-12 col-sm-12" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
