﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceParametrosSistema;
using KiiniHelp.ServiceSeguridad;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using Menu = KiiniNet.Entities.Cat.Sistema.Menu;

namespace KiiniHelp
{
    public partial class UsuariosMaster : MasterPage
    {
        private readonly ServiceSecurityClient _servicioSeguridad = new ServiceSecurityClient();
        private readonly ServiceParametrosClient _servicioParametros = new ServiceParametrosClient();

        private List<string> _lstError = new List<string>();

        private List<string> Alerta
        {
            set
            {
                if (value.Any())
                {
                    string error = value.Aggregate("<ul>", (current, s) => current + ("<li>" + s + "</li>"));
                    error += "</ul>";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert", "ErrorAlert('Error','" + error + "');", true);
                }
            }
        }

        private void ObtenerAreas()
        {
            try
            {
                List<Rol> lstRoles = _servicioSeguridad.ObtenerRolesUsuario(((Usuario)Session["UserData"]).Id);
                if (lstRoles.Count == 1)
                {
                    Session["RolSeleccionado"] = lstRoles.First().Id;
                    Session["CargaInicialModal"] = "True";
                    lnkBtnRol_OnClick(
                        new LinkButton
                        {
                            CommandArgument = lstRoles.First().Id.ToString(),
                            Text = lstRoles.First().Descripcion
                        }, null);
                }
                if (Session["RolSeleccionado"] != null) lblAreaSeleccionada.Text =
                        lstRoles.Single(s => s.Id == int.Parse(Session["RolSeleccionado"].ToString())).Descripcion;
                rptRolesPanel.DataSource = lstRoles;
                rptRolesPanel.DataBind();
                lblBadgeRoles.Text = lstRoles.Count.ToString();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        private void UcTicketPortal_OnAceptarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptClose", "CierraPopup(\"#modal-new-ticket\");", true);

                lblNoTicket.Text = ucTicketPortal.TicketGenerado.ToString();
                lblRandom.Text = ucTicketPortal.RandomGenerado;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptOpen", "MostrarPopup(\"#modalExitoTicket\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                HttpCookie myCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (myCookie == null || Session["UserData"] == null)
                {
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                }
                lblBranding.Text = WebConfigurationManager.AppSettings["Brand"];
                ucTicketPortal.OnAceptarModal += UcTicketPortal_OnAceptarModal;

                if (Session["UserData"] != null && HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Count() - 1] != "FrmCambiarContrasena.aspx")
                    if (_servicioSeguridad.CaducaPassword(((Usuario)Session["UserData"]).Id))
                        Response.Redirect(ResolveUrl("~/Users/Administracion/Usuarios/FrmCambiarContrasena.aspx"));
                lnkBtnCerrar.Visible = !ContentPlaceHolder1.Page.ToString().ToUpper().Contains("DASHBOARD");

                if (!IsPostBack && Session["UserData"] != null)
                {
                    bool administrador = false;
                    Usuario usuario = ((Usuario)Session["UserData"]);
                    if (usuario.UsuarioRol.Any(rol => rol.RolTipoUsuario.IdRol == (int)BusinessVariables.EnumRoles.Administrador))
                    {
                        administrador = true;
                    }
                    if (administrador)
                        Session["CargaInicialModal"] = true.ToString();
                    hfCargaInicial.Value = (Session["CargaInicialModal"] ?? "False").ToString();
                    //btnSwitchRol.Visible = !administrador;
                    lblUsuario.Text = usuario.NombreCompleto;
                    lblTipoUsr.Text = usuario.TipoUsuario.Descripcion;
                    ObtenerAreas();
                    int rolSeleccionado = 0;
                    if (Session["RolSeleccionado"] != null)
                        rolSeleccionado = int.Parse(Session["RolSeleccionado"].ToString());
                    rptMenu.DataSource = _servicioSeguridad.ObtenerMenuUsuario(usuario.Id, rolSeleccionado, rolSeleccionado != 0);
                    rptMenu.DataBind();
                    divTickets.Visible = rolSeleccionado != (int)BusinessVariables.EnumRoles.Administrador;
                    divMensajes.Visible = rolSeleccionado != (int)BusinessVariables.EnumRoles.Administrador;
                    divTickets.Visible = rolSeleccionado == (int) BusinessVariables.EnumRoles.ResponsableDeAtención;
                    divMensajes.Visible = rolSeleccionado == (int) BusinessVariables.EnumRoles.Usuario;
                }

                Session["ParametrosGenerales"] = _servicioParametros.ObtenerParametrosGenerales();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }
        protected void rptMenu_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu1"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }

            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void rptSubMenu1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu2"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }

        }

        protected void rptSubMenu2_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu3"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void rptSubMenu3_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu4"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void rptSubMenu4_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu5"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void rptSubMenu5_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu6"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void rptSubMenu6_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu7"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void btnsOut_OnClick(object sender, EventArgs e)
        {
            try
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void lnkBtnCerrar_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ResolveUrl("~/Users/DashBoard.aspx"));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void lnkBtnRol_OnClick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Usuario usuario = ((Usuario)Session["UserData"]);
                    Session["RolSeleccionado"] = ((LinkButton)sender).CommandArgument;
                    lblAreaSeleccionada.Text = ((LinkButton)sender).Text;
                    int areaSeleccionada = 0;
                    if (Session["RolSeleccionado"] != null)
                        areaSeleccionada = int.Parse(Session["RolSeleccionado"].ToString());
                    rptMenu.DataSource = _servicioSeguridad.ObtenerMenuUsuario(usuario.Id, areaSeleccionada, areaSeleccionada != 0);
                    rptMenu.DataBind();
                    Session["CargaInicialModal"] = "True";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalRol\");", true);
                    //TODO: Tickets redirect
                    //if (areaSeleccionada == (int)BusinessVariables.EnumRoles.ResponsableDeAtención)
                    //    Response.Redirect("~/Agente/FrmBandejaTickets.aspx");
                }
                catch (Exception ex)
                {
                    if (_lstError == null)
                    {
                        _lstError = new List<string>();
                    }
                    _lstError.Add(ex.Message);
                    Alerta = _lstError;
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void btnSwitchRol_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalRol\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void btnCerrarTicket_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucTicketPortal.Limpiar();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptClose", "CierraPopup(\"#modal-new-ticket\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void btnCerrarExito_OnClick(object sender, EventArgs e)
        {
            try
            {
                ucTicketPortal.Limpiar();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptOpen", "MostrarPopup(\"#modalExitoTicket\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                Alerta = _lstError;
            }
        }

        protected void btnMiPerfil_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Users/Administracion/Usuarios/FrmEdicionUsuario.aspx?Detail=1");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}