<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcCambiarEstatusTicket.ascx.cs" Inherits="KiiniHelp.UserControls.Operacion.UcCambiarEstatusTicket" %>
<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" Text="status Ticket"/>
                <asp:Label runat="server" ID="lblIdticket"/>
            </div>
             <div class="form-group">
                <asp:Label runat="server" Text="Ticket"/>
                <asp:Label runat="server" ID="Label1"/>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
