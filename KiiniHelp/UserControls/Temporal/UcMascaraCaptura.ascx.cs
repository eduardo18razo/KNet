using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using KiiniHelp.ServiceMascaraAcceso;
using KiiniHelp.Ticket;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Helper;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Temporal
{
    public partial class UcMascaraCaptura : UserControl
    {
        readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        private List<Control> _lstControles;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Control myControl = GetPostBackControl(Page);

            if ((myControl != null))
            {
                if ((myControl.ClientID == "btnAddTextBox"))
                {

                }
            }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _lstControles = new List<Control>();
            int? idMascara = ((FrmTicket)Page).IdMascara;
            if (idMascara != null) IdMascara = (int)idMascara;
            Mascara mascara = _servicioMascaras.ObtenerMascaraCaptura(IdMascara);
            if (mascara != null)
            {
                hfComandoInsertar.Value = mascara.ComandoInsertar;
                hfComandoActualizar.Value = mascara.ComandoInsertar;
                lblDescripcionMascara.Text = mascara.Descripcion;
                PintaControles(mascara.CampoMascara);
                Session["MascaraActiva"] = mascara;
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

        public int IdMascara
        {
            get { return Convert.ToInt32(hfIdMascara.Value); }
            set { hfIdMascara.Value = value.ToString(); }
        }

        public string ComandoInsertar
        {
            get { return hfComandoInsertar.Value; }
        }

        public string ComandoActualizar
        {
            get { return hfComandoActualizar.Value; }
        }

        public List<HelperCampoMascaraCaptura> ObtenerCapturaMascara()
        {
            List<HelperCampoMascaraCaptura> lstCamposCapturados;
            try
            {
                ValidaMascaraCaptura();
                Mascara mascara = (Mascara)Session["MascaraActiva"];
                string nombreControl = null;
                lstCamposCapturados = new List<HelperCampoMascaraCaptura>();
                foreach (CampoMascara campo in mascara.CampoMascara)
                {
                    bool campoTexto = true;
                    switch (campo.TipoCampoMascara.Descripcion)
                    {
                        case "ALFANUMERICO":
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case "DECIMAL":
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case "ENTERO":
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case "FECHA":
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case "HORA":
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case "MONEDA":
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case "CATALOGO":
                            nombreControl = "ddl" + campo.NombreCampo;
                            campoTexto = false;
                            break;
                        case "SI/NO":
                            nombreControl = "chk" + campo.NombreCampo;
                            campoTexto = false;
                            break;
                    }

                    if (campoTexto && nombreControl != null)
                    {
                        TextBox txt = (TextBox)divControles.FindControl(nombreControl);
                        if (txt != null)
                        {
                            HelperCampoMascaraCaptura campoCapturado = new HelperCampoMascaraCaptura
                            {
                                NombreCampo = campo.NombreCampo,
                                Valor = txt.Text.Trim().ToUpper()
                            };
                            lstCamposCapturados.Add(campoCapturado);
                        }
                    }
                    else if (!campoTexto)
                    {
                        switch (campo.TipoCampoMascara.Descripcion)
                        {
                            case "CATALOGO":
                                DropDownList ddl = (DropDownList)divControles.FindControl(nombreControl);
                                if (ddl != null)
                                {
                                    HelperCampoMascaraCaptura campoCapturado = new HelperCampoMascaraCaptura
                                    {
                                        NombreCampo = campo.NombreCampo,
                                        Valor = ddl.SelectedValue
                                    };
                                    lstCamposCapturados.Add(campoCapturado);
                                }
                                break;
                            case "SI/NO":
                                CheckBox chk = (CheckBox)divControles.FindControl(nombreControl);
                                if (chk != null)
                                {
                                    HelperCampoMascaraCaptura campoCapturado = new HelperCampoMascaraCaptura
                                    {
                                        NombreCampo = campo.NombreCampo,
                                        Valor = chk.Checked.ToString()
                                    };
                                    lstCamposCapturados.Add(campoCapturado);
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lstCamposCapturados;
        }

        public void PintaControles(List<CampoMascara> lstControles)
        {
            try
            {

                foreach (CampoMascara campo in lstControles)
                {
                    HtmlGenericControl createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + campo.NombreCampo };
                    createDiv.Attributes["class"] = "form-group";
                    //createDiv.InnerHtml = campo.Descripcion;
                    Label lbl = new Label { Text = campo.Descripcion, CssClass = "control-label" };
                    switch (campo.TipoCampoMascara.Descripcion)
                    {
                        case "ALFANUMERICO":

                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtAlfanumerico = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "form-control",

                            };
                            txtAlfanumerico.Attributes["MaxLength"] = campo.LongitudMaxima.ToString();
                            txtAlfanumerico.Attributes["placeholder"] = campo.Descripcion;
                            _lstControles.Add(txtAlfanumerico);
                            createDiv.Controls.Add(txtAlfanumerico);
                            break;
                        case "CATALOGO":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            DropDownList ddlCatalogo = new DropDownList
                            {
                                ID = "ddl" + campo.NombreCampo,
                                Text = campo.Descripcion,
                                CssClass = "DropSelect"
                            };
                            foreach (BusinessMascarasCatalogoGenerico cat in _servicioMascaras.ObtenerCatalogoCampoMascara(campo.Catalogos.Tabla))
                            {
                                ddlCatalogo.Items.Add(new ListItem(cat.Descripcion, cat.Id.ToString()));
                            }
                            createDiv.Controls.Add(ddlCatalogo);
                            _lstControles.Add(ddlCatalogo);
                            break;
                        case "DECIMAL":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtDecimal = new TextBox
                            {
                                ID = "txt" + campo.Descripcion.Replace(" ", string.Empty),
                                Text = campo.Descripcion,
                                CssClass = "form-control"
                            };
                            txtDecimal.Attributes["placeholder"] = campo.Descripcion;
                            txtDecimal.Attributes["max"] = campo.ValorMaximo.ToString();
                            MaskedEditExtender meeDecimal = new MaskedEditExtender
                            {
                                ID = "mee" + campo.NombreCampo,
                                TargetControlID = txtDecimal.ID,
                                InputDirection = MaskedEditInputDirection.LeftToRight,
                                Mask = "999999.99",
                                MaskType = MaskedEditType.Date,
                                AcceptAMPM = false,
                                AcceptNegative = MaskedEditShowSymbol.None,
                                ClearTextOnInvalid = true,
                                CultureDecimalPlaceholder = "000000.00"
                            };
                            createDiv.Controls.Add(meeDecimal);
                            createDiv.Controls.Add(txtDecimal);
                            _lstControles.Add(txtDecimal);
                            break;
                        case "ENTERO":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtEntero = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                Text = campo.Descripcion,
                                CssClass = "form-control"
                            };
                            txtEntero.Attributes["placeholder"] = campo.Descripcion;
                            txtEntero.Attributes["type"] = "number";
                            txtEntero.Attributes["min"] = "1";
                            txtEntero.Attributes["max"] = campo.ValorMaximo.ToString();
                            createDiv.Controls.Add(txtEntero);
                            _lstControles.Add(txtEntero);
                            break;
                        case "FECHA":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtFecha = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "form-control"
                            };
                            txtFecha.Attributes["placeholder"] = campo.Descripcion;
                            MaskedEditExtender meeFecha = new MaskedEditExtender
                            {
                                ID = "mee" + campo.NombreCampo,
                                TargetControlID = txtFecha.ID,
                                InputDirection = MaskedEditInputDirection.LeftToRight,
                                Mask = "99/99/9999",
                                MaskType = MaskedEditType.Date,
                                AcceptAMPM = false
                            };
                            createDiv.Controls.Add(txtFecha);
                            createDiv.Controls.Add(meeFecha);
                            _lstControles.Add(txtFecha);
                            break;
                        case "HORA":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtHora = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "form-control"
                            };
                            txtHora.Attributes["placeholder"] = campo.Descripcion;
                            MaskedEditExtender meeHora = new MaskedEditExtender
                            {
                                ID = "mee" + campo.NombreCampo,
                                TargetControlID = txtHora.ID,
                                InputDirection = MaskedEditInputDirection.RightToLeft,
                                Mask = "23:59:59",
                                MaskType = MaskedEditType.Time,
                                AcceptAMPM = false
                            };
                            createDiv.Controls.Add(meeHora);
                            createDiv.Controls.Add(txtHora);
                            _lstControles.Add(txtHora);
                            break;
                        case "MONEDA":
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtMoneda = new TextBox
                            {
                                ID = "txt" + campo.Descripcion.Replace(" ", string.Empty),
                                CssClass = "form-control"
                            };
                            txtMoneda.Attributes["placeholder"] = campo.Descripcion;
                            _lstControles.Add(txtMoneda);
                            createDiv.Controls.Add(txtMoneda);
                            break;
                        case "SI/NO":
                            CheckBox chk = new CheckBox { ID = "chk" + campo.NombreCampo, Text = campo.Descripcion, ViewStateMode = ViewStateMode.Inherit };
                            _lstControles.Add(chk);
                            createDiv.Controls.Add(chk);
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


            }
        }

        public void ValidaMascaraCaptura()
        {
            try
            {
                Mascara mascara = (Mascara)Session["MascaraActiva"];
                foreach (CampoMascara campo in mascara.CampoMascara)
                {
                    string nombreControl;
                    switch (campo.TipoCampoMascara.Descripcion)
                    {
                        case "ALFANUMERICO":
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtAlfanumerico = (TextBox)divControles.FindControl(nombreControl);
                            if (txtAlfanumerico != null)
                            {
                                if (campo.Requerido)
                                    if (txtAlfanumerico.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                if (txtAlfanumerico.Text.Trim().Length < campo.LongitudMinima)
                                    throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                if (txtAlfanumerico.Text.Trim().Length > campo.LongitudMaxima)
                                    throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));

                            }
                            break;
                        case "DECIMAL":
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtDecimal = (TextBox)divControles.FindControl(nombreControl);
                            if (txtDecimal != null)
                            {
                                if (campo.Requerido)
                                    if (txtDecimal.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                //TODO: AGREGAR VALOR MINIMO A ESQUEMA
                                //if (decimal.Parse(txtDecimal.Text.Trim()) < campo.LongitudMinima)
                                //    throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                if (decimal.Parse(txtDecimal.Text.Trim()) > campo.ValorMaximo)
                                    throw new Exception(string.Format("Campo {0} debe se menor o igual a {1}", campo.Descripcion, campo.ValorMaximo));

                            }
                            break;
                        case "ENTERO":
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtEntero = (TextBox)divControles.FindControl(nombreControl);
                            if (txtEntero != null)
                            {
                                if (campo.Requerido)
                                    if (txtEntero.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                //TODO: AGREGAR VALOR MINIMO A ESQUEMA
                                //if (txtEntero.Text.Trim().Length < campo.LongitudMinima)
                                //    throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                if (int.Parse(txtEntero.Text.Trim()) > campo.ValorMaximo)
                                    throw new Exception(string.Format("Campo {0} debe se menor o igual a {1}", campo.Descripcion, campo.ValorMaximo));

                            }
                            break;
                        case "FECHA":
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtFecha = (TextBox)divControles.FindControl(nombreControl);
                            if (txtFecha != null)
                            {
                                try
                                {
                                    DateTime.Parse(txtFecha.Text.Trim());
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(string.Format("Campo {0} contiene una fecha no valida", campo.Descripcion));
                                }
                                if (campo.Requerido)
                                    if (txtFecha.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                            }
                            break;
                        case "HORA":
                            nombreControl = "txt" + campo.NombreCampo;

                            DateTime outTime;
                            //return ;
                            TextBox txtHora = (TextBox)divControles.FindControl(nombreControl);
                            if (txtHora != null)
                            {
                                try
                                {
                                    DateTime.TryParseExact(txtHora.Text.Trim(), "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(string.Format("Campo {0} contiene una hora no valida", campo.Descripcion));
                                }
                                if (campo.Requerido)
                                    if (txtHora.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                            }
                            break;
                        case "MONEDA":
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtMoneda = (TextBox)divControles.FindControl(nombreControl);
                            if (txtMoneda != null)
                            {
                                if (campo.Requerido)
                                    if (txtMoneda.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                //TODO: AGREGAR VALOR MINIMO A ESQUEMA
                                //if (decimal.Parse(txtDecimal.Text.Trim()) < campo.LongitudMinima)
                                //    throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                if (decimal.Parse(txtMoneda.Text.Trim()) > campo.LongitudMaxima)
                                    throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));

                            }
                            break;
                        case "CATALOGO":
                            nombreControl = "ddl" + campo.NombreCampo;
                            DropDownList ddl = (DropDownList)divControles.FindControl(nombreControl);
                            if (ddl != null)
                            {
                                if (campo.Requerido)
                                    if (ddl.SelectedIndex == BusinessVariables.ComboBoxCatalogo.Index)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}