using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using KiiniHelp.ServiceSistemaEstatus;
using KiiniHelp.ServiceTicket;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Operacion
{
    public partial class UcCambiarEstatusTicket : UserControl, IControllerModal
    {
        private readonly ServiceEstatusClient _servicioEstatus = new ServiceEstatusClient();
        private readonly ServiceTicketClient _servicioTicketClient = new ServiceTicketClient();
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        private List<string> _lstError = new List<string>();

        public int IdTicket
        {
            get { return Convert.ToInt32(lblIdticket.Text); }
            set { lblIdticket.Text = value.ToString(); }
        }
        public int IdUsuario
        {
            get { return Convert.ToInt32(ViewState["IdUsuarioTicket"]); }
            set
            {
                ViewState["IdUsuarioTicket"] = value;
                LLenaEstatus();
            }
        }

        public int IdEstatusActual
        {
            get { return int.Parse(hfEstatusActual.Value); }
            set { hfEstatusActual.Value = value.ToString(); }
        }
        public bool CerroTicket
        {
            get { return Convert.ToBoolean(hfTicketCerrado.Value); }
            set { hfTicketCerrado.Value = value.ToString(); }
        }

        public bool EsPropietario
        {
            get { return ddlEstatus.Enabled; }
            set { ddlEstatus.Enabled = value; }
        }

        private void LLenaEstatus()
        {
            try
            {
                ddlEstatus.DataSource = _servicioEstatus.ObtenerEstatusTicketUsuario(IdUsuario, IdEstatusActual, EsPropietario, true);
                ddlEstatus.DataTextField = "Descripcion";
                ddlEstatus.DataValueField = "Id";
                ddlEstatus.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlertaGeneral.Visible = value.Any();
                if (!panelAlertaGeneral.Visible) return;
                rptErrorGeneral.DataSource = value.Select(s => new { Detalle = s }).ToList();
                rptErrorGeneral.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                if (!IsPostBack)
                {
                    
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

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlEstatus.SelectedValue != BusinessVariables.ComboBoxCatalogo.Value.ToString())
                {
                    CerroTicket = Convert.ToInt32(ddlEstatus.SelectedValue) == (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado;
                    _servicioTicketClient.CambiarEstatus(IdTicket, Convert.ToInt32(ddlEstatus.SelectedValue), IdUsuario, txtComentarios.Text.Trim());
                }
                
                if (OnAceptarModal != null)
                    OnAceptarModal();
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


        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
               if (OnCancelarModal != null)
                   OnCancelarModal();
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