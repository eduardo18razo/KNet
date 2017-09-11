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

        public int IdHorarioEditar
        {
            get { return int.Parse(hdIdHorario.Value); }
            set
            {
                hdIdHorario.Value = value.ToString();
                //if (value != 0)
                //{
                //    DiasFeriados diasDescanso = _servicioDias.ObtenerDiasFeriadosUserById(value);
                //    if (diasDescanso != null)
                //    {
                //        txtDescripcionDias.Text = diasDescanso.Descripcion;
                //        foreach (DiasFeriadosDetalle detalle in diasDescanso.DiasFeriadosDetalle)
                //        {
                //            DiasFeriados = new List<DiaFeriado>();
                //            DiasFeriados.Add(_servicioDias.ObtenerDiaByFecha(detalle.Dia));
                //        }
                //        LlenaDias();
                //    }
                    
                //}
            }

        }
        private List<DiaFeriado> DiasFeriados
        {
            get { return (List<DiaFeriado>)Session["DiasSeleccionados"]; }
            set { Session["DiasSeleccionados"] = value; }
        }

        public bool EsAlta
        {
            get { return Convert.ToBoolean(hfEsAlta.Value); }
            set
            {
                hfEsAlta.Value = value.ToString();
                IdHorarioEditar = value ? 0 : IdHorarioEditar;
            }
        }
        public int IdDiaFeriado
        {
            get { return int.Parse(hfIdDiaFeriado.Value); }
            set
            {
                hfIdDiaFeriado.Value = value.ToString();
                DiasFeriados dia = _servicioDias.ObtenerDiasFeriadosUserById(value);
                if (dia != null)
                {
                    IdHorarioEditar = dia.Id;
                    txtDescripcionDias.Text = dia.Descripcion;
                    DiasFeriados = new List<DiaFeriado>();
                    foreach (DiasFeriadosDetalle detalle in dia.DiasFeriadosDetalle)
                    {
                        DiaFeriado diaAdd = new DiaFeriado();
                        diaAdd.Descripcion = detalle.Descripcion;
                        diaAdd.Fecha = detalle.Dia;
                        diaAdd.Id = _servicioDias.ObtenerDiaByFecha(detalle.Dia).Id;
                        DiasFeriados.Add(diaAdd);
                    }
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
                DiasFeriados = new List<DiaFeriado>();
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
                IdDiaFeriado = 0;
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
                rptDias.DataSource = DiasFeriados.OrderBy(o => o.Fecha).ToList();
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
                if(ddlDiasFeriados.SelectedIndex<= BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    throw new Exception();
                int idDiaFeriado = int.Parse(ddlDiasFeriados.SelectedValue);
                DiaFeriado selectedDay = _servicioDias.ObtenerDiaFeriado(idDiaFeriado);
                List<DiaFeriado> tmpSeleccionados = DiasFeriados ?? new List<DiaFeriado>();
                if (hfEditando.Value != string.Empty)
                {
                    tmpSeleccionados.Single(a => a.Fecha == selectedDay.Fecha).Descripcion = txtDescripcionDia.Text.ToUpper();
                    tmpSeleccionados.Single(a => a.Fecha == selectedDay.Fecha).Fecha = Convert.ToDateTime(txtDate.Text);
                }
                else
                {
                    if (tmpSeleccionados.Any(a => a.Fecha == selectedDay.Fecha))
                        throw new Exception("Ya se ha ingresado esta fecha");
                    tmpSeleccionados.Add(selectedDay);
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

                    DiaFeriado newDay = new DiaFeriado {Id = IdDiaFeriado == 0 ? 0 : IdDiaFeriado,  Descripcion = txtDescripcionDia.Text, Fecha = Convert.ToDateTime(txtDate.Text) };
                _servicioDias.AgregarDiaFeriado(newDay);
                UsuariosMaster master = (UsuariosMaster) Page.Master;
                if(master != null)
                    master.AlertaSucces();
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
                    IdDiaFeriado = _servicioDias.ObtenerDiaByFecha(fecha).Id;
                    //hfEditando.Value = btn.CommandArgument;
                    //List<DiasFeriadosDetalle> tmpSeleccionados = DiasFeriados ?? new List<DiasFeriadosDetalle>();
                    //DiasFeriadosDetalle diaSeleccion = tmpSeleccionados.Single(f => f.Dia == fecha);
                    //txtDescripcionDia.Text = diaSeleccion.Descripcion;
                    //txtDate.Text = diaSeleccion.Dia.ToString("yyyy-MM-dd");
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
                    List<DiaFeriado> tmpSeleccionados = DiasFeriados ?? new List<DiaFeriado>();
                    tmpSeleccionados.Remove(tmpSeleccionados.Single(f => f.Fecha == fecha));
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
                if (EsAlta)
                {
                    DiasFeriados newDias = new DiasFeriados();
                    newDias.Descripcion = txtDescripcionDias.Text;
                    newDias.DiasFeriadosDetalle = new List<DiasFeriadosDetalle>();
                    foreach (DiaFeriado feriado in DiasFeriados)
                    {
                        DiaFeriado day = _servicioDias.ObtenerDiaFeriado(feriado.Id);
                        if (day != null)
                        {
                            newDias.DiasFeriadosDetalle.Add(new DiasFeriadosDetalle
                            {
                                Dia = day.Fecha,
                                Descripcion = day.Descripcion,
                                Habilitado = true
                            });
                        }
                    }
                    _servicioDias.CrearDiasFestivos(newDias);
                }
                else
                {
                    DiasFeriados newDias = new DiasFeriados();
                    newDias.Id = IdHorarioEditar;
                    newDias.Descripcion = txtDescripcionDias.Text;
                    newDias.DiasFeriadosDetalle = new List<DiasFeriadosDetalle>();
                    foreach (DiaFeriado feriado in DiasFeriados)
                    {
                        DiaFeriado day = _servicioDias.ObtenerDiaFeriado(feriado.Id);
                        if (day != null)
                        {
                            newDias.DiasFeriadosDetalle.Add(new DiasFeriadosDetalle
                            {
                                Dia = day.Fecha,
                                Descripcion = day.Descripcion,
                                Habilitado = true
                            });
                        }
                    }
                    _servicioDias.ActualizarDiasFestivos(newDias);
                }
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