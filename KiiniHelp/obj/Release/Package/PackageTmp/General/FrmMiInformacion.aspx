<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmMiInformacion.aspx.cs" Inherits="KiiniHelp.General.FrmMiInformacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#datosPersonales" aria-controls="home" role="tab" data-toggle="tab">Mis Datos Personales</a></li>
            <li role="presentation"><a href="#tickets" aria-controls="profile" role="tab" data-toggle="tab">Mis Tickets</a></li>
        </ul>

        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="datosPersonales">Se muestran los datos personales</div>
            <div role="tabpanel" class="tab-pane" id="tickets">Se muestran los tickets</div>
        </div>

    </div>
</asp:Content>
