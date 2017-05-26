﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceParametrosSistema;
using KiiniHelp.ServicePuesto;
using KiiniHelp.ServiceSistemaTipoTelefono;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas.Usuarios
{
    public partial class UcAltaUsuario : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public event DelegateTerminarModal OnTerminarModal;

        private readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        private readonly ServiceParametrosClient _servicioParametros = new ServiceParametrosClient();
        private readonly ServiceUsuariosClient _servicioUsuarios = new ServiceUsuariosClient();
        private readonly ServicePuestoClient _servicioPuesto = new ServicePuestoClient();
        private readonly ServiceTipoTelefonoClient _servicioTipoTelefono = new ServiceTipoTelefonoClient();
        private List<string> _lstError = new List<string>();

        public bool Alta
        {
            get { return Convert.ToBoolean(hfAlta.Value); }
            set
            {
                ddlTipoUsuario.Enabled = value;
                hfAlta.Value = value.ToString();
                if (!value)
                {
                    btnModalOrganizacion.CssClass = "btn btn-primary";
                    btnModalUbicacion.CssClass = "btn btn-primary";
                }
            }
        }

        private bool EsDetalle
        {
            get { return bool.Parse(hdEsDetalle.Value); }
            set { hdEsDetalle.Value = value.ToString(); }
        }

        private bool EditarDetalle
        {
            get { return bool.Parse(hfEditaDetalle.Value); }
            set { hfEditaDetalle.Value = value.ToString(); }
        }

        public int IdUsuario
        {
            get { return Convert.ToInt32(hfIdUsuario.Value); }
            set
            {
                hfIdUsuario.Value = value.ToString();
                SetUser();
            }
        }

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(ddlTipoUsuario.SelectedValue); }
            set
            {
                ddlTipoUsuario.SelectedValue = value.ToString();
                ddlTipoUsuario_OnSelectedIndexChanged(null, null);
                ddlTipoUsuario.Enabled = false;
                ucAltaOrganizaciones.IdTipoUsuario = value;
                ucAltaUbicaciones.IdTipoUsuario = value;
            }
        }

        private List<string> Alerta
        {
            set
            {
                if (value.Any())
                {
                    string error = value.Aggregate("<ul>", (current, s) => current + ("<li>" + s + "</li>"));
                    error += "</ul>";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "ScriptErrorAlert",
                        "ErrorAlert('Error','" + error + "');", true);
                }
            }
        }

        #region Metodos

        private void LlenaCombos(bool usuariosResidentes)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<TipoUsuario> lstTipoUsuario = usuariosResidentes
                        ? _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true)
                        : _servicioSistemaTipoUsuario.ObtenerTiposUsuario(true);
                    Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
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

        private void LimpiarPantalla()
        {
            try
            {
                Session["UsuarioGrupo"] = null;
                txtAp.Text = string.Empty;
                txtAm.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtUserName.Text = string.Empty;
                Metodos.LimpiarCombo(ddlPuesto);
                chkVip.Checked = false;
                chkDirectoriActivo.Checked = false;
                ucRolGrupo.Limpiar();
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

        private void SetUser()
        {
            try
            {
                Usuario user = _servicioUsuarios.ObtenerDetalleUsuario(IdUsuario);
                if (user != null)
                {
                    LlenaCombos(false);
                    ddlTipoUsuario.SelectedValue = user.IdTipoUsuario.ToString();
                    ddlTipoUsuario_OnSelectedIndexChanged(ddlTipoUsuario, null);
                    imgPerfil.ImageUrl = user.Foto != null
                        ? "~/DisplayImages.ashx?id=" + user.Id
                        : "~/assets/images/profiles/profile-square-1.png";
                    txtAp.Text = user.ApellidoPaterno;
                    txtAm.Text = user.ApellidoMaterno;
                    txtNombre.Text = user.Nombre;
                    Metodos.LlenaComboCatalogo(ddlPuesto,
                        _servicioPuesto.ObtenerPuestosByTipoUsuario(IdTipoUsuario, true));
                    if (user.IdPuesto != null)
                        ddlPuesto.SelectedValue = user.IdPuesto.ToString();
                    chkVip.Checked = user.Vip;
                    chkDirectoriActivo.Checked = user.DirectorioActivo;
                    int contadorCasa, contadorCel, contadorOficina;
                    foreach (TelefonoUsuario telefonoUsuario in user.TelefonoUsuario)
                    {
                        foreach (RepeaterItem item in rptTelefonos.Items)
                        {
                            DropDownList ddlTipoTelefono = (DropDownList) item.FindControl("ddlTipoTelefono");
                            if (telefonoUsuario.IdTipoTelefono == Convert.ToInt32(ddlTipoTelefono.SelectedValue))
                            {
                                TextBox txtNumero = (TextBox) item.FindControl("txtNumero");
                                if (txtNumero.Text == string.Empty)
                                {
                                    TextBox txtExtencion = (TextBox) item.FindControl("txtExtension");
                                    txtNumero.Text = telefonoUsuario.Numero;
                                    txtExtencion.Text = telefonoUsuario.Extension;
                                    break;
                                }
                            }
                        }
                    }
                    foreach (CorreoUsuario correoUsuario in user.CorreoUsuario)
                    {
                        foreach (RepeaterItem item in rptCorreos.Items)
                        {
                            TextBox txtCorreo = (TextBox) item.FindControl("txtCorreo");
                            if (txtCorreo.Text != string.Empty) continue;
                            txtCorreo.Text = correoUsuario.Correo;
                            break;
                        }
                    }
                    ucAltaOrganizaciones.OrganizacionSeleccionada = user.Organizacion;
                    MuestraOrganizacion(new List<Organizacion>());
                    ucAltaUbicaciones.UbicacionSeleccionada = user.Ubicacion;
                    MuestraUbicacion(new List<Ubicacion>());

                    List<HelperAsignacionRol> lstRoles =
                        user.UsuarioGrupo.Select(s => new {s.IdRol, s.Rol.Descripcion})
                            .Distinct()
                            .Select(typeAnonymous => new HelperAsignacionRol
                            {
                                IdRol = typeAnonymous.IdRol,
                                DescripcionRol = typeAnonymous.Descripcion,
                                Grupos = new List<HelperAsignacionGrupoUsuarios>()
                            }).ToList();
                    foreach (UsuarioGrupo usuarioGrupo in user.UsuarioGrupo)
                    {

                        HelperAsignacionRol rolActivo = lstRoles.Single(s => s.IdRol == usuarioGrupo.IdRol);
                        HelperAsignacionGrupoUsuarios grupoToAdd;
                        if (rolActivo.Grupos.Any(s => s.IdGrupo == usuarioGrupo.IdGrupoUsuario))
                        {
                            grupoToAdd =
                                rolActivo.Grupos[
                                    rolActivo.Grupos.FindIndex(s => s.IdGrupo == usuarioGrupo.IdGrupoUsuario)];
                        }
                        else
                        {
                            grupoToAdd = new HelperAsignacionGrupoUsuarios
                            {
                                IdGrupo = usuarioGrupo.IdGrupoUsuario,
                                DescripcionGrupo = usuarioGrupo.GrupoUsuario.Descripcion,
                            };

                        }
                        if (usuarioGrupo.IdSubGrupoUsuario != null)
                        {
                            if (grupoToAdd.SubGrupos == null)
                                grupoToAdd.SubGrupos = new List<HelperSubGurpoUsuario>();
                            if (!grupoToAdd.SubGrupos.Any(a => a.Id == usuarioGrupo.IdSubGrupoUsuario))
                            {
                                grupoToAdd.SubGrupos = new List<HelperSubGurpoUsuario>();
                                HelperSubGurpoUsuario subGpoToAdd = new HelperSubGurpoUsuario
                                {
                                    Id = (int) usuarioGrupo.IdSubGrupoUsuario,
                                    Descripcion = usuarioGrupo.SubGrupoUsuario.SubRol.Descripcion
                                };
                                grupoToAdd.SubGrupos.Add(subGpoToAdd);
                            }
                        }
                        rolActivo.Grupos.Add(grupoToAdd);



                    }
                    ucRolGrupo.GruposSeleccionados = lstRoles;
                    MostrarGruposSeleccionados(ucRolGrupo.GruposSeleccionados);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LlenaTelefonosUsuarios()
        {
            try
            {
                rptTelefonos.DataSource = Session["TelefonosUsuario"];
                rptTelefonos.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LlenaCorreosUsuarios()
        {
            try
            {
                rptCorreos.DataSource = Session["CorreoUsuario"];
                rptCorreos.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Validaciones

        private void ValidaCapturaDatosGenerales()
        {
            StringBuilder sb = new StringBuilder();

            if (txtAp.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Apellido Paterno es un campo obligatorio.</li>");
            if (txtAm.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Apellido Materno es un campo obligatorio.</li>");
            if (txtNombre.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Nombre es un campo obligatorio.</li>");
            if (txtNombre.Text.Trim() == string.Empty)
                sb.AppendLine("<li>Nombre de Usuario es un campo obligatorio.</li>");

            List<ParametrosTelefonos> lstParamTelefonos =
                _servicioParametros.TelefonosObligatorios(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
            List<TelefonoUsuario> telefonoUsuario = new List<TelefonoUsuario>();
            foreach (RepeaterItem item in rptTelefonos.Items)
            {
                DropDownList ddlTipoTelefono = (DropDownList) item.FindControl("ddlTipoTelefono");
                TextBox numero = (TextBox) item.FindControl("txtNumero");
                TextBox extension = (TextBox) item.FindControl("txtExtension");
                if (ddlTipoTelefono != null && numero != null && extension != null)
                {
                    telefonoUsuario.Add(new TelefonoUsuario
                    {
                        IdTipoTelefono = Convert.ToInt32(ddlTipoTelefono.SelectedValue.Trim()),
                        Numero = numero.Text.Trim(),
                        Extension = extension.Text.Trim()
                    });
                }
            }

            foreach (TelefonoUsuario telefono in telefonoUsuario)
            {
                if (telefono.Numero.Trim() != string.Empty && telefono.Numero.Length < 10)
                {
                    sb.Append(string.Format("<li>El telefono {0} debe ser de 10 digitos.</li>", telefono.Numero));
                }
                ParametrosTelefonos parametroTipoTelefono =
                    lstParamTelefonos.Single(s => s.IdTipoTelefono == telefono.IdTipoTelefono);
                if (
                    telefonoUsuario.Count(
                        c => c.IdTipoTelefono == telefono.IdTipoTelefono && c.Numero.Trim() != string.Empty) <
                    parametroTipoTelefono.Obligatorios)
                {
                    sb.AppendLine(String.Format("<li>Debe capturar al menos {0} telefono(s) de {1}.</li>",
                        parametroTipoTelefono.Obligatorios, parametroTipoTelefono.TipoTelefono.Descripcion));
                    break;
                }
            }
            var sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            List<TextBox> lstCorreos =
                rptCorreos.Items.Cast<RepeaterItem>()
                    .Select(item => (TextBox) item.FindControl("txtCorreo"))
                    .Where(w => w.Text != string.Empty)
                    .ToList();
            foreach (TextBox txtMail in lstCorreos)
            {
                if (!Regex.IsMatch(txtMail.Text.Trim(), sFormato))
                {
                    if (Regex.Replace(txtMail.Text.Trim(), sFormato, String.Empty).Length != 0)
                    {
                        sb.AppendLine(string.Format("Correo {0} con formato invalido", txtMail.Text.Trim()));
                    }
                }
            }

            List<CorreoUsuario> correos =
                rptCorreos.Items.Cast<RepeaterItem>()
                    .Select(item => (TextBox) item.FindControl("txtCorreo"))
                    .Where(correo => correo != null & correo.Text.Trim() != string.Empty)
                    .Select(correo => new CorreoUsuario {Correo = correo.Text.Trim()})
                    .ToList();

            //TODO: Implementar metodo unico
            TipoUsuario paramCorreos =
                _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(false)
                    .SingleOrDefault(s => s.Id == int.Parse(ddlTipoUsuario.SelectedValue));
            if (paramCorreos != null &&
                (correos.Count(c => c.Correo != string.Empty) < paramCorreos.CorreosObligatorios))
                sb.AppendLine(String.Format("<li>Debe captura al menos {0} correo(s).</li>.",
                    paramCorreos.CorreosObligatorios));

            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Datos Generales</h3>");
                throw new Exception(sb.ToString());
            }
        }

        private void ValidaCapturaRoles()
        {
            StringBuilder sb = new StringBuilder();

            if (!rptRoles.Items.Cast<RepeaterItem>().Any())
                sb.AppendLine("<li>Debe seleccionar un Rol Grupo de usuario.</li>");

            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Roles</h3>");
                throw new Exception(sb.ToString());
            }
        }

        private void ValidaCapturaGrupos()
        {
            StringBuilder sb = new StringBuilder();


            if (!ucRolGrupo.ValidaCapturaGrupos())
                sb.AppendLine("<li>Debe asignar al menos un Grupo.</li>");

            if (sb.ToString() != string.Empty)
            {
                sb.Append("</ul>");
                sb.Insert(0, "<ul>");
                sb.Insert(0, "<h3>Grupos</h3>");
                throw new Exception(sb.ToString());
            }
        }

        #endregion Validaciones

        #endregion Metodos

        private void HabilitaDetalle(bool habilitado)
        {
            divAvatar.Visible = habilitado || EditarDetalle;
            divTipousuario.Visible = !habilitado && !EditarDetalle;
            txtNombre.ReadOnly = habilitado;
            txtAp.ReadOnly = habilitado;
            txtAm.ReadOnly = habilitado;
            txtUserName.ReadOnly = habilitado;
            ddlPuesto.Enabled = !habilitado && !EditarDetalle;
            btnAddPuesto.Visible = !habilitado && !EditarDetalle;
            chkVip.Enabled = !habilitado;
            chkDirectoriActivo.Enabled = !habilitado;
            btnAddTelefono.Visible = !habilitado;
            foreach (RepeaterItem item in rptTelefonos.Items)
            {
                DropDownList ddlTipoTelefono = (DropDownList) item.FindControl("ddlTipoTelefono");
                TextBox numero = (TextBox) item.FindControl("txtNumero");
                TextBox extension = (TextBox) item.FindControl("txtExtension");
                if (ddlTipoTelefono != null && numero != null && extension != null)
                {
                    ddlTipoTelefono.Enabled = !habilitado && !EditarDetalle;
                    numero.ReadOnly = habilitado;
                    extension.ReadOnly = habilitado;
                }
            }
            btnAddCorreo.Visible = !habilitado;
            foreach (RepeaterItem item in rptCorreos.Items)
            {
                TextBox txt = (TextBox) item.FindControl("txtCorreo");
                if (txt != null)
                    txt.ReadOnly = habilitado; 
            }

            foreach (RepeaterItem item in rptRoles.Items)
            {
                Repeater rpt = (Repeater) item.FindControl("rptGrupos");
                if (rpt != null)
                {
                    foreach (RepeaterItem gpoItem in rpt.Items)
                    {
                        LinkButton btn = (LinkButton)gpoItem.FindControl("btnRemove");
                        if (btn != null)
                            btn.Visible = !habilitado && !EditarDetalle;
                    }
                }
            }
            btnModalOrganizacion.Visible = !habilitado && !EditarDetalle;
            btnModalUbicacion.Visible = !habilitado && !EditarDetalle;
            btnModalRoles.Visible = !habilitado && !EditarDetalle;
            btnGuardar.Visible = !habilitado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Alerta = new List<string>();
                FileUpload1.Attributes["onchange"] = "UploadFile(this)";

                ucAltaPuesto.OnTerminarModal += UcAltaPuestoOnOnAceptarModal;
                ucAltaPuesto.OnCancelarModal += UcAltaPuestoOnOnCancelarModal;

                ucAltaOrganizaciones.OnTerminarModal += ucOrganizacion_OnTerminarModal;
                ucAltaOrganizaciones.OnCancelarModal += ucOrganizacion_OnCancelarModal;

                ucAltaUbicaciones.OnTerminarModal += UcUbicacion_OnTerminarModal;
                ucAltaUbicaciones.OnCancelarModal += UcUbicacion_OnCancelarModal;

                ucRolGrupo.OnTerminarModal += UcRolGrupoOnTerminarModal;

                ucRolGrupo.AsignacionAutomatica = false;
                if (!IsPostBack)
                {
                    EditarDetalle = false;
                    if (Request.QueryString["IdUsuario"] != null && Request.QueryString["Detail"] == null)
                    {
                        IdUsuario = int.Parse(Request.QueryString["IdUsuario"]);
                        lblTitle.Text = "EDITAR USUARIO";
                        EsDetalle = false;
                        Alta = false;
                    }
                    else if (Request.QueryString["Detail"] != null)
                    {
                        lblTitle.Text = "Mi Perfil";
                        IdUsuario = ((Usuario) Session["UserData"]).Id;
                        EsDetalle = true;
                    }
                    else
                    {
                        lblTitle.Text = "ALTA DE USUARIO";
                        EsDetalle = false;
                        Alta = true;
                    }
                    HabilitaDetalle(EsDetalle);
                    if (Request.QueryString["all"] != null && Request.QueryString["all"] != string.Empty)
                        LlenaCombos(false);
                    else
                        LlenaCombos(true);
                    Session["UsuarioTemporal"] = null;
                    Session["UsuarioGrupo"] = null;
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

        private void MostrarGruposSeleccionados(List<HelperAsignacionRol> gruposAsignados)
        {
            try
            {
                rptRoles.DataSource = gruposAsignados;
                rptRoles.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UcRolGrupoOnTerminarModal()
        {
            try
            {
                //TODO: AGREGAR SUBGRUPO USUARIO
                MostrarGruposSeleccionados(ucRolGrupo.GruposSeleccionados);
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "CierraPopup(\"#modalRoles\");",
                    true);
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

        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex != BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    int idTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                    ucAltaOrganizaciones.EsSeleccion = true;
                    ucAltaOrganizaciones.EsAlta = true;
                    ucAltaOrganizaciones.IdTipoUsuario = idTipoUsuario;

                    ucAltaUbicaciones.EsSeleccion = true;
                    ucAltaUbicaciones.EsAlta = true;
                    ucAltaUbicaciones.IdTipoUsuario = idTipoUsuario;

                    Session["TelefonosUsuario"] =
                        _servicioParametros.ObtenerTelefonosParametrosIdTipoUsuario(idTipoUsuario, false);
                    LlenaTelefonosUsuarios();

                    rptCorreos.DataSource = _servicioParametros.ObtenerCorreosParametrosIdTipoUsuario(idTipoUsuario,
                        false);
                    rptCorreos.DataBind();

                    ucRolGrupo.IdTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue);
                    Session["UsuarioTemporal"] = new Usuario();
                    LimpiarPantalla();

                    Metodos.LlenaComboCatalogo(ddlPuesto,
                        _servicioPuesto.ObtenerPuestosByTipoUsuario(IdTipoUsuario, true));
                    if (
                        _servicioSistemaTipoUsuario.ObtenerTipoUsuarioById(int.Parse(ddlTipoUsuario.SelectedValue))
                            .EsMoral)
                    {
                        divPuesto.Visible = true;
                        btnModalOrganizacion.Text = "Organización";
                        btnModalUbicacion.Text = "Ubicación";
                    }
                    else
                    {
                        divPuesto.Visible = false;
                        btnModalOrganizacion.Text = "Actividad";
                        btnModalUbicacion.Text = "Direccion";
                    }
                }
                else
                {
                    LimpiarPantalla();
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
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    throw new Exception("Seleccione un tipo de usuario.<br>");
                ValidaCapturaDatosGenerales();
                ValidaCapturaRoles();
                ValidaCapturaGrupos();
                Usuario usuario = new Usuario
                {
                    IdTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue),
                    ApellidoPaterno = txtAp.Text.Trim(),
                    ApellidoMaterno = txtAm.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    DirectorioActivo = chkDirectoriActivo.Checked,
                    IdPuesto =
                        ddlPuesto.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione
                            ? (int?) null
                            : Convert.ToInt32(ddlPuesto.SelectedValue),
                    Vip = chkVip.Checked,
                    NombreUsuario = txtUserName.Text.Trim(),
                    Password = ResolveUrl("~/ConfirmacionCuenta.aspx"),
                    Habilitado = true
                };
                List<ParametrosTelefonos> lstParamTelefonos =
                    _servicioParametros.TelefonosObligatorios(Convert.ToInt32(ddlTipoUsuario.SelectedValue));
                int telefonosObligatoriosCasa =
                    lstParamTelefonos.Count(w => w.IdTipoTelefono == (int) BusinessVariables.EnumTipoTelefono.Casa);
                int telefonosObligatoriosCelular =
                    lstParamTelefonos.Count(w => w.IdTipoTelefono == (int) BusinessVariables.EnumTipoTelefono.Celular);
                int telefonosObligatoriosOficina =
                    lstParamTelefonos.Count(w => w.IdTipoTelefono == (int) BusinessVariables.EnumTipoTelefono.Oficina);
                int contadorCasa = 0;
                int contadorCelular = 0;
                int contadorOficina = 0;
                usuario.TelefonoUsuario = new List<TelefonoUsuario>();
                foreach (RepeaterItem item in rptTelefonos.Items)
                {
                    DropDownList ddlTipoTelefono = (DropDownList) item.FindControl("ddlTipoTelefono");
                    TextBox numero = (TextBox) item.FindControl("txtNumero");
                    TextBox extension = (TextBox) item.FindControl("txtExtension");
                    bool obligatorio = false;
                    if (ddlTipoTelefono != null && numero != null && extension != null)
                    {
                        switch (Convert.ToInt32(ddlTipoTelefono.SelectedValue.Trim()))
                        {
                            case (int) BusinessVariables.EnumTipoTelefono.Casa:
                                if (contadorCasa < telefonosObligatoriosCasa)
                                {
                                    obligatorio = true;
                                    contadorCasa++;
                                }
                                break;
                            case (int) BusinessVariables.EnumTipoTelefono.Celular:
                                if (contadorCelular < telefonosObligatoriosCelular)
                                {
                                    obligatorio = true;
                                    contadorCelular++;
                                }
                                break;
                            case (int) BusinessVariables.EnumTipoTelefono.Oficina:
                                if (contadorOficina < telefonosObligatoriosOficina)
                                {
                                    obligatorio = true;
                                    contadorOficina++;
                                }
                                break;
                        }

                        usuario.TelefonoUsuario.Add(new TelefonoUsuario
                        {
                            IdTipoTelefono = Convert.ToInt32(ddlTipoTelefono.SelectedValue.Trim()),
                            Numero = numero.Text.Trim(),
                            Extension = extension.Text.Trim(),
                            Obligatorio = numero.CssClass.Contains("obligatorio"),
                            Confirmado = false
                        });
                    }
                }
                usuario.CorreoUsuario = new List<CorreoUsuario>();
                foreach (
                    TextBox correo in
                        rptCorreos.Items.Cast<RepeaterItem>()
                            .Select(item => (TextBox) item.FindControl("txtCorreo"))
                            .Where(correo => correo != null & correo.Text.Trim() != string.Empty))
                {
                    usuario.CorreoUsuario.Add(new CorreoUsuario
                    {
                        Correo = correo.Text.Trim(),
                        Obligatorio = correo.CssClass.Contains("obligatorio")
                    });
                }

                usuario.IdOrganizacion = ucAltaOrganizaciones.OrganizacionSeleccionada.Id;
                usuario.IdUbicacion = ucAltaUbicaciones.UbicacionSeleccionada.Id;

                #region Rol

                usuario.UsuarioRol = new List<UsuarioRol>();
                //TODO: CAMBIAR ASIGNACION DE ROL
                foreach (HelperAsignacionRol item in ucRolGrupo.GruposSeleccionados)
                {
                    usuario.UsuarioRol.Add(new UsuarioRol
                    {
                        RolTipoUsuario =
                            new RolTipoUsuario
                            {
                                IdRol = item.IdRol,
                                IdTipoUsuario = Convert.ToInt32(ddlTipoUsuario.SelectedValue)
                            }
                    });
                }

                #endregion Rol

                #region Grupo

                usuario.UsuarioGrupo = new List<UsuarioGrupo>();

                foreach (HelperAsignacionRol item in ucRolGrupo.GruposSeleccionados)
                {
                    foreach (HelperAsignacionGrupoUsuarios grupoUsuarios in item.Grupos)
                    {
                        if (grupoUsuarios.SubGrupos != null && grupoUsuarios.SubGrupos.Count > 0)
                        {
                            foreach (HelperSubGurpoUsuario subGrupo in grupoUsuarios.SubGrupos)
                            {
                                usuario.UsuarioGrupo.Add(new UsuarioGrupo
                                {
                                    IdUsuario = Alta ? 0 : IdUsuario,
                                    IdGrupoUsuario = grupoUsuarios.IdGrupo,
                                    IdRol = item.IdRol,
                                    IdSubGrupoUsuario = subGrupo.Id
                                });
                            }
                        }
                        else
                        {
                            usuario.UsuarioGrupo.Add(new UsuarioGrupo
                            {
                                IdUsuario = Alta ? 0 : IdUsuario,
                                IdGrupoUsuario = grupoUsuarios.IdGrupo,
                                IdRol = item.IdRol,
                                IdSubGrupoUsuario = null
                            });
                        }
                    }

                }

                #endregion Grupos

                if (Alta)
                {
                    _servicioUsuarios.GuardarUsuario(usuario);
                }
                else
                {
                    _servicioUsuarios.ActualizarUsuario(IdUsuario, usuario);
                }

                LimpiarPantalla();
                Response.Redirect("~/Users/Administracion/Usuarios/FrmConsultaUsuarios.aspx");
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                ddlTipoUsuario.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                ddlTipoUsuario_OnSelectedIndexChanged(ddlTipoUsuario, null);
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

        protected void btnAddPuesto_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    throw new Exception("Seleccione el Tipo de usuario");
                ucAltaPuesto.EsAlta = true;
                ucAltaPuesto.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "MostrarPopup(\"#modalAreas\");",
                    true);
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

        protected void txtAp_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text.Trim().Length <= 0)
                    return;
                string username = txtUserName.Text.Trim() == string.Empty
                    ? (txtNombre.Text.Substring(0, 1).ToLower() + txtAp.Text.Trim().ToLower()).Replace(" ", string.Empty)
                    : txtUserName.Text.Trim();
                int limite = 2;
                if (_servicioUsuarios.ValidaUserName(username))
                {
                    for (int i = 1; i < limite; i++)
                    {
                        string tmpUsername = username + i;
                        if (!_servicioUsuarios.ValidaUserName(tmpUsername))
                        {
                            username = tmpUsername;
                            break;
                        }
                        limite++;
                    }
                }
                txtUserName.Text = username;
                if (txtAp.ID == ((TextBox) sender).ID)
                {
                    txtAm.Focus();
                }
                else if (txtNombre.ID == ((TextBox) sender).ID)
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

        #region botones cerrar Cancelar

        protected void btnAceptarDatosGenerales_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaDatosGenerales();
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

        protected void btnCerrarDatosGenerales_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script",
                    "CierraPopup(\"#modalDatosGenerales\");", true);
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

        protected void btnAceptarRoles_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaRoles();
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "CierraPopup(\"#modalRoles\");",
                    true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                //AlertaRoles = _lstError;
            }
        }

        protected void btnCerrarRoles_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "CierraPopup(\"#modalRoles\");",
                    true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                //AlertaRoles = _lstError;
            }
        }

        protected void btnAceptarGrupos_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (!ucRolGrupo.ValidaCapturaGrupos()) return;
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "CierraPopup(\"#modalGrupos\");",
                    true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);

            }
        }

        protected void btnCerrarGrupos_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "CierraPopup(\"#modalGrupos\");",
                    true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);

            }
        }

        #endregion botones cerrar Cancelar

        #region Delegados


        private void UcAltaPuestoOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "CierraPopup(\"#modalAreas\");",
                    true);
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

        private void UcAltaPuestoOnOnAceptarModal()
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlPuesto, _servicioPuesto.ObtenerPuestosByTipoUsuario(IdTipoUsuario, true));
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "CierraPopup(\"#modalAreas\");",
                    true);
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

        protected void ddlTipoTelefono_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList) sender;
                if (sender != null)
                {
                    RepeaterItem item = (RepeaterItem) ddl.NamingContainer;
                    item.FindControl("divExtension").Visible = false;
                    if (int.Parse(ddl.SelectedValue) == (int) BusinessVariables.EnumTipoTelefono.Oficina)
                    {
                        item.FindControl("divExtension").Visible = true;
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

        protected void rptTelefonos_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DropDownList ddlTipoTelefono = (DropDownList) e.Item.FindControl("ddlTipoTelefono");
                if (ddlTipoTelefono != null)
                {
                    ddlTipoTelefono.DataSource = _servicioTipoTelefono.ObtenerTiposTelefono(true);
                    ddlTipoTelefono.DataTextField = "Descripcion";
                    ddlTipoTelefono.DataValueField = "Id";
                    ddlTipoTelefono.DataBind();
                    if (((TelefonoUsuario) e.Item.DataItem).IdTipoTelefono == 0)
                    {
                        e.Item.FindControl("divExtension").Visible = false;
                        ddlTipoTelefono.Enabled = true;
                    }
                    else
                    {
                        ddlTipoTelefono.SelectedValue = ((TelefonoUsuario) e.Item.DataItem).IdTipoTelefono.ToString();
                        e.Item.FindControl("divExtension").Visible =
                            ((TelefonoUsuario) e.Item.DataItem).IdTipoTelefono ==
                            (int) BusinessVariables.EnumTipoTelefono.Oficina;
                        ddlTipoTelefono.Enabled = false;
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

        protected void btnAddTelefono_OnClick(object sender, EventArgs e)
        {
            try
            {
                List<TelefonoUsuario> tmpTelefonos = new List<TelefonoUsuario>();
                foreach (RepeaterItem item in rptTelefonos.Items)
                {
                    Label lblObligatorio = (Label) item.FindControl("lblObligatorio");
                    TextBox txtNumero = (TextBox) item.FindControl("txtNumero");
                    TextBox txtExtension = (TextBox) item.FindControl("txtExtension");
                    DropDownList ddlTipoTelefono = (DropDownList) item.FindControl("ddlTipoTelefono");
                    TelefonoUsuario tel = new TelefonoUsuario
                    {
                        Obligatorio = bool.Parse(lblObligatorio.Text),
                        Numero = txtNumero.Text,
                        Extension = txtExtension.Text,
                        IdTipoTelefono = int.Parse(ddlTipoTelefono.SelectedValue)
                    };
                    tmpTelefonos.Add(tel);
                }
                tmpTelefonos.Add(new TelefonoUsuario());
                Session["TelefonosUsuario"] = tmpTelefonos;
                LlenaTelefonosUsuarios();
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

        #region Organizacion

        protected void btnModalOrganizacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    throw new Exception("Seleccione el Tipo de usuario");
                ucAltaOrganizaciones.EsAlta = true;
                ucAltaOrganizaciones.EsSeleccion = true;
                if (rptOrganizacion.Items.Count > 0)
                {
                    ucAltaOrganizaciones.IdOrganizacion =
                        int.Parse(((Label) rptOrganizacion.Items[0].FindControl("lblIdOrganizacion")).Text);
                    ucAltaOrganizaciones.SetOrganizacionSeleccion();
                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script",
                    "MostrarPopup(\"#modalOrganizacion\");", true);
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

        private void MuestraOrganizacion(List<Organizacion> org)
        {
            try
            {
                org.Add(ucAltaOrganizaciones.OrganizacionSeleccionada);
                rptOrganizacion.DataSource = org;
                rptOrganizacion.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ucOrganizacion_OnTerminarModal()
        {
            try
            {
                MuestraOrganizacion(new List<Organizacion>());
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script",
                    "CierraPopup(\"#modalOrganizacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
            }
        }

        private void ucOrganizacion_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script",
                    "CierraPopup(\"#modalOrganizacion\");", true);
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

        #endregion Organizacion

        #region Ubicacion

        protected void btnModalUbicacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    throw new Exception("Seleccione el Tipo de usuario");
                ucAltaUbicaciones.EsAlta = true;
                ucAltaUbicaciones.EsSeleccion = true;
                if (rptUbicacion.Items.Count > 0)
                {
                    ucAltaUbicaciones.IdUbicacion =
                        int.Parse(((Label) rptOrganizacion.Items[0].FindControl("lblIdUbicacion")).Text);
                    ucAltaUbicaciones.SetUbicacionSeleccion();
                }
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script",
                    "MostrarPopup(\"#modalUbicacion\");", true);
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

        private void MuestraUbicacion(List<Ubicacion> ub)
        {
            try
            {
                ub.Add(ucAltaUbicaciones.UbicacionSeleccionada);
                rptUbicacion.DataSource = ub;
                rptUbicacion.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void UcUbicacion_OnTerminarModal()
        {
            try
            {
                MuestraUbicacion(new List<Ubicacion>());
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script",
                    "CierraPopup(\"#modalUbicacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
            }

        }

        private void UcUbicacion_OnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script",
                    "CierraPopup(\"#modalUbicacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                //UcUbicacion.AlertaUbicacion = _lstError;
            }
        }

        #endregion Ubicacion

        #region RolesGrupos

        protected void btnModalRoles_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    throw new Exception("Seleccione el Tipo de usuario");
                ucRolGrupo.AsignacionAutomatica = false;
                ucRolGrupo.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                ScriptManager.RegisterClientScriptBlock(Page, typeof (Page), "Script", "MostrarPopup(\"#modalRoles\");",
                    true);
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

        protected void rptRoles_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
                Repeater rpt = (Repeater) e.Item.FindControl("rptGrupos");
                rpt.DataSource = ((HelperAsignacionRol) e.Item.DataItem).Grupos;
                rpt.DataBind();
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

        protected void rptGrupos_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
                Repeater rpt = (Repeater) e.Item.FindControl("rptSubGrupos");
                rpt.DataSource = ((HelperAsignacionGrupoUsuarios) e.Item.DataItem).SubGrupos;
                rpt.DataBind();
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
                ucRolGrupo.EliminarSeleccion(int.Parse(((LinkButton) sender).CommandArgument));
                MostrarGruposSeleccionados(ucRolGrupo.GruposSeleccionados);
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

        #endregion RolesGrupos

        protected void btnAddCorreo_OnClick(object sender, EventArgs e)
        {
            try
            {
                List<CorreoUsuario> tmpCorreos = new List<CorreoUsuario>();
                foreach (RepeaterItem item in rptCorreos.Items)
                {
                    TextBox txtCorreo = (TextBox) item.FindControl("txtCorreo");
                    CorreoUsuario correo = new CorreoUsuario
                    {
                        Correo = txtCorreo.Text
                    };
                    tmpCorreos.Add(correo);
                }
                tmpCorreos.Add(new CorreoUsuario());
                Session["CorreoUsuario"] = tmpCorreos;
                LlenaCorreosUsuarios();
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

        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                EditarDetalle = true;
                EsDetalle = false;
                HabilitaDetalle(EsDetalle);
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

        protected void btnCambiarImagen_OnClick(object sender, EventArgs e)
        {
            //try
            //{
            //    BusinessFile.Imagenes.ImageToByteArray()
            //}
            //catch (Exception ex)
            //{
            //    if (_lstError == null)
            //    {
            //        _lstError = new List<string>();
            //    }
            //    _lstError.Add(ex.Message);
            //    Alerta = _lstError;
            //}
        }
        protected void Upload(object sender, EventArgs e)
        {
            try
            {
                _servicioUsuarios.GuardarFoto(IdUsuario, FileUpload1.FileBytes);
                imgPerfil.ImageUrl = "~/DisplayImages.ashx?id=" + IdUsuario;
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