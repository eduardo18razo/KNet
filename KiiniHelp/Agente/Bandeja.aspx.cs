using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceAtencionTicket;
using KiiniHelp.ServiceGrupoUsuario;
using KiiniHelp.ServiceTicket;
using KiiniHelp.ServiceUsuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using Telerik.Web.UI;

namespace KiiniHelp.Agente
{
    //File change
    public partial class Bandeja : Page
    {
        private readonly ServiceTicketClient _servicioTickets = new ServiceTicketClient();
        private readonly ServiceGrupoUsuarioClient _servicioGrupos = new ServiceGrupoUsuarioClient();
        private readonly ServiceUsuariosClient _servicioUsuarios = new ServiceUsuariosClient();
        private readonly ServiceAtencionTicketClient _servicioAtencionTicket = new ServiceAtencionTicketClient();
        private List<string> _lstError = new List<string>();
        private List<string> Alerta
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

        private List<int> EstatusAbierto
        {
            get
            {
                List<int> result = new List<int>
                {
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto,
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto,
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera
                };

                return result;
            }
        }
        private List<int> EstatusEspera
        {
            get
            {
                List<int> result = new List<int>
                {
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera
                };

                return result;
            }
        }

        private List<int> EstatusSinAsignar
        {
            get
            {
                List<int> result = new List<int>
                {
                    (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto,
                    (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto,
                    (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera,
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera
                };

                return result;
            }
        }

        private List<int> EstatusResuletos
        {
            get
            {
                List<int> result = new List<int>
                {
                    (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Resuelto,
                    (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Cerrado
                };

                return result;
            }
        }

        private List<int> EstatusFueraSla
        {
            get
            {
                List<int> result = new List<int>
                {
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.Abierto,
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.ReAbierto,
                    (int) BusinessVariables.EnumeradoresKiiniNet.EnumEstatusTicket.EnEspera
                };

                return result;
            }
        }

        private List<int> EstatusSeleccionado
        {
            get { return (List<int>)Session["EstatusSeleccionado"]; }
            set { Session["EstatusSeleccionado"] = value; }
        }


        public List<HelperTickets> Tickets
        {
            get
            {
                List<HelperTickets> data = (List<HelperTickets>)Session["Helpertickets"];

                if (data == null)
                {
                    ObtenerTicketsPage();
                    data = (List<HelperTickets>)Session["Helpertickets"];
                }

                return data;
            }
            set { Session["Helpertickets"] = value; }
        }



        private void ObtieneTotales(List<HelperTickets> lst)
        {
            try
            {

                ((Label)btnFiltroAbierto.FindControl("lblTicketsAbiertos")).Text = lst.Count(c => EstatusAbierto.Contains(c.EstatusTicket.Id)).ToString();

                ((Label)btnFiltroEspera.FindControl("lblTicketsEspera")).Text = lst.Count(c => EstatusEspera.Contains(c.EstatusTicket.Id)).ToString();

                const int statusSinAsignar = (int)BusinessVariables.EnumeradoresKiiniNet.EnumEstatusAsignacion.PorAsignar;
                ((Label)btnFiltroSinAsignar.FindControl("lblTicketsSinAsignar")).Text = lst.Count(w => EstatusSinAsignar.Contains(w.EstatusTicket.Id) && w.EstatusAsignacion.Id == statusSinAsignar).ToString();

                ((Label)btnFiltroResuelto.FindControl("lblTicketsResueltos")).Text = lst.Count(w => EstatusResuletos.Contains(w.EstatusTicket.Id)).ToString();

                ((Label)btnFueraSla.FindControl("lblTicketsFueraSla")).Text = lst.Count(w => EstatusFueraSla.Contains(w.EstatusTicket.Id) && !w.DentroSla).ToString();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void ObtenerTicketsPage()
        {
            try
            {

                List<HelperTickets> lst = _servicioTickets.ObtenerTickets(((Usuario)Session["UserData"]).Id, EstatusSeleccionado, 0, 100000);
                if (lst != null)
                {
                    int? idGrupo = null;
                    if (ddlGrupo.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                        idGrupo = int.Parse(ddlGrupo.SelectedValue);
                    int? idAgente = null;
                    if (ddlAgente.SelectedIndex > BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                        idAgente = int.Parse(ddlGrupo.SelectedValue);

                    if (idGrupo != null || idAgente != null)
                    {
                        if (idGrupo != null && idAgente != null)
                        {
                            lst = lst.Where(w => w.IdGrupoAsignado == idGrupo && w.IdUsuarioAsignado == idAgente).ToList();
                        }
                        else if (idGrupo != null)
                            lst = lst.Where(w => w.IdGrupoAsignado == idGrupo).ToList();
                        else
                            lst = lst.Where(w => w.IdUsuarioAsignado == idAgente).ToList();
                    }
                    ObtieneTotales(lst);
                    ViewState["Tipificaciones"] = lst.Select(s => s.Tipificacion).Distinct().ToList();
                    Tickets = lst;
                    gvTickets.Rebind();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        private void LlenaCombos()
        {
            try
            {
                ddlGrupo.DataSource = _servicioGrupos.ObtenerGruposAtencionByIdUsuario(((Usuario)Session["UserData"]).Id, true);
                ddlGrupo.DataTextField = "Descripcion";
                ddlGrupo.DataValueField = "Id";
                ddlGrupo.DataBind();


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _lstError = new List<string>();

            ucCambiarEstatusAsignacion.OnAceptarModal += UcCambiarEstatusAsignacion_OnAceptarModal;
            ucCambiarEstatusAsignacion.OnCancelarModal += UcCambiarEstatusAsignacionOnCancelarModal;

            UcDetalleUsuario.OnAceptarModal += UcDetalleUsuario_OnAceptarModal;
            UcDetalleUsuario.OnCancelarModal += UcDetalleUsuario1OnOnCancelarModal;

            UcCambiarEstatusTicket.OnAceptarModal += UcCambiarEstatusTicket_OnAceptarModal;
            UcCambiarEstatusTicket.OnCancelarModal += UcCambiarEstatusTicketOnCancelarModal;

            if (!IsPostBack)
            {
                if (int.Parse(Session["RolSeleccionado"].ToString()) != (int)BusinessVariables.EnumRoles.Agente)
                    Response.Redirect("~/Users/DashBoard.aspx");
                Session["EstatusSeleccionado"] = EstatusAbierto;
                ObtenerTicketsPage();
                LlenaCombos();
                AgenteMaster master = Master as AgenteMaster;
                if (master != null)
                {
                    master.CambiaTicket = false;
                }
            }
            //gvTickets.Rebind();
        }

        void UcCambiarEstatusAsignacionOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAsignacionCambio\");", true);
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

        void UcCambiarEstatusAsignacion_OnAceptarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalAsignacionCambio\");", true);
                ObtenerTicketsPage();
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

        void UcDetalleUsuario_OnAceptarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalDetalleUsuario\");", true);
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

        private void UcDetalleUsuario1OnOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalDetalleUsuario\");", true);
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

        void UcCambiarEstatusTicket_OnAceptarModal()
        {
            try
            {

                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalEstatusCambio\");", true);
                if (UcCambiarEstatusTicket.CerroTicket)
                {
                    string url = ResolveUrl("~/FrmEncuesta.aspx?IdTipoServicio=" + (int)BusinessVariables.EnumTipoArbol.SolicitarServicio + "IdTicket=" + hfTicketActivo.Value);
                    //string s = "window.open('" + url + "', 'popup_window', 'width=600,height=600,left=300,top=100,resizable=yes');";
                    //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptEncuesta", "OpenWindow(\"" + url + "\");", true);
                }
                hfTicketActivo.Value = string.Empty;
                ObtenerTicketsPage();

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
        void UcCambiarEstatusTicketOnCancelarModal()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "CierraPopup(\"#modalEstatusCambio\");", true);
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

        protected void btnRefresh_OnClick(object sender, EventArgs e)
        {
            try
            {
                ObtenerTicketsPage();
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
                AgenteMaster master = Master as AgenteMaster;
                if (master != null)
                {
                    master.AddTicketOpen(1, "prueba");
                    master.AddTicketOpen(2, "prueba");
                    master.AddTicketOpen(3, "prueba");
                    master.AddTicketOpen(4, "prueba");
                    master.AddTicketOpen(5, "prueba");
                    master.AddTicketOpen(6, "prueba");
                    master.AddTicketOpen(7, "prueba");
                    master.AddTicketOpen(8, "prueba");
                    master.AddTicketOpen(9, "prueba");
                    master.AddTicketOpen(11, "prueba");
                    master.AddTicketOpen(12, "prueba");
                    master.AddTicketOpen(13, "prueba");
                    master.AddTicketOpen(14, "prueba");
                    master.AddTicketOpen(15, "prueba");
                    master.AddTicketOpen(16, "prueba");
                    master.AddTicketOpen(17, "prueba");
                    master.AddTicketOpen(18, "prueba");
                    master.AddTicketOpen(19, "prueba");
                    master.AddTicketOpen(20, "prueba");
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

        protected void ddlGrupo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Metodos.LimpiarCombo(ddlAgente);
                if (ddlGrupo.SelectedIndex <= BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                    return;
                ddlAgente.DataSource = _servicioUsuarios.ObtenerUsuariosByGrupoAtencion(int.Parse(ddlGrupo.SelectedValue), true);
                ddlAgente.DataTextField = "NombreCompleto";
                ddlAgente.DataValueField = "Id";
                ddlAgente.DataBind();
                ObtenerTicketsPage();

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

        protected void ddlAgente_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerTicketsPage();
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

        protected void btnAutoasignar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (gvTickets.SelectedItems.Count <= 0)
                    throw new Exception("Seleccione un ticket");
                foreach (GridDataItem item in gvTickets.SelectedItems)
                {
                    int idTicket = int.Parse(item.GetDataKeyValue("NumeroTicket").ToString());
                    _servicioAtencionTicket.AutoAsignarTicket(idTicket, ((Usuario)Session["UserData"]).Id);
                }

                ObtenerTicketsPage();
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

        protected void btnAsignar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (gvTickets.SelectedItems.Count <= 0)
                    throw new Exception("Seleccione un ticket");
                foreach (GridDataItem item in gvTickets.SelectedItems)
                {
                    int idTicket = int.Parse(item.GetDataKeyValue("NumeroTicket").ToString());
                    var propietario = item["EsPropietario"].Text;
                    var gpoAsignado = item["IdGrupoAsignado"].Text;
                    var estatusasignacion = item["IdEstatusAsignacion"].Text;

                    ucCambiarEstatusAsignacion.IdEstatusAsignacionActual = Convert.ToInt32(estatusasignacion);
                    ucCambiarEstatusAsignacion.EsPropietario = Convert.ToBoolean(propietario);
                    ucCambiarEstatusAsignacion.IdTicket = Convert.ToInt32(idTicket);
                    ucCambiarEstatusAsignacion.IdGrupo = Convert.ToInt32(gpoAsignado);
                    ucCambiarEstatusAsignacion.IdUsuario = ((Usuario)Session["UserData"]).Id;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAsignacionCambio\");", true);
                }

                ObtenerTicketsPage();

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

        protected void btnEscalar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (gvTickets.SelectedItems.Count <= 0)
                    throw new Exception("Seleccione un ticket");
                foreach (GridDataItem item in gvTickets.SelectedItems)
                {
                    int idTicket = int.Parse(item.GetDataKeyValue("NumeroTicket").ToString());
                    var propietario = item["EsPropietario"].Text;
                    var gpoAsignado = item["IdGrupoAsignado"].Text;
                    var estatusasignacion = item["IdEstatusAsignacion"].Text;

                    ucCambiarEstatusAsignacion.IdEstatusAsignacionActual = Convert.ToInt32(estatusasignacion);
                    ucCambiarEstatusAsignacion.EsPropietario = Convert.ToBoolean(propietario);
                    ucCambiarEstatusAsignacion.IdTicket = Convert.ToInt32(idTicket);
                    ucCambiarEstatusAsignacion.IdGrupo = Convert.ToInt32(gpoAsignado);
                    ucCambiarEstatusAsignacion.IdUsuario = ((Usuario)Session["UserData"]).Id;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalAsignacionCambio\");", true);
                    break;
                }

                ObtenerTicketsPage();
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
        protected void btnCambiarEstatus_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (gvTickets.SelectedItems.Count <= 0)
                    throw new Exception("Seleccione un ticket");
                foreach (GridDataItem item in gvTickets.SelectedItems)
                {
                    int idTicket = int.Parse(item.GetDataKeyValue("NumeroTicket").ToString());
                    int estatusTicket = int.Parse(item["IdEstatusTicket"].Text);
                    hfTicketActivo.Value = idTicket.ToString();
                    UcCambiarEstatusTicket.EsPropietario = true;
                    UcCambiarEstatusTicket.IdTicket = idTicket;
                    UcCambiarEstatusTicket.IdEstatusActual = estatusTicket;
                    UcCambiarEstatusTicket.IdUsuario = ((Usuario)Session["UserData"]).Id;
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalEstatusCambio\");", true);
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

        protected void gvTickets_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            gvTickets.DataSource = Tickets;
        }

        protected void btnFiltro_OnClick(object sender, EventArgs e)
        {
            try
            {
                RadButton btn = (RadButton)sender;
                if (btn != null)
                {
                    switch (btn.CommandArgument)
                    {
                        case "Abierto":
                            EstatusSeleccionado = EstatusAbierto;
                            break;
                        case "Espera":
                            EstatusSeleccionado = EstatusEspera;
                            break;
                        case "SinAsignar":
                            EstatusSeleccionado = EstatusSinAsignar;
                            break;
                        case "Resuelto":
                            EstatusSeleccionado = EstatusResuletos;
                            break;
                        case "FueraSla":
                            EstatusSeleccionado = EstatusFueraSla;
                            break;
                        default:
                            EstatusSeleccionado = EstatusAbierto;
                            break;
                    }
                    ObtenerTicketsPage();
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

        protected void gvTickets_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "RowClick":
                        GridDataItem row = (GridDataItem)e.Item;
                        int idTicket = int.Parse(row.GetDataKeyValue("NumeroTicket").ToString());
                        bool asigna = bool.Parse(row["puedeasignar"].Text);
                        bool propietaro = bool.Parse(row["EsPropietario"].Text);
                        btnAutoasignar.Enabled = asigna;
                        btnAsignar.Enabled = asigna;
                        btnCambiarEstatus.Enabled = propietaro;
                        //AgenteMaster master = Master as AgenteMaster;
                        //if (master != null)
                        //{
                        //    master.AddTicketOpen(idTicket, row["Tipificacion"].Text);
                        //}
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

        protected void gvTickets_OnItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridFilteringItem)
                {
                    GridFilteringItem filteringItem = e.Item as GridFilteringItem;
                    TextBox box = filteringItem["TipoUsuario"].Controls[0] as TextBox;
                    if (box != null)
                        box.Width = Unit.Pixel(25);
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

        protected void RadComboBox1_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string filterValue = ((RadComboBox)sender).SelectedValue;
                gvTickets.EnableLinqExpressions = false;
                gvTickets.MasterTableView.FilterExpression = "([ImagenPrioridad] LIKE '%" + filterValue + "%') ";
                GridColumn column = gvTickets.MasterTableView.GetColumnSafe("ImagenPrioridad");
                column.CurrentFilterFunction = GridKnownFunction.Contains;
                gvTickets.MasterTableView.Rebind();
                gvTickets.EnableLinqExpressions = true;
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

        protected void lbntIdticket_OnClick(object sender, EventArgs e)
        {
            try
            {
                UcDetalleTicket.IdTicket = Convert.ToInt32(((LinkButton)sender).CommandArgument);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalDetalleTicket\");", true);
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

        protected void btnUsuario_OnClick(object sender, EventArgs e)
        {
            try
            {
                UcDetalleUsuario.FromModal = true;
                UcDetalleUsuario.IdUsuario = Convert.ToInt32(((LinkButton)sender).CommandArgument);
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "MostrarPopup(\"#modalDetalleUsuario\");", true);
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

        protected void btnTipificacion_OnClick(object sender, EventArgs e)
        {
            try
            {
                AgenteMaster master = Master as AgenteMaster;
                if (master != null)
                {
                    master.AddTicketOpen(Convert.ToInt32(((LinkButton)sender).CommandArgument), ((LinkButton)sender).Text);
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