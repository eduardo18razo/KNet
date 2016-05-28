<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KiiniHelp._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="UTF-8">
    <title>Home</title>
    <meta name="generator" content="Serif WebPlus X8 (16,0,4,32)">
    <meta name="viewport" content="width=980">
    <link rel="stylesheet" type="text/css" href="wpscripts/wpstyles.css">
    <style type="text/css">
      @font-face { font-family: 'Neuropol'; src: url('wpscripts/wp138308dc.ttf'); }
      .C-1 { line-height:67.00px;font-family:"Neuropol", sans-serif;font-style:normal;font-weight:normal;color:#663694;background-color:transparent;text-decoration:none;font-variant:normal;font-size:64.0px;vertical-align:0; }
      @font-face { font-family: 'TitilliumMaps29L'; src: url('wpscripts/wpa6ffa686.ttf'); }
      .C-2 { line-height:28.00px;font-family:"TitilliumMaps29L", monospace;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:24.0px;vertical-align:0; }
      .C-3 { line-height:18.00px;font-family:"Verdana", sans-serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:16.0px;vertical-align:0; }
      .C-4 { line-height:21.00px;font-family:"Neuropol", sans-serif;font-style:normal;font-weight:normal;color:#663694;background-color:transparent;text-decoration:none;font-variant:normal;font-size:18.7px;vertical-align:0; }
      .C-5 { line-height:16.00px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#000000;background-color:transparent;text-decoration:none;font-variant:normal;font-size:13.3px;vertical-align:0; }
      .C-6 { line-height:16.00px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:13.3px;vertical-align:0; }
      .C-7 { line-height:16.00px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:13.3px;vertical-align:0; }
      a.C-7:link, a:link .C-7, a.C-7:visited, a:visited .C-7, a.C-7:hover, a:hover .C-7, a.C-7:active, a:active .C-7 { color:#636567;text-decoration:none; }
      .C-8 { line-height:16.00px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:13.3px;vertical-align:0; }
      a.C-8:link, a:link .C-8, a.C-8:visited, a:visited .C-8, a.C-8:hover, a:hover .C-8, a.C-8:active, a:active .C-8 { color:inherit;text-decoration:inherit; }
      .C-9 { line-height:18.00px;font-family:"Georgia", serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:16.0px;vertical-align:0; }
      .P-1 { margin-bottom:2.7px;line-height:1px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;font-variant:normal;font-size:13.0px;vertical-align:0; }
      .OBJ-1 { background:transparent url('wpimages/wp931ecf8d_06.png') no-repeat 0px 0px; }
      .OBJ-2,.OBJ-2:link,.OBJ-2:visited { background-image:url('wpimages/wp936f0386_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-2:hover { background-position:0px -96px; }
      .OBJ-2:active,a:link.OBJ-2.Activated,a:link.OBJ-2.Down,a:visited.OBJ-2.Activated,a:visited.OBJ-2.Down,.OBJ-2.Activated,.OBJ-2.Down { background-position:0px -48px; }
      .OBJ-2:focus { outline-style:none; }
      button.OBJ-2 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-2:disabled { pointer-events:none; }
      .OBJ-2.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-2 span,.OBJ-2:link span,.OBJ-2:visited span { color:#ffffff;font-family:Verdana,sans-serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:0px;top:13px;width:220px;height:24px;line-height:24px;font-size:19px;display:block;position:absolute;cursor:pointer; }
      .OBJ-3 { line-height:48px; }
      .OBJ-4,.OBJ-4:link,.OBJ-4:visited { background-image:url('wpimages/wp3fcd58aa_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-4:hover { background-position:0px -96px; }
      .OBJ-4:active,a:link.OBJ-4.Activated,a:link.OBJ-4.Down,a:visited.OBJ-4.Activated,a:visited.OBJ-4.Down,.OBJ-4.Activated,.OBJ-4.Down { background-position:0px -48px; }
      .OBJ-4:focus { outline-style:none; }
      button.OBJ-4 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-4:disabled { pointer-events:none; }
      .OBJ-4.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-4 span,.OBJ-4:link span,.OBJ-4:visited span { color:#ffffff;font-family:Verdana,sans-serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:0px;top:13px;width:225px;height:24px;line-height:24px;font-size:19px;display:block;position:absolute;cursor:pointer; }
      .OBJ-5,.OBJ-5:link,.OBJ-5:visited { background-image:url('wpimages/wp8981f712_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-5:hover { background-position:0px -96px; }
      .OBJ-5:active,a:link.OBJ-5.Activated,a:link.OBJ-5.Down,a:visited.OBJ-5.Activated,a:visited.OBJ-5.Down,.OBJ-5.Activated,.OBJ-5.Down { background-position:0px -48px; }
      .OBJ-5:focus { outline-style:none; }
      button.OBJ-5 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-5:disabled { pointer-events:none; }
      .OBJ-5.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-5 span,.OBJ-5:link span,.OBJ-5:visited span { color:#ffffff;font-family:Verdana,sans-serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:0px;top:13px;width:233px;height:24px;line-height:24px;font-size:19px;display:block;position:absolute;cursor:pointer; }
      .OBJ-6,.OBJ-6:link,.OBJ-6:visited { background-image:url('wpimages/wpcf3e27c9_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-6:hover { background-position:0px -96px; }
      .OBJ-6:active,a:link.OBJ-6.Activated,a:link.OBJ-6.Down,a:visited.OBJ-6.Activated,a:visited.OBJ-6.Down,.OBJ-6.Activated,.OBJ-6.Down { background-position:0px -48px; }
      .OBJ-6:focus { outline-style:none; }
      button.OBJ-6 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-6:disabled { pointer-events:none; }
      .OBJ-6.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-6 span,.OBJ-6:link span,.OBJ-6:visited span { color:#ffffff;font-family:Verdana,sans-serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:0px;top:13px;width:250px;height:24px;line-height:24px;font-size:19px;display:block;position:absolute;cursor:pointer; }
      .C-10 { line-height:23.00px;font-family:"Georgia", serif;font-style:normal;font-weight:normal;color:#ffffff;background-color:transparent;text-decoration:none;font-variant:normal;font-size:18.7px;vertical-align:0; }
      .C-11 { line-height:18.00px;font-family:"Verdana", sans-serif;font-style:normal;font-weight:normal;color:#000000;background-color:transparent;text-decoration:none;font-variant:normal;font-size:14.7px;vertical-align:0; }
      .C-12 { line-height:18.00px;font-family:"Georgia", serif;font-style:normal;font-weight:normal;color:#000000;background-color:transparent;text-decoration:none;font-variant:normal;font-size:14.7px;vertical-align:0; }
      .C-13 { line-height:16.00px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#663694;background-color:transparent;text-decoration:none;font-variant:normal;font-size:13.3px;vertical-align:0; }
      .P-2 { text-align:center;line-height:1px;font-family:"Georgia", serif;font-style:normal;font-weight:normal;color:#000000;background-color:transparent;font-variant:normal;font-size:15.0px;vertical-align:0; }
      .OBJ-7,.OBJ-7:link,.OBJ-7:visited { background-image:url('wpimages/wp9952e8da_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-7:hover { background-position:0px -66px; }
      .OBJ-7:active,a:link.OBJ-7.Activated,a:link.OBJ-7.Down,a:visited.OBJ-7.Activated,a:visited.OBJ-7.Down,.OBJ-7.Activated,.OBJ-7.Down { background-position:0px -33px; }
      .OBJ-7:focus { outline-style:none; }
      button.OBJ-7 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-7:disabled { pointer-events:none; }
      .OBJ-7.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-7 span,.OBJ-7:link span,.OBJ-7:visited span { color:#00122d;font-family:Georgia,serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:10px;top:5px;width:161px;height:20px;line-height:20px;font-size:15px;display:block;position:absolute;cursor:pointer; }
      .OBJ-8 { line-height:33px; }
      .OBJ-9,.OBJ-9:link,.OBJ-9:visited { background-image:url('wpimages/wp0b495dc6_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-9:hover { background-position:0px -66px; }
      .OBJ-9:active,a:link.OBJ-9.Activated,a:link.OBJ-9.Down,a:visited.OBJ-9.Activated,a:visited.OBJ-9.Down,.OBJ-9.Activated,.OBJ-9.Down { background-position:0px -33px; }
      .OBJ-9:focus { outline-style:none; }
      button.OBJ-9 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-9:disabled { pointer-events:none; }
      .OBJ-9.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-9 span,.OBJ-9:link span,.OBJ-9:visited span { color:#00122d;font-family:Georgia,serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:10px;top:5px;width:147px;height:20px;line-height:20px;font-size:15px;display:block;position:absolute;cursor:pointer; }
      .OBJ-10 { margin:0; }
      .OBJ-11 { background:#ffffff;font-family:Tahoma;font-size:16px;color:#000000;text-decoration:none;font-style:normal;font-variant:normal;font-weight:Normal;text-align:left; }
      .OBJ-12,.OBJ-12:link,.OBJ-12:visited { background-image:url('wpimages/wp9161e45f_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-12:hover { background-position:0px -66px; }
      .OBJ-12:active,a:link.OBJ-12.Activated,a:link.OBJ-12.Down,a:visited.OBJ-12.Activated,a:visited.OBJ-12.Down,.OBJ-12.Activated,.OBJ-12.Down { background-position:0px -33px; }
      .OBJ-12:focus { outline-style:none; }
      button.OBJ-12 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-12:disabled { pointer-events:none; }
      .OBJ-12.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-12 span,.OBJ-12:link span,.OBJ-12:visited span { color:#00122d;font-family:Georgia,serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:10px;top:5px;width:80px;height:20px;line-height:20px;font-size:15px;display:block;position:absolute;cursor:pointer; }
      .C-14 { line-height:23.00px;font-family:"Georgia", serif;font-style:normal;font-weight:normal;color:#001488;background-color:transparent;text-decoration:none;font-variant:normal;font-size:18.7px;vertical-align:0; }
      .P-3 { text-align:center;line-height:1px;font-family:"Georgia", serif;font-style:normal;font-weight:normal;color:#001488;background-color:transparent;font-variant:normal;font-size:16.0px;vertical-align:0; }
      .C-15 { line-height:18.00px;font-family:"Georgia", serif;font-style:normal;font-weight:normal;color:#001488;background-color:transparent;text-decoration:none;font-variant:normal;font-size:16.0px;vertical-align:0; }
      .C-16 { line-height:18.00px;font-family:"Verdana", sans-serif;font-style:normal;font-weight:normal;color:#663694;background-color:transparent;text-decoration:none;font-variant:normal;font-size:16.0px;vertical-align:0; }
      .C-17 { line-height:18.00px;font-family:"Verdana", sans-serif;font-style:normal;font-weight:normal;color:#001488;background-color:transparent;text-decoration:none;font-variant:normal;font-size:16.0px;vertical-align:0; }
      .C-18 { line-height:19.00px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:16.0px;vertical-align:0; }
      .OBJ-13,.OBJ-13:link,.OBJ-13:visited { background-image:url('wpimages/wpc653b62f_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-13:hover { background-position:0px -66px; }
      .OBJ-13:active,a:link.OBJ-13.Activated,a:link.OBJ-13.Down,a:visited.OBJ-13.Activated,a:visited.OBJ-13.Down,.OBJ-13.Activated,.OBJ-13.Down { background-position:0px -33px; }
      .OBJ-13:focus { outline-style:none; }
      button.OBJ-13 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-13:disabled { pointer-events:none; }
      .OBJ-13.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-13 span,.OBJ-13:link span,.OBJ-13:visited span { color:#00122d;font-family:Georgia,serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:10px;top:5px;width:118px;height:20px;line-height:20px;font-size:15px;display:block;position:absolute;cursor:pointer; }
      .OBJ-14,.OBJ-14:link,.OBJ-14:visited { background-image:url('wpimages/wpae24440c_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-14:hover { background-position:0px -66px; }
      .OBJ-14:active,a:link.OBJ-14.Activated,a:link.OBJ-14.Down,a:visited.OBJ-14.Activated,a:visited.OBJ-14.Down,.OBJ-14.Activated,.OBJ-14.Down { background-position:0px -33px; }
      .OBJ-14:focus { outline-style:none; }
      button.OBJ-14 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-14:disabled { pointer-events:none; }
      .OBJ-14.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-14 span,.OBJ-14:link span,.OBJ-14:visited span { color:#00122d;font-family:Georgia,serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:10px;top:5px;width:110px;height:20px;line-height:20px;font-size:15px;display:block;position:absolute;cursor:pointer; }
      .OBJ-15,.OBJ-15:link,.OBJ-15:visited { background-image:url('wpimages/wp7fb2cd02_06.png');background-repeat:no-repeat;background-position:0px 0px;text-decoration:none;display:block;position:absolute; }
      .OBJ-15:hover { background-position:0px -66px; }
      .OBJ-15:active,a:link.OBJ-15.Activated,a:link.OBJ-15.Down,a:visited.OBJ-15.Activated,a:visited.OBJ-15.Down,.OBJ-15.Activated,.OBJ-15.Down { background-position:0px -33px; }
      .OBJ-15:focus { outline-style:none; }
      button.OBJ-15 { background-color:transparent;border:none 0px;padding:0;display:inline-block;cursor:pointer; }
      button.OBJ-15:disabled { pointer-events:none; }
      .OBJ-15.Inline { display:inline-block;position:relative;line-height:normal; }
      .OBJ-15 span,.OBJ-15:link span,.OBJ-15:visited span { color:#00122d;font-family:Georgia,serif;font-weight:normal;text-decoration:none;text-align:center;text-transform:none;font-style:normal;left:10px;top:5px;width:124px;height:20px;line-height:20px;font-size:15px;display:block;position:absolute;cursor:pointer; }
      .C-19 { line-height:19.00px;font-family:"Arial", sans-serif;font-style:normal;font-weight:normal;color:#636567;background-color:transparent;text-decoration:none;font-variant:normal;font-size:16.0px;vertical-align:0; }
      a.C-19:link, a:link .C-19 { color:#c8012c;text-decoration:none; }
      a.C-19:visited, a:visited .C-19 { color:#c8012c;text-decoration:none; }
      a.C-19:hover, a:hover .C-19 { color:#001488;text-decoration:none; }
      a.C-19:active, a:active .C-19 { color:#c8012c;text-decoration:none; }
    </style>
    <script type="text/javascript" src="wpscripts/jquery.js"></script>
    <script type="text/javascript" src="wpscripts/jquery.wputils.js"></script>
    <script type="text/javascript" src="wpscripts/jquery.validate.min.js"></script>
    <script type="text/javascript">
        wpRedirectMobile('http://kiininet.com/m.index.html', 0);
        $(document).ready(function () {
            $("#form_5").validate({ onkeyup: false, showErrors: function (errorMap, errorList) { if (errorList.length) alert(errorList[0].message); }, onclick: false, rules: { 'NAME': { required: true }, 'NAME': { required: true }, 'EMAIL': { email: true } }, onfocusout: false, messages: { 'NAME': { required: "Please give your full name" }, 'NAME': { required: "Please give your full name" }, 'EMAIL': { email: "Please give your e-mail address" } } });
            $("a.ActiveButton").bind({ mousedown: function () { if ($(this).attr('disabled') === undefined) $(this).addClass('Activated'); }, mouseleave: function () { if ($(this).attr('disabled') === undefined) $(this).removeClass('Activated'); }, mouseup: function () { if ($(this).attr('disabled') === undefined) $(this).removeClass('Activated'); } });
            $('#btn_4').click(function (e) { alert('Thanks now you are already Sign Up for Updates!'); e.stopPropagation() });
        });
    </script>
    <style type="text/css">
 #twitter-widget-0 {
 
 width:190px;
 height:285px;

}
 
 </style>
       <link rel="stylesheet" href="nivo-default.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="nivo-slider.css" type="text/css" media="screen" />
    <script type="text/javascript" src="jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="jquery.nivo.slider.js"></script>
    
    <script type="text/javascript">
        $.noConflict();
        jQuery(document).ready(function ($) {
            $('#slider').nivoSlider({
                effect: 'random',
                slices: 5,
                animSpeed: 1000,
                pauseTime: 3000,
                captionOpacity: 0.8,
                controlNav: true,
                directionNav: false,
                directionNavHide: false,
                pauseonhover: true
            });
        });
    </script>

<style type="text/css">
    .theme-default #slider {
    margin:0px 0px 0px 0px;
    height:380px;
    width:960px;
	}

    .theme-default .nivo-caption {
    bottom:20px;
    width:40%;
    height:10%;
    background:#636567;
    color:#ffffff;
    font-size:24px;
    font-weight:normal;
    }

</style>

</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="divMain" style="background:transparent;margin-left:auto;margin-right:auto;position:relative;width:980px;height:2440px;">
      <img alt="" src="wpimages/wp70dcdcb9_06.png" style="position:absolute;left:10px;top:544px;width:960px;height:1896px;">
      <div style="position:absolute;left:8px;top:18px;width:371px;height:61px;overflow:hidden;">
        <p class="Body"><span class="C-1">Kiininet</span></p>
      </div>
      <div style="position:absolute;left:8px;top:79px;width:487px;height:28px;overflow:hidden;">
        <p class="Body"><span class="C-2">Vision Strategic Technologic</span></p>
      </div>
      <img alt="" src="wpimages/wpaf25a1bd_06.png" style="position:absolute;left:10px;top:2014px;width:960px;height:360px;">
      <img alt="" src="wpimages/wpdcd519e9_06.png" style="position:absolute;left:10px;top:2375px;width:960px;height:65px;">
      <img alt="" src="wpimages/wp04fad44b_06.png" style="position:absolute;left:10px;top:2015px;width:960px;height:1px;">
      <img alt="" src="wpimages/wp3704e3d3_06.png" style="position:absolute;left:10px;top:2373px;width:960px;height:2px;">
      <div style="position:absolute;left:20px;top:2050px;width:220px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-3">ABOUT THE COMPANY</span></p>
      </div>
      <div style="position:absolute;left:20px;top:2090px;width:220px;height:250px;overflow:hidden;">
        <p class="Body"><span class="C-4">Kiininet</span></p>
        <p class="Body"><span class="C-5">Will born in January 2016 and be oriented those companies that need provide better service to customer and also for employees given them the facility to solve his questions, request or incidents in a modern Kristal Web Self Service Portal.</span></p>
        <p class="Body"><span class="C-5">&nbsp;</span></p>
      </div>
      <div style="position:absolute;left:20px;top:2398px;width:329px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-6">© December 2015</span></p>
      </div>
      <div style="position:absolute;left:735px;top:2398px;width:225px;height:20px;overflow:hidden;">
        <p class="Body"><a href="contact.html" class="C-7">Contact</a><span class="C-6"> &nbsp;&nbsp;&nbsp;&nbsp;<a href="internship.html" class="C-8">Interns</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href="privacy.html" class="C-8">Privacy</a> &nbsp;&nbsp;&nbsp;&nbsp;<a href="terms.html" class="C-8">Terms</a></span></p>
      </div>
      <div style="position:absolute;left:267px;top:2050px;width:180px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-9">LATEST NEWS</span></p>
      </div>
      <img alt="" src="wpimages/wpe15c6b8a_05_06.jpg" style="position:absolute;left:267px;top:2090px;width:58px;height:58px;">
      <div style="position:absolute;left:332px;top:2091px;width:212px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-6">LATEST NEWS NUMBER ONE</span></p>
      </div>
      <div style="position:absolute;left:332px;top:2111px;width:212px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">. </span></p>
      </div>
      <img alt="" src="wpimages/wp9ad04001_06.png" style="position:absolute;left:267px;top:2166px;width:280px;height:1px;">
      <img alt="" src="wpimages/wpe15c6b8a_05_06.jpg" style="position:absolute;left:267px;top:2176px;width:58px;height:58px;">
      <div style="position:absolute;left:332px;top:2177px;width:212px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-6">LATEST NEWS NUMBER TWO</span></p>
      </div>
      <div style="position:absolute;left:332px;top:2196px;width:212px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">&nbsp;</span></p>
      </div>
      <img alt="" src="wpimages/wp9ad04001_06.png" style="position:absolute;left:267px;top:2252px;width:280px;height:1px;">
      <img alt="" src="wpimages/wpe15c6b8a_05_06.jpg" style="position:absolute;left:267px;top:2268px;width:58px;height:58px;">
      <div style="position:absolute;left:332px;top:2269px;width:212px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-6">LATEST NEWS NUMBER THREE</span></p>
      </div>
      <div style="position:absolute;left:332px;top:2288px;width:212px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">. </span></p>
      </div>
      <div style="position:absolute;left:800px;top:2050px;width:170px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-9">CONTACT US</span></p>
      </div>
      <div style="position:absolute;left:800px;top:2090px;width:170px;height:150px;overflow:hidden;">
        <p class="Body P-1"><span class="C-6">Estrella Errante 28</span></p>
        <p class="Body P-1"><span class="C-6">Col. Prados de Coyoacan</span></p>
        <p class="Body P-1"><span class="C-6">Del. Coyoacan</span></p>
        <p class="Body P-1"><span class="C-6">Mexico DF</span></p>
        <p class="Body P-1"><span class="C-6">CP 04810</span></p>
        <p class="Body P-1"><span class="C-6"><br></span></p>
        <p class="Body P-1"><a href="contact.html" class="C-7">Tel: 55-<wbr>5678-<wbr>2013</a></p>
        <p class="Body P-1"><a href="contact.html" class="C-7"><br></a></p>
        <p class="Body P-1"><span class="C-6"><br></span></p>
        <p class="Body P-1"><span class="C-6">Info@yourdomain.com</span></p>
      </div>
      <img alt="" src="wpimages/wp3e3a9306_06.png" style="position:absolute;left:969px;top:544px;width:1px;height:1896px;">
      <img alt="" src="wpimages/wp3e3a9306_06.png" style="position:absolute;left:10px;top:544px;width:1px;height:1896px;">
      <img alt="" src="wpimages/wp6b08e20f_06.png" style="position:absolute;left:10px;top:116px;width:960px;height:48px;">
      <div style="position:absolute;left:582px;top:2050px;width:180px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-9">TWITTER FEED</span></p>
      </div>
      <div style="position:absolute;left:583px;top:2075px;width:191px;height:285px;">
        <a class="twitter-timeline" data-tweet-limit="2" data-link-color="#636567" data-chrome="noheader nofooter transparent" data-dnt="true" href="https://twitter.com/iPlus360"  data-widget-id="344793793951842304">Tweets by @iPlus360</a>
<script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + "://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>

      </div>
      <div class="OBJ-1" style="position:absolute;left:15px;top:111px;width:958px;height:58px;">
        <a href="index.html" id="nav_426_B1" class="OBJ-2 ActiveButton Down OBJ-3" style="display:block;position:absolute;left:0px;top:5px;width:220px;height:48px;">
          <span>Home</span>
        </a>
        <a href="about.html" id="nav_426_B2" class="OBJ-4 ActiveButton OBJ-3" style="display:block;position:absolute;left:230px;top:5px;width:225px;height:48px;">
          <span>About</span>
        </a>
        <a href="services.html" id="nav_426_B3" class="OBJ-5 ActiveButton OBJ-3" style="display:block;position:absolute;left:465px;top:5px;width:233px;height:48px;">
          <span>Services</span>
        </a>
        <a href="contact.html" id="nav_426_B4" class="OBJ-6 ActiveButton OBJ-3" style="display:block;position:absolute;left:708px;top:5px;width:250px;height:48px;">
          <span>Contact</span>
        </a>
      </div>
      <a href="index.html">
        <img alt="Home" src="wpimages/wp5533b116.gif" style="position:absolute;left:15px;top:23px;width:516px;height:112px;">
      </a>
      <img alt="" src="wpimages/wpb79d7131_06.png" style="position:absolute;left:680px;top:544px;width:1px;height:1472px;">
      <img alt="" src="wpimages/wpae22cb19_06.png" style="position:absolute;left:680px;top:767px;width:290px;height:40px;">
      <div style="position:absolute;left:691px;top:774px;width:262px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-10">CONNECT WITH US</span></p>
      </div>
      <div style="position:absolute;left:759px;top:827px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-11">FOLLOW US ON TWITTER</span></p>
      </div>
      <div style="position:absolute;left:759px;top:850px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">. Kiininet</span></p>
      </div>
      <img alt="" src="wpimages/wp902ca929_06.png" style="position:absolute;left:690px;top:892px;width:265px;height:1px;">
      <img src="wpimages/wpa93ea8b6_06.png" alt="" width="35" height="25" style="position:absolute;left:709px;top:846px;width:35px;height:25px;">
      <div style="position:absolute;left:759px;top:999px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-12">A GLOBAL COMMUNITY</span></p>
      </div>
      <div style="position:absolute;left:759px;top:1022px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">Soon</span></p>
      </div>
      <img alt="" src="wpimages/wp902ca929_06.png" style="position:absolute;left:690px;top:1064px;width:265px;height:1px;">
      <img src="wpimages/wp09c17fd0_06.png" alt="" width="33" height="33" style="position:absolute;left:711px;top:1014px;width:33px;height:33px;">
      <img alt="" src="wpimages/wp2748d385_06.png" style="position:absolute;left:729px;top:1032px;width:1px;height:0px;">
      <img alt="" src="wpimages/wpb39627cc_06.png" style="position:absolute;left:727px;top:1032px;width:1px;height:0px;">
      <img alt="" src="wpimages/wp72857996_06.png" style="position:absolute;left:726px;top:1016px;width:3px;height:2px;">
      <img alt="" src="wpimages/wpb39627cc_06.png" style="position:absolute;left:725px;top:1016px;width:1px;height:1px;">
      <img alt="" src="wpimages/wpb39627cc_06.png" style="position:absolute;left:725px;top:1017px;width:1px;height:0px;">
      <img alt="" src="wpimages/wp43237b45_06.png" style="position:absolute;left:724px;top:1017px;width:1px;height:0px;">
      <img alt="" src="wpimages/wpd39c66d4_06.png" style="position:absolute;left:724px;top:1017px;width:1px;height:0px;">
      <img alt="" src="wpimages/wp852fa2f7_06.png" style="position:absolute;left:723px;top:1018px;width:1px;height:1px;">
      <img alt="" src="wpimages/wp32897b9f_06.png" style="position:absolute;left:721px;top:1018px;width:2px;height:0px;">
      <img alt="" src="wpimages/wp46232c56_06.png" style="position:absolute;left:722px;top:1017px;width:1px;height:0px;">
      <div style="position:absolute;left:759px;top:913px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-12">IMPORTANT FACTS</span></p>
      </div>
      <div style="position:absolute;left:759px;top:936px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">Soon 2016</span></p>
      </div>
      <img alt="" src="wpimages/wp902ca929_06.png" style="position:absolute;left:690px;top:978px;width:265px;height:1px;">
      <img src="wpimages/wp92c966c9_06.png" alt="" width="35" height="10" style="position:absolute;left:710px;top:951px;width:35px;height:10px;">
      <img alt="" src="wpimages/wp868c11a2_06.png" style="position:absolute;left:713px;top:937px;width:8px;height:20px;">
      <img alt="" src="wpimages/wp8e878173_06.png" style="position:absolute;left:723px;top:928px;width:8px;height:29px;">
      <img alt="" src="wpimages/wp70f00073_06.png" style="position:absolute;left:733px;top:944px;width:8px;height:14px;">
      <img alt="" src="wpimages/wpe4329e57_06.png" style="position:absolute;left:714px;top:938px;width:6px;height:1px;">
      <img alt="" src="wpimages/wp0cef2d5f_06.png" style="position:absolute;left:724px;top:929px;width:6px;height:1px;">
      <img alt="" src="wpimages/wpe4329e57_06.png" style="position:absolute;left:734px;top:944px;width:6px;height:1px;">
      <div style="position:absolute;left:759px;top:1085px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-12">EVENTS WE’RE HOLDING</span></p>
      </div>
      <div style="position:absolute;left:759px;top:1108px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">January 2016 </span></p>
      </div>
      <img alt="" src="wpimages/wpf2d0ab7e_06.png" style="position:absolute;left:714px;top:1099px;width:25px;height:34px;">
      <img alt="" src="wpimages/wp5631816c_06.png" style="position:absolute;left:741px;top:1117px;width:3px;height:11px;">
      <img alt="" src="wpimages/wp96055def_06.png" style="position:absolute;left:741px;top:1105px;width:3px;height:11px;">
      <img alt="" src="wpimages/wpe1e0ce1d_06.png" style="position:absolute;left:711px;top:1105px;width:8px;height:3px;">
      <img alt="" src="wpimages/wpe1e0ce1d_06.png" style="position:absolute;left:711px;top:1115px;width:8px;height:3px;">
      <img alt="" src="wpimages/wpe1e0ce1d_06.png" style="position:absolute;left:711px;top:1124px;width:8px;height:3px;">
      <img alt="" src="wpimages/wpae22cb19_06.png" style="position:absolute;left:680px;top:1176px;width:290px;height:40px;">
      <div style="position:absolute;left:691px;top:1183px;width:280px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-10">COMPANY EVENTS</span></p>
      </div>
      <div style="position:absolute;left:759px;top:1236px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-12">EVENT NUMBER ONE</span></p>
      </div>
      <div style="position:absolute;left:759px;top:1259px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">January 2016 will be founded</span></p>
      </div>
      <img alt="" src="wpimages/wp082bf5ed_06.png" style="position:absolute;left:690px;top:1301px;width:270px;height:1px;">
      <div style="position:absolute;left:759px;top:1408px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-12">EVENT NUMBER THREE</span></p>
      </div>
      <div style="position:absolute;left:759px;top:1431px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">March 2016 Stay with us</span></p>
      </div>
      <img alt="" src="wpimages/wp082bf5ed_06.png" style="position:absolute;left:690px;top:1473px;width:270px;height:1px;">
      <div style="position:absolute;left:759px;top:1322px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-12">EVENT NUMBER TWO</span></p>
      </div>
      <div style="position:absolute;left:759px;top:1345px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-13">Kiininoc <span class="C-6">go to the market end of February 2016</span></span></p>
      </div>
      <img alt="" src="wpimages/wp082bf5ed_06.png" style="position:absolute;left:690px;top:1387px;width:270px;height:1px;">
      <div style="position:absolute;left:759px;top:1494px;width:211px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-12">EVENT NUMBER FOUR</span></p>
      </div>
      <div style="position:absolute;left:759px;top:1517px;width:211px;height:39px;overflow:hidden;">
        <p class="Body"><span class="C-6">April 2016 our gift store will be open</span></p>
      </div>
      <img alt="" src="wpimages/wp082bf5ed_06.png" style="position:absolute;left:690px;top:1559px;width:270px;height:1px;">
      <img alt="" src="wpimages/wpf92ff528_05_06.jpg" style="position:absolute;left:690px;top:1241px;width:50px;height:50px;">
      <img alt="" src="wpimages/wp70c48d96_05_06.jpg" style="position:absolute;left:691px;top:1327px;width:50px;height:50px;">
      <img alt="" src="wpimages/wp93d21605_05_06.jpg" style="position:absolute;left:691px;top:1413px;width:50px;height:50px;">
      <img alt="" src="wpimages/wp0e4cc61a_05_06.jpg" style="position:absolute;left:691px;top:1499px;width:50px;height:50px;">
      <img alt="" src="wpimages/wpae22cb19_06.png" style="position:absolute;left:681px;top:1641px;width:290px;height:40px;">
      <div style="position:absolute;left:691px;top:1648px;width:280px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-10">COMPANY GIFT SHOP</span></p>
      </div>
      <div style="position:absolute;left:680px;top:1884px;width:290px;height:20px;overflow:hidden;">
        <p class="Body P-2"><span class="C-12"><br></span></p>
      </div>
      <a href="events.html" id="btn_2" class="OBJ-7 ActiveButton OBJ-8" style="position:absolute;left:739px;top:1585px;width:184px;height:33px;">
        <span>VIEW&nbsp;ALL&nbsp;EVENTS</span>
      </a>
      <div id="btn_3" class="OBJ-8" style="position:absolute;left:746px;top:1919px;width:170px;height:33px;">
        <button type="button" class="OBJ-9 ActiveButton" style="width:170px;height:33px;">
          <span>VIEW&nbsp;ALL&nbsp;ITEMS</span>
        </button>
      </div>
      <form name="Contact_1" id="form_5" method="post" target="_self" enctype="application/x-www-form-urlencoded" class="OBJ-10" style="position:absolute;left:680px;top:584px;width:290px;height:182px;">
        <input name="NAME" class="OBJ-11" maxlength="50" value="FIRST NAME" value="FIRST NAME" onblur="if(this.value=='')this.value='FIRST NAME'" onFocus="if (this.value == 'FIRST NAME') {this.value=''};"__AddCode="here" style="position:absolute;left:24px;top:27px;width:120px;height:22px;height:35px;background-color:#001488;border:0px solid #616161;font-family:georgia;font-size:10pt;text-align:center;color:#e0e0de;padding-left:5px;padding-right:0px;line-height:35px;/*Tag Style*/">
        <input name="NAME" class="OBJ-11" maxlength="50" value="LAST NAME" value="LAST NAME" onblur="if(this.value=='')this.value='LAST NAME'" onFocus="if (this.value == 'LAST NAME') {this.value=''};"__AddCode="here" style="position:absolute;left:154px;top:27px;width:120px;height:22px;height:35px;background-color:#001488;border:0px solid #616161;font-family:georgia;font-size:10pt;text-align:center;color:#e0e0de;padding-left:5px;padding-right:0px;line-height:35px;/*Tag Style*/">
        <input name="EMAIL" class="OBJ-11" maxlength="100" value="E-MAIL ADDRESS" value="E-MAIL ADDRESS" onblur="if(this.value=='')this.value='E-MAIL ADDRESS'" onFocus="if (this.value == 'E-MAIL ADDRESS') {this.value=''};"__AddCode="here" style="position:absolute;left:24px;top:70px;width:250px;height:22px;height:35px;background-color:#001488;border:0px solid #616161;font-family:georgia;text-align:center;font-size:10pt;color:#e0e0de;padding-left:5px;padding-right:0px;line-height:35px; /*Tag Style*/">
        <a href="mailto:kiininet@hotmail.com?cc=gvega@kiininet.com&amp;subject=Suscripcion" id="btn_4" class="OBJ-12 ActiveButton OBJ-8" style="position:absolute;left:100px;top:128px;width:103px;height:33px;">
          <span>SIGN&nbsp;UP</span>
        </a>
      </form>
      <div style="position:absolute;left:690px;top:559px;width:262px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-14">SIGN UP FOR UPDATES</span></p>
      </div>
      <img alt="" src="wpimages/wp4c3c37a3_06.png" style="position:absolute;left:680px;top:592px;width:290px;height:1px;">
      <img alt="" src="wpimages/wp0b1c7404_06.png" style="position:absolute;left:26px;top:554px;width:194px;height:147px;">
      <div style="position:absolute;left:28px;top:708px;width:188px;height:20px;overflow:hidden;">
        <p class="Body P-3"><span class="C-15">WHY</span><span class="C-16"> </span><span class="C-4">Kiininet</span><span class="C-17">?</span></p>
      </div>
      <div style="position:absolute;left:30px;top:739px;width:188px;height:105px;overflow:hidden;">
        <p class="Body"><span class="C-4">Kiini</span><span class="C-18"> is a word in Swahili that means Cell and </span><span class="C-4">Net</span><span class="C-18"> is the network. &nbsp;All the cells are connected in the Web.</span></p>
        <p class="Body"><span class="C-18"><br></span></p>
      </div>
      <a href="about.html" id="btn_5" class="OBJ-13 ActiveButton OBJ-8" style="position:absolute;left:49px;top:867px;width:141px;height:33px;">
        <span>ABOUT&nbsp;US</span>
      </a>
      <img alt="" src="wpimages/wp6a9f8677_06.png" style="position:absolute;left:246px;top:554px;width:194px;height:147px;">
      <div style="position:absolute;left:244px;top:708px;width:188px;height:20px;overflow:hidden;">
        <p class="Body P-3"><span class="C-15">WHAT WE CAN DO</span></p>
      </div>
      <div style="position:absolute;left:245px;top:739px;width:188px;height:117px;overflow:hidden;">
        <p class="Body"><span class="C-18">We provide solutions end to end, means service satisfaction for your Customers and Employees that support the services</span></p>
      </div>
      <a href="services.html" id="btn_6" class="OBJ-14 ActiveButton OBJ-8" style="position:absolute;left:277px;top:867px;width:133px;height:33px;">
        <span>SERVICES</span>
      </a>
      <img alt="" src="wpimages/wp6f2b8254_06.png" style="position:absolute;left:464px;top:554px;width:194px;height:147px;">
      <div style="position:absolute;left:468px;top:708px;width:187px;height:20px;overflow:hidden;">
        <p class="Body P-3"><span class="C-15">BETTER SERVICES</span></p>
      </div>
      <div style="position:absolute;left:468px;top:739px;width:187px;height:105px;overflow:hidden;">
        <p class="Body"><span class="C-18">Provide better services for your customers, give instant answers to their questions, services or incidents</span></p>
      </div>
      <div id="btn_7" class="OBJ-8" style="position:absolute;left:488px;top:867px;width:147px;height:33px;">
        <button type="button" class="OBJ-15 ActiveButton" style="width:147px;height:33px;">
          <span>SOON</span>
        </button>
      </div>
      <img alt="" src="wpimages/wp2e194ccb_06.png" style="position:absolute;left:10px;top:905px;width:670px;height:1px;">
      <div style="position:absolute;left:30px;top:928px;width:625px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-14">MAIN COMPANY EVENT</span></p>
      </div>
      <img alt="" src="wpimages/wp0ee5a207_06.png" style="position:absolute;left:23px;top:956px;width:639px;height:222px;">
      <div style="position:absolute;left:30px;top:1186px;width:250px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-15">2016 </span><span class="C-4">Kiininet</span><span class="C-15"> Year </span></p>
      </div>
      <div style="position:absolute;left:30px;top:1216px;width:625px;height:103px;overflow:hidden;">
        <p class="Body"><span class="C-18">2016 is our year in January </span><span class="C-4">Kiininet</span><span class="C-18"> will born, now is time you join us, just SIGN UP for have on hand the last news!. <a href="events.html" class="C-19">Learn More</a></span></p>
      </div>
      <div style="position:absolute;left:30px;top:1362px;width:300px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-14">COMPANY EVENT</span></p>
      </div>
      <img alt="" src="wpimages/wp2af1eafe_06.png" style="position:absolute;left:26px;top:1392px;width:308px;height:218px;">
      <div style="position:absolute;left:30px;top:1620px;width:250px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-15">EVENT NAME</span></p>
      </div>
      <div style="position:absolute;left:30px;top:1650px;width:300px;height:103px;overflow:hidden;">
        <p class="Body"><span class="C-18">&nbsp;<a href="events.html" class="C-19">Learn More</a></span></p>
      </div>
      <div style="position:absolute;left:355px;top:1362px;width:300px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-14">COMPANY EVENT</span></p>
      </div>
      <img alt="" src="wpimages/wp2af1eafe_06.png" style="position:absolute;left:352px;top:1392px;width:308px;height:218px;">
      <div style="position:absolute;left:355px;top:1620px;width:250px;height:20px;overflow:hidden;">
        <p class="Body"><span class="C-15">EVENT NAME</span></p>
      </div>
      <div style="position:absolute;left:355px;top:1650px;width:300px;height:103px;overflow:hidden;">
        <p class="Body"><span class="C-18">&nbsp;<a href="events.html" class="C-19">Learn More</a></span></p>
      </div>
      <img alt="" src="wpimages/wp2e194ccb_06.png" style="position:absolute;left:10px;top:1795px;width:670px;height:1px;">
      <div style="position:absolute;left:30px;top:1818px;width:160px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-14">NEWS</span></p>
      </div>
      <img alt="" src="wpimages/wpf56d6d35_06.png" style="position:absolute;left:29px;top:1850px;width:164px;height:114px;">
      <div style="position:absolute;left:200px;top:1888px;width:455px;height:75px;overflow:hidden;">
        <p class="Body"><span class="C-18">January 2016 we start operations. <a href="events.html" class="C-19">Learn More</a></span></p>
      </div>
      <div style="position:absolute;left:200px;top:1853px;width:455px;height:26px;overflow:hidden;">
        <p class="Body"><span class="C-15">NEWS EVENT NUMBER ONE</span></p>
      </div>
      <img alt="" src="wpimages/wpb2e7fbe9_05_06.jpg" style="position:absolute;left:710px;top:1706px;width:240px;height:160px;">
      <div style="position:absolute;left:10px;top:164px;width:960px;height:380px;">
           <div id="wrapper">
        <div class="slider-wrapper theme-default">
                <div id="slider" class="nivoSlider">
                <a href="./about.html"><img src="Logo Kiininet.jpg" alt="Add alt title" title="Building a Smart Future"/></a>
                <a href="./about.html"><img src="call center 2.jpg" alt="Add an alt title"title="Call Center Solutions"/></a>
                <a href="./about.html"><img src="Self services.jpg" alt="Add an alt title" title="Smart Self Service Portal"/></a>
                <a href="./about.html"><img src="Monitoring.jpg" alt="Add alt title" title="Systems Monitoring"/></a>
                </div>
        </div>
    </div>
      </div>
    </div>

</asp:Content>
