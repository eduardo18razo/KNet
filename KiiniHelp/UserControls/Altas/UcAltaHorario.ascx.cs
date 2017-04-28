using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceDiasHorario;
using KiiniNet.Entities.Cat.Usuario;

namespace KiiniHelp.UserControls.Altas
{
    //TODO: Eliminar comentarios
    public partial class UcAltaHorario : UserControl, IControllerModal
    {
        private readonly ServiceDiasHorarioClient _servicioHorario = new ServiceDiasHorarioClient();
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

        public bool EsAlta
        {
            get { return Convert.ToBoolean(hfEsAlta.Value); }
            set
            {
                hfEsAlta.Value = value.ToString();
                Session["NuevoHorario"] = null;
            }
        }

        public int IdSubRol
        {
            get { return int.Parse(hfIdSubRol.Value); }
            set { hfIdSubRol.Value = value.ToString(); }
        }

        public RepeaterItemCollection HorariosSubRol
        {
            get { return rptHorarios.Items; }
        }
        private void LimpiarCampos()
        {
            try
            {
                txtDescripcion.Text = string.Empty;
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "SetTable();", true);
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
                Session["NuevoHorario"] = new List<HorarioDetalle>();
                hfLunes.Value = string.Empty;
                hfMartes.Value = string.Empty;
                hfMiercoles.Value = string.Empty;
                hfJueves.Value = string.Empty;
                hfViernes.Value = string.Empty;
                hfSabado.Value = string.Empty;
                hfDomingo.Value = string.Empty;
                MuestraHorarios((List<HorarioDetalle>)Session["NuevoHorario"]);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void MuestraHorarios(List<HorarioDetalle> lst)
        {
            try
            {
                if (Session["NuevoHorario"] == null && lst.Count <= 0)
                {
                    rptHorarios.DataSource = null;
                    rptHorarios.DataBind();
                    return;
                }
                Session["NuevoHorario"] = lst;
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
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "SetTable();", true);
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
                Alerta = _lstError;
            }
        }

        private void ValidaCapturaHorario()
        {
            StringBuilder sb = new StringBuilder();
            if(txtDescripcion.Text == string.Empty)
                sb.Append("Debe especificar un nombre.");
            if (hfLunes.Value == string.Empty && hfMartes.Value == string.Empty && hfMiercoles.Value == string.Empty && hfJueves.Value == string.Empty && hfViernes.Value == string.Empty && hfSabado.Value == string.Empty && hfDomingo.Value == string.Empty)
            {
                sb.Append("Seleccione un rango de horas.");
            }
            if(sb.ToString() != string.Empty)
                throw new Exception(sb.ToString());
        }

        private void ValidDatosHorario()
        {
            StringBuilder sb = new StringBuilder();
            if (txtDescripcion.Text.Trim() == string.Empty)
                sb.AppendLine("Ingrese una descripción.");
            if (rptHorarios.Items.Count <= 0)
                sb.AppendLine("Ingrese al menos un horario.");

            if (sb.ToString() != string.Empty)
            {
                //sb.Insert(0, "<h3>Agregar Horario</h3>");
                throw new Exception(sb.ToString());
            }
        }

        protected void btnAgregar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaHorario();
                int[] diasLun = hfLunes.Value != string.Empty ? hfLunes.Value.Split(',').Select(int.Parse).ToArray() : new int[0];
                int[] diasMar = hfMartes.Value != string.Empty ? hfMartes.Value.Split(',').Select(int.Parse).ToArray() : new int[0];
                int[] diasMie = hfMiercoles.Value != string.Empty ? hfMiercoles.Value.Split(',').Select(int.Parse).ToArray() : new int[0];
                int[] diasJue = hfJueves.Value != string.Empty ? hfJueves.Value.Split(',').Select(int.Parse).ToArray() : new int[0];
                int[] diasVie = hfViernes.Value != string.Empty ? hfViernes.Value.Split(',').Select(int.Parse).ToArray() : new int[0];
                int[] diasSab = hfSabado.Value != string.Empty ? hfSabado.Value.Split(',').Select(int.Parse).ToArray() : new int[0];
                int[] diasDom = hfDomingo.Value != string.Empty ? hfDomingo.Value.Split(',').Select(int.Parse).ToArray() : new int[0];
                Horario nuevoHorario = new Horario
                {
                    Descripcion = txtDescripcion.Text,
                    HorarioDetalle = new List<HorarioDetalle>()
                };
                if (diasLun.Any())
                {
                    foreach (int i in diasLun)
                    {
                        HorarioDetalle detalle = new HorarioDetalle
                        {
                            Dia = 1,
                            HoraInicio = TimeSpan.FromHours(i).ToString(),
                            HoraFin = TimeSpan.FromHours(i + 1).ToString()
                        };
                        nuevoHorario.HorarioDetalle.Add(detalle);
                    }
                }
                if (diasMar.Any())
                {
                    foreach (int i in diasMar)
                    {
                        HorarioDetalle detalle = new HorarioDetalle
                        {
                            Dia = 2,
                            HoraInicio = TimeSpan.FromHours(i).ToString(),
                            HoraFin = TimeSpan.FromHours(i + 1).ToString()
                        };
                        nuevoHorario.HorarioDetalle.Add(detalle);
                    }
                }
                if (diasMie.Any())
                {
                    foreach (int i in diasMie)
                    {
                        HorarioDetalle detalle = new HorarioDetalle
                        {
                            Dia = 3,
                            HoraInicio = TimeSpan.FromHours(i).ToString(),
                            HoraFin = TimeSpan.FromHours(i + 1).ToString()
                        };
                        nuevoHorario.HorarioDetalle.Add(detalle);
                    }
                }
                if (diasJue.Any())
                {
                    foreach (int i in diasJue)
                    {
                        HorarioDetalle detalle = new HorarioDetalle
                        {
                            Dia = 4,
                            HoraInicio = TimeSpan.FromHours(i).ToString(),
                            HoraFin = TimeSpan.FromHours(i + 1).ToString()
                        };
                        nuevoHorario.HorarioDetalle.Add(detalle);
                    }
                }
                if (diasVie.Any())
                {
                    foreach (int i in diasVie)
                    {
                        HorarioDetalle detalle = new HorarioDetalle
                        {
                            Dia = 5,
                            HoraInicio = TimeSpan.FromHours(i).ToString(),
                            HoraFin = TimeSpan.FromHours(i + 1).ToString()
                        };
                        nuevoHorario.HorarioDetalle.Add(detalle);
                    }
                }
                if (diasSab.Any())
                {
                    foreach (int i in diasSab)
                    {
                        HorarioDetalle detalle = new HorarioDetalle
                        {
                            Dia = 6,
                            HoraInicio = TimeSpan.FromHours(i).ToString(),
                            HoraFin = TimeSpan.FromHours(i + 1).ToString()
                        };
                        nuevoHorario.HorarioDetalle.Add(detalle);
                    }
                }
                if (diasDom.Any())
                {
                    foreach (int i in diasDom)
                    {
                        HorarioDetalle detalle = new HorarioDetalle
                        {
                            Dia = 0,
                            HoraInicio = TimeSpan.FromHours(i).ToString(),
                            HoraFin = TimeSpan.FromHours(i + 1).ToString()
                        };
                        nuevoHorario.HorarioDetalle.Add(detalle);
                    }
                }
                _servicioHorario.CrearHorario(nuevoHorario);
                LimpiarPantalla();
                if (OnAceptarModal != null)
                    OnAceptarModal();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptTable", "SetTable();", true);
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
                List<HorarioDetalle> lst = (List<HorarioDetalle>)Session["NuevoHorario"];
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
                ValidDatosHorario();
                Horario horario = new Horario
                {
                    Descripcion = txtDescripcion.Text.Trim(),
                    Habilitado = true,
                    HorarioDetalle = (List<HorarioDetalle>)Session["NuevoHorario"]
                };
                _servicioHorario.CrearHorario(horario);
                
                
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