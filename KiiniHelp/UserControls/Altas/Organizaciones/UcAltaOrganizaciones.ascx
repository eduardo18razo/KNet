﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaOrganizaciones.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.Organizaciones.UcAltaOrganizaciones" %>

<asp:UpdatePanel ID="upCatlogos" runat="server">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIdOrganizacion" />
        <asp:HiddenField runat="server" ID="hfCatalogo" />
        <asp:HiddenField runat="server" ID="hfAlta" />
        <asp:HiddenField runat="server" ID="hfEsSeleccion" />
        <asp:HiddenField runat="server" ID="hfOrganizacionSeleccionada" />

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <asp:LinkButton CssClass="close" runat="server" OnClick="btnCancelarCatalogo_OnClick" Text='&times'></asp:LinkButton>
                    <%--<h2 class="modal-title" id="modal-new-ticket-label">
                        <asp:Label runat="server" ID="lblBrandingModal" /></h2> jgb --%>
                    <p class="text-center">
                        <asp:Label runat="server" ID="lblTitleCatalogo" />
                    </p>
                </div>
                <div class="modal-body">
                    <!-- TIPO DE USUARIO-->
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            Tipo de Usuario<br />
                            <div class="form-group">
                                <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="form-control" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" />
                            </div>
                        </div>
                    </div>
                    <hr />

                    <!--CONTAINER IZQUIERDA-->
                    <div class="row" runat="server" id="divData" visible="False">
                        <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">
                            <!--Step 1-->
                            <div class="margin-top" runat="server" id="divStep1">
                                <asp:HiddenField runat="server" ID="hfNivel1" />
                                <asp:Label runat="server" ID="lblAliasNivel1" />
                                <i class="fa fa-check text-success" aria-hidden="true" runat="server" id="succNivel1" visible="False"></i>
                                <br />
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-square" ID="btnStatusNivel1" CommandArgument="1" OnClick="btnStatusNivel1_OnClick">
                                    <asp:Image ImageUrl="~/assets/images/step1.png" Width="20" Height="20" alt="" runat="server" />&nbsp;<asp:Label runat="server" ID="lblStepNivel1" />
                                </asp:LinkButton>
                            </div>

                            <!--Step 2-->
                            <div class="margin-top" runat="server" id="divStep2" visible="False">
                                <asp:HiddenField runat="server" ID="hfNivel2" />
                                <asp:Label runat="server" ID="lblAliasNivel2" />
                                <i class="fa fa-check text-success" aria-hidden="true" runat="server" id="succNivel2" visible="False"></i>
                                <br />
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-square" ID="btnStatusNivel2" CommandArgument="2" OnClick="btnStatusNivel1_OnClick">
                                    <asp:Image ImageUrl="~/assets/images/step2.png" Width="20" Height="20" alt="" runat="server" />&nbsp;<asp:Label runat="server" ID="lblStepNivel2" />
                                </asp:LinkButton>
                            </div>

                            <!--Step 3-->
                            <div class="margin-top" runat="server" id="divStep3" visible="False">
                                <asp:HiddenField runat="server" ID="hfNivel3" />
                                <asp:Label runat="server" ID="lblAliasNivel3" />
                                <i class="fa fa-check text-success" aria-hidden="true" runat="server" id="succNivel3" visible="False"></i>
                                <br />
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-square" ID="btnStatusNivel3" CommandArgument="3" OnClick="btnStatusNivel1_OnClick">
                                    <asp:Image ImageUrl="~/assets/images/step3.png" Width="20" Height="20" alt="" runat="server" />&nbsp;
                                       
                                    <asp:Label runat="server" ID="lblStepNivel3" />
                                </asp:LinkButton>
                            </div>

                            <!--Step 4-->
                            <div class="margin-top" runat="server" id="divStep4" visible="False">
                                <asp:HiddenField runat="server" ID="hfNivel4" />
                                <asp:Label runat="server" ID="lblAliasNivel4" />
                                <i class="fa fa-check text-success" aria-hidden="true" runat="server" id="succNivel4" visible="False"></i>
                                <br />
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-square" ID="btnStatusNivel4" CommandArgument="4" OnClick="btnStatusNivel1_OnClick">
                                    <asp:Image ImageUrl="~/assets/images/step4.png" Width="20" Height="20" alt="" runat="server" />&nbsp;
                                       
                                    <asp:Label runat="server" ID="lblStepNivel4" />
                                </asp:LinkButton>
                            </div>

                            <!--Step 5-->
                            <div class="margin-top" runat="server" id="divStep5" visible="False">
                                <asp:HiddenField runat="server" ID="hfNivel5" />
                                <asp:Label runat="server" ID="lblAliasNivel5" />
                                <i class="fa fa-check text-success" aria-hidden="true" runat="server" id="succNivel5" visible="False"></i>
                                <br />
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-square" ID="btnStatusNivel5" CommandArgument="5" OnClick="btnStatusNivel1_OnClick">
                                    <asp:Image ImageUrl="~/assets/images/step5.png" Width="20" Height="20" alt="" runat="server" />&nbsp;
                                       
                                    <asp:Label runat="server" ID="lblStepNivel5" />
                                </asp:LinkButton>
                            </div>

                            <!--Step 6-->
                            <div class="margin-top" runat="server" id="divStep6" visible="False">
                                <asp:HiddenField runat="server" ID="hfNivel6" />
                                <asp:Label runat="server" ID="lblAliasNivel6" />
                                <i class="fa fa-check text-success" aria-hidden="true" runat="server" id="succNivel6" visible="False"></i>
                                <br />
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-square" ID="btnStatusNivel6" CommandArgument="6" OnClick="btnStatusNivel1_OnClick">
                                    <asp:Image ImageUrl="~/assets/images/step6.png" Width="20" Height="20" alt="" runat="server" />&nbsp;
                                       
                                    <asp:Label runat="server" ID="lblStepNivel6" />
                                </asp:LinkButton>
                            </div>

                            <!--Step 7-->
                            <div class="margin-top" runat="server" id="divStep7" visible="False">
                                <asp:HiddenField runat="server" ID="hfNivel7" />
                                <asp:Label runat="server" ID="lblAliasNivel7" />
                                <i class="fa fa-check text-success" aria-hidden="true" runat="server" id="succNivel7" visible="False"></i>
                                <br />
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-square" ID="btnStatusNivel7" CommandArgument="7" OnClick="btnStatusNivel1_OnClick">
                                    <asp:Image ImageUrl="~/assets/images/step7.png" Width="20" Height="20" alt="" runat="server" />&nbsp;<asp:Label runat="server" ID="lblStepNivel7" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <!--/CONTAINER IZQUIERDA-->

                        <!--CONTAINER DERECHA-->

                        <!--Filtro 1 ORGANIZACIÓN-->
                        <div>
                            <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                                <div class="bg-grey">
                                    <h3 class="text-left">
                                        <asp:Label runat="server" Text=""></asp:Label><asp:Label runat="server" ID="lblOperacion"></asp:Label></h3>
                                    <hr />
                                    <!--CAMPO-->
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlNivelSeleccionModal" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNivelSeleccionModal_OnSelectedIndexChanged" />
                                    </div>

                                    <!--CAMPO-->
                                    <p class="margin-top-40">
                                        <asp:Button class="btn btn-primary" runat="server" Text="Siguiente" ID="btnSeleccionarModal" OnClick="btnSeleccionarModal_OnClick" />
                                    </p>

                                    <hr />
                                    <div runat="server" ID="divClas"></div>
                                    <!--CAMPO-->
                                    <div runat="server" ID="pnlAlta" Visible="true">
                                        <div class="form-group margin-top">
                                            Nombre de
                                            <asp:Label runat="server" ID="lblOperacionDescripcion" />*<br />
                                            <asp:TextBox CssClass="form-control" ID="txtDescripcionCatalogo" runat="server" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);" MaxLength="50" autofocus="autofocus" />
                                            <asp:LinkButton runat="server"  OnClick="btnGuardarCatalogo_OnClick" class="fa fa-plus-circle">
                                            </asp:LinkButton>
                                        </div>
                                    </div>


                                </div>
                                <!--DIV que cierra el bg-grey -->

                                <!--BTN-TERMINAR-->
                                <p class="text-right margin-top-40">
                                    <asp:Button CssClass="btn btn-success" ID="btnTerminar" runat="server" Text="Terminar" OnClick="btnTerminar_OnClick"></asp:Button>
                                </p>
                                <!--/BTN-TERMINAR-->
                            </div>
                        </div>
                        <!--/Filtro 1 ORGANIZACIÓN-->
                        <!--/CONTAINER DERECHA-->

                    </div>
                </div>
                <asp:CheckBox runat="server" ID="chkHabilitado" Visible="False" Checked="True" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
