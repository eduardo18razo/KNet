﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using KinniNet.Business.Utils;

namespace KiiniHelp
{
    public partial class Default1 : Page
    {
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

        private void UcTicketPortal_OnAceptarModal()
        {
            try
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CierraPopup(\"#modal-new-ticket\");", true);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptClose", "CierraPopup(\"#modal-new-ticket\");", true);

                lblNoTicket.Text = ucTicketPortal.TicketGenerado.ToString();
                lblRandom.Text = ucTicketPortal.RandomGenerado;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MostrarPopup(\"#modalExitoTicket\");", true);
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
                lblBranding.Text = WebConfigurationManager.AppSettings["Brand"];
                ucTicketPortal.OnAceptarModal += UcTicketPortal_OnAceptarModal;
                if(UcLogCopia.Fail)
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "OpenDropLogin();", true);

                
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

        protected void lnkBtnEmpleado_OnClick(object sender, EventArgs e)
        {
            try
            {
                Public master = Master as Public;
                if (master != null)
                {
                    master.CargaPerfil((int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado);
                }
                Response.Redirect("~/Publico/FrmUserSelect.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado);
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

        protected void lnkBtnCliente_OnClick(object sender, EventArgs e)
        {
            try
            {
                Public master = Master as Public;
                if (master != null)
                {
                    master.CargaPerfil((int)BusinessVariables.EnumTiposUsuario.ClienteInvitado);
                }
                Response.Redirect("~/Publico/FrmUserSelect.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.ClienteInvitado);
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

        protected void lnkBtnProveedor_OnClick(object sender, EventArgs e)
        {
            try
            {
                Public master = Master as Public;
                if (master != null)
                {
                    master.CargaPerfil((int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado);
                }
                Response.Redirect("~/Publico/FrmUserSelect.aspx?userTipe=" + (int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado);
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
    }
}