<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmConsultaUsuarios.aspx.cs" Inherits="KiiniHelp.Administracion.Usuarios.FrmConsultaUsuarios" %>

<%@ Register Src="~/UserControls/Consultas/UcConsultaUsuarios.ascx" TagPrefix="uc1" TagName="UcConsultaUsuarios" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <uc1:UcConsultaUsuarios runat="server" id="UcConsultaUsuarios" />

</asp:Content>
