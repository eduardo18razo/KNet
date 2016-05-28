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
        <%--<video src="20160525_163911.mp4" preload="preload" controls></video>
        <object height="320px" width="240px" type="video/x-shockwave-flash">
            <param name="src" value="" />
        </object>--%>
        <video width="640" height="360" controls preload="none">
            <!-- MP4 must be first for iPad! -->
            <source src="20160525_163911.mp4" type="video/mp4" /><!-- WebKit video    -->
            <source src="__VIDEO__.webm" type="video/webm" /><!-- Chrome / Newest versions of Firefox and Opera -->
            <source src="__VIDEO__.OGV" type="video/ogg" /><!-- Firefox / Opera -->
            <!-- fallback to Flash: -->
            <object width="640" height="384" type="application/x-shockwave-flash" data="__FLASH__.SWF">
                <!-- Firefox uses the `data` attribute above, IE/Safari uses the param below -->
                <param name="movie" value="__FLASH__.SWF" />
                <param name="flashvars" value="image=__POSTER__.JPG&amp;file=__VIDEO__.MP4" />
                <!-- fallback image. note the title field below, put the title of the video there -->
                <img src="__VIDEO__.JPG" width="640" height="360" alt="__TITLE__"
                    title="No video playback capabilities, please download the video below" />
            </object>
        </video>
    </div>
</asp:Content>
