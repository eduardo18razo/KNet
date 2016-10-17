<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmConsultaEncuestaNombre.aspx.cs" Inherits="KiiniHelp.Consultas.FrmConsultaEncuestaNombre" %>

<%@ Register Src="~/UserControls/Filtros/Componentes/UcFiltroFechasGrafico.ascx" TagPrefix="uc1" TagName="UcFiltroFechasGrafico" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
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
                <div class="panel-body">
                    <div class="form-inline">
                        <div class="form-group col-sm-12">
                            <asp:Label runat="server" Text="Encuesta" CssClass="col-sm-1" />
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="ddlEncuesta" CssClass="DropSelect" />
                            </div>
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnFiltroFechas" Text="Fechas" OnClick="btnFiltroFechas_OnClick" />
                        </div>
                    </div>
                    <br />
                    <div class="center-content-div">
                        <asp:Chart ID="cGrafico" runat="server" Width="800px" Height="600px" Visible="False">
                            <Titles>
                                <asp:Title ShadowOffset="3" Name="Items" />
                            </Titles>
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" Title="Titulo" />
                            </Legends>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                </div>
                <div class="panel-footer text-center">
                    <asp:Button runat="server" CssClass="btn btn-success" ID="btnbtnGraficar" Text="Graficar" OnClick="btnbtnGraficar_OnClick" />
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-body">
                    <asp:Repeater runat="server">
                        <ItemTemplate>
                            <div class="panel panel-primary">
                                <div class="panel-heading"></div>
                                <div class="panel-body">
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="modalFiltroFechas" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true">
        <asp:UpdatePanel ID="upFiltroFechas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog modal-lg" style="width: 450px">
                    <div class="modal-content" style="width: 450px">
                        <uc1:UcFiltroFechasGrafico runat="server" ID="ucFiltroFechasGrafico" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
