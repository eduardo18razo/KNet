<%@ Page Title="Consulta Ticket" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="FrmConsultaTicket.aspx.cs" Inherits="KiiniHelp.Publico.Consultas.FrmConsultaTicket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullheight">
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 1.0;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loading.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 25%; left: 35%; border: 10px;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <header class="modal-header" id="pnlAlertaGeneral" runat="server" visible="false">
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
                    <div class="panel-heading">
                        <h3>Consultar ticket</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Ticket" CssClass="col-sm-1" />
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTicket" />
                                </div>
                                <asp:Label runat="server" Text="Clave registro" CssClass="col-sm-2" />
                                <div class="col-sm-2">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtClave" />
                                </div>
                                <asp:Button runat="server" Text="Consultar" ID="btnConsultar" CssClass="btn btn-primary" OnClick="btnConsultar_OnClick" />
                            </div>
                        </div>
                        <div class="form-horizontal" runat="server" id="divResultado" visible="False">
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-xs-2" Text="Ticket " />
                                <div class="col-xs-10">
                                    <asp:Label runat="server" CssClass="form-control" ID="lblticket" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-xs-2" Text="Estatus Actual " />
                                <div class="col-xs-10">
                                    <asp:Label runat="server" CssClass="form-control" ID="lblestatus" />
                                </div>

                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-xs-2" Text="Asignacion Actual " />
                                <div class="col-xs-10">
                                    <asp:Label runat="server" CssClass="form-control" ID="lblAsignacion" />
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" CssClass="col-xs-2" Text="Fecha y hora Creacion " />
                                <div class="col-xs-10">
                                    <asp:Label runat="server" CssClass="form-control" ID="lblfecha" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
