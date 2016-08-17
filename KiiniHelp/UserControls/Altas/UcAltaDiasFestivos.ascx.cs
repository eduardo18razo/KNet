using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniHelp.UserControls.Altas
{
    public partial class UcAltaDiasFestivos : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        private List<string> _lstError = new List<string>();

        private List<string> Alerta
        {
            set
            {
                panelAlerta.Visible = value.Any();
                if (!panelAlerta.Visible) return;
                rptErrorGeneral.DataSource = value;
                rptErrorGeneral.DataBind();
            }
        }

        public int IdSubRol
        {
            get { return Convert.ToInt32(hfIdSubRol.Value); }
            set
            {
                hfIdSubRol.Value = value.ToString();
            }
        }

        public RepeaterItemCollection DiasDescansoSubRol
        {
            get { return rptDias.Items; }
        }

        public void SetDiasFestivosSubRol(List<DiaFestivoSubGrupo> lstDiasfestivos, int idSubRol)
        {
            try
            {
                IdSubRol = idSubRol;
                MuestraDias(lstDiasfestivos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void LimpiarCampos()
        {
            try
            {
                txtDate.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtDate.Focus();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LimpiarPantalla()
        {
            try
            {
                LimpiarCampos();
                MuestraDias(new List<DiaFestivoSubGrupo>());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void MuestraDias(List<DiaFestivoSubGrupo> lst)
        {
            try
            {
                Session["DiasFestivosSubRol"] = lst;
                rptDias.DataSource = lst;
                rptDias.DataBind();
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
                Alerta = new List<string>();
                if (!IsPostBack)
                {
                    txtDate.Focus();
                    txtDescripcion.Focus();
                    txtDate.Focus();
                    Session["DiasFestivosSubRol"] = null;
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

        protected void btnAgregar_OnClick(object sender, EventArgs e)
        {
            try
            {
                List<DiaFestivoSubGrupo> lst = (List<DiaFestivoSubGrupo>)Session["DiasFestivosSubRol"] ?? new List<DiaFestivoSubGrupo>();
                if (txtDate.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese una fecha");
                if (txtDescripcion.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese una descripción");
                if (lst.Any(a => a.Fecha == Convert.ToDateTime(txtDate.Text)))
                    throw new Exception("Ya se ha ingresado esta fecha");
                var dates = Convert.ToDateTime(txtDate.Text);
                DateTime date = Convert.ToDateTime(txtDate.Text);
                lst.Add(new DiaFestivoSubGrupo
                {
                    IdSubGrupoUsuario = IdSubRol,
                    Fecha = Convert.ToDateTime(txtDate.Text),
                    Descripcion = txtDescripcion.Text.Trim().ToUpper()
                });
                MuestraDias(lst);
                LimpiarCampos();
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

        protected void OnClick(object sender, EventArgs e)
        {
            try
            {
                List<DiaFestivoSubGrupo> lst = (List<DiaFestivoSubGrupo>)Session["DiasFestivosSubRol"];
                lst.Remove(lst.Single(s => s.Fecha == Convert.ToDateTime(((Button)sender).CommandArgument)));
                MuestraDias(lst);
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

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
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
                Alerta = _lstError;
            }
        }

        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarPantalla();
                if (OnLimpiarModal != null)
                    OnLimpiarModal();
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarPantalla();
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
                Alerta = _lstError;
            }
        }
    }
}