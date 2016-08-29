using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using KiiniHelp.ServiceEncuesta;
using KiiniHelp.Test;
using KiiniHelp.Users.Operacion;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Temporal
{
    public partial class UcEncuestaCaptura : UserControl, IControllerModal
    {
        readonly ServiceEncuestaClient _servicioEncuesta = new ServiceEncuestaClient();
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
            int? idEncuesta = ((FrmEncuesta)Page).IdTicket;
            if (idEncuesta != null) IdTicket = (int)idEncuesta;
            Encuesta encuesta = _servicioEncuesta.ObtenerEncuestaByIdTicket(IdTicket);
            if (encuesta != null)
            {
                lblDescripcionMascara.Text = encuesta.Descripcion;
                PintaControles(encuesta.EncuestaPregunta, encuesta.IdTipoEncuesta);
                Session["MascaraActiva"] = encuesta;
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
            get { return Convert.ToInt32(hfIdEncuesta.Value); }
            set { hfIdEncuesta.Value = value.ToString(); }
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
                            HelperCampoMascaraCaptura campoCapturado;
                            switch (txt.Attributes["for"])
                            {
                                case "FECHA":
                                    campoCapturado = new HelperCampoMascaraCaptura
                                    {
                                        NombreCampo = campo.NombreCampo,
                                        Valor = Convert.ToDateTime(txt.Text.Trim().ToUpper()).ToString("yyyy-MM-dd"),
                                    };
                                    lstCamposCapturados.Add(campoCapturado);
                                    break;
                                    lstCamposCapturados.Add(campoCapturado);
                                    break;
                                default:
                                    campoCapturado = new HelperCampoMascaraCaptura
                                    {
                                        NombreCampo = campo.NombreCampo,
                                        Valor = txt.Text.Trim().ToUpper()
                                    };
                                    lstCamposCapturados.Add(campoCapturado);
                                    break;
                            }

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

        public void PintaControles(List<EncuestaPregunta> lstControles, int tipoEncuesta)
        {
            try
            {
                switch (tipoEncuesta)
                {
                    case (int)BusinessVariables.EnumTipoEncuesta.Logica:
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.Calificacion:
                        foreach (EncuestaPregunta pregunta in lstControles)
                        {
                            HtmlGenericControl createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + pregunta.Pregunta };
                            createDiv.Attributes["class"] = "form-group";
                            Label lbl = new Label { Text = pregunta.Pregunta, CssClass = "control-label" };
                            createDiv.Controls.Add(lbl);
                            divControles.Controls.Add(createDiv);
                            createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + pregunta.Pregunta };
                            createDiv.Attributes["class"] = "form-group";
                            for (int i = 0; i < 10; i++)
                            {

                                RadioButton rb = new RadioButton();
                                rb.Text = i.ToString();
                                rb.GroupName = "EncuestaCalificacion" + pregunta.Id;
                                rb.Style.Add("padding", "10px");
                                createDiv.Controls.Add(rb);
                            }
                            divControles.Controls.Add(createDiv);
                        }
                        break;
                    case (int)BusinessVariables.EnumTipoEncuesta.OpcionMultiple:
                        break;
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
                                    var d = DateTime.Parse(txtFecha.Text.Trim());
                                }
                                catch
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
                                catch
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

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnAceptarModal != null)
                    OnAceptarModal();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnCancelarModal != null)
                    OnCancelarModal();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
    }
}