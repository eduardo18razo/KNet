using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceTicket;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.UserControls.Detalles
{
    public partial class UcTicketDetalle : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public event DelegateTerminarModal OnTerminarModal;

        private readonly ServiceTicketClient _servicioTicket = new ServiceTicketClient();
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

        public int IdEstatusAsignacionActual
        {
            get { return Convert.ToInt32(hfEstatusAsignacionActual.Value); }
            set
            {
                hfEstatusAsignacionActual.Value = value.ToString();
            }
        }

        public bool EsPropietario
        {
            get { return Convert.ToBoolean(hfEsPropietario.Value); }
            set { hfEsPropietario.Value = value.ToString(); }
        }

        public void LlenaTicket(int idTicket)
        {
            try
            {
                HelperTicketDetalle ticket = _servicioTicket.ObtenerTicket(idTicket, ((Usuario)Session["UserData"]).Id);
                if (ticket != null)
                {
                    lblNoticket.Text = ticket.IdTicket.ToString();
                    lblTituloTicket.Text = ticket.Tipificacion;
                    lblNombreCorreo.Text = string.Format("{0} {1}", ticket.UsuarioLevanto, ticket.DetalleUsuarioLevanto.CorreoUsuario.First().Correo);
                    lblFechaAlta.Text = ticket.FechaSolicitud.ToString();
                    imgPrioridad.ImageUrl = "~/assets/images/icons/prioridadalta.png";
                    imgSLA.ImageUrl = "~/assets/images/icons/prioridadbaja.png";
                    lblTiempoRestanteSLa.Text = ticket.DiferenciaSla;
                    divEstatus.Style.Add("background-color", ticket.EstatusTicket.Color);
                    lblEstatus.Text = ticket.EstatusTicket.Descripcion;

                    LlenaDatosUsuario(ticket.DetalleUsuarioLevanto);
                    lblFechaAltaDetalle.Text = ticket.FechaSolicitud.ToShortDateString();
                    lblfechaUltimaActualizacion.Text = ticket.UltimaActualizacion.ToShortDateString();
                    rptConversaciones.DataSource = ticket.ConversacionDetalle;
                    rptConversaciones.DataBind();
                    UcDetalleMascaraCaptura.IdTicket = idTicket;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaDatosUsuario(Usuario usuario)
        {
            try
            {
                if (usuario != null)
                {
                    lblNombreDetalle.Text = usuario.NombreCompleto;
                    lblTipoUsuarioDetalle.Text = usuario.TipoUsuario.Descripcion.Substring(0, 1);
                    imgVip.Visible = usuario.Vip;
                    lblFechaUltimaconexion.Text = usuario.BitacoraAcceso.Last().Fecha.ToString();
                    ddlTicketUsuario.DataSource = usuario.TicketsLevantados;
                    ddlTicketUsuario.DataTextField = "Id";
                    ddlTicketUsuario.DataValueField = "Id";
                    ddlTicketUsuario.DataBind();

                    lblPuesto.Text = usuario.Puesto != null ? usuario.Puesto.Descripcion : string.Empty;
                    lblCorreoPrincipal.Text = usuario.CorreoUsuario.FirstOrDefault(s => s.Obligatorio) != null ? usuario.CorreoUsuario.First(s => s.Obligatorio).Correo : string.Empty;
                    lblTelefonoPrincipal.Text = usuario.TelefonoUsuario.FirstOrDefault(s => s.Obligatorio) != null ? usuario.TelefonoUsuario.First(s => s.Obligatorio).Numero : string.Empty;
                    lblOrganizacion.Text = usuario.OrganizacionCompleta;
                    lblUbicacion.Text = usuario.UbicacionCompleta;
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        LlenaTicket(int.Parse(Request.QueryString["id"]));
                    }
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