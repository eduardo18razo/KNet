<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FrmConfiguracion.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $('#myTabs a').click(function (e) {
            e.preventDefault()
            $(this).tab('show')
        })
        $('#myTabs a[href="#profile"]').tab('show') // Select tab by name
        $('#myTabs a:first').tab('show') // Select first tab
        $('#myTabs a:last').tab('show') // Select last tab
        $('#myTabs li:eq(2) a').tab('show') // Select third tab (0-indexed)
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation">
                <a href="#aplicacion" aria-controls="profile" role="tab" data-toggle="tab">Aplicaciones</a>
            </li>
            <li role="presentation">
                <a href="#messages" aria-controls="messages" role="tab" data-toggle="tab">Tipo de Usuarios</a>
            </li>
            <li role="presentation">
                <a href="#settings" aria-controls="settings" role="tab" data-toggle="tab">Settings</a>
            </li>
            
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane slide" id="aplicacion">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <asp:Repeater runat="server" ID="rptAplicaciones">
                            <HeaderTemplate>
                                <div class="list-group-item list-group-item-heading" style="border-radius: 10px; height: 50px">
                                    <h4 class="listview-item-heading" style="height: 40px">
                                        <div class="container-fluid">
                                            <div class="row" style="color: #688A08">
                                                <div class="form-inline">
                                                    <div class="form-group" style="display: none">
                                                        <label>id</label>
                                                    </div>
                                                    <div class="col-xs-4 col-sm-2 form-group ">
                                                        <label>Aplicacion</label>
                                                    </div>
                                                    <div class="col-xs-4 col-sm-2 form-group">
                                                        <label>Activado</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </h4>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="<%# Container.ItemIndex % 2 == 0 ? "list-group-item list-group-item-warning" : "list-group-item list-group-item-success" %>" style="border-radius: 10px; height: 50px">
                                    <h4 class="listview-item-heading" style="height: 40px">
                                        <div class="container-fluid">
                                            <div class="row" style="color: #688A08">
                                                <div class="" style="display: none">
                                                    <asp:Label runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                                </div>
                                                <div class="col-xs-4 col-sm-2">
                                                    <label><%# Eval("Descripcion")%></label>
                                                </div>
                                                <div class="col-xs-4 col-sm-2" style="align-items: center">
                                                    <input type="checkbox" runat="server" id="chkAppEnabled" checked='<%# Eval("Activa")%>' />
                                                </div>
                                            </div>
                                        </div>
                                    </h4>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div role="tabpanel" class="tab-pane" id="messages">
                <asp:Repeater runat="server" ID="rptTipoUsuairio">
                    <ItemTemplate>
                        <div class="panel panel-primary">
                            <div class="panel-heading" style="height: 75px">
                                <div class="form-inline">
                                    <div class="form-group" style="display: none">
                                        <asp:Label runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                    </div>
                                    <div class="col-xs-4 col-sm-2 form-group">
                                        <label><%# Eval("Descripcion")%></label>
                                    </div>
                                    <div class="col-xs-4 col-sm-2 form-group" style="align-items: center">
                                        <input type="checkbox" runat="server" id="chkAppEnabled" checked='<%# Eval("Habilitado")%>' />
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div role="tabpanel" class="tab-pane" id="settings">sett</div>
             <div role="tabpanel" class="tab-pane slide" id="polAsignacionDefault"></div>
                  <div role="tabpanel" class="tab-pane slide" id="polAsignacionGrupo"></div>
                  <div role="tabpanel" class="tab-pane slide" id="polEstTicketDefault"></div>
                  <div role="tabpanel" class="tab-pane slide" id="polEstTicketGrupo"></div>
                  <div role="tabpanel" class="tab-pane slide" id="polEscalacion"></div>
        </div>

    </div>
</asp:Content>
