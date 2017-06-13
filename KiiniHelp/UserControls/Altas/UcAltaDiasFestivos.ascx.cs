using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceDiasHorario;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class UcAltaDiasFestivos : UserControl, IControllerModal
    {
        private readonly ServiceDiasHorarioClient _servicioDias = new ServiceDiasHorarioClient();
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public event DelegateTerminarModal OnTerminarModal;

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

        private List<DiasFeriadosDetalle> DiasFeriados
        {
            get { return (List<DiasFeriadosDetalle>)Session["DiasSeleccionados"]; }
            set { Session["DiasSeleccionados"] = value; }
        }

        public bool EsAlta
        {
            get { return Convert.ToBoolean(hfEsAlta.Value); }
            set { hfEsAlta.Value = value.ToString(); }
        }

        public int IdDiaFeriado
        {
            get { return int.Parse(hdIdHorario.Value); }
            set
            {
                hdIdHorario.Value = value.ToString();
                DiasFeriados dia = _servicioDias.ObtenerDiasFeriadosUserById(value);
                if (dia != null)
                {
                    txtDescripcionDias.Text = dia.Descripcion;
                    DiasFeriados = dia.DiasFeriadosDetalle;
                    LlenaDias();
                }
            }
        }

        private void LimpiarPantalla()
        {
            try
            {
                txtDescripcionDias.Text = string.Empty;
                LimpiaCapturaDias();
                DiasFeriados = new List<DiasFeriadosDetalle>();
                LlenaDias();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LimpiaCapturaDias()
        {
            try
            {
                txtDescripcionDia.Text = string.Empty;
                txtDate.Text = string.Empty;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaCombos()
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlDiasFeriados, _servicioDias.ObtenerDiasFeriados(true));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void LlenaDias()
        {
            try
            {
                rptDias.DataSource = DiasFeriados.OrderBy(o => o.Dia).ToList();
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
                    LlenaCombos();
                    txtDate.Focus();
                    txtDescripcionDias.Focus();
                    txtDate.Focus();
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

        protected void btnSeleccionar_OnClick(object sender, EventArgs e)
        {
            try
            {
                int idDiaFeriado = int.Parse(ddlDiasFeriados.SelectedValue);
                DiaFeriado selectedDay = _servicioDias.ObtenerDiaFeriado(idDiaFeriado);
                List<DiasFeriadosDetalle> tmpSeleccionados = DiasFeriados ?? new List<DiasFeriadosDetalle>();
                if (hfEditando.Value != string.Empty)
                {
                    tmpSeleccionados.Single(a => a.Dia == selectedDay.Fecha).Descripcion = txtDescripcionDia.Text.ToUpper();
                    tmpSeleccionados.Single(a => a.Dia == selectedDay.Fecha).Dia = Convert.ToDateTime(txtDate.Text);
                }
                else
                {
                    if (tmpSeleccionados.Any(a => a.Dia == selectedDay.Fecha))
                        throw new Exception("Ya se ha ingresado esta fecha");
                    tmpSeleccionados.Add(new DiasFeriadosDetalle
                    {
                        Descripcion = selectedDay.Descripcion,
                        Dia = selectedDay.Fecha
                    });
                }

                DiasFeriados = tmpSeleccionados;
                LlenaDias();
                ddlDiasFeriados.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                
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

        protected void btnAddDiaDescanso_OnClick(object sender, EventArgs e)
        {
            try
            {

                if (txtDate.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese una fecha");
                if (txtDescripcionDia.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese una descripción");
                if (DateTime.Parse(txtDate.Text) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                    throw new Exception("La fecha no puede ser anterior al dia actual");

                DiaFeriado newDay = new DiaFeriado { Descripcion = txtDescripcionDia.Text, Fecha = Convert.ToDateTime(txtDate.Text) };
                _servicioDias.AgregarDiaFeriado(newDay);
                LimpiaCapturaDias();
                LlenaCombos();
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
        protected void lnkBtnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                if (btn != null)
                {
                    DateTime fecha = Convert.ToDateTime(btn.CommandArgument);
                    hfEditando.Value = btn.CommandArgument;
                    List<DiasFeriadosDetalle> tmpSeleccionados = DiasFeriados ?? new List<DiasFeriadosDetalle>();
                    DiasFeriadosDetalle diaSeleccion = tmpSeleccionados.Single(f => f.Dia == fecha);
                    txtDescripcionDia.Text = diaSeleccion.Descripcion;
                    txtDate.Text = diaSeleccion.Dia.ToString("yyyy-MM-dd");
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
        protected void lbkBtnBorrar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                if (btn != null)
                {
                    DateTime fecha = Convert.ToDateTime(btn.CommandArgument);
                    List<DiasFeriadosDetalle> tmpSeleccionados = DiasFeriados ?? new List<DiasFeriadosDetalle>();
                    tmpSeleccionados.Remove(tmpSeleccionados.Single(f => f.Dia == fecha));
                    DiasFeriados = tmpSeleccionados;
                    LlenaDias();
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
        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcionDias.Text.Trim() == string.Empty)
                    throw new Exception("Ingrese un nombre");
                if (DiasFeriados.Count <= 0)
                    throw new Exception("Ingrese la menos un día");
                DiasFeriados newDias = new DiasFeriados();
                newDias.Descripcion = txtDescripcionDias.Text;
                newDias.DiasFeriadosDetalle = DiasFeriados;
                _servicioDias.CrearDiasFestivos(newDias);
                LlenaCombos();
                LimpiarPantalla();
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