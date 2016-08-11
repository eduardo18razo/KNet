using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceArbolAcceso;
using KiiniHelp.ServiceArea;
using KiiniHelp.ServiceSeguridad;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using Menu = KiiniNet.Entities.Cat.Sistema.Menu;

namespace KiiniHelp
{
    public partial class UsuariosMaster : MasterPage
    {
        private readonly ServiceSecurityClient _servicioSeguridad = new ServiceSecurityClient();
        private readonly ServiceArbolAccesoClient _servicioArbolAcceso = new ServiceArbolAccesoClient();
        private readonly ServiceAreaClient _servicioArea = new ServiceAreaClient();

        private List<string> _lstError = new List<string>();

        private List<string> AlertaGeneral
        {
            set
            {
                panelAlert.Visible = value.Any();
                if (!panelAlert.Visible) return;
                rptHeaderError.DataSource = value;
                rptHeaderError.DataBind();
            }
        }

        private void ObtenerAreas()
        {
            try
            {
                List<Area> lstAreas = _servicioArea.ObtenerAreasUsuario(((Usuario)Session["UserData"]).Id, true);
                ddlAreas.DataSource = lstAreas;
                ddlAreas.DataTextField = "Descripcion";
                ddlAreas.DataValueField = "Id";
                ddlAreas.DataBind();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["AreaSeleccionada"] != null)
                    ddlAreas.SelectedValue = Session["AreaSeleccionada"].ToString();
                if (!IsPostBack && Session["UserData"] != null)
                {
                    Usuario usuario = ((Usuario)Session["UserData"]);
                    lblUsuario.Text = usuario.NombreCompleto;
                    lblTipoUsr.Text = usuario.TipoUsuario.Descripcion;
                    ObtenerAreas();
                    int areaSeleccionada = 0;
                    if (Session["AreaSeleccionada"] != null)
                        areaSeleccionada = int.Parse(Session["AreaSeleccionada"].ToString());
                    rptMenu.DataSource = _servicioSeguridad.ObtenerMenuUsuario(usuario.Id, areaSeleccionada, areaSeleccionada != 0);
                    rptMenu.DataBind();

                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void rptMenu_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu1"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }

            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void rptSubMenu1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu2"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }

        }

        protected void rptSubMenu2_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu3"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void rptSubMenu3_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu4"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void rptSubMenu4_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu5"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void rptSubMenu5_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu6"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void rptSubMenu6_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater rptSubMenu = ((Repeater)e.Item.FindControl("rptSubMenu7"));
                if (rptSubMenu != null)
                {
                    rptSubMenu.DataSource = ((Menu)e.Item.DataItem).Menu1;
                    rptSubMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void btnsOut_OnClick(object sender, EventArgs e)
        {
            try
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }

        protected void ddlAreas_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAreas.SelectedIndex > BusinessVariables.ComboBoxCatalogo.Index)
                {
                    Usuario usuario = ((Usuario)Session["UserData"]);
                    Session["AreaSeleccionada"] = ddlAreas.SelectedValue;
                    int areaSeleccionada = 0;
                    if (Session["AreaSeleccionada"] != null)
                        areaSeleccionada = int.Parse(Session["AreaSeleccionada"].ToString());
                    rptMenu.DataSource = _servicioSeguridad.ObtenerMenuUsuario(usuario.Id, areaSeleccionada, areaSeleccionada != 0);
                    rptMenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                if (_lstError == null)
                {
                    _lstError = new List<string>();
                }
                _lstError.Add(ex.Message);
                AlertaGeneral = _lstError;
            }
        }
    }
}