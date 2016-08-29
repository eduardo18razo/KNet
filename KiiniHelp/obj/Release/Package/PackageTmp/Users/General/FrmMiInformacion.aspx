<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmMiInformacion.aspx.cs" Inherits="KiiniHelp.Users.General.FrmMiInformacion" %>

<%@ Register Src="~/UserControls/Detalles/UcDetalleUsuario.ascx" TagPrefix="uc1" TagName="UcDetalleUsuario" %>
<%@ Register Src="~/UserControls/Consultas/UcConsultaTicketUsuario.ascx" TagPrefix="uc1" TagName="UcConsultaTicketUsuario" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $('#myTabs a').click(function (e) {
            e.preventDefault();
            $(this).tab('show');
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="exTab2" class="container">
        <ul class="nav nav-tabs">
            <li class="active">
                <a href="#1" data-toggle="tab">Información</a>
            </li>
            <li><a href="#2" data-toggle="tab">Mis tickets</a>
            </li>
        </ul>

        <div class="tab-content ">
            <div class="tab-pane active" id="1">
                <h6>
                    <uc1:UcDetalleUsuario runat="server" ID="UcDetalleUsuario" />
                </h6>
            </div>
        <div class="tab-pane" id="2">
            <h6>
                <uc1:UcConsultaTicketUsuario runat="server" ID="UcConsultaTicketUsuario" />
            </h6>
        </div>
    </div>
</asp:Content>
