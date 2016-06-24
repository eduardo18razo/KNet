using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniHelp.ServiceSistemaSubRol;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Cat.Usuario;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaGrupoUsuario : UserControl
    {
        readonly ServiceGrupoUsuarioClient _servicioGrupoUsuario = new ServiceGrupoUsuarioClient();
        readonly ServiceSubRolClient _servicioSistemaSubRol = new ServiceSubRolClient();
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
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Administrador";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.Acceso:
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Acceso";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.EspecialDeConsulta:
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Especial de consulta";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención:
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Responsable de atención";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeMantenimiento:
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Responsable de Mantenimiento";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeOperación:
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Responsable de Operación";
                        break;
                    case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeDesarrollo:
                        lblTitle.Text = "Alta Grupo de Usaurio de tipo Responsable de Desarrollo";
                        break;
                }
                List<SubRol> lstRoles = _servicioSistemaSubRol.ObtenerSubRolesByTipoGrupo(value, false);
                divSubRoles.Visible = lstRoles.Count > 0;
                Metodos.LlenaListBoxCatalogo(chklbxSubRoles, lstRoles);
                if (lstRoles.Count > 0)
                {
                    try
                    {
                        switch (IdTipoGrupo)
                        {
                            case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeMantenimiento:
                                foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.Dueño))
                                {
                                    item.Selected = true;
                                    item.Enabled = false;
                                }
                                break;
                            case (int)BusinessVariables.EnumTiposGrupos.ResponsableDeAtención:
                                foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.PrimererNivel))
                                {
                                    item.Selected = true;
                                    item.Enabled = false;
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
            }
        }

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(hfIdTipoUsuario.Value); }
            set { hfIdTipoUsuario.Value = value.ToString(); }
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
                GrupoUsuario grupoUsuario = new GrupoUsuario
                {
                    IdTipoUsuario = IdTipoUsuario,
                    IdTipoGrupo = Convert.ToInt32(IdTipoGrupo),
                    Descripcion = txtDescripcionGrupoUsuario.Text,
                    Habilitado = chkHabilitado.Checked,
                    SubGrupoUsuario = new List<SubGrupoUsuario>()
                };
                foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => item.Selected))
                {
                    grupoUsuario.SubGrupoUsuario.Add(new SubGrupoUsuario
                    {
                        IdSubRol = Convert.ToInt32(item.Value)
                    });
                }
                grupoUsuario.TieneSupervisor = grupoUsuario.SubGrupoUsuario.Any(a => a.IdSubRol == (int)BusinessVariables.EnumSubRoles.Supervisor);
                _servicioGrupoUsuario.GuardarGrupoUsuario(grupoUsuario);
                LimpiarCampos();
                IdTipoGrupo = Convert.ToInt32(hfIdGrupo.Value);
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
        
        protected void chklbxSubRoles_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int valueSelected = 0;
                string result = Request.Form["__EVENTTARGET"];
                string[] checkedBox = result.Split('$');
                int index = int.Parse(checkedBox[checkedBox.Length - 1]);
                if (chklbxSubRoles.Items[index].Selected)
                {
                    valueSelected = Convert.ToInt32(chklbxSubRoles.Items[index].Value);
                }
                switch (valueSelected)
                {
                    case (int)BusinessVariables.EnumSubRoles.TercerNivel:
                        if (chklbxSubRoles.Items.Cast<ListItem>().Any(item => (int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.SegundoNivel) && !item.Selected))
                        {
                            foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.TercerNivel))
                            {
                                item.Selected = false;
                                break;
                            }
                            throw new Exception("Require Segundo nivel.");
                        }
                        break;
                    case (int)BusinessVariables.EnumSubRoles.CuartoNivel:
                        if (chklbxSubRoles.Items.Cast<ListItem>().Any(item => (int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.TercerNivel) && !item.Selected))
                        {
                            foreach (ListItem item in chklbxSubRoles.Items.Cast<ListItem>().Where(item => int.Parse(item.Value) == (int)BusinessVariables.EnumSubRoles.CuartoNivel))
                            {
                                item.Selected = false;
                                break;
                            }
                            throw new Exception("Require Tercer nivel.");
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
    }
}