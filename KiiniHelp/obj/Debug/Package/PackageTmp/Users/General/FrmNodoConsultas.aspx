﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmNodoConsultas.aspx.cs" Inherits="KiiniHelp.Users.General.FrmNodoConsultas" %>

<%@ Register Src="~/UserControls/Genericos/UcInformacionConsulta.ascx" TagPrefix="uc1" TagName="UcInformacionConsulta" %>
<%@ Register Src="~/UserControls/Preview/UcPreviewConsulta.ascx" TagPrefix="uc1" TagName="UcPreviewConsulta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
         function OpenWindow(url) {
             window.open( url , "test", 'type=fullWindow, fullscreen, height=700,width=760');
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:UcPreviewConsulta runat="server" id="UcPreviewConsulta" />
</asp:Content>
