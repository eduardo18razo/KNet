<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcLogCopia.ascx.cs" Inherits="KiiniHelp.UserControls.UcLogCopia" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>


<div class="login-form" role="form">
    <div class="form-group email">
        <label class="sr-only">Email or username</label>
        <span class="fa fa-user icon"></span>
        <asp:TextBox class="form-control login-email" ID="txtUsuario" placeholder="Correo electrónico" runat="server" Style="text-transform: none" autofocus="autofocus"></asp:TextBox>
    </div>
    <div class="form-group password">
        <label class="sr-only">Password</label>
        <span class="fa fa-lock icon"></span>
        <asp:TextBox class="form-control login-password" ID="txtpwd" placeholder="Enter Password" runat="server" TextMode="Password" Style="text-transform: none"></asp:TextBox>
        <p class="forgot-password">
            <asp:LinkButton ID="lnkBtnRecuperar" runat="server" Text="¿Olvidaste tu contraseña?" OnClick="lnkBtnRecuperar_OnClick"></asp:LinkButton>
        </p>
    </div>
    <div class="checkbox remember">
        <label>
            <input type="checkbox">No soy un Robot
        </label>
    </div>
    <asp:Button CssClass="btn btn-block btn-primary" ID="btnLogin" OnClick="btnLogin_Click" runat="server" Text="Acceder"></asp:Button>
    <div class="checkbox remember">
        <label>
            <input type="checkbox">
            Recordarme
								
        </label>
    </div>
    
    <p class="alt-path">¿No tienes una cuenta? <a class="signup-link" href="signup.html">Regístrate aquí</a></p>
    <div class="form-group">
        <asp:CustomValidator ErrorMessage="" OnServerValidate="OnServerValidate" runat="server" />
        <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="4" CssClass="col-sm-2"
            CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
            FontColor="#D20B0C" NoiseColor="#B1B1B1" />
    </div>
    <div class="form-group">
        <div class="col-sm-3">
            <asp:TextBox class="form-control obligatorio" ID="txtCaptcha" runat="server"></asp:TextBox>
        </div>
    </div>
</div>


<asp:Button CssClass="btn btn-danger" ID="btnCacelar" OnClick="btnCacelar_OnClick" runat="server" Text="Cancelar" Visible="False"></asp:Button>
