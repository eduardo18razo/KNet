using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceOrganizacion;
using KiiniHelp.ServiceParametrosSistema;
using KiiniHelp.ServiceSistemaTipoUsuario;
using KiiniNet.Entities.Cat.Arbol.Organizacion;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;
using Page = System.Web.UI.Page;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaOrganizacion : UserControl, IControllerModal
    {
        public event DelegateSeleccionOrganizacionModal OnSeleccionOrganizacionModal;
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public delegate void DelegateSeleccionOrganizacionModal();

        readonly ServiceTipoUsuarioClient _servicioSistemaTipoUsuario = new ServiceTipoUsuarioClient();
        readonly ServiceOrganizacionClient _servicioOrganizacion = new ServiceOrganizacionClient();
        readonly ServiceParametrosClient _servicioParametros = new ServiceParametrosClient();
        private List<string> _lstError = new List<string>();
        private int PageSize = 10;

        public bool Modal
        {
            get { return Convert.ToBoolean(hfModal.Value); }
            set
            {
                hfModal.Value = value.ToString();
                lblTitleOrganizacion.Text = value ? "Agregar Organización" : "Organizaciones";
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
            }
        }
        public string ModalName
        {
            set { hfModalName.Value = value; }
        }
        public List<string> AlertaOrganizacion
        {
            set
            {
                panelAlertaOrganizacion.Visible = value.Any();
                if (!panelAlertaOrganizacion.Visible) return;
                rptErrorOrganizacion.DataSource = value;
                rptErrorOrganizacion.DataBind();
            }
        }
        private List<string> AlertaCatalogos
        {
            set
            {
                panelAlertaCatalogo.Visible = value.Any();
                if (!panelAlertaCatalogo.Visible) return;
                rptErrorCatalogo.DataSource = value;
                rptErrorCatalogo.DataBind();
            }
        }
        public int OrganizacionSeleccionada
        {
            get
            {
                if (hfIdSeleccion.Value == null || hfIdSeleccion.Value.Trim() == string.Empty)
                    throw new Exception("Debe Seleccionar una organización");
                return Convert.ToInt32(hfIdSeleccion.Value);
            }
            set
            {
                LlenaOrganizaciones();
                foreach (RepeaterItem item in rptResultados.Items)
                {
                    if ((((DataBoundLiteralControl)item.Controls[0])).Text.Split('\n')[1].Contains("id='" + value + "'"))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Scripts", "SeleccionaOrganizacion(\"" + value + "\");", true);
                }
                hfIdSeleccion.Value = value.ToString();
            }
        }
        private void PopulatePager(int recordCount, int currentPage)
        {
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            List<ListItem> pages = new List<ListItem>();
            if (pageCount > 0)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
            this.LlenaOrganizaciones();
        }

        private void LimpiaCatalogo()
        {
            try
            {
                txtDescripcionCatalogo.Text = string.Empty;
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
        private void LlenaCombos()
        {
            try
            {
                List<TipoUsuario> lstTipoUsuario = _servicioSistemaTipoUsuario.ObtenerTiposUsuarioResidentes(true);
                if (lstTipoUsuario.Count >= 2)
                    lstTipoUsuario.Insert(BusinessVariables.ComboBoxCatalogo.IndexTodos, new TipoUsuario { Id = BusinessVariables.ComboBoxCatalogo.ValueTodos, Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionTodos });
                Metodos.LlenaComboCatalogo(ddlTipoUsuario, lstTipoUsuario);
                Metodos.LlenaComboCatalogo(ddlTipoUsuarioCatalogo, lstTipoUsuario);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void LlenaComboOrganizacion(int idTipoUsuario)
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlHolding, _servicioOrganizacion.ObtenerHoldings(idTipoUsuario, true));
                if (ddlHolding.Items.Count != 2) return;
                ddlHolding.SelectedIndex = 1;
                ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void FiltraCombo(DropDownList ddlFiltro, DropDownList ddlLlenar, object source)
        {
            try
            {
                ddlLlenar.Items.Clear();
                if (ddlFiltro.SelectedValue != BusinessVariables.ComboBoxCatalogo.ValueSeleccione.ToString())
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void LlenaOrganizaciones()
        {
            try
            {
                int? idTipoUsuario = null;
                int? idHolding = null;
                int? idCompania = null;
                int? idDireccion = null;
                int? idSubDireccion = null;
                int? idGerencia = null;
                int? idSubGerencia = null;
                int? idJefatura = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                if (ddlHolding.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idHolding = int.Parse(ddlHolding.SelectedValue);

                if (ddlCompañia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idCompania = int.Parse(ddlCompañia.SelectedValue);

                if (ddlDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idDireccion = int.Parse(ddlDireccion.SelectedValue);

                if (ddlSubDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idSubDireccion = int.Parse(ddlSubDireccion.SelectedValue);

                if (ddlGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idGerencia = int.Parse(ddlGerencia.SelectedValue);

                if (ddlSubGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idSubGerencia = int.Parse(ddlSubGerencia.SelectedValue);

                if (ddlJefatura.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idJefatura = int.Parse(ddlJefatura.SelectedValue);
                List<Organizacion> lstOrganizaciones = _servicioOrganizacion.ObtenerOrganizaciones(idTipoUsuario, idHolding, idCompania, idDireccion, idSubDireccion, idGerencia, idSubGerencia, idJefatura);
                if (Modal)
                    lstOrganizaciones = lstOrganizaciones.Where(w => w.Habilitado == Modal).ToList();
                rptResultados.DataSource = lstOrganizaciones;
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void LimpiarOrganizaciones()
        {
            try
            {
                rptResultados.DataSource = null;
                rptResultados.DataBind();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string ObtenerRuta(int command, string modulo)
        {
            string result = "<h3>AGREGAR " + modulo + "</h3><span style=\"font-size: x-small;\">";
            switch (command)
            {
                //case 1:
                //    result += ddlHolding.SelectedItem.Text;
                //    break;
                case 2:
                    result += ddlHolding.SelectedItem.Text;//+ ">" + ddlCompañia.SelectedItem.Text;
                    break;
                case 3:
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text;//+ ">" + ddlDireccion.SelectedItem.Text;
                    break;
                case 4:
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text + ">" + ddlDireccion.SelectedItem.Text;//+ ">" + ddlSubDireccion.SelectedItem.Text;
                    break;
                case 5:
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text + ">" + ddlDireccion.SelectedItem.Text + ">" + ddlSubDireccion.SelectedItem.Text;// + ">" + ddlGerencia.SelectedItem.Text;
                    break;
                case 6:
                    result += ddlHolding.SelectedItem.Text + ">" + ddlCompañia.SelectedItem.Text + ">" + ddlDireccion.SelectedItem.Text + ">" + ddlSubDireccion.SelectedItem.Text + ">" + ddlGerencia.SelectedItem.Text;// + ">" + ddlSubGerencia.SelectedItem.Text;
                    break;
            }
            result += "</span>";
            return result;
        }
        private string ObtenerRuta(Organizacion organizacion, ref int nivel, ref string descripcion)
        {
            string result = null;
            try
            {
                if (organizacion.Holding != null)
                {
                    nivel = 1;
                    result = organizacion.Holding.Descripcion;
                    descripcion = organizacion.Holding.Descripcion;
                }
                if (organizacion.Compania != null)
                {
                    nivel = 2;
                    result += ">" + organizacion.Compania.Descripcion;
                    descripcion = organizacion.Compania.Descripcion;
                }
                if (organizacion.Direccion != null)
                {
                    nivel = 3;
                    result += ">" + organizacion.Direccion.Descripcion;
                    descripcion = organizacion.Direccion.Descripcion;
                }
                if (organizacion.SubDireccion != null)
                {
                    nivel = 4;
                    result += ">" + organizacion.SubDireccion.Descripcion;
                    descripcion = organizacion.SubDireccion.Descripcion;
                }
                if (organizacion.Gerencia != null)
                {
                    nivel = 5;
                    result += ">" + organizacion.Gerencia.Descripcion;
                    descripcion = organizacion.Gerencia.Descripcion;
                }
                if (organizacion.SubGerencia != null)
                {
                    nivel = 6;
                    result += ">" + organizacion.SubGerencia.Descripcion;
                    descripcion = organizacion.SubGerencia.Descripcion;
                }
                if (organizacion.Jefatura != null)
                {
                    nivel = 7;
                    result += ">" + organizacion.Jefatura.Descripcion;
                    descripcion = organizacion.Jefatura.Descripcion;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
        private void SetAlias()
        {
            try
            {
                lblNivel1.Text = "Nivel 1";
                lblNivel2.Text = "Nivel 2";
                lblNivel3.Text = "Nivel 3";
                lblNivel4.Text = "Nivel 4";
                lblNivel5.Text = "Nivel 5";
                lblNivel6.Text = "Nivel 6";
                lblNivel7.Text = "Nivel 7";
                if (ddlTipoUsuario.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.IndexTodos) return;
                List<AliasOrganizacion> alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue));
                if (alias.Count != 7)
                {
                    return;
                }
                lblNivel1.Text = alias.Single(s => s.Nivel == 1).Descripcion;
                lblNivel2.Text = alias.Single(s => s.Nivel == 2).Descripcion;
                lblNivel3.Text = alias.Single(s => s.Nivel == 3).Descripcion;
                lblNivel4.Text = alias.Single(s => s.Nivel == 4).Descripcion;
                lblNivel5.Text = alias.Single(s => s.Nivel == 5).Descripcion;
                lblNivel6.Text = alias.Single(s => s.Nivel == 6).Descripcion;
                lblNivel7.Text = alias.Single(s => s.Nivel == 7).Descripcion;
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

                AlertaOrganizacion = new List<string>();
                AlertaCatalogos = new List<string>();
                if (!IsPostBack)
                {
                    LlenaCombos();
                }
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
        protected void ddlTipoUsuario_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlHolding);
                Metodos.LimpiarCombo(ddlCompañia);
                Metodos.LimpiarCombo(ddlDireccion);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                txtFiltroDecripcion.Text = string.Empty;
                SetAlias();
                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    LimpiarOrganizaciones();
                    btnNew.Visible = false;
                    return;
                }

                if (ddlTipoUsuario.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexTodos)
                {
                    btnNew.Visible = false;
                }
                else if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos && int.Parse(ddlTipoUsuario.SelectedValue) != (int)BusinessVariables.EnumTiposUsuario.Empleado)
                {
                    LlenaComboOrganizacion(IdTipoUsuario);
                    btnNew.Visible = true;
                    btnNew.CommandName = "Holding";
                    btnNew.Text = "Agregar Holding";
                    btnNew.CommandArgument = "1";
                    if (ddlHolding.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione) 
                        ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                }
                else
                {
                    btnNew.Visible = false;
                    LlenaComboOrganizacion(IdTipoUsuario);
                }

                LlenaOrganizaciones();
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
        protected void ddlHolding_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DropDownList) sender).SelectedValue == "")
                    return;
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlCompañia);
                Metodos.LimpiarCombo(ddlDireccion);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlCompañia, _servicioOrganizacion.ObtenerCompañias(idTipoUsuario, id, true));
                LlenaOrganizaciones();
                AliasOrganizacion alias;
                string nivel;
                if (ddlHolding.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 2);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 2";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "2";
                }
                else
                {
                    if (int.Parse(ddlTipoUsuario.SelectedValue) == (int)BusinessVariables.EnumTiposUsuario.Empleado)
                    {
                        btnNew.Visible = false;
                    }
                    else
                    {
                        alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 1);
                        nivel = alias != null ? alias.Descripcion : "NIVEL 1";
                        btnNew.Visible = true;
                        btnNew.CommandName = nivel;
                        btnNew.Text = nivel;
                        btnNew.CommandArgument = "1";
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
                AlertaOrganizacion = _lstError;
            }
        }
        protected void ddlCompañia_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((DropDownList)sender).SelectedValue == "")
                    return;
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlDireccion);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlDireccion, _servicioOrganizacion.ObtenerDirecciones(idTipoUsuario, id, true));
                LlenaOrganizaciones();
                AliasOrganizacion alias;
                string nivel;
                if (ddlCompañia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 3);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 3";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "3";
                }
                else
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 2);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 2";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "2";
                }

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
                if (((DropDownList)sender).SelectedValue == "")
                    return;
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlSubDireccion);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlSubDireccion, _servicioOrganizacion.ObtenerSubDirecciones(idTipoUsuario, id, true));
                LlenaOrganizaciones();
                AliasOrganizacion alias;
                string nivel;
                if (ddlDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 4);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 4";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "4";
                }
                else
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 3);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 3";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "3";
                }

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
                if (((DropDownList)sender).SelectedValue == "")
                    return;
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlGerencia);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlGerencia, _servicioOrganizacion.ObtenerGerencias(idTipoUsuario, id, true));
                LlenaOrganizaciones();
                AliasOrganizacion alias;
                string nivel;
                if (ddlSubDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 5);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 5";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "5";
                }
                else
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 4);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 4";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "4";
                }

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
                if (((DropDownList)sender).SelectedValue == "")
                    return;
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlSubGerencia);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlSubGerencia, _servicioOrganizacion.ObtenerSubGerencias(idTipoUsuario, id, true));
                LlenaOrganizaciones();
                AliasOrganizacion alias;
                string nivel;
                if (ddlGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 6);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 6";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "6";
                }
                else
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 5);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 5";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "5";
                }

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
                if (((DropDownList)sender).SelectedValue == "")
                    return;
                int idTipoUsuario = IdTipoUsuario;
                int id = Convert.ToInt32(((DropDownList)sender).SelectedValue);
                Metodos.LimpiarCombo(ddlJefatura);
                FiltraCombo((DropDownList)sender, ddlJefatura, _servicioOrganizacion.ObtenerJefaturas(idTipoUsuario, id, true));
                LlenaOrganizaciones();
                AliasOrganizacion alias;
                string nivel;
                if (ddlSubGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 7);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 7";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "7";
                }
                else
                {
                    alias = _servicioParametros.ObtenerAliasOrganizacion(int.Parse(ddlTipoUsuario.SelectedValue)).SingleOrDefault(w => w.Nivel == 6);
                    nivel = alias != null ? alias.Descripcion : "NIVEL 6";
                    btnNew.Visible = true;
                    btnNew.CommandName = nivel;
                    btnNew.Text = nivel;
                    btnNew.CommandArgument = "6";
                }

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
            try
            {
                if (((DropDownList)sender).SelectedValue == "")
                    return;
                LlenaOrganizaciones();
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
        protected void btnEditar_OnClick(object sender, EventArgs e)
        {
            try
            {
                int nivel = 0;
                string descripcion = null;
                Organizacion org = _servicioOrganizacion.ObtenerOrganizacionById(int.Parse(((Button)sender).CommandArgument));
                if (org == null) return;
                ddlTipoUsuario.SelectedValue = org.IdTipoUsuario.ToString();
                ddlTipoUsuario_OnSelectedIndexChanged(ddlTipoUsuario, null);
                if (org.Holding != null)
                {
                    ddlHolding.SelectedValue = org.IdHolding.ToString();
                    ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                }
                if (org.Compania != null)
                {
                    ddlCompañia.SelectedValue = org.IdCompania.ToString();
                    ddlCompañia_OnSelectedIndexChanged(ddlCompañia, null);
                }
                if (org.Direccion != null)
                {
                    ddlDireccion.SelectedValue = org.IdDireccion.ToString();
                    ddlDirecion_OnSelectedIndexChanged(ddlDireccion, null);
                }
                if (org.SubDireccion != null)
                {
                    ddlSubDireccion.SelectedValue = org.IdSubDireccion.ToString();
                    ddlSubDireccion_OnSelectedIndexChanged(ddlSubDireccion, null);
                }
                if (org.Gerencia != null)
                {
                    ddlGerencia.SelectedValue = org.IdGerencia.ToString();
                    ddlGerencia_OnSelectedIndexChanged(ddlGerencia, null);
                }
                if (org.SubGerencia != null)
                {
                    ddlSubGerencia.SelectedValue = org.IdSubGerencia.ToString();
                    ddlSubGerencia_OnSelectedIndexChanged(ddlSubGerencia, null);
                }
                if(org.Jefatura != null)
                {
                    ddlJefatura.SelectedValue = org.IdJefatura.ToString();
                    ddlJefatura_OnSelectedIndexChanged(ddlJefatura, null);
                }
                Session["OrganizacionSeleccionada"] = org;
                lblTitleCatalogo.Text = ObtenerRuta(org, ref nivel, ref descripcion);
                txtDescripcionCatalogo.Text = descripcion;
                hfCatalogo.Value = nivel.ToString();
                hfAlta.Value = false.ToString();
                ddlTipoUsuarioCatalogo.SelectedValue = org.IdTipoUsuario.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoOrganizacion\");", true);
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
        protected void btnGuardarCatalogo_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (!Metodos.ValidaCapturaCatalogo(txtDescripcionCatalogo.Text)) return;
                Organizacion organizacion;
                if (Convert.ToBoolean(hfAlta.Value))
                {
                    organizacion = new Organizacion
                    {
                        IdTipoUsuario = IdTipoUsuario,
                        IdHolding = Convert.ToInt32(ddlHolding.SelectedValue)
                    };
                    switch (int.Parse(hfCatalogo.Value))
                    {
                        case 1:
                            organizacion.Holding = new Holding
                            {
                                IdTipoUsuario = Convert.ToInt32(ddlTipoUsuarioCatalogo.SelectedValue),
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            LlenaComboOrganizacion(Convert.ToInt32(ddlTipoUsuarioCatalogo.SelectedValue));
                            ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                            ddlHolding.SelectedValue = ddlHolding.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            break;
                        case 2:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.Compania = new Compania
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                            ddlCompañia.SelectedValue = ddlCompañia.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlCompañia_OnSelectedIndexChanged(ddlCompañia, null);
                            break;
                        case 3:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.Direccion = new Direccion
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlCompañia_OnSelectedIndexChanged(ddlCompañia, null);
                            ddlDireccion.SelectedValue = ddlDireccion.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlDirecion_OnSelectedIndexChanged(ddlDireccion, null);
                            break;
                        case 4:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.SubDireccion = new SubDireccion
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlDirecion_OnSelectedIndexChanged(ddlDireccion, null);
                            ddlSubDireccion.SelectedValue = ddlSubDireccion.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlSubDireccion_OnSelectedIndexChanged(ddlSubDireccion, null);
                            break;
                        case 5:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.IdSubDireccion = Convert.ToInt32(ddlSubDireccion.SelectedValue);
                            organizacion.Gerencia = new Gerencia
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlSubDireccion_OnSelectedIndexChanged(ddlSubDireccion, null);
                            ddlGerencia.SelectedValue = ddlGerencia.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlGerencia_OnSelectedIndexChanged(ddlGerencia, null);
                            break;
                        case 6:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.IdSubDireccion = Convert.ToInt32(ddlSubDireccion.SelectedValue);
                            organizacion.IdGerencia = Convert.ToInt32(ddlGerencia.SelectedValue);
                            organizacion.SubGerencia = new SubGerencia
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlGerencia_OnSelectedIndexChanged(ddlGerencia, null);
                            ddlSubGerencia.SelectedValue = ddlSubGerencia.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlSubGerencia_OnSelectedIndexChanged(ddlSubGerencia, null);
                            break;
                        case 7:
                            organizacion.IdHolding = Convert.ToInt32(ddlHolding.SelectedValue);
                            organizacion.IdCompania = Convert.ToInt32(ddlCompañia.SelectedValue);
                            organizacion.IdDireccion = Convert.ToInt32(ddlDireccion.SelectedValue);
                            organizacion.IdSubDireccion = Convert.ToInt32(ddlSubDireccion.SelectedValue);
                            organizacion.IdGerencia = Convert.ToInt32(ddlGerencia.SelectedValue);
                            organizacion.IdSubGerencia = Convert.ToInt32(ddlSubGerencia.SelectedValue);
                            organizacion.Jefatura = new Jefatura
                            {
                                IdTipoUsuario = IdTipoUsuario,
                                Descripcion = txtDescripcionCatalogo.Text.Trim(),
                                Habilitado = chkHabilitado.Checked
                            };
                            _servicioOrganizacion.GuardarOrganizacion(organizacion);
                            ddlSubGerencia_OnSelectedIndexChanged(ddlSubGerencia, null);
                            ddlJefatura.SelectedValue = ddlJefatura.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlJefatura_OnSelectedIndexChanged(ddlJefatura, null);
                            break;
                    }
                }
                else
                {
                    organizacion = (Organizacion)Session["OrganizacionSeleccionada"];
                    switch (int.Parse(hfCatalogo.Value))
                    {
                        case 1:
                            organizacion.Holding.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                            ddlTipoUsuario_OnSelectedIndexChanged(ddlTipoUsuario, null);
                            ddlHolding.SelectedValue = ddlHolding.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                            break;
                        case 2:
                            organizacion.Compania.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                            ddlHolding_OnSelectedIndexChanged(ddlHolding, null);
                            ddlCompañia.SelectedValue = ddlCompañia.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlCompañia_OnSelectedIndexChanged(ddlCompañia, null);
                            break;
                        case 3:
                            organizacion.Direccion.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                            ddlCompañia_OnSelectedIndexChanged(ddlCompañia, null);
                            ddlDireccion.SelectedValue = ddlDireccion.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlDirecion_OnSelectedIndexChanged(ddlDireccion, null);
                            break;
                        case 4:
                            organizacion.SubDireccion.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                            ddlDirecion_OnSelectedIndexChanged(ddlDireccion, null);
                            ddlSubDireccion.SelectedValue = ddlSubDireccion.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlSubDireccion_OnSelectedIndexChanged(ddlSubDireccion, null);
                            break;
                        case 5:
                            organizacion.Gerencia.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                            ddlSubDireccion_OnSelectedIndexChanged(ddlSubDireccion, null);
                            ddlGerencia.SelectedValue = ddlGerencia.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlGerencia_OnSelectedIndexChanged(ddlGerencia, null);
                            break;
                        case 6:
                            organizacion.SubGerencia.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                            ddlGerencia_OnSelectedIndexChanged(ddlGerencia, null);
                            ddlSubGerencia.SelectedValue = ddlSubGerencia.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            ddlSubGerencia_OnSelectedIndexChanged(ddlSubGerencia, null);
                            break;
                        case 7:
                            organizacion.Jefatura.Descripcion = txtDescripcionCatalogo.Text.Trim();
                            _servicioOrganizacion.ActualizarOrganizacion(organizacion);
                            ddlSubGerencia_OnSelectedIndexChanged(ddlSubGerencia, null);
                            ddlJefatura.SelectedValue = ddlJefatura.Items.FindByText(txtDescripcionCatalogo.Text.Trim().ToUpper()).Value;
                            break;
                    }
                }
                txtFiltroDecripcion.Text = string.Empty;
                LimpiaCatalogo();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
                LlenaOrganizaciones();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaCatalogos = _lstError;
            }
        }
        protected void btnCancelarCatalogo_OnClick(object sender, EventArgs e)
        {
            LimpiaCatalogo();
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#editCatalogoOrganizacion\");", true);
        }
        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (sender == null) return;
                lblTitleCatalogo.Text = ObtenerRuta(int.Parse(btn.CommandArgument), btn.CommandName);
                hfCatalogo.Value = btn.CommandArgument;
                hfAlta.Value = true.ToString();
                ddlTipoUsuarioCatalogo.SelectedValue = IdTipoUsuario.ToString();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#editCatalogoOrganizacion\");", true);
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
        protected void btnCerrar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCerrar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnSeleccionOrganizacionModal != null)
                    OnSeleccionOrganizacionModal();
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
        protected void rptResultados_OnItemCreated(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    if (e.Item.ItemType == ListItemType.Header)
                    {
                        List<AliasOrganizacion> alias = _servicioParametros.ObtenerAliasOrganizacion(IdTipoUsuario);
                        if (alias.Count != 7) return;
                        ((Label)e.Item.FindControl("lblHolding")).Text = alias.Single(s => s.Nivel == 1).Descripcion;
                        ((Label)e.Item.FindControl("lblCompania")).Text = alias.Single(s => s.Nivel == 2).Descripcion;
                        ((Label)e.Item.FindControl("lblDireccion")).Text = alias.Single(s => s.Nivel == 3).Descripcion;
                        ((Label)e.Item.FindControl("lblSubDireccion")).Text = alias.Single(s => s.Nivel == 4).Descripcion;
                        ((Label)e.Item.FindControl("lblGerencia")).Text = alias.Single(s => s.Nivel == 5).Descripcion;
                        ((Label)e.Item.FindControl("lblSubGerencia")).Text = alias.Single(s => s.Nivel == 6).Descripcion;
                        ((Label)e.Item.FindControl("lblJefatura")).Text = alias.Single(s => s.Nivel == 7).Descripcion;
                    }
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
        protected void btnBajaAlta_OnClick(object sender, EventArgs e)
        {
            try
            {
                bool alta = bool.Parse(((Button)sender).CommandArgument);
                _servicioOrganizacion.HabilitarOrganizacion(int.Parse(((Button)sender).CommandName), !alta);
                LlenaOrganizaciones();
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
        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
                int? idTipoUsuario = null;
                int? idHolding = null;
                int? idCompania = null;
                int? idDireccion = null;
                int? idSubDireccion = null;
                int? idGerencia = null;
                int? idSubGerencia = null;
                int? idJefatura = null;
                if (ddlTipoUsuario.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexTodos)
                    idTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                if (ddlHolding.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idHolding = int.Parse(ddlHolding.SelectedValue);

                if (ddlCompañia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idCompania = int.Parse(ddlCompañia.SelectedValue);

                if (ddlDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idDireccion = int.Parse(ddlDireccion.SelectedValue);

                if (ddlSubDireccion.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idSubDireccion = int.Parse(ddlSubDireccion.SelectedValue);

                if (ddlGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idGerencia = int.Parse(ddlGerencia.SelectedValue);

                if (ddlSubGerencia.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idSubGerencia = int.Parse(ddlSubGerencia.SelectedValue);

                if (ddlJefatura.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    idJefatura = int.Parse(ddlJefatura.SelectedValue);

                List<Organizacion> lstOrganizaciones = _servicioOrganizacion.BuscarPorPalabra(idTipoUsuario, idHolding, idCompania, idDireccion, idSubDireccion, idGerencia, idSubGerencia, idJefatura, txtFiltroDecripcion.Text.Trim());
                if (Modal)
                    lstOrganizaciones = lstOrganizaciones.Where(w => w.Habilitado == Modal).ToList();

                rptResultados.DataSource = lstOrganizaciones;
                rptResultados.DataBind();
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