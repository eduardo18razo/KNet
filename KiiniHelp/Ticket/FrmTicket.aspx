<%@ Page Title="" Language="C#" MasterPageFile="~/Usuarios.Master" AutoEventWireup="true" CodeBehind="FrmTicket.aspx.cs" Inherits="KiiniHelp.Ticket.FrmTicket" %>

<%@ Register Src="~/UserControls/Temporal/UcMascaraCaptura.ascx" TagPrefix="uc1" TagName="UcMascaraCaptura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hfIdMascara"/>
    <uc1:UcMascaraCaptura runat="server" ID="UcMascaraCaptura" />
</asp:Content>
