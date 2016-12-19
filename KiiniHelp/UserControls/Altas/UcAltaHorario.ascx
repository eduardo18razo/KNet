<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaHorario.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.UcAltaHorario" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:UpdatePanel runat="server" ID="updateAltaAreas">
    <ContentTemplate>
        <header id="panelAlerta" runat="server" visible="false">
            <div class="alert alert-danger">
                <div>
                    <div style="float: left">
                        <asp:Image runat="server" ImageUrl="~/Images/error.jpg" />
                    </div>
                    <div style="float: left">
                        <h3>Error</h3>
                        <ajaxToolkit:MaskedEditValidator ID="timeStartValidator" runat="server" ControlExtender="timeStarExtender"
                            ControlToValidate="txtHoraInicio" Display="Dynamic"
                            MaximumValue="23:59" MinimumValue="00:00" MaximumValueMessage="Hora maxima es 23:59" MinimumValueMessage="Hora minima es 00:00" SetFocusOnError="True">
                        </ajaxToolkit:MaskedEditValidator>
                        <br />
                        <ajaxToolkit:MaskedEditValidator ID="timeEndValidator" runat="server" ControlExtender="timeEndExtender"
                            ControlToValidate="txtHoraFin" Display="Dynamic" MaximumValue="23:59" MinimumValue="00:00" MaximumValueMessage="Hora maxima es 23:59" MinimumValueMessage="Hora minima es 00:00" CssClass="alert alert-danger"
                            SetFocusOnError="True">
                        </ajaxToolkit:MaskedEditValidator>
                    </div>
                    <div class="clearfix clear-fix" />
                </div>
                <hr />
                <asp:Repeater runat="server" ID="rptErrorGeneral">
                    <ItemTemplate>
                        <ul>
                            <li><%# Container.DataItem %></li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </header>
        <div class="panel panel-primary">
            <div class="panel-heading">
                Asignacion de horarios
            </div>
            <div class="panel-body">
                <asp:HiddenField runat="server" ID="hfIdSubRol" />
                <asp:HiddenField runat="server" ID="hfEsAlta" />
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Grupo" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-4">
                            <asp:DropDownList runat="server" ID="ddlGrupoSolicito" CssClass="DropSelect"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Descripcion" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-4">
                             <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control obligatorio" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Hora Inicio" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);"/>
                            <ajaxToolkit:MaskedEditExtender ID="timeStarExtender" runat="server" AcceptAMPM="False" 
                                                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtHoraInicio"></ajaxToolkit:MaskedEditExtender>

                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Hora Fin" CssClass="col-sm-2 control-label" />
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control" onkeydown="return (event.keyCode!=13 && event.keyCode!=27);"/>
                            <ajaxToolkit:MaskedEditExtender ID="timeEndExtender" runat="server" AcceptAMPM="False"
                                                            Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtHoraFin"></ajaxToolkit:MaskedEditExtender>

                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <asp:CheckBoxList runat="server" RepeatDirection="Horizontal" ID="chklbxDias" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-1 col-sm-11">
                            <asp:Button runat="server" ID="btnAgregar" Text="Agregar" CssClass="btn btn-primary" OnClick="btnAgregar_OnClick" />
                        </div>
                    </div>
                    <br />
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">Grupos asignados</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Repeater runat="server" ID="rptHorarios">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="row form-control" style="margin-top: 5px; height: 48px">
                                        <asp:Label runat="server" ID="lblId" Text='<%# Eval("Id") %>' Visible="False" />
                                        <asp:Label runat="server" ID="lblIdHorario" Text='<%# Eval("IdHorario") %>' Visible="False" />
                                        <asp:Label runat="server" ID="lblDia" Text='<%# Eval("Dia") %>' Visible="False" />
                                        <asp:Label runat="server" Text='<%# (int)Eval("Dia") == 1 ? "Lunes" : (int)Eval("Dia") == 2 ? "MARTES" : (int)Eval("Dia") == 3 ? "MIERCOLES" : (int)Eval("Dia") == 4 ? "JUEVES" : (int)Eval("Dia") == 5 ? "VIERNES" : (int)Eval("Dia") == 6 ? "SABADO" : "DOMINGO"%>' CssClass="col-sm-4" />
                                        <asp:Label runat="server" ID="lblHoraInicio" Text='<%# Eval("HoraInicio") %>' CssClass="col-sm-2" />
                                        <asp:Label runat="server" Text=" - " CssClass="col-sm-1"></asp:Label>
                                        <asp:Label runat="server" ID="lblHoraFin" Text='<%# Eval("HoraFin") %>' CssClass="col-sm-2" />
                                        <asp:Button runat="server" class="btn btn-danger col-sm-2 glyphicon-remove" CommandArgument='<%# Eval("Dia") %>' CommandName='<%# Eval("HoraInicio") %>' Text="Eliminar"  ID="btnEliminar" OnClick="btnEliminar_OnClick"/>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button runat="server" CssClass="btn btn-success" Text="Aceptar" ID="btnAceptar" OnClick="btnAceptar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger" Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_OnClick" />
                <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
