using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceSistemaEstatus;
using KiiniHelp.ServiceTicket;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;

namespace KiiniHelp.Operacion
{
    public partial class FrmOperacionTickets : Page
    {
        readonly ServiceTicketClient _servicioTickets = new ServiceTicketClient();
        readonly ServiceEstatusClient _servicioEstatus = new ServiceEstatusClient();

        private int _pageSize = 20;
        private void ObtenerTicketsPage(int pageIndex, Dictionary<string, string> filtros, bool orden, bool asc, string ordering = "")
        {
            try
            {
                List<HelperTickets> lst = _servicioTickets.ObtenerTickets(((Usuario)Session["UserData"]).Id, pageIndex, _pageSize);
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
                rptTickets.DataSource = lst;
                rptTickets.DataBind();
                if (lst.Count == 0 && pageIndex == 1) return;
                int recordCount = pageIndex * _pageSize;
                GeneraPaginado(recordCount, pageIndex);
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
                double dblPageCount = (double)(recordCount / Convert.ToDecimal(_pageSize));
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Column"] = "DateTime";
                ViewState["Sortorder"] = "ASC";
                ViewState["PageIndex"] = "0";
                ViewState["Filtros"] = new Dictionary<string, string>();
                ObtenerTicketsPage(int.Parse(ViewState["PageIndex"].ToString()), (Dictionary<string, string>)ViewState["Filtros"], true, ViewState["Sortorder"].ToString() == "ASC", ViewState["Column"].ToString());

            }
            UcDetalleUsuario1.OnCerraModal += UcDetalleUsuario1OnOnCerraModal;
        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = int.Parse(((LinkButton)(sender)).CommandArgument);
            ViewState["PageIndex"] = pageIndex;
            ObtenerTicketsPage(int.Parse(ViewState["PageIndex"].ToString()), (Dictionary<string, string>)ViewState["Filtros"], true, ViewState["Sortorder"].ToString() == "ASC", ViewState["Column"].ToString());
        }

        protected void rptTickets_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DropDownList ddlEstatus = (DropDownList)e.Item.FindControl("ddlEstatus");
                DropDownList ddlAsignacion = (DropDownList)e.Item.FindControl("ddlAsignacion");
                DropDownList ddlTipificacion = (DropDownList)e.Item.FindControl("ddlTipificacion");
                if (ddlEstatus != null)
                {
                    ddlEstatus.DataSource = _servicioEstatus.ObtenerEstatusTicket(true);
                    ddlEstatus.DataTextField = "Descripcion";
                    ddlEstatus.DataValueField = "Id";
                    ddlEstatus.DataBind();
                }
                if (ddlAsignacion != null)
                {
                    ddlAsignacion.DataSource = _servicioEstatus.ObtenerEstatusAsignacion(true);
                    ddlAsignacion.DataTextField = "Descripcion";
                    ddlAsignacion.DataValueField = "Id";
                    ddlAsignacion.DataBind();
                }
                if (ddlTipificacion != null)
                {
                    ddlTipificacion.DataSource = ViewState["Tipificaciones"];
                    ddlTipificacion.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void rptTickets_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == ViewState["Column"].ToString())
                {
                    if (ViewState["Sortorder"].ToString() == "ASC")
                        ViewState["Sortorder"] = "DESC";
                    else
                        ViewState["Sortorder"] = "ASC";
                }
                else
                {
                    ViewState["Column"] = e.CommandName;
                    ViewState["Sortorder"] = "ASC";
                }
                ObtenerTicketsPage(int.Parse(ViewState["PageIndex"].ToString()), (Dictionary<string, string>)ViewState["Filtros"], true, ViewState["Sortorder"].ToString() == "ASC", ViewState["Column"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void txtFilerNumeroTicket_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> dictionary = (Dictionary<string, string>)ViewState["Filtros"];
                if (dictionary.Any(a => a.Key == "NumeroTicket"))
                    dictionary[dictionary.SingleOrDefault(s => s.Key == "NumeroTicket").Key] = ((TextBox)sender).Text;
                else
                    dictionary.Add("NumeroTicket", ((TextBox)sender).Text);
                ViewState["Filtros"] = dictionary;
                ((TextBox) sender).Text = ((TextBox) sender).Text;
                ObtenerTicketsPage(int.Parse(ViewState["PageIndex"].ToString()), (Dictionary<string, string>)ViewState["Filtros"], true, ViewState["Sortorder"].ToString() == "ASC", ViewState["Column"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnUsuario_OnClick(object sender, EventArgs e)
        {
            try
            {
                UcDetalleUsuario1.IdUsuario = Convert.ToInt32(((LinkButton) sender).CommandArgument);
                
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalDetalleUsuario\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void UcDetalleUsuario1OnOnCerraModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalDetalleUsuario\");", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void btnCambiarEstatus_OnClick(object sender, EventArgs e)
        {
            

        }

        protected void btnAsignar_OnClick(object sender, EventArgs e)
        {
            

        }
    }
}