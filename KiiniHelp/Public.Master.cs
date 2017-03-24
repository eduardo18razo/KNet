using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceArea;
using KiiniHelp.ServiceSeguridad;
using KinniNet.Business.Utils;
using Menu = KiiniNet.Entities.Cat.Sistema.Menu;

namespace KiiniHelp
{
    public partial class Public : MasterPage
    {
        private readonly ServiceSecurityClient _servicioSeguridad = new ServiceSecurityClient();
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
                //rptClientes.DataSource = _servicioArea.ObtenerAreasTipoUsuario((int)BusinessVariables.EnumTiposUsuario.ClienteInvitado, false);
                //rptClientes.DataBind();
                //rptEmpleados.DataSource = _servicioArea.ObtenerAreasTipoUsuario((int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado, false);
                //rptEmpleados.DataBind();
                //rptProveedores.DataSource = _servicioArea.ObtenerAreasTipoUsuario((int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado, false);
                //rptProveedores.DataBind();
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
                UcLogIn.OnAceptarModal += UcLogInOnOnCancelarModal;
                UcLogIn.OnCancelarModal += UcLogInOnOnCancelarModal;
                if (Request.Params["userTipe"] != null)
                {
                    int areaSeleccionada;
                    int tipoUsuario = int.Parse(Request.Params["userTipe"]);
                    switch (tipoUsuario)
                    {
                        case (int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado:
                            areaSeleccionada = 6;
                            Session["AreaSeleccionada"] = areaSeleccionada;
                            rptMenu.DataSource = _servicioSeguridad.ObtenerMenuPublico((int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado, areaSeleccionada, areaSeleccionada != 0);
                            rptMenu.DataBind();
                            UcLogIn.AutenticarUsuarioPublico((int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado);
                            break;
                        case (int)BusinessVariables.EnumTiposUsuario.ClienteInvitado:
                            areaSeleccionada = 6;
                            Session["AreaSeleccionada"] = areaSeleccionada;
                            rptMenu.DataSource = _servicioSeguridad.ObtenerMenuPublico((int)BusinessVariables.EnumTiposUsuario.ClienteInvitado, areaSeleccionada, areaSeleccionada != 0);
                            rptMenu.DataBind();
                            UcLogIn.AutenticarUsuarioPublico((int)BusinessVariables.EnumTiposUsuario.ClienteInvitado);
                            break;
                        case (int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado:
                            areaSeleccionada = 6;
                            Session["AreaSeleccionada"] = areaSeleccionada;
                            rptMenu.DataSource = _servicioSeguridad.ObtenerMenuPublico((int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado, areaSeleccionada, areaSeleccionada != 0);
                            rptMenu.DataBind();
                            UcLogIn.AutenticarUsuarioPublico((int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado);
                            break;

                    }

                }
                if (!IsPostBack)
                {
                    ObtenerAreas();
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

        private void UcLogInOnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalSingIn\");", true);
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
                    rptSubMenu.DataSource = ((KiiniNet.Entities.Cat.Sistema.Menu)e.Item.DataItem).Menu1;
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
                    rptSubMenu.DataSource = ((KiiniNet.Entities.Cat.Sistema.Menu)e.Item.DataItem).Menu1;
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
                    rptSubMenu.DataSource = ((KiiniNet.Entities.Cat.Sistema.Menu)e.Item.DataItem).Menu1;
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
                    rptSubMenu.DataSource = ((KiiniNet.Entities.Cat.Sistema.Menu)e.Item.DataItem).Menu1;
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
                    rptSubMenu.DataSource = ((KiiniNet.Entities.Cat.Sistema.Menu)e.Item.DataItem).Menu1;
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
                    rptSubMenu.DataSource = ((KiiniNet.Entities.Cat.Sistema.Menu)e.Item.DataItem).Menu1;
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

        protected void lbtnCteArea_OnClick(object sender, EventArgs e)
        {
            try
            {
                int areaSeleccionada = Convert.ToInt32(((LinkButton)sender).CommandArgument);
                Session["AreaSeleccionada"] = areaSeleccionada;
                rptMenu.DataSource = _servicioSeguridad.ObtenerMenuPublico((int)BusinessVariables.EnumTiposUsuario.ClienteInvitado, areaSeleccionada, areaSeleccionada != 0);
                rptMenu.DataBind();
                UcLogIn.AutenticarUsuarioPublico((int)BusinessVariables.EnumTiposUsuario.ClienteInvitado);
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

        protected void lbtnEmpleadoArea_OnClick(object sender, EventArgs e)
        {
            try
            {
                int areaSeleccionada = Convert.ToInt32(((LinkButton)sender).CommandArgument);
                Session["AreaSeleccionada"] = areaSeleccionada;
                rptMenu.DataSource = _servicioSeguridad.ObtenerMenuPublico((int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado, areaSeleccionada, areaSeleccionada != 0);
                rptMenu.DataBind();
                UcLogIn.AutenticarUsuarioPublico((int)BusinessVariables.EnumTiposUsuario.EmpleadoInvitado);
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

        protected void lbtnProveedorArea_OnClick(object sender, EventArgs e)
        {
            try
            {
                int areaSeleccionada = Convert.ToInt32(((LinkButton)sender).CommandArgument);
                Session["AreaSeleccionada"] = areaSeleccionada;
                rptMenu.DataSource = _servicioSeguridad.ObtenerMenuPublico((int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado, areaSeleccionada, areaSeleccionada != 0);
                rptMenu.DataBind();
                UcLogIn.AutenticarUsuarioPublico((int)BusinessVariables.EnumTiposUsuario.ProveedorInvitado);
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

        protected void btnLogIn_OnClick(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalSingIn\");", true);
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

        protected void lnkConsultaticket_OnClick(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ResolveUrl("~/Publico/Consultas/FrmConsultaTicket.aspx"));
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