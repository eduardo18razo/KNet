using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceTicket;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.UserControls.Consultas
{
    public partial class UcConsultaMisTickets : System.Web.UI.UserControl
    {
        private readonly ServiceTicketClient _servicioTickets = new ServiceTicketClient();
        private List<string> _lstError = new List<string>();
        private const int PageSize = 20;

        public List<string> Alerta
        {
            set
            {
                if (value.Any())
                {
                    string error = value.Aggregate("<ul>", (current, s) => current + ("<li>" + s + "</li>"));
                    error += "</ul>";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptErrorAlert", "ErrorAlert('Error','" + error + "');", true);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblBranding.Text = WebConfigurationManager.AppSettings["Brand"];
                Alerta = new List<string>();
                if (!IsPostBack)
                {
                    ViewState["Column"] = "DateTime";
                    ViewState["Sortorder"] = "ASC";
                    ViewState["PageIndex"] = "0";
                    ViewState["Filtros"] = new Dictionary<string, string>();
                    ObtenerTicketsPage(int.Parse(ViewState["PageIndex"].ToString()), (Dictionary<string, string>)ViewState["Filtros"], true, ViewState["Sortorder"].ToString() == "ASC", ViewState["Column"].ToString());
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

        private void ObtenerTicketsPage(int pageIndex, Dictionary<string, string> filtros, bool orden, bool asc, string ordering = "")
        {
            try
            {
                List<HelperTickets> lst = _servicioTickets.ObtenerTicketsUsuario(((Usuario)Session["UserData"]).Id, pageIndex, PageSize);
                if (lst != null)
                {
                    foreach (KeyValuePair<string, string> filtro in filtros)
                    {
                        switch (filtro.Key)
                        {
                            case "NumeroTicket":
                                lst = lst.Where(w => w.NumeroTicket == int.Parse(filtro.Value)).ToList();
                                break;
                        }
                    }
                    if (orden && asc)
                        switch (ordering)
                        {
                            case "DateTime":
                                lst = lst.OrderBy(o => o.FechaHora).ToList();
                                break;
                        }
                    else
                        switch (ordering)
                        {
                            case "DateTime":
                                lst = lst.OrderByDescending(o => o.FechaHora).ToList();
                                break;
                        }

                    ViewState["Tipificaciones"] = lst.Select(s => s.Tipificacion).Distinct().ToList();
                    rptResultados.DataSource = lst;
                    rptResultados.DataBind();
                    if (lst.Count == 0 && pageIndex == 1) return;
                    int recordCount = pageIndex * PageSize;
                    GeneraPaginado(recordCount, pageIndex);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        private void GeneraPaginado(int recordCount, int currentPage)
        {
            try
            {
                double dblPageCount = (double)(recordCount / Convert.ToDecimal(PageSize));
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected void btnNew_OnClick(object sender, EventArgs e)
        {
            try
            {
                
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

        protected void btnBuscar_OnClick(object sender, EventArgs e)
        {
            try
            {
                
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