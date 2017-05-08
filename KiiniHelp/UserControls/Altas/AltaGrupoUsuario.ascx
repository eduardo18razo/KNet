﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaGrupoUsuario.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaGrupoUsuario" %>
<%@ Register Src="~/UserControls/Altas/UcAltaDiasFestivos.ascx" TagPrefix="uc1" TagName="UcAltaDiasFestivos" %>
<%@ Register Src="~/UserControls/Altas/UcAltaHorario.ascx" TagPrefix="uc1" TagName="UcAltaHorario" %>


<style>
    .hideCheck {
    }

        .hideCheck input {
            left: -9999px;
            display: none;
        }

        .hideCheck label {
            width: 100%;
            height: 100%;
            cursor: pointer;
            padding: 6px 12px;
        }
</style>
<asp:UpdatePanel ID="upGrupoUsuario" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIdGrupo" />
        <asp:HiddenField runat="server" ID="hfFromOpcion" />
        <asp:HiddenField runat="server" ID="hfIdTipoSubGrupo" />
        <div class="modal-header">
            <asp:LinkButton class="close" ID="btnClose" OnClick="btnCancelar_OnClick" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
            <h2 class="modal-title" id="modal-new-ticket-label">
                <br />
                NUEVO GRUPO</h2>
            <asp:Label runat="server" Text="Alta Grupo de Usuario" ID="lblTitle"></asp:Label>
        </div>
        <hr />
        <div class="modal-body">
            <div class="row" style="margin-top: -30px">
                <div>
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <asp:DropDownList runat="server" ID="ddlTipoUsuario" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoUsuario_OnSelectedIndexChanged" />
                                </div>
                                <div class="form-group margin-top">
                                    <asp:DropDownList runat="server" ID="ddlTipoGrupo" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoGrupo_OnSelectedIndexChanged" />
                                </div>
                                <div class="module-content-inner">
                                    <div class="faq-section text-center margin-bottom-lg">
                                        <div class="faqs-tabbed tabpanel" role="tabpanel">
                                            <ul class="nav nav-tabs nav-tabs-theme-3 margin-bottom-lg" role="tablist">
                                                <li role="presentation" class="active"><a href="#tabAltaGrupo" aria-controls="tabAltaGrupo" role="tab" data-toggle="tab"><span class="pe-icon fa fa-group icon"></span>
                                                    <br>
                                                    Crear Nuevo Grupo</a> </li>
                                                <li role="presentation"><a href="#tablAltaHorario" aria-controls="tablAltaHorario" role="tab" data-toggle="tab"><span class="pe-icon fa fa-clock-o icon"></span>
                                                    <br>
                                                    Crear Nuevo Horario</a> </li>
                                                <li role="presentation"><a href="#tabAltaDiasFestivos" aria-controls="tabAltaDiasFestivos" role="tab" data-toggle="tab"><span class="pe-icon fa fa-calendar icon"></span>
                                                    <br>
                                                    Crear Nuevo Horario</a> </li>
                                            </ul>

                                            <div class="tab-content text-left">

                                                <div role="tabpanel" class="tab-pane tab-pane fade in active" id="tabAltaGrupo">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div class="row center-block center-content-div centered">
                                                                <div class="form-group margin-top">
                                                                    Nombre del Nuevo grupo<br />
                                                                </div>
                                                                <div class="col-sm-6 col-lg-offset-3">
                                                                    <asp:TextBox runat="server" ID="txtDescripcionGrupoUsuario" placeholder="DESCRIPCION" class="form-control col-sm-3" onkeydown="return (event.keyCode!=13);" />
                                                                </div>
                                                            </div>
                                                            <asp:CheckBox runat="server" ID="chkHabilitado" Checked="True" Visible="False" />
                                                            <div class="form-group" runat="server" id="divParametros" visible="False">
                                                                <asp:RadioButton runat="server" ID="rbtnLevanta" GroupName="gpoContacCenterParamtro" Text="Levanta Tickets" CssClass="col-sm-12" />
                                                                <asp:RadioButton runat="server" ID="rbtnRecado" GroupName="gpoContacCenterParamtro" Text="Levanta Recados" CssClass="col-sm-12" />
                                                            </div>

                                                            <div class="row center-block center-content-div centered">
                                                                <div runat="server" id="divSubRoles" visible="False">
                                                                    <div class="form-group margin-top">
                                                                        Selecciona los niveles de escalación que tendrá este Grupo:<br />
                                                                    </div>
                                                                    <asp:HiddenField runat="server" ID="hfOperacion" />
                                                                    <asp:Repeater runat="server" ID="rptSubRoles" OnItemDataBound="rptSubRoles_OnItemDataBound">
                                                                        <ItemTemplate>
                                                                            <div class="row">
                                                                                <div class="form-horizontal">
                                                                                    <div class="col-lg-4 col-md-4">
                                                                                        <div class="margin-top">
                                                                                            <asp:Label runat="server" ID="lblId" Text='<%# Eval("Id") %>' Visible="False" />
                                                                                            <asp:CheckBox CssClass="btn btn-primary btn-block hideCheck" Style="padding: 0; cursor: default" runat="server" ID="chkSubRol" value='<%# Eval("Id") %>' Text='<%# Eval("Descripcion") %>' AutoPostBack="True" OnCheckedChanged="OnCheckedChanged" />

                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-lg-4 col-md-4">
                                                                                        <div class="margin-top">
                                                                                            <asp:DropDownList runat="server" ID="ddlHorario" CssClass="form-control" Enabled="False" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="col-lg-4 col-md-4">
                                                                                        <div class="margin-top">
                                                                                            <asp:DropDownList runat="server" ID="ddlDiasFeriados" CssClass="form-control" Enabled="False" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <%--<asp:Button runat="server" CssClass="col-sm-2 btn btn-sm btn-primary disabled" Style="margin-left: 5px" CommandArgument='<%# Eval("Id") %>' ID="btnHorarios" Text="Agregar" OnClick="btnHorarios_OnClick" />
                                                            <asp:Button runat="server" CssClass="col-sm-2 btn btn-sm btn-primary disabled" Style="margin-left: 5px" CommandArgument='<%# Eval("Id") %>' ID="btnDiasDescanso" Text="Días Festivos" OnClick="btnDiasDescanso_OnClick" />--%>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row text-right">
                                                                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_OnClick" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div role="tabpanel" class="tab-pane tab-pane fade in" id="tablAltaHorario">
                                                    <div class="row">
                                                        <asp:UpdatePanel runat="server" ID="upHorario" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <uc1:UcAltaHorario runat="server" id="ucAltaHorario" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                                <div role="tabpanel" class="tab-pane tab-pane fade in" id="tabAltaDiasFestivos">
                                                    <div class="row">
                                                        <asp:UpdatePanel runat="server" ID="upDias" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <uc1:UcAltaDiasFestivos runat="server" id="ucAltaDiasFestivos" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Label runat="server" ID="lblOperacion"></asp:Label>
                </div>
            </div>
            <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-danger" Text="Limpiar" OnClick="btnLimpiar_OnClick" Visible="False" />
            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelar_OnClick" Visible="False" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

