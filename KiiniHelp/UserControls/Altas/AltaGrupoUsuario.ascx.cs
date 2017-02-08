using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceDiasHorario;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniHelp.ServiceSistemaSubRol;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaGrupoUsuario : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        private readonly ServiceDiasHorarioClient _servicioDiaHorario = new ServiceDiasHorarioClient();
        private readonly ServiceGrupoUsuarioClient _servicioGrupoUsuario = new ServiceGrupoUsuarioClient();
        private readonly ServiceSubRolClient _servicioSistemaSubRol = new ServiceSubRolClient();
        private readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
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

        public int IdTipoGrupo
        {
            get { return Convert.ToInt32(hfIdGrupo.Value); }
            set
            {
                hfIdGrupo.Value = value.ToString();
                divParametros.Visible = false;
                lblTitle.Text = "Agregar Grupo ";
                switch (value)
                {
                    case (int)BusinessVariables.EnumTiposGrupos.Administrador:
                        lblTitle.Text += "Administrador";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.Usuario:
                        lblTitle.Text += "Usuario";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ConsultasEspeciales:
                        lblTitle.Text += "Consultas Especiales";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención:
                        lblTitle.Text += "Responsable de Atención";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeContenido:
                        lblTitle.Text += "Responsable de Contenido";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación:
                        lblTitle.Text += "Responsable de Operación";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo:
                        lblTitle.Text += "Responsable de Desarrollo";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableServicio:
                        lblTitle.Text += "Responsable de Servicio";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.AgenteUniversal:
                        divParametros.Visible = true;
                        lblTitle.Text = "Contac Center";
                        break;
                }
                List<SubRol> lstRoles = _servicioSistemaSubRol.ObtenerSubRolesByTipoGrupo(value, false);
                divSubRoles.Visible = lstRoles.Count > 0;

                //if ((int)BusinessVariables.EnumTiposGrupos.DueñoDelServicio == value)
                //    divSubRoles.Visible = true;
                rptSubRoles.DataSource = lstRoles;
                rptSubRoles.DataBind();
            }
        }

        public bool FromOpcion
        {
            get { return Convert.ToBoolean(hfFromOpcion.Value); }
            set { hfFromOpcion.Value = value.ToString(); }
        }

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(ddlTipoUsuarioAltaGrupo.SelectedValue); }
            set
            {
                Metodos.LlenaComboCatalogo(ddlTipoUsuarioAltaGrupo, _servicioSistemaTipoUsuario.ObtenerTiposUsuario(true));
                ddlTipoUsuarioAltaGrupo.SelectedValue = value.ToString();
                ddlTipoUsuarioAltaGrupo.Enabled = FromOpcion;
            }
        }

        public int IdRol
        {
            get { return Convert.ToInt32(btnCancelar.CommandArgument); }
            set { btnCancelar.CommandArgument = value.ToString(); }
        }

        public bool Alta
        {
            get { return Convert.ToBoolean(ViewState["Alta"].ToString()); }
            set
            {
                ViewState["Alta"] = value.ToString();
            }
        }

        public GrupoUsuario GrupoUsuario
        {
            get { return (GrupoUsuario)Session["GrupoUsuarioEditar"]; }
            set
            {
                try
                {
                    Session["GrupoUsuarioEditar"] = value;
                    IdTipoUsuario = value.IdTipoUsuario;
                    IdTipoGrupo = value.IdTipoGrupo;
                    txtDescripcionGrupoUsuario.Text = value.Descripcion;
                    if (value.IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.AgenteUniversal)
                    {
                        rbtnLevanta.Checked = value.LevantaTicket;
                        rbtnRecado.Checked = value.RecadoTicket;
                    }
                    if (value.SubGrupoUsuario == null) return;
                    foreach (SubGrupoUsuario subGrupo in value.SubGrupoUsuario.OrderBy(o => o.IdSubRol))
                    {
                        foreach (RepeaterItem item in rptSubRoles.Items)
                        {
                            CheckBox chk = (CheckBox)item.FindControl("chkSubRol");
                            if (chk != null)
                            {
                                Button btnHorarios = (Button)item.FindControl("btnHorarios");
                                btnHorarios.CommandName = subGrupo.Id.ToString();

                                Button btnDias = (Button)item.FindControl("btnDiasDescanso");
                                btnDias.CommandName = subGrupo.Id.ToString();
                                if (subGrupo.IdSubRol == Convert.ToInt32(chk.Attributes["value"]))
                                {
                                    chk.Checked = subGrupo.IdSubRol == Convert.ToInt32(chk.Attributes["value"]);
                                    if (chk.Checked)
                                    {
                                        OnCheckedChanged(chk, null);
                                        ((DropDownList)item.FindControl("ddlHorario")).SelectedValue = subGrupo.HorarioSubGrupo.First(s => s.IdSubGrupoUsuario == subGrupo.Id).IdHorario.ToString();
                                        CargaHorario();
                                        ucAltaDiasFestivos.SetDiasFestivosSubRol(_servicioGrupoUsuario.ObtenerDiasByIdSubGrupo(subGrupo.Id), int.Parse(btnDias.CommandArgument));
                                        CargaDias();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void CargaHorario()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CargaDias()
        {
            try
            {
                Session["DiasSubRoles"] = Session["DiasSubRoles"] ?? new List<DiaFestivoSubGrupo>();
                foreach (RepeaterItem item in ucAltaDiasFestivos.DiasDescansoSubRol)
                {
                    List<DiaFestivoSubGrupo> tmpEliminar = ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Where(w => w.IdSubGrupoUsuario == ucAltaDiasFestivos.IdSubRol).ToList();
                    foreach (DiaFestivoSubGrupo dia in tmpEliminar)
                    {
                        ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Remove(dia);
                    }
                }

                foreach (RepeaterItem item in ucAltaDiasFestivos.DiasDescansoSubRol)
                {
                    Label lblFecha = (Label)item.FindControl("lblFecha");
                    Label lblDescripcion = (Label)item.FindControl("lblDescripcion");
                    DiaFestivoSubGrupo dia = new DiaFestivoSubGrupo
                    {
                        IdSubGrupoUsuario = ucAltaDiasFestivos.IdSubRol,
                        Fecha = Convert.ToDateTime(lblFecha.Text),
                        Descripcion = lblDescripcion.Text.Trim().ToUpper()
                    };
                    ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Add(dia);
                }
                foreach (RepeaterItem itemSr in rptSubRoles.Items)
                {
                    Label lbl = (Label)itemSr.FindControl("lblId");
                    if (int.Parse(lbl.Text) == ucAltaDiasFestivos.IdSubRol)
                    {
                        Button btn = (Button)itemSr.FindControl("btnDiasDescanso");
                        btn.CssClass = "col-sm-3 btn btn-success";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ValidaCapturaGrupoUsuario()
        {
            StringBuilder sb = new StringBuilder();

            if (txtDescripcionGrupoUsuario.Text.Trim() == string.Empty)
                sb.AppendLine("Descripcion es un campo obligatorio.<br>");
            if (IdTipoGrupo == (int) BusinessVariables.EnumTiposGrupos.AgenteUniversal)
            {
                if(!rbtnLevanta.Checked && !rbtnRecado.Checked)
                    sb.AppendLine("Seleccione una opción para este grupo.<br>");
            }

            if (sb.ToString() != string.Empty)
                throw new Exception(sb.ToString());
        }

        private void LimpiarCampos()
        {
            try
            {
                txtDescripcionGrupoUsuario.Text = String.Empty;
                rptSubRoles.DataSource = null;
                rptSubRoles.DataBind();
                if (Session["GrupoUsuarioEditar"] != null)
                    foreach (SubGrupoUsuario subGrupo in ((GrupoUsuario)Session["GrupoUsuarioEditar"]).SubGrupoUsuario.OrderBy(o => o.IdSubRol))
                    {
                        Session.Remove("DiasFestivos" + subGrupo.IdSubRol);
                        Session.Remove(subGrupo.IdSubRol.ToString());
                    }
                Session.Remove("GrupoUsuarioEditar");
                Session.Remove("HorariosSubRoles");
                Session.Remove("DiasSubRoles");
                Session.Remove("DiasFestivos");
                IdTipoGrupo = IdTipoGrupo;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Alerta = new List<string>();
                ucAltaDiasFestivos.OnAceptarModal += UcAltaDiasFestivosOnOnAceptarModal;
                ucAltaDiasFestivos.OnCancelarModal += UcAltaDiasFestivosOnOnCancelarModal;

                ucAltaHorario.OnAceptarModal += UcHorarioOnOnAceptarModal;
                ucAltaHorario.OnCancelarModal += UcHorarioOnOnCancelarModal;

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

        private void UcHorarioOnOnAceptarModal()
        {
            try
            {
                foreach (RepeaterItem item in rptSubRoles.Items)
                {
                    Label lblId = (Label)item.FindControl("lblId");
                    if (lblId == null) continue;
                    if (int.Parse(lblId.Text) != ucAltaHorario.IdSubRol) continue;
                    var parent = lblId.NamingContainer;
                    DropDownList ddl = (DropDownList)parent.FindControl("ddlHorario");
                    ddl.DataSource = _servicioDiaHorario.ObtenerHorarioDefault(true);
                    ddl.DataTextField = "Descripcion";
                    ddl.DataValueField = "Id";
                    ddl.DataBind();
                    break;
                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalHorarios\");", true);
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

        private void UcHorarioOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalHorarios\");", true);
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

        private void UcAltaDiasFestivosOnOnAceptarModal()
        {
            try
            {
                Session["DiasSubRoles"] = Session["DiasSubRoles"] ?? new List<DiaFestivoSubGrupo>();
                foreach (RepeaterItem item in ucAltaDiasFestivos.DiasDescansoSubRol)
                {
                    List<DiaFestivoSubGrupo> tmpEliminar = ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Where(w => w.IdSubGrupoUsuario == ucAltaDiasFestivos.IdSubRol).ToList();
                    foreach (DiaFestivoSubGrupo dia in tmpEliminar)
                    {
                        ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Remove(dia);
                    }
                }

                foreach (RepeaterItem item in ucAltaDiasFestivos.DiasDescansoSubRol)
                {
                    Label lblFecha = (Label)item.FindControl("lblFecha");
                    Label lblDescripcion = (Label)item.FindControl("lblDescripcion");
                    DiaFestivoSubGrupo dia = new DiaFestivoSubGrupo
                    {
                        IdSubGrupoUsuario = ucAltaDiasFestivos.IdSubRol,
                        Fecha = Convert.ToDateTime(lblFecha.Text),
                        Descripcion = lblDescripcion.Text.Trim().ToUpper()
                    };
                    ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Add(dia);
                }
                foreach (RepeaterItem itemSr in rptSubRoles.Items)
                {
                    Label lbl = (Label)itemSr.FindControl("lblId");
                    if (int.Parse(lbl.Text) == ucAltaDiasFestivos.IdSubRol)
                    {
                        Button btn = (Button)itemSr.FindControl("btnDiasDescanso");
                        btn.CssClass = "col-sm-3 btn btn-success";
                    }
                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalDiasDescanso\");", true);
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

        private void UcAltaDiasFestivosOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalDiasDescanso\");", true);
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
                ValidaCapturaGrupoUsuario();
                GrupoUsuario grupoUsuario;
                if (Alta)
                {
                    grupoUsuario = new GrupoUsuario
                    {
                        IdTipoUsuario = IdTipoUsuario,
                        IdTipoGrupo = Convert.ToInt32(IdTipoGrupo),
                        Descripcion = txtDescripcionGrupoUsuario.Text,
                        Habilitado = chkHabilitado.Checked,
                        SubGrupoUsuario = new List<SubGrupoUsuario>()
                    };
                    if (IdTipoGrupo == (int) BusinessVariables.EnumTiposGrupos.AgenteUniversal)
                    {
                        grupoUsuario.LevantaTicket = rbtnLevanta.Checked;
                        grupoUsuario.RecadoTicket = rbtnRecado.Checked;
                    }
                    else
                    {
                        grupoUsuario.LevantaTicket = false;
                        grupoUsuario.RecadoTicket = false;
                    }
                    Dictionary<int, int> horarios = new Dictionary<int, int>();
                    Dictionary<int, List<DiaFestivoSubGrupo>> diasDescanso = new Dictionary<int, List<DiaFestivoSubGrupo>>();
                    foreach (CheckBox chk in (from RepeaterItem item in rptSubRoles.Items select (CheckBox)item.FindControl("chkSubRol")).Where(chk => chk.Checked))
                    {
                        DropDownList ddlHorario = (DropDownList)chk.NamingContainer.FindControl("ddlHorario");
                        if (ddlHorario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                            throw new Exception("Debe capturar horarios");

                        horarios.Add(int.Parse(chk.Attributes["value"]), int.Parse(ddlHorario.SelectedValue));
                        if (Session["DiasSubRoles"] != null)
                        {
                            diasDescanso.Add(int.Parse(chk.Attributes["value"]), ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Where(w => w.IdSubGrupoUsuario == Convert.ToInt32(chk.Attributes["value"])).ToList());
                        }
                    }
                    _servicioGrupoUsuario.GuardarGrupoUsuario(grupoUsuario, horarios, diasDescanso);
                }
                else
                {
                    grupoUsuario = GrupoUsuario;
                    grupoUsuario.Descripcion = txtDescripcionGrupoUsuario.Text;
                    Dictionary<int, int> horarios = new Dictionary<int, int>();
                    Dictionary<int, List<DiaFestivoSubGrupo>> diasDescanso = new Dictionary<int, List<DiaFestivoSubGrupo>>();
                    foreach (CheckBox chk in (from RepeaterItem item in rptSubRoles.Items select (CheckBox)item.FindControl("chkSubRol")).Where(chk => chk.Checked))
                    {
                        DropDownList ddlHorario = (DropDownList)chk.NamingContainer.FindControl("ddlHorario");
                        if (ddlHorario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                            throw new Exception("Debe capturar horarios");

                        horarios.Add(int.Parse(chk.Attributes["value"]), int.Parse(ddlHorario.SelectedValue));
                        if (Session["DiasSubRoles"] != null)
                        {
                            diasDescanso.Add(int.Parse(chk.Attributes["value"]), ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Where(w => w.IdSubGrupoUsuario == Convert.ToInt32(chk.Attributes["value"])).ToList());
                        }
                    }
                    if (IdTipoGrupo == (int)BusinessVariables.EnumTiposGrupos.AgenteUniversal)
                    {
                        grupoUsuario.LevantaTicket = rbtnLevanta.Checked;
                        grupoUsuario.RecadoTicket = rbtnRecado.Checked;
                    }
                    else
                    {
                        grupoUsuario.LevantaTicket = false;
                        grupoUsuario.RecadoTicket = false;
                    }
                    _servicioGrupoUsuario.ActualizarGrupo(grupoUsuario, horarios, diasDescanso);
                }
                LimpiarCampos();
                IdTipoGrupo = Convert.ToInt32(hfIdGrupo.Value);
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
                LimpiarCampos();
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
                LimpiarCampos();
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

        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                if (chk != null)
                {

                    if (chk.DataItemContainer != null)
                    {
                        int valueSelected = Convert.ToInt32(chk.Attributes["value"]);

                        switch (valueSelected)
                        {
                            case (int)BusinessVariables.EnumSubRoles.TercerNivel:
                                if ((from RepeaterItem item in rptSubRoles.Items select (CheckBox)item.FindControl("chkSubRol")).Where(chkRepeater => Convert.ToInt32(chkRepeater.Attributes["value"]) == (int)BusinessVariables.EnumSubRoles.SegundoNivel).Any(chkRepeater => !chkRepeater.Checked))
                                {
                                    chk.Checked = false;
                                    throw new Exception("Require Segundo nivel.");
                                }
                                break;
                            case (int)BusinessVariables.EnumSubRoles.CuartoNivel:
                                if ((from RepeaterItem item in rptSubRoles.Items select (CheckBox)item.FindControl("chkSubRol")).Where(chkRepeater => Convert.ToInt32(chkRepeater.Attributes["value"]) == (int)BusinessVariables.EnumSubRoles.TercerNivel).Any(chkRepeater => !chkRepeater.Checked))
                                {
                                    chk.Checked = false;
                                    throw new Exception("Require Tercer nivel.");
                                }
                                break;
                        }
                        DropDownList ddl = (DropDownList)chk.DataItemContainer.FindControl("ddlHorario");
                        ddl.Enabled = chk.Checked;
                        ddl.DataSource = _servicioDiaHorario.ObtenerHorarioDefault(true);
                        ddl.DataTextField = "Descripcion";
                        ddl.DataValueField = "Id";
                        ddl.DataBind();
                        Button btnHorarios = (Button)chk.DataItemContainer.FindControl("btnHorarios");
                        Button btnDiasDescanso = (Button)chk.DataItemContainer.FindControl("btnDiasDescanso");
                        if (chk.Checked)
                        {
                            btnHorarios.CssClass = "col-sm-2 btn btn-sm btn-primary";
                            btnDiasDescanso.CssClass = "col-sm-2 btn btn-sm btn-primary";
                        }
                        else
                        {
                            Metodos.LimpiarCombo(ddl);
                            btnHorarios.CssClass = "col-sm-2 btn btn-sm btn-primary disabled";
                            btnDiasDescanso.CssClass = "col-sm-2 btn btn-sm btn-primary disabled";
                        }
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

        protected void rptSubRoles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                SubRol sbRol = (SubRol)e.Item.DataItem;
                switch (IdTipoGrupo)
                {
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención:
                        if (sbRol.Id == (int)BusinessVariables.EnumSubRoles.PrimererNivel)
                        {
                            ((CheckBox)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol")).Checked = true;
                            OnCheckedChanged(((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol"), null);
                            ((CheckBox)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol")).Enabled = false;
                            ((Button)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("btnHorarios")).CssClass = "col-sm-2 btn btn-sm btn-primary";
                            ((Button)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("btnDiasDescanso")).CssClass = "col-sm-2 btn btn-sm btn-primary";
                        }
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeContenido:
                        if (sbRol.Id == (int)BusinessVariables.EnumSubRoles.Autorizador)
                        {
                            ((CheckBox)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol")).Checked = true;
                            OnCheckedChanged(((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol"), null);
                            ((CheckBox)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol")).Enabled = false;
                            ((Button)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("btnHorarios")).CssClass = "col-sm-2 btn btn-sm btn-primary";
                            ((Button)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("btnDiasDescanso")).CssClass = "col-sm-2 btn-sm btn btn-primary";
                        }
                        break;
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

        protected void btnHorarios_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Alta)
                {
                    ucAltaHorario.EsAlta = true;
                    ucAltaHorario.IdSubRol = Convert.ToInt32(((Button)sender).CommandArgument);
                }
                else
                {

                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalHorarios\");", true);
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

        protected void btnDiasDescanso_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (Alta)
                {
                    ucAltaDiasFestivos.EsAlta = true;
                    ucAltaDiasFestivos.IdSubRol = Convert.ToInt32(((Button)sender).CommandArgument);
                }
                else
                {
                    ucAltaDiasFestivos.EsAlta = false;
                    ucAltaDiasFestivos.IdSubRol = Convert.ToInt32(((Button)sender).CommandArgument);
                    ucAltaDiasFestivos.SetDiasFestivosSubRol(((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Where(w => w.IdSubGrupoUsuario == Convert.ToInt32(((Button)sender).CommandArgument)).ToList(), Convert.ToInt32(((Button)sender).CommandArgument));
                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalDiasDescanso\");", true);
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