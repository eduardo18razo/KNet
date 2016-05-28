<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcMascaraCaptura.ascx.cs" Inherits="KiiniHelp.UserControls.Temporal.UcMascaraCaptura" %>
<asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upMascara">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfIdMascara"/>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <asp:Label runat="server" ID="lblDescripcionMascara"></asp:Label>
            </div>
            <div class="panel-body">
                <div runat="server" id="divControles">
                </div>
            </div>
            <div class="panel-footer">
                <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_OnClick" CssClass="btn btn-lg btn-success"/>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
