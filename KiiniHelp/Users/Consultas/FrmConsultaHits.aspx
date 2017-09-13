<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmConsultaHits.aspx.cs" Inherits="KiiniHelp.Users.Consultas.FrmConsultaHits" %>
<%@ Register Src="~/UserControls/Filtros/Consultas/UcFiltrosConsulta.ascx" TagPrefix="uc1" TagName="UcFiltrosConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="pnlAlertaGral">
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
                    <uc1:UcFiltrosConsulta runat="server" ID="UcFiltrosConsulta" />                    
                </div>
            </div>
            <div class="panel panel-primary">
                <div class="panel-body">
                    <asp:Panel runat="server" ScrollBars="Both" Width="100%" Height="180px">
                    <asp:GridView runat="server" ID="gvResult" CssClass="table table-bordered table-hover table-responsive" AutoGenerateColumns="false" Font-Size="11px">
                        <EmptyDataTemplate>
                         ¡No hay resultados para mostrar!
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Rol"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text=''/>
                                </ItemTemplate>
                            </asp:TemplateField>       
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Grupos"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text=''/>
                                </ItemTemplate>
                            </asp:TemplateField>                       
                             <%--<asp:TemplateField ControlStyle-Width="80px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Tipo de Servicio"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("TipoServicio") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                             <asp:TemplateField ControlStyle-Width="60px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Tipo de Usuario"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("NombreUsuario") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="120px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Organización"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Organizacion") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="120px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Ubicación"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Ubicacion") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField ControlStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Tipificación"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Tipificacion") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="20px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Fecha/Hora"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("FechaHora") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField  ControlStyle-Width="20px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="Total"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("Total") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField ControlStyle-Width="15px">
                                <HeaderTemplate>
                                    <asp:Label runat="server" Text="IdHit"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("IdHit") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
