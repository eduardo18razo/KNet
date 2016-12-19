using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceMascaraAcceso;
using KiiniHelp.Test;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Helper;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Detalles
{
    public partial class UcDetalleMascaraCaptura : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        private readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    Control myControl = GetPostBackControl(Page);

        //    if ((myControl != null))
        //    {
        //        if ((myControl.ClientID == "btnAddTextBox"))
        //        {

        //        }
        //    }
        //}
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    _lstControles = new List<Control>();
        //    int? idMascara = ((FrmTest)Page).IdMascara;
        //    if (idMascara != null) IdMascara = (int)idMascara;
        //    int? idticket = ((FrmTest)Page).IdTicket;
        //    if (idticket != null) IdTicket = (int)idticket;
        //    Mascara mascara = _servicioMascaras.ObtenerMascaraCaptura(IdMascara);
        //    if (mascara != null)
        //    {
        //        lblDescripcionMascara.Text = mascara.Descripcion;
        //        PintaControles(mascara.CampoMascara, _servicioMascaras.ObtenerDatosMascara(IdMascara, IdTicket));
        //    }
        //}
        public void CargarDatos()
        {
            try
            {
                Mascara mascara = _servicioMascaras.ObtenerMascaraCapturaByIdTicket(IdTicket);
                if (mascara != null)
                {
                    lblDescripcionMascara.Text = mascara.Descripcion;
                    PintaControles(mascara.CampoMascara, _servicioMascaras.ObtenerDatosMascara(mascara.Id, IdTicket));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Control GetPostBackControl(Page thePage)
        {
            Control myControl = null;
            string ctrlName = thePage.Request.Params.Get("__EVENTTARGET");
            if (((ctrlName != null) & (ctrlName != string.Empty)))
            {
                myControl = thePage.FindControl(ctrlName);
            }
            else
            {
                foreach (string item in thePage.Request.Form)
                {
                    Control c = thePage.FindControl(item);
                    if (((c) is Button))
                    {
                        myControl = c;
                    }
                }

            }
            return myControl;
        }


        public int IdTicket
        {
            get { return Convert.ToInt32(hfIdTicket.Value); }
            set
            {
                hfIdTicket.Value = value.ToString();
                CargarDatos();
            }
        }

        public void PintaControles(List<CampoMascara> lstControles, List<HelperMascaraData> datosMascara)
        {
            try
            {
                divControles.Controls.Clear();
                foreach (CampoMascara campo in lstControles)
                {
                    HtmlGenericControl createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + campo.NombreCampo };
                    createDiv.Attributes["class"] = "form-group";
                    //createDiv.InnerHtml = campo.Descripcion;
                    Label lbl = new Label { Text = campo.Descripcion, CssClass = "col-sm-2 control-label" };
                    TextBox txtAlfanumerico;
                    switch (campo.TipoCampoMascara.Descripcion)
                    {
                        case "ALFANUMERICO":
                        case "CATALOGO":
                        case "DECIMAL":
                        case "ENTERO":
                        case "FECHA":
                        case "MASCARA":
                        case "HORA":

                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            txtAlfanumerico = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "col-sm-6 form-label",
                                Text = datosMascara.Single(s => s.Campo == campo.NombreCampo).Value
                            };
                            txtAlfanumerico.Style.Add("margin-left", "10px");
                            createDiv.Controls.Add(txtAlfanumerico);
                            break;
                        case "MONEDA":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            txtAlfanumerico = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "col-sm-6 form-label",
                                Text = campo.SimboloMoneda + " " + datosMascara.Single(s => s.Campo == campo.NombreCampo).Value
                            };
                            txtAlfanumerico.Style.Add("margin-left", "10px");
                            createDiv.Controls.Add(txtAlfanumerico);
                            break;
                        case "SI/NO":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            txtAlfanumerico = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "col-sm-6 form-label",
                                Text = Convert.ToBoolean(datosMascara.Single(s => s.Campo == campo.NombreCampo).Value) ? "SI" : "NO"
                            };
                            txtAlfanumerico.Style.Add("margin-left", "10px");
                            createDiv.Controls.Add(txtAlfanumerico);
                            break;
                        case "CARGA DE ARCHIVO":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            string archivo = datosMascara.Single(s => s.Campo == campo.NombreCampo).Value;
                            HyperLink lk = new HyperLink();
                            if (archivo != string.Empty)
                            {
                                lk.Text = archivo;
                                lk.NavigateUrl = ResolveUrl(string.Format("~/Downloads/FrmDownloads.aspx?file={0}", BusinessVariables.Directorios.RepositorioMascara + "~" + archivo));
                                lk.Style.Add("margin-left", "10px");
                                createDiv.Controls.Add(lk);
                            }
                            break;
                    }

                    divControles.Controls.Add(createDiv);
                }
                upMascara.Update();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Response.Clear();
                //Response.ContentType = "text/csv";
                //Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", ""));
                //Response.WriteFile("ruta archivo");
                //Response.End();
            }
        }


    }
}