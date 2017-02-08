using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceDiasHorario;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class UcAltaHorario : UserControl, IControllerModal
    {
        private readonly ServiceDiasHorarioClient _servicioHorario = new ServiceDiasHorarioClient();
        private readonly ServiceGrupoUsuarioClient _servicioGrupoUsuario = new ServiceGrupoUsuarioClient();
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

        private void LlenaCombo()
        {
            try
            {
                ddlGrupoSolicito.DataSource = _servicioGrupoUsuario.ObtenerGrupos(true);
                ddlGrupoSolicito.DataTextField = "Descripcion";
                ddlGrupoSolicito.DataValueField = "Id";
                ddlGrupoSolicito.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void LimpiarCampos()
        {
            try
            {
                txtHoraInicio.Text = string.Empty;
                txtHoraFin.Text = string.Empty;
                foreach (ListItem item in chklbxDias.Items)
                {
                    item.Selected = false;
                }
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
                Session["NuevoHorario"] = new List<HorarioDetalle>();
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
                if (!IsPostBack)
                {
                    LlenaCombo();
                    chklbxDias.Items.Add(new ListItem("TODOS", "99"));
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

        private void ValidaCapturaHorario()
        {
            StringBuilder sb = new StringBuilder();

            if (txtHoraInicio.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Ingrese hora inicio.</li>");
            if (txtHoraFin.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Ingrese hora Fin.</li>");
            if (!timeStartValidator.IsValid || !timeStartValidator.IsValidEmpty)
                sb.AppendLine("<li>Introdusca una hora de inicio valida.</li>");
            if (!timeEndValidator.IsValid || !timeEndValidator.IsValidEmpty)
                sb.AppendLine("<li>Introdusca una hora fin valida.</li>");
            if (chklbxDias.Items.Cast<ListItem>().Count(item => item.Selected) <= 0)
                sb.AppendLine("<li>Seleccione al menos un día.</li>");
            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Datos Horario</h3>");
                throw new Exception(sb.ToString());
            }
        }

        private void ValidDatosHorario()
        {
            StringBuilder sb = new StringBuilder();
            if (ddlGrupoSolicito.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                sb.AppendLine("<li>Selecciones un Grupo.</li>");
            if (txtDescripcion.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Ingrese una descripción.</li>");
            if (rptHorarios.Items.Count <= 0)
                sb.AppendLine("<li>Ingrese al menos un horario.</li>");
            
            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Agregar Horario</h3>");
                throw new Exception(sb.ToString());
            }
        }

        protected void btnAgregar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaHorario();
                List<HorarioDetalle> lst = (List<HorarioDetalle>)Session["NuevoHorario"] ?? new List<HorarioDetalle>();
                foreach (ListItem dia in chklbxDias.Items.Cast<ListItem>().Where(w => w.Value != "99"))
                {
                    if (!dia.Selected) continue;
                    foreach (RepeaterItem itemHorario in rptHorarios.Items)
                    {
                        Label lblDia = (Label) itemHorario.FindControl("lblDia");
                        if (lblDia != null)
                        {
                            if (lblDia.Text == dia.Value)
                            {
                                Label lblHoraInicio = (Label) itemHorario.FindControl("lblHoraInicio");
                                Label lblHoraFin = (Label) itemHorario.FindControl("lblHoraFin");
                                DateTime horaInicioIngresar = DateTime.ParseExact(Convert.ToDateTime(txtHoraInicio.Text.Trim()).ToString("HH:mm:ss"), "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                                DateTime horaFinIngresar = DateTime.ParseExact(Convert.ToDateTime(txtHoraFin.Text.Trim()).ToString("HH:mm:ss"), "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                                DateTime horaInicioEncontrada = DateTime.ParseExact(lblHoraInicio.Text, "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                                DateTime horaFinEncontrada = DateTime.ParseExact(lblHoraFin.Text, "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                                if (horaInicioIngresar >= horaInicioEncontrada)
                                {
                                    if (horaInicioIngresar <= horaFinEncontrada)
                                    {
                                        throw new Exception("No se puede ingresar hora traslapada");
                                    }
                                }

                                if (horaFinIngresar <= horaFinEncontrada)
                                {
                                    if (horaFinIngresar >= horaInicioEncontrada)
                                    {
                                        throw new Exception("No se puede ingresar hora traslapada");
                                    }
                                }
                                
                                    
                            }
                        }
                    }

                    if (!lst.Any(s =>s.HoraInicio == Convert.ToDateTime(txtHoraInicio.Text.Trim()).ToString("HH:mm:ss") &&
                                     s.Dia == Convert.ToInt32(dia.Value)))
                        lst.Add(new HorarioDetalle
                        {
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
                    IdGrupoSolicito = int.Parse(ddlGrupoSolicito.SelectedValue),
                    Habilitado = true,
                    HorarioDetalle = (List<HorarioDetalle>)Session["NuevoHorario"]
                };
                _servicioHorario.CrearHorario(horario);
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

        protected void chklbxDias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chklbxDias.Items.Cast<ListItem>().Any(item => item.Selected && item.Value == "99"))
                {
                    foreach (ListItem dia in chklbxDias.Items)
                    {
                        dia.Selected = true;
                    }
                }

                if (chklbxDias.Items.Cast<ListItem>().Any(item => item.Selected && item.Value == "99") && chklbxDias.Items.Cast<ListItem>().Any(item => !item.Selected))
                {
                    ListItem item = chklbxDias.Items.Cast<ListItem>().SingleOrDefault(s => s.Value == "99");
                    if (item != null)
                        item.Selected = false;
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