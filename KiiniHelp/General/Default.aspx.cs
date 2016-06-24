using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceArea;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.General
{
    public partial class Default : Page
    {
        readonly ServiceAreaClient _servicioArea = new ServiceAreaClient();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    List<Area> lstAreas = _servicioArea.ObtenerAreasUsuario(((Usuario)Session["UserData"]).Id);
                    divAreas.Visible = lstAreas.Count > 0;
                    rptAreas.DataSource = lstAreas;
                    rptAreas.DataBind();
                    if (Session["AreaSeleccionada"] == null) return;
                    foreach (Button btn in rptAreas.Items.Cast<RepeaterItem>().Select(item => (Button) item.FindControl("btnArea")).Where(btn => btn.CommandArgument == Session["AreaSeleccionada"].ToString()))
                    {
                        btn.CssClass = "btn btn-lg btn-success";
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
                AlertaGeneral = _lstError;
            }
        }


        protected void btnArea_OnClick(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn != null)
                {
                    Session["AreaSeleccionada"] = btn.CommandArgument;
                    Response.Redirect("~/General/Default.aspx");
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

        protected void rptAreas_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (Session["AreaSeleccionada"] == null) return;

                if (((Button)e.Item.FindControl("btnArea")).CommandArgument == (string) Session["AreaSeleccionada"])
                {
                    ((Button)e.Item.FindControl("btnArea")).CssClass = "btn btn-lg btn-success";
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