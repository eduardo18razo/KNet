using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniHelp.UserControls.Altas
{
    public partial class UcHorario : UserControl, IControllerModal
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
            set { hfIdSubRol.Value = value.ToString(); }
        }

        public bool EsAlta
        {
            get { return Convert.ToBoolean(hfEsAlta.Value); }
            set { hfEsAlta.Value = value.ToString(); }
        }

        public RepeaterItemCollection HorariosSubRol
        {
            get { return rptHorarios.Items; }
        }

        public void SetHorariosSubRol(List<HorarioSubGrupo> lstHorarios, int idSubRol)
        {
            try
            {
                IdSubRol = idSubRol;
                MuestraHorarios(lstHorarios);
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
                txtHoraInicio.Text = string.Empty;
                txtHoraFin.Text = string.Empty;
                txtHoraInicio.Focus();
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
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void MuestraHorarios(List<HorarioSubGrupo> lst)
        {
            try
            {
                Session["TiemposSubGrupo"] = lst;
                rptHorarios.DataSource = lst;
                rptHorarios.DataBind();
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
                    Session["TiemposSubGrupo"] = null;
                    chklbxDias.Items.Add(new ListItem("LUNES", "1"));
                    chklbxDias.Items.Add(new ListItem("MARTES", "2"));
                    chklbxDias.Items.Add(new ListItem("MIERCOLES", "3"));
                    chklbxDias.Items.Add(new ListItem("JUEVES", "4"));
                    chklbxDias.Items.Add(new ListItem("VIERNES", "5"));
                    chklbxDias.Items.Add(new ListItem("SABADO", "6"));
                    chklbxDias.Items.Add(new ListItem("DOMINGO", "0"));
                }
                foreach (ListItem listItem in chklbxDias.Items)
                {
                    listItem.Attributes.Add("style", "margin-right:5px;");
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
                List<HorarioSubGrupo> lst = (List<HorarioSubGrupo>)Session["TiemposSubGrupo"] ?? new List<HorarioSubGrupo>();
                if (txtHoraInicio.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese hora inicio");
                if (txtHoraFin.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese hora inicio");

                if (!timeStartValidator.IsValid || !timeStartValidator.IsValidEmpty)
                    throw new Exception("Introdusca una hora de inicio valida.");
                if (!timeEndValidator.IsValid || !timeEndValidator.IsValidEmpty)
                    throw new Exception("Introdusca una hora fin valida.");
                foreach (ListItem dia in chklbxDias.Items)
                {
                    if (dia.Selected)
                        if (!lst.Any(s => s.HoraInicio == Convert.ToDateTime(txtHoraInicio.Text.Trim()).ToString("HH:mm:ss") && s.Dia == Convert.ToInt32(dia.Value)))
                            lst.Add(new HorarioSubGrupo
                            {
                                IdSubGrupoUsuario = IdSubRol,
                                Dia = Convert.ToInt32(dia.Value),
                                HoraInicio = Convert.ToDateTime(txtHoraInicio.Text.Trim()).ToString("HH:mm:ss"),
                                HoraFin = Convert.ToDateTime(txtHoraFin.Text.Trim()).ToString("HH:mm:ss")
                            });
                }

                MuestraHorarios(lst);
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

        protected void btnEliminar_OnClick(object sender, EventArgs e)
        {
            try
            {
                List<HorarioSubGrupo> lst = (List<HorarioSubGrupo>)Session["TiemposSubGrupo"];
                lst.Remove(lst.Single(s => s.HoraInicio == ((Button)sender).CommandName && s.Dia == int.Parse(((Button)sender).CommandArgument)));
                MuestraHorarios(lst);
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