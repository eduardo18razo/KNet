﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceTicket;
using KiiniNet.Entities.Helper;

namespace KiiniHelp.Publico.Consultas
{
    public partial class FrmConsultaTicket : Page
    {
        private readonly ServiceTicketClient _servicioticket = new ServiceTicketClient();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Alerta = new List<string>();
                targetEditor.Attributes.Add("onfocus", "CargaEditor(\"#targetEditor\");");

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

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtTicket.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese número de ticket");
                if (txtClave.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese clave de registro");
                HelperDetalleTicket detalle = _servicioticket.ObtenerDetalleTicketNoRegistrado(int.Parse(txtTicket.Text.Trim()), txtClave.Text.Trim());
                if (detalle != null)
                {
                    divConsulta.Visible = false;
                    divDetalle.Visible = true;
                    lblticket.Text = detalle.IdTicket.ToString();
                    lblCveRegistro.Text = detalle.CveRegistro;
                    lblFechaActualiza.Text = detalle.AsignacionesDetalle.OrderBy(o => o.FechaMovimiento).First().FechaMovimiento.ToShortDateString();
                    lblestatus.Text = detalle.EstatusActual;
                    lblfecha.Text = detalle.FechaCreacion.ToString(CultureInfo.InvariantCulture);
                    rptComentrios.DataSource = detalle.ConversacionDetalle;
                    rptComentrios.DataBind();
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

        protected void rptComentrios_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptDownloads = ((Repeater)e.Item.FindControl("rptDownloads"));
                if (rptDownloads != null)
                {
                    rptDownloads.DataSource = ((HelperConversacionDetalle)e.Item.DataItem).Archivo;
                    rptDownloads.DataBind();
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
    }
}