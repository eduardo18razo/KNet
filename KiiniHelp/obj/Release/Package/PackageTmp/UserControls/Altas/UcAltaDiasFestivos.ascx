<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcAltaDiasFestivos.ascx.cs" Inherits="KiiniHelp.UserControls.Altas.UcAltaDiasFestivos" %>
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
            <asp:HiddenField runat="server" ID="hfIdSubRol" />

            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Fecha" CssClass="col-sm-2" />
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" type="date" step="1"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Descripción" CssClass="col-sm-2" />
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-1 col-sm-2">
                            <asp:Button runat="server" CssClass="btn btn-primary" Text="Agregar" ID="btnAgregar" OnClick="btnAgregar_OnClick" />
                        </div>
                    </div>
                </div>
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Días Festivos</h3>
                    </div>
                    <div class="panel-body">
                        <asp:Repeater runat="server" ID="rptDias">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="row form-control" style="margin-top: 5px; height: 48px">
                                    <asp:Label runat="server" ID="lblId" Text='<%# Eval("Id") %>' Visible="False" />
                                    <asp:Label runat="server" Text='<%# Eval("IdSubGrupoUsuario") %>' ID="lblSubRol" Visible="False" />
                                    <asp:Label runat="server" Text='<%# Eval("Fecha", "{0:d}") %>' ID="lblFecha" CssClass="col-sm-2" />
                                    <asp:Label runat="server" Text='<%# Eval("Descripcion") %>' ID="lblDescripcion" CssClass="col-sm-7" />
                                    <asp:Button runat="server" class="btn btn-danger col-sm-2 glyphicon-remove" CommandArgument='<%# Eval("Fecha") %>' Text="Quitar" OnClick="OnClick"></asp:Button>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            </div>
            <div class="modal-footer">
                <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-success" Text="Aceptar" OnClick="btnAceptar_OnClick" />
                <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-danger" Text="Limpiar" OnClick="btnLimpiar_OnClick" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelar_OnClick" />
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


