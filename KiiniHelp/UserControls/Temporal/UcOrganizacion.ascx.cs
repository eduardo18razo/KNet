using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceOrganizacion;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Temporal
{
    public partial class UcOrganizacion : UserControl
    {
        readonly ServiceOrganizacionClient _serivicioOrganizacion = new ServiceOrganizacionClient();
        private List<string> _lstError = new List<string>();
        private List<string> AlertaOrganizacion
        {
            set
            {
                panelAlertaOrganizacion.Visible = value.Any();
                if (!panelAlertaOrganizacion.Visible) return;
                rptErrorOrganizacion.DataSource = value;
                rptErrorOrganizacion.DataBind();
            }
        }

        public int IdTipoUsuario
        {
            get { return Convert.ToInt32(hfIdTipoUsuario.Value); }
            set { hfIdTipoUsuario.Value = value.ToString(); }
        }

        private static void FiltraCombo(DropDownList ddlFiltro, DropDownList ddlLlenar, object source)
        {
            try
            {
                ddlLlenar.Items.Clear();
                if (ddlFiltro.SelectedValue != BusinessVariables.ComboBoxCatalogo.Value.ToString())
                {
                    ddlLlenar.Enabled = true;
                    Metodos.LlenaComboCatalogo(ddlLlenar, source);
                }
                else
                {
                    ddlLlenar.DataSource = null;
                    ddlLlenar.DataBind();
                }

                ddlLlenar.Enabled = ddlLlenar.DataSource != null;

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
                if (!IsPostBack)
                {
                    Metodos.LlenaComboCatalogo(ddlHolding, _serivicioOrganizacion.ObtenerHoldings(IdTipoUsuario, true));
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        protected void ddlHolding_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                FiltraCombo((DropDownList)sender, ddlCompañia, _serivicioOrganizacion.ObtenerCompañias(IdTipoUsuario, id, true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        protected void ddlCompañia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                FiltraCombo((DropDownList)sender, ddlDireccion, _serivicioOrganizacion.ObtenerDirecciones(IdTipoUsuario, id, true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        protected void ddlDirecion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                FiltraCombo((DropDownList)sender, ddlSubDireccion, _serivicioOrganizacion.ObtenerSubDirecciones(IdTipoUsuario, id, true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        protected void ddlSubDireccion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                FiltraCombo((DropDownList)sender, ddlGerencia, _serivicioOrganizacion.ObtenerGerencias(IdTipoUsuario, id, true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        protected void ddlGerencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                FiltraCombo((DropDownList)sender, ddlSubGerencia, _serivicioOrganizacion.ObtenerSubGerencias(IdTipoUsuario, id, true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        protected void ddlSubGerencia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                FiltraCombo((DropDownList)sender, ddlJefatura, _serivicioOrganizacion.ObtenerJefaturas(IdTipoUsuario, id, true));
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }

        protected void ddlJefatura_OnSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void OnClick(object sender, EventArgs e)
        {
            try
            {
                Button lbtn = (Button)sender;
                if (sender == null) return;
                if (lbtn.CommandArgument == "0")
                {
                    //ddlTipoUsuarioCampus.SelectedIndex = ddlTipoUsuario.SelectedIndex;
                }
                else
                {
                    modalCataogoAlta.Titulo = lbtn.CommandName;
                    modalCataogoAlta.IdCatalogo = Convert.ToInt32(lbtn.CommandArgument);
                    modalCataogoAlta.IdTipoUsuario = IdTipoUsuario;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalManagerCatalogo\");", true);
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                //AlertaCampus = _lstError;
            }
        }
        private void ValidaCapturaOrganizacion()
        {
            StringBuilder sb = new StringBuilder();
            if (ddlHolding.SelectedValue == BusinessVariables.ComboBoxCatalogo.Value.ToString())
                sb.AppendLine("Debe especificar al menos un Holding.<br>");

            if (sb.ToString() != string.Empty)
                throw new Exception(sb.ToString());
        }

        protected void btnCerrarOrganizacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                ValidaCapturaOrganizacion();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalOrganizacion\");", true);
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaOrganizacion = _lstError;
            }
        }
    }
}