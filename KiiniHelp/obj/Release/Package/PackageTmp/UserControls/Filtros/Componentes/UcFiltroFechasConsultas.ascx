﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcFiltroFechasConsultas.ascx.cs" Inherits="KiiniHelp.UserControls.Filtros.Componentes.UcFiltroFechasConsultas" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <header class="modal-header" id="panelAlerta" runat="server" visible="false">
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
                <asp:Repeater runat="server" ID="rptError">
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
                Rango de fechas
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label CssClass="col-sm-3" runat="server" Text="Fecha Inicio" />
                        <div class="col-sm-6">
                            <asp:TextBox runat="server" CssClass="form-control" type="date" step="1" ID="txtFechaInicio"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label CssClass="col-sm-3" runat="server" Text="Fecha Fin" />
                        <div class="col-sm-6">
                            <asp:TextBox runat="server" CssClass="form-control" type="date" step="1" ID="txtFechaFin"/>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-footer text-center">
                <asp:Button runat="server" CssClass="btn btn-success" Text="Aceptar" ID="btnAceptar" OnClick="btnAceptar_OnClick"/>
                <asp:Button runat="server" CssClass="btn btn-primary" Text="Limpiar" ID="btnLimpiar" OnClick="btnLimpiar_OnClick"/>
                <asp:Button runat="server" CssClass="btn btn-danger" Text="Cancelar" ID="btnCancelar" OnClick="btnCancelar_OnClick"/>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
