using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace KiiniHelp.UserControls.Filtros
{
    public partial class UcFiltrosConsulta : UserControl, IControllerModal
    {
        #region Propiedades publicas
        public List<int> FiltroGrupos
        {
            get { return ucFiltroGrupo.GruposSeleccionados; }
        }
        public List<int> FiltroOrganizaciones
        {
            get { return ucFiltroOrganizacion.OrganizacionesSeleccionadas; }
        }
        public List<int> FiltroUbicaciones
        {
            get { return ucFiltroUbicacion.UbicacionesSeleccionadas; }
        }
        public List<int> FiltroTipoArbol
        {
            get { return ucFiltroServicioIncidente.TipoArbolSeleccionados; }
        }
        public List<int> FiltroTipificaciones
        {
            get { return ucFiltroTipificacion.TipificacionesSeleccionadas; }
        }
        public List<int> FiltroPrioridad
        {
            get { return ucFiltroPrioridad.ImpactosSeleccionados; }
        }
        public List<int> FiltroEstatus
        {
            get { return ucFiltroEstatus.EstatusSeleccionados; }
        }
        public bool FiltroSla
        {
            get { return ucFiltroSla.SlaSeleccionado; }
        }
        public Dictionary<string, DateTime> FiltroFechas
        {
            get { return ucFiltroFechas.RangoFechas; }
        }
        #endregion Propiedades publicas

        private List<string> _lstError = new List<string>();
        private List<string> Alerta
        {
            set
            {
                panelAlerta.Visible = value.Any();
                if (!panelAlerta.Visible) return;
                rptError.DataSource = value;
                rptError.DataBind();
            }
        }

        public bool EsTicket
        {
            get { return Convert.ToBoolean(hfTicket.Value); }
            set
            {
                if (value)
                {
                    btnFiltroPrioridad.Visible = true;
                    btnFiltroEstatus.Visible = true;
                    btnFiltroSla.Visible = true;
                }
                ucFiltroServicioIncidente.EsTicket = value;
                hfTicket.Value = value.ToString();
                upFiltroServicioIncidente.Update();
            }
        }

        public bool EsConsulta
        {
            get { return Convert.ToBoolean(hfConsulta.Value); }
            set
            {
                if (value)
                {
                    btnFiltroPrioridad.Visible = false;
                    btnFiltroEstatus.Visible = false;
                    btnFiltroSla.Visible = false;
                }
                ucFiltroServicioIncidente.EsConsulta = value;
                hfEncuesta.Value = value.ToString();
                upFiltroServicioIncidente.Update();
            }
        }

        public bool EsEncuesta
        {
            get { return Convert.ToBoolean(hfEncuesta.Value); }
            set
            {
                ucFiltroServicioIncidente.EsEncuesta = value;
                hfConsulta.Value = value.ToString();
                upFiltroServicioIncidente.Update();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ucFiltroGrupo.OnAceptarModal += ucFiltroGrupo_OnAceptarModal;
                ucFiltroGrupo.OnCancelarModal += ucFiltroGrupo_OnCancelarModal;

                ucFiltroOrganizacion.OnAceptarModal += ucFiltroOrganizacion_OnAceptarModal;
                ucFiltroOrganizacion.OnCancelarModal += ucFiltroOrganizacion_OnCancelarModal;

                ucFiltroUbicacion.OnAceptarModal += ucFiltroUbicacion_OnAceptarModal;
                ucFiltroUbicacion.OnCancelarModal += ucFiltroUbicacion_OnCancelarModal;

                ucFiltroServicioIncidente.OnAceptarModal += ucFiltroServicioIncidente_OnAceptarModal;
                ucFiltroServicioIncidente.OnCancelarModal += ucFiltroServicioIncidente_OnCancelarModal;

                ucFiltroTipificacion.OnAceptarModal += ucFiltroTipificacion_OnAceptarModal;
                ucFiltroTipificacion.OnCancelarModal += ucFiltroTipificacion_OnCancelarModal;

                ucFiltroPrioridad.OnAceptarModal += ucFiltroPrioridad_OnAceptarModal;
                ucFiltroPrioridad.OnCancelarModal += ucFiltroPrioridad_OnCancelarModal;

                ucFiltroEstatus.OnAceptarModal += ucFiltroEstatus_OnAceptarModal;
                ucFiltroEstatus.OnCancelarModal += UcFiltroEstatusOnOnCancelarModal;

                ucFiltroSla.OnAceptarModal += ucFiltroSla_OnAceptarModal;
                ucFiltroSla.OnCancelarModal += UcFiltroSlaOnOnCancelarModal;

                ucFiltroFechas.OnAceptarModal += ucFiltroFechas_OnAceptarModal;
                ucFiltroFechas.OnCancelarModal += ucFiltroFechas_OnCancelarModal;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Delegados
        void ucFiltroGrupo_OnAceptarModal()
        {
            try
            {
                btnFiltroGrupo.CssClass = "btn btn-success";
                ucFiltroOrganizacion.Grupos = ucFiltroGrupo.GruposSeleccionados;
                ucFiltroUbicacion.Grupos = ucFiltroGrupo.GruposSeleccionados;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroGrupo\");", true);
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
        void ucFiltroGrupo_OnCancelarModal()
        {
            try
            {

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroGrupo\");", true);
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

        void ucFiltroOrganizacion_OnAceptarModal()
        {
            try
            {
                btnFiltroOrganizacion.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroOrganizacion\");", true);
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
        void ucFiltroOrganizacion_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroOrganizacion\");", true);
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

        void ucFiltroUbicacion_OnAceptarModal()
        {
            try
            {
                btnFiltroUbicacion.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroUbicacion\");", true);
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
        void ucFiltroUbicacion_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroUbicacion\");", true);
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

        void ucFiltroServicioIncidente_OnAceptarModal()
        {
            try
            {
                btnFiltroServicioIncidente.CssClass = "btn btn-success";
                btnFiltroTipificacion.CssClass = "btn btn-primary";
                ucFiltroTipificacion.TipoArbol = ucFiltroServicioIncidente.TipoArbolSeleccionados.First();
                upFiltroTipificacion.Update();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroServicioIncidente\");", true);
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
        void ucFiltroServicioIncidente_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroServicioIncidente\");", true);
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

        void ucFiltroTipificacion_OnAceptarModal()
        {
            try
            {
                btnFiltroTipificacion.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroTipificacion\");", true);
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
        void ucFiltroTipificacion_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroTipificacion\");", true);
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

        void ucFiltroPrioridad_OnAceptarModal()
        {
            try
            {
                btnFiltroPrioridad.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroPrioridad\");", true);
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
        void ucFiltroPrioridad_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroPrioridad\");", true);
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

        void ucFiltroEstatus_OnAceptarModal()
        {
            try
            {
                btnFiltroEstatus.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroEstatus\");", true);
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
        void UcFiltroEstatusOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroEstatus\");", true);
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

        void ucFiltroSla_OnAceptarModal()
        {
            try
            {
                btnFiltroSla.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroSla\");", true);
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
        void UcFiltroSlaOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroSla\");", true);
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

        void ucFiltroFechas_OnAceptarModal()
        {
            try
            {
                btnFiltroFechas.CssClass = "btn btn-success";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroFechas\");", true);
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
        void ucFiltroFechas_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalFiltroFechas\");", true);
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
        #endregion Delegados

        #region AbreModales
        protected void btnFiltroGrupo_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroGrupo\");", true);
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

        protected void btnFiltroOrganizacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroOrganizacion\");", true);
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

        protected void btnFiltroUbicacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroUbicacion\");", true);
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

        protected void btnFiltroServicioIncidente_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroServicioIncidente\");", true);
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

        protected void btnFiltroTipificacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroTipificacion\");", true);
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

        protected void btnFiltroPrioridad_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroPrioridad\");", true);
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

        protected void btnFiltroEstatus_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroEstatus\");", true);
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

        protected void btnFiltroSla_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroSla\");", true);
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

        protected void btnFiltroFechas_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalFiltroFechas\");", true);

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
        #endregion AbreModales

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
    }
}