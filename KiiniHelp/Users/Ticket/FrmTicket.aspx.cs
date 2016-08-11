using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceArbolAcceso;
using KiiniHelp.ServiceMascaraAcceso;
using KiiniHelp.ServiceTicket;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.Users.Ticket
{
    public partial class FrmTicket : Page
    {
        readonly ServiceArbolAccesoClient _servicioArbolAcceso = new ServiceArbolAccesoClient();
        readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        readonly ServiceTicketClient _servicioTicket = new ServiceTicketClient();
        private List<string> _lstError = new List<string>();

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlert.Visible = value.Any();
                if (!panelAlert.Visible) return;
                rptHeaderError.DataSource = value;
                rptHeaderError.DataBind();
            }
        }
        public int? IdConsulta
        {
            get
            {
                int result = 0;
                if (hfIdConsulta.Value != string.Empty)
                    result = Convert.ToInt32(hfIdConsulta.Value);
                else
                    result = (int)Session["IdConsultaTicket"];
                return result;
            }
            set
            {
                if (hfIdConsulta != null)
                {
                    hfIdConsulta.Value = value.ToString();
                    Session.Remove("IdConsultaTicket");
                }
                else
                    Session["IdConsultaTicket"] = value;
            }
        }
        public int? IdMascara
        {
            get
            {
                int result = 0;
                if (hfIdMascara.Value != string.Empty)
                    result = Convert.ToInt32(hfIdMascara.Value);
                else
                    result = (int)Session["IdMascaraTicket"];
                return result;
            }
            set
            {
                if (hfIdMascara != null)
                {
                    hfIdMascara.Value = value.ToString();
                    Session.Remove("IdMascaraTicket");
                }
                else
                    Session["IdMascaraTicket"] = value;
            }
        }

        public int IdSla
        {
            get
            {
                int result = 0;
                if (hfIdSla.Value != string.Empty)
                    result = Convert.ToInt32(hfIdSla.Value);
                else
                    result = (int)Session["IdSlaTicket"];
                return result;
            }
            set
            {
                if (hfIdSla != null)
                {
                    hfIdSla.Value = value.ToString();
                    Session.Remove("IdSlaTicket");
                }
                else
                    Session["IdSlaTicket"] = value;
            }
        }

        public int IdEncuesta
        {
            get
            {
                int result = 0;
                if (hfIdEncuesta.Value != string.Empty)
                {
                    result = Convert.ToInt32(hfIdEncuesta.Value);
                    Session.Remove("IdEncuestaTicket");
                }
                else
                    result = (int)Session["IdEncuestaTicket"];
                return result;
            }
            set
            {
                if (hfIdEncuesta != null)
                    hfIdEncuesta.Value = value.ToString();
                else
                    Session["IdEncuestaTicket"] = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            int idArbol = Convert.ToInt32(Request.QueryString["IdArbol"]);
            ArbolAcceso arbol = _servicioArbolAcceso.ObtenerArbolAcceso(idArbol);
            Session["ArbolAcceso"] = arbol;
            IdMascara = arbol.InventarioArbolAcceso.First().IdMascara ?? 0;
            IdEncuesta = arbol.InventarioArbolAcceso.First().IdEncuesta ?? 0;

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                if (!IsPostBack)
                {
                    ArbolAcceso arbol = ((ArbolAcceso) Session["ArbolAcceso"]);
                    UcInformacionConsulta.IdArbol = arbol.Id;
                    lblTicketDescripcion.Text = "TICKET";
                    if (arbol.Nivel1 != null)
                        lblTicketDescripcion.Text += arbol.Nivel1.Descripcion;
                    if (arbol.Nivel2 != null)
                        lblTicketDescripcion.Text += " > " + arbol.Nivel2.Descripcion;
                    if (arbol.Nivel3 != null)
                        lblTicketDescripcion.Text += " > " + arbol.Nivel3.Descripcion;
                    if (arbol.Nivel4 != null)
                        lblTicketDescripcion.Text += " > " + arbol.Nivel4.Descripcion;
                    if (arbol.Nivel5 != null)
                        lblTicketDescripcion.Text += " > " + arbol.Nivel5.Descripcion;
                    if (arbol.Nivel6 != null)
                        lblTicketDescripcion.Text += " > " + arbol.Nivel6.Descripcion;
                    if (arbol.Nivel7 != null)
                        lblTicketDescripcion.Text += " > " + arbol.Nivel7.Descripcion;
                    if (IdMascara == 0)
                    {
                        UcMascaraCaptura.Visible = false;
                        btnGuardar.CommandArgument = UcMascaraCaptura.ComandoInsertar;
                    }
                    UcInformacionConsulta.Visible = arbol.InventarioArbolAcceso.First().InventarioInfConsulta.Any();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {
                
                List<HelperCampoMascaraCaptura> capturaMascara = UcMascaraCaptura.ObtenerCapturaMascara();
                _servicioTicket.CrearTicket(((Usuario)Session["UserData"]).Id, Convert.ToInt32(Request.QueryString["IdArbol"]), capturaMascara, UcMascaraCaptura.CampoRandom);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }
    }
}