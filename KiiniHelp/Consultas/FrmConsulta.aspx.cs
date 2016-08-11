using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceTicket;
using KiiniNet.Entities.Helper;

namespace KiiniHelp.Consultas
{
    public partial class FrmConsulta : System.Web.UI.Page
    {
        private readonly ServiceTicketClient _servicioticket = new ServiceTicketClient();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_OnClick(object sender, EventArgs e)
        {
            try
            {
                HelperDetalleTicket detalle = _servicioticket.ObtenerDetalleTicketNoRegistrado(int.Parse(txtTicket.Text.Trim()), txtClave.Text.Trim());
                divResultado.Visible = detalle != null;
                if (detalle != null)
                {
                    lblticket.Text = detalle.IdTicket.ToString();
                    lblestatus.Text = detalle.EstatusActual;
                    lblAsignacion.Text = detalle.AsignacionActual;
                    lblfecha.Text = detalle.FechaCreacion.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}