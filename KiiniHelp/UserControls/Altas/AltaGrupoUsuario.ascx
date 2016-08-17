<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltaGrupoUsuario.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.AltaGrupoUsuario" %>
<%@ Register Src="~/UserControls/Altas/UcAltaDiasFestivos.ascx" TagPrefix="uc1" TagName="UcAltaDiasFestivos" %>
<%@ Register Src="~/UserControls/Altas/UcHorario.ascx" TagPrefix="uc1" TagName="UcHorario" %>


<asp:UpdatePanel ID="upGrupoUsuario" runat="server">
    <ContentTemplate>
        <header class="alert alert-danger" id="panelAlerta" runat="server" visible="false">
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
                    <ul>
                        <li><%# Container.DataItem %></li>
                    </ul>
                </ItemTemplate>
            </asp:Repeater>
        </header>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <asp:Label runat="server" Text="Alta Grupo de Usuario" ID="lblTitle"></asp:Label>
            </div>
            <div class="panel-body">
                <div>
                    <asp:HiddenField runat="server" ID="hfIdGrupo" />
                    <asp:HiddenField runat="server" ID="hfFromOpcion" />
                    <asp:HiddenField runat="server" ID="hfIdTipoSubGrupo" />
                    <div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Tipo de Usuario</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList runat="server" ID="ddlTipoUsuarioAltaGrupo" CssClass="DropSelect" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Descripcion</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" ID="txtDescripcionGrupoUsuario" placeholder="DESCRIPCION" class="form-control" onkeydown="return (event.keyCode!=13);" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:CheckBox runat="server" ID="chkHabilitado" Checked="True" Visible="False" />
                                </div>
                            </div>
                            <div class="panel-body" runat="server" id="divSubRoles" visible="False">
                                <asp:HiddenField runat="server" ID="hfOperacion" />
                                <div>
                                    <%-- <div class="form-group">
                                        <div class="form-group">
                                            <asp:CheckBoxList runat="server" ID="chklbxSubRoles" Checked="True" Visible="True" OnSelectedIndexChanged="chklbxSubRoles_OnSelectedIndexChanged" AutoPostBack="True" />
                                        </div>
                                    </div>--%>
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:Repeater runat="server" ID="rptSubRoles" OnItemDataBound="rptSubRoles_OnItemDataBound">
                                                <ItemTemplate>
                                                    <div class="row" style="margin-top: 5px">
                                                        <asp:CheckBox CssClass="col-sm-3" runat="server" ID="chkSubRol" value='<%# Eval("Id") %>' Text='<%# Eval("Descripcion") %>' AutoPostBack="True" OnCheckedChanged="OnCheckedChanged" />
                                                        <asp:Button runat="server" CssClass="col-sm-3 btn btn-primary disabled" Style="margin-left: 5px" CommandArgument='<%# Eval("Id") %>' ID="btnHorarios" Text="Horarios" OnClick="btnHorarios_OnClick" />
                                                        <asp:Button runat="server" CssClass="col-sm-3 btn btn-primary disabled" Style="margin-left: 5px" CommandArgument='<%# Eval("Id") %>' ID="btnDiasDescanso" Text="Días Festivos" OnClick="btnDiasDescanso_OnClick" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer" style="text-align: center">
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardar_OnClick" />
                <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-danger" Text="Limpiar" OnClick="btnLimpiar_OnClick" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>


<%--MODAL HORARO--%>
<div class="modal fade" id="modalHorarios" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true" style="overflow: auto">
    <%--<asp:UpdatePanel ID="upHorarios" runat="server" >
                <ContentTemplate>--%>
    <div class="modal-dialog" >
        <div class="modal-content">
            <uc1:UcHorario runat="server" ID="ucHorario" />
        </div>
    </div>
    <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
</div>

<%--MODAL DIAS DESCANSO--%>
<div class="modal fade" id="modalDiasDescanso" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true" style="overflow: auto">
    <%--<asp:UpdatePanel ID="upDiasDescanso" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div class="modal-dialog">
        <div class="modal-content">
            <uc1:UcAltaDiasFestivos runat="server" ID="ucAltaDiasFestivos" />
        </div>
    </div>
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
