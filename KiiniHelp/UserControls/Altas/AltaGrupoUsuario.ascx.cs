using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
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
                switch (value)
                {
                    case (int)BusinessVariables.EnumTiposGrupos.Administrador:
                        lblTitle.Text = "Alta Grupo de Usuario de tipo Administrador";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.Acceso:
                        lblTitle.Text = "Alta Grupo de Usuario de tipo Acceso";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta:
                        lblTitle.Text = "Alta Grupo de Usuario de tipo Especial de consulta";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención:
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Responsable de atención";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeMantenimiento:
                        lblTitle.Text = "Alta Grupo de Usuario de tipo Responsable de Mantenimiento";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación:
                        lblTitle.Text = "Alta Grupo de Usuario de tipo Responsable de Operación";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo:
                        lblTitle.Text = "Alta Grupo de Usuario de tipo Responsable de Desarrollo";
                        break;
                }
                List<SubRol> lstRoles = _servicioSistemaSubRol.ObtenerSubRolesByTipoGrupo(value, false);
                divSubRoles.Visible = lstRoles.Count > 0;
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
                rptSubRoles.Visible = value;
            }
        }

        public GrupoUsuario GrupoUsuario
        {
            get { return (GrupoUsuario)Session["GrupoUsuarioEditar"]; }
            set
            {
                Session["GrupoUsuarioEditar"] = value;
                IdTipoUsuario = value.IdTipoUsuario;
                IdTipoGrupo = value.IdTipoGrupo;
                txtDescripcionGrupoUsuario.Text = value.Descripcion;
                foreach (CheckBox chk in from RepeaterItem item in rptSubRoles.Items select (CheckBox)item.FindControl("chkSubRol"))
                {
                    chk.Checked = false;
                    OnCheckedChanged(chk, null);
                }
                if (value.SubGrupoUsuario == null) return;
                foreach (SubGrupoUsuario subGrupo in value.SubGrupoUsuario)
                {
                    foreach (CheckBox chk in from RepeaterItem item in rptSubRoles.Items select (CheckBox)item.FindControl("chkSubRol"))
                    {
                        chk.Checked = subGrupo.Id == Convert.ToInt32(chk.Attributes["value"]);
                        if (chk.Checked)
                            OnCheckedChanged(chk, null);
                    }
                }
            }
        }

        private void ValidaCapturaGrupoUsuario()
        {
            StringBuilder sb = new StringBuilder();

            if (txtDescripcionGrupoUsuario.Text.Trim() == string.Empty)
                sb.AppendLine("Descripcion es un campo obligatorio.<br>");

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

                ucHorario.OnAceptarModal += UcHorarioOnOnAceptarModal;
                ucHorario.OnCancelarModal += UcHorarioOnOnCancelarModal;

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
                Session["HorariosSubRoles"] = Session["HorariosSubRoles"] ?? new List<HorarioSubGrupo>();
                foreach (RepeaterItem item in ucHorario.HorariosSubRol)
                {
                    Label lblIdSubRol = (Label)item.FindControl("lblIdSubRol");
                    var tmpEliminar = ((List<HorarioSubGrupo>)Session["HorariosSubRoles"]).Where(w => w.IdSubGrupoUsuario == Convert.ToInt32(lblIdSubRol.Text));
                    foreach (HorarioSubGrupo dia in tmpEliminar)
                    {
                        ((List<HorarioSubGrupo>)Session["HorariosSubRoles"]).Remove(dia);
                    }
                }
                foreach (RepeaterItem item in ucHorario.HorariosSubRol)
                {
                    Label lblIdSubRol = (Label)item.FindControl("lblIdSubRol");
                    Label lblDia = (Label)item.FindControl("lblDia");
                    Label lblHoraInicio = (Label)item.FindControl("lblHoraInicio");
                    Label lblHorafin = (Label)item.FindControl("lblHoraFin");
                    HorarioSubGrupo dia = new HorarioSubGrupo
                    {
                        IdSubGrupoUsuario = Convert.ToInt32(lblIdSubRol.Text),
                        Dia = Convert.ToInt32(lblDia.Text),
                        HoraInicio = lblHoraInicio.Text,
                        HoraFin = lblHorafin.Text
                    };
                    ((List<HorarioSubGrupo>)Session["HorariosSubRoles"]).Add(dia);
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
                    Label lblIdSubrol = (Label)item.FindControl("lblSubRol");
                    var tmpEliminar = ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Where(w => w.IdSubGrupoUsuario == Convert.ToInt32(lblIdSubrol.Text));
                    foreach (DiaFestivoSubGrupo dia in tmpEliminar)
                    {
                        ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Remove(dia);
                    }
                }

                foreach (RepeaterItem item in ucAltaDiasFestivos.DiasDescansoSubRol)
                {
                    Label lblIdSubrol = (Label)item.FindControl("lblSubRol");
                    Label lblFecha = (Label)item.FindControl("lblFecha");
                    Label lblDescripcion = (Label)item.FindControl("lblDescripcion");
                    DiaFestivoSubGrupo dia = new DiaFestivoSubGrupo
                    {
                        IdSubGrupoUsuario = Convert.ToInt32(lblIdSubrol.Text),
                        Fecha = Convert.ToDateTime(lblFecha.Text),
                        Descripcion = lblDescripcion.Text.Trim().ToUpper()
                    };
                    ((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Add(dia);
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

        //protected void chklbxSubRoles_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int valueSelected = 0;
        //        string result = Request.Form["__EVENTTARGET"];
        //        string[] checkedBox = result.Split('$');
        //        int index = int.Parse(checkedBox[checkedBox.Length - 1]);
        //        if (chklbxSubRoles.Items[index].Selected)
        //        {
        //            valueSelected = Convert.ToInt32(chklbxSubRoles.Items[index].Value);
        //        }
        //        switch (valueSelected)
        //        {
        //            case (int)BusinessVariables.EnumSubRoles.TercerNivel:
        //                if (chklbxSubRoles.Items.Cast<ListItem>().Any(item => (int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.SegundoNivel) && !item.Selected))
        //                {
        //                    foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.TercerNivel))
        //                    {
        //                        item.Selected = false;
        //                        break;
        //                    }
        //                    throw new Exception("Require Segundo nivel.");
        //                }
        //                break;
        //            case (int)BusinessVariables.EnumSubRoles.CuartoNivel:
        //                if (chklbxSubRoles.Items.Cast<ListItem>().Any(item => (int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.TercerNivel) && !item.Selected))
        //                {
        //                    foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.CuartoNivel))
        //                    {
        //                        item.Selected = false;
        //                        break;
        //                    }
        //                    throw new Exception("Require Tercer nivel.");
        //                }
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (_lstError == null)
        //        {
        //            _lstError = new List<string>();
        //        }
        //        _lstError.Add(ex.Message);
        //        Alerta = _lstError;
        //    }
        //}

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
                    foreach (CheckBox chk in from RepeaterItem item in rptSubRoles.Items select (CheckBox)item.FindControl("chkSubRol"))
                    {
                        grupoUsuario.SubGrupoUsuario.Add(new SubGrupoUsuario
                        {
                            IdSubRol = Convert.ToInt32(chk.Attributes["value"]),
                            DiaFestivoSubGrupo = new List<DiaFestivoSubGrupo>(((List<DiaFestivoSubGrupo>)Session["DiasSubRoles"]).Where(w => w.IdSubGrupoUsuario == Convert.ToInt32(chk.Attributes["value"]))),
                            HorarioSubGrupo = new List<HorarioSubGrupo>(((List<HorarioSubGrupo>)Session["HorariosSubRoles"]).Where(w => w.IdSubGrupoUsuario == Convert.ToInt32(chk.Attributes["value"])))
                        });

                    }
                    grupoUsuario.TieneSupervisor = grupoUsuario.SubGrupoUsuario.Any(a => a.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor);
                    _servicioGrupoUsuario.GuardarGrupoUsuario(grupoUsuario);
                }
                else
                {
                    grupoUsuario = GrupoUsuario;
                    grupoUsuario.Descripcion = txtDescripcionGrupoUsuario.Text;
                    if (grupoUsuario.SubGrupoUsuario != null)
                        grupoUsuario.TieneSupervisor = grupoUsuario.SubGrupoUsuario.Any(a => a.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor);
                    _servicioGrupoUsuario.ActualizarGrupo(grupoUsuario);

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
                        Button btnHorarios = (Button)chk.DataItemContainer.FindControl("btnHorarios");
                        Button btnDiasDescanso = (Button)chk.DataItemContainer.FindControl("btnDiasDescanso");
                        if (chk.Checked)
                        {
                            btnHorarios.CssClass = "col-sm-3 btn btn-primary";
                            btnDiasDescanso.CssClass = "col-sm-3 btn btn-primary";
                        }
                        else
                        {
                            btnHorarios.CssClass = "col-sm-3 btn btn-primary disabled";
                            btnDiasDescanso.CssClass = "col-sm-3 btn btn-primary disabled";
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
                            ((CheckBox)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol")).Enabled = false;
                            ((Button)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("btnHorarios")).CssClass = "col-sm-3 btn btn-primary";
                            ((Button)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("btnDiasDescanso")).CssClass = "col-sm-3 btn btn-primary";
                        }
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeMantenimiento:
                        if (sbRol.Id == (int)BusinessVariables.EnumSubRoles.Dueño)
                        {
                            ((CheckBox)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol")).Checked = true;
                            ((CheckBox)((Repeater)sender).Controls[e.Item.ItemIndex].FindControl("chkSubRol")).Enabled = false;
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
                ucHorario.IdSubRol = Convert.ToInt32(((Button)sender).CommandArgument);
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
                ucAltaDiasFestivos.IdSubRol = Convert.ToInt32(((Button)sender).CommandArgument);
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