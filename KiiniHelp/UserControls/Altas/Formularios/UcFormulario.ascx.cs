﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using KiiniHelp.ServiceArbolAcceso;
using KiiniHelp.ServiceMascaraAcceso;
using KiiniHelp.ServiceSistemaCatalogos;
using KiiniHelp.ServiceTicket;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using RepeatDirection = System.Web.UI.WebControls.RepeatDirection;

namespace KiiniHelp.UserControls.Altas.Formularios
{
    public partial class UcFormulario : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;
        public event DelegateTerminarModal OnTerminarModal;

        private readonly ServiceCatalogosClient _servicioCatalogos = new ServiceCatalogosClient();
        private readonly ServiceMascarasClient _servicioMascaras = new ServiceMascarasClient();
        private readonly ServiceTicketClient _servicioTicket = new ServiceTicketClient();
        private readonly ServiceArbolAccesoClient _servicioArbolAccesoClient = new ServiceArbolAccesoClient();
        private List<Control> _lstControles;
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
            ArbolAcceso arbol;
            if (Request.QueryString["IdArbol"] != null)
                arbol = _servicioArbolAccesoClient.ObtenerArbolAcceso(int.Parse(Request.QueryString["IdArbol"]));
            else
                arbol = (ArbolAcceso)Session["ArbolAcceso"];

            if (arbol != null)
            {

                int? idMascara = arbol.InventarioArbolAcceso.First().IdMascara;
                if (idMascara != null) IdMascara = (int)idMascara;
                Mascara mascara = _servicioMascaras.ObtenerMascaraCaptura(IdMascara);
                if (mascara != null)
                {
                    hfComandoInsertar.Value = mascara.ComandoInsertar;
                    hfComandoActualizar.Value = mascara.ComandoInsertar;
                    hfRandom.Value = mascara.Random.ToString();
                    PintaControles(mascara.CampoMascara);
                    Session["MascaraActiva"] = mascara;
                }
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

        public bool CampoRandom
        {
            get { return Convert.ToBoolean(hfRandom.Value); }
        }

        public int TicketGenerado
        {
            get { return int.Parse(hfTicketGenerado.Value); }
        }
        public string RandomGenerado
        {
            get { return hfRandomGenerado.Value; }
        }

        public bool Preview
        {
            get { return Convert.ToBoolean(hfPreview.Value); }
            set
            {
                hfPreview.Value = value.ToString();
                btnGuardar.Enabled = !value;
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
                    switch (campo.IdTipoCampoMascara)
                    {
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Texto:
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtSimple = (TextBox)divControles.FindControl(nombreControl);
                            if (txtSimple != null)
                            {
                                if (campo.Requerido)
                                    if (txtSimple.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                    else
                                    {
                                        if (txtSimple.Text.Trim().Length < campo.LongitudMinima)
                                            throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                        if (txtSimple.Text.Trim().Length > campo.LongitudMaxima)
                                            throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));
                                    }
                                if (!campo.Requerido && txtSimple.Text.Trim() != string.Empty)
                                {
                                    if (txtSimple.Text.Trim().Length < campo.LongitudMinima)
                                        throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                    if (txtSimple.Text.Trim().Length > campo.LongitudMaxima)
                                        throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));
                                }

                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.TextoMultiLinea:
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtMultilinea = (TextBox)divControles.FindControl(nombreControl);
                            if (txtMultilinea != null)
                            {
                                if (campo.Requerido)
                                    if (txtMultilinea.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                    else
                                    {
                                        if (txtMultilinea.Text.Trim().Length < campo.LongitudMinima)
                                            throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                        if (txtMultilinea.Text.Trim().Length > campo.LongitudMaxima)
                                            throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));
                                    }
                                if (!campo.Requerido && txtMultilinea.Text.Trim() != string.Empty)
                                {
                                    if (txtMultilinea.Text.Trim().Length < campo.LongitudMinima)
                                        throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                    if (txtMultilinea.Text.Trim().Length > campo.LongitudMaxima)
                                        throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));
                                }


                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.RadioBoton:
                            nombreControl = "lstRadio" + campo.NombreCampo;
                            RadioButtonList rbtnList = (RadioButtonList)divControles.FindControl(nombreControl);
                            if (rbtnList != null)
                            {
                                if (campo.Requerido)
                                    if (rbtnList.SelectedIndex < (BusinessVariables.ComboBoxCatalogo.IndexSeleccione))
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.ListaDepledable:
                            nombreControl = "ddl" + campo.NombreCampo;
                            DropDownList ddl = (DropDownList)divControles.FindControl(nombreControl);
                            if (ddl != null)
                            {
                                if (campo.Requerido)
                                    if (ddl.SelectedIndex == BusinessVariables.ComboBoxCatalogo.IndexSeleccione)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.CasillaDeVerificación:
                            nombreControl = "chklist" + campo.NombreCampo;
                            CheckBoxList chklist = (CheckBoxList)divControles.FindControl(nombreControl);
                            if (chklist != null)
                            {
                                if (campo.Requerido)
                                    if (!chklist.Items.Cast<ListItem>().Any(item => item.Selected))
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.NúmeroEntero:
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtEntero = (TextBox)divControles.FindControl(nombreControl);
                            if (txtEntero != null)
                            {
                                if (campo.Requerido)
                                    if (txtEntero.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                    else
                                    {
                                        if (int.Parse(txtEntero.Text.Trim()) < campo.ValorMinimo)
                                            throw new Exception(string.Format("Campo {0} debe se mayor o igual a {1}", campo.Descripcion, campo.ValorMinimo));
                                        if (int.Parse(txtEntero.Text.Trim()) > campo.ValorMaximo)
                                            throw new Exception(string.Format("Campo {0} debe se menor o igual a {1}", campo.Descripcion, campo.ValorMaximo));
                                    }
                                if (!campo.Requerido && txtEntero.Text.Trim() != string.Empty)
                                {
                                    if (int.Parse(txtEntero.Text.Trim()) < campo.ValorMinimo)
                                        throw new Exception(string.Format("Campo {0} debe se mayor o igual a {1}", campo.Descripcion, campo.ValorMinimo));
                                    if (int.Parse(txtEntero.Text.Trim()) > campo.ValorMaximo)
                                        throw new Exception(string.Format("Campo {0} debe se menor o igual a {1}", campo.Descripcion, campo.ValorMaximo));
                                }
                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.NúmeroDecimal:
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtDecimal = (TextBox)divControles.FindControl(nombreControl);
                            if (txtDecimal != null)
                            {
                                if (campo.Requerido)
                                    if (txtDecimal.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                    else
                                    {
                                        if (decimal.Parse(txtDecimal.Text.Trim().Replace('_', '0')) < campo.ValorMinimo)
                                            throw new Exception(string.Format("Campo {0} debe se mayor o igual a {1}", campo.Descripcion, campo.ValorMinimo));
                                        if (decimal.Parse(txtDecimal.Text.Trim().Replace('_', '0')) > campo.ValorMaximo)
                                            throw new Exception(string.Format("Campo {0} debe se menor o igual a {1}", campo.Descripcion, campo.ValorMaximo));
                                    }
                                if (!campo.Requerido && txtDecimal.Text.Trim() != string.Empty)
                                {
                                    if (decimal.Parse(txtDecimal.Text.Trim().Replace('_', '0')) < campo.ValorMinimo)
                                        throw new Exception(string.Format("Campo {0} debe se mayor o igual a {1}", campo.Descripcion, campo.ValorMinimo));
                                    if (decimal.Parse(txtDecimal.Text.Trim().Replace('_', '0')) > campo.ValorMaximo)
                                        throw new Exception(string.Format("Campo {0} debe se menor o igual a {1}", campo.Descripcion, campo.ValorMaximo));
                                }
                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Logico:

                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.SelecciónCascada:

                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Fecha:
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
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.FechaRango:
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtFechaInicio = (TextBox)divControles.FindControl(nombreControl + BusinessVariables.ParametrosMascaraCaptura.PrefijoFechaInicio);
                            TextBox txtFechaFin = (TextBox)divControles.FindControl(nombreControl + BusinessVariables.ParametrosMascaraCaptura.PrefijoFechaFin);
                            if (txtFechaInicio != null && txtFechaFin != null)
                            {
                                if (campo.Requerido)
                                {
                                    if (txtFechaInicio.Text.Trim() == String.Empty || txtFechaFin.Text.Trim() == string.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                    try
                                    {
                                        var dI = DateTime.Parse(txtFechaInicio.Text.Trim());
                                        var dF = DateTime.Parse(txtFechaFin.Text.Trim());
                                        if (dI > dF)
                                            throw new Exception(string.Format("Campo {0} no es un rango de fechas valido", campo.Descripcion));
                                    }
                                    catch
                                    {
                                        throw new Exception(string.Format("Campo {0} contiene una fecha no valida", campo.Descripcion));
                                    }
                                }
                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.ExpresiónRegular:
                            nombreControl = "txt" + campo.NombreCampo;
                            TextBox txtMascara = (TextBox)divControles.FindControl(nombreControl);
                            if (txtMascara != null)
                            {
                                if (campo.Requerido)
                                    if (txtMascara.Text.Trim() == String.Empty)
                                        throw new Exception(string.Format("Campo {0} es obligatorio", campo.Descripcion));
                                    else
                                    {
                                        if (txtMascara.Text.Trim().Length < campo.LongitudMinima)
                                            throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                        if (txtMascara.Text.Trim().Length > campo.LongitudMaxima)
                                            throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));
                                    }
                                if (!campo.Requerido && txtMascara.Text.Trim() != string.Empty)
                                {
                                    if (txtMascara.Text.Trim().Length < campo.LongitudMinima)
                                        throw new Exception(string.Format("Campo {0} debe tener al menos {1} caracteres", campo.Descripcion, campo.LongitudMinima));
                                    if (txtMascara.Text.Trim().Length > campo.LongitudMaxima)
                                        throw new Exception(string.Format("Campo {0} debe no puede tener mas de {1} caracteres", campo.Descripcion, campo.LongitudMaxima));
                                }

                            }
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.AdjuntarArchivo:
                            if (campo.Requerido)
                            {
                                if (Session["Files"] != null)
                                {
                                    if (!((List<string>)Session["Files"]).Any())
                                    {
                                        throw new Exception(string.Format("Campo {0} debe seleccionar un archivo", campo.Descripcion));
                                    }
                                }
                                else
                                    throw new Exception(string.Format("Campo {0} debe seleccionar un archivo", campo.Descripcion));
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

        public void ConfirmaArchivos(int idTicket)
        {
            try
            {

                for (int i = 0; i < ((List<string>)Session["Files"]).Count; i++)
                {
                    File.Move(BusinessVariables.Directorios.RepositorioTemporalMascara + ((List<string>)Session["Files"])[i], BusinessVariables.Directorios.RepositorioTemporalMascara + ((List<string>)Session["Files"])[i].Replace("ticketid", idTicket.ToString()));
                    ((List<string>)Session["Files"])[i] = ((List<string>)Session["Files"])[i].Replace("ticketid", idTicket.ToString());
                }
                if (Session["Files"] != null)
                {
                    BusinessFile.MoverTemporales(BusinessVariables.Directorios.RepositorioTemporalMascara, BusinessVariables.Directorios.RepositorioMascara, (List<string>)Session["Files"]);
                    Session["Files"] = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                    bool rango = false;
                    switch (campo.IdTipoCampoMascara)
                    {
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Texto:
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.TextoMultiLinea:
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.RadioBoton:
                            nombreControl = "lstRadio" + campo.NombreCampo;
                            campoTexto = false;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.ListaDepledable:
                            nombreControl = "ddl" + campo.NombreCampo;
                            campoTexto = false;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.CasillaDeVerificación:
                            nombreControl = "chklist" + campo.NombreCampo;
                            campoTexto = false;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.NúmeroEntero:
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.NúmeroDecimal:
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Logico:
                            nombreControl = "chk" + campo.NombreCampo;
                            campoTexto = false;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.SelecciónCascada:
                            nombreControl = null;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Fecha:
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.FechaRango:
                            nombreControl = "txt" + campo.NombreCampo;
                            rango = true;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.ExpresiónRegular:
                            nombreControl = "txt" + campo.NombreCampo;
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.AdjuntarArchivo:
                            nombreControl = "fu" + campo.NombreCampo;
                            campoTexto = false;
                            break;
                    }

                    if (campoTexto && nombreControl != null)
                    {
                        TextBox txt = (TextBox)divControles.FindControl(nombreControl);
                        if (txt != null || rango)
                        {
                            HelperCampoMascaraCaptura campoCapturado;
                            if (rango)
                            {
                                TextBox txtFechaInicio = (TextBox)divControles.FindControl(nombreControl + BusinessVariables.ParametrosMascaraCaptura.PrefijoFechaInicio);
                                TextBox txtFechaFin = (TextBox)divControles.FindControl(nombreControl + BusinessVariables.ParametrosMascaraCaptura.PrefijoFechaFin);
                                campoCapturado = new HelperCampoMascaraCaptura
                                {
                                    NombreCampo = campo.NombreCampo,
                                    Valor = Convert.ToDateTime(txtFechaInicio.Text.Trim().ToUpper()).ToString("yyyy-MM-dd") + "|" + Convert.ToDateTime(txtFechaFin.Text.Trim().ToUpper()).ToString("yyyy-MM-dd"),
                                };
                                lstCamposCapturados.Add(campoCapturado);
                            }
                            else
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
                                    case "FECHAINICIO":

                                        break;
                                    case "DECIMAL":
                                        campoCapturado = new HelperCampoMascaraCaptura
                                        {
                                            NombreCampo = campo.NombreCampo,
                                            Valor = campo.Requerido ? txt.Text.Trim().Replace('_', '0') : txt.Text.Trim() == string.Empty ? "0" : txt.Text.Trim().Replace('_', '0')
                                        };
                                        lstCamposCapturados.Add(campoCapturado);
                                        break;

                                    default:
                                        campoCapturado = new HelperCampoMascaraCaptura
                                        {
                                            NombreCampo = campo.NombreCampo,
                                            Valor = campo.Requerido ? txt.Text.Trim().ToUpper() : txt.Text.Trim() == string.Empty ? "0" : txt.Text.Trim().ToUpper()
                                        };
                                        lstCamposCapturados.Add(campoCapturado);
                                        break;
                                }

                        }
                    }
                    else if (!campoTexto)
                    {
                        switch (campo.IdTipoCampoMascara)
                        {
                            case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.RadioBoton:
                                RadioButtonList rbtnList = (RadioButtonList)divControles.FindControl(nombreControl);
                                if (rbtnList != null)
                                {
                                    HelperCampoMascaraCaptura campoCapturado = new HelperCampoMascaraCaptura
                                    {
                                        NombreCampo = campo.NombreCampo,
                                        Valor = rbtnList.SelectedValue
                                    };
                                    lstCamposCapturados.Add(campoCapturado);
                                }
                                break;
                            case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.ListaDepledable:
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
                            case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.CasillaDeVerificación:
                                CheckBoxList chkList = (CheckBoxList)divControles.FindControl(nombreControl);
                                if (chkList != null)
                                {
                                    string valores = chkList.Items.Cast<ListItem>().Where(item => item.Selected).Aggregate<ListItem, string>(null, (current, item) => current + (item.Value + "|"));
                                    if (valores != null)
                                    {
                                        valores = valores.TrimEnd('|');
                                        HelperCampoMascaraCaptura campoCapturado = new HelperCampoMascaraCaptura
                                        {
                                            NombreCampo = campo.NombreCampo,
                                            Valor = valores
                                        };
                                        lstCamposCapturados.Add(campoCapturado);
                                    }
                                    else
                                    {
                                        HelperCampoMascaraCaptura campoCapturado = new HelperCampoMascaraCaptura
                                        {
                                            NombreCampo = campo.NombreCampo,
                                            Valor = string.Empty
                                        };
                                        lstCamposCapturados.Add(campoCapturado);
                                    }
                                }
                                break;
                            case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Logico:
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
                            case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.AdjuntarArchivo:
                                AsyncFileUpload upload = (AsyncFileUpload)divControles.FindControl(nombreControl);
                                if (upload != null)
                                {
                                    HelperCampoMascaraCaptura campoCapturado = new HelperCampoMascaraCaptura
                                    {
                                        NombreCampo = campo.NombreCampo,
                                        Valor = Session[nombreControl] == null ? string.Empty : Session[nombreControl].ToString(),
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
                    HtmlGenericControl hr = new HtmlGenericControl("HR");
                    HtmlGenericControl createDiv = new HtmlGenericControl("DIV") { ID = "createDiv" + campo.NombreCampo };
                    createDiv.Attributes["class"] = "form-group clearfix";
                    //createDiv.InnerHtml = campo.Descripcion;
                    Label lbl = new Label { Text = campo.Descripcion, CssClass = "col-sm-12 control-label" };
                    switch (campo.TipoCampoMascara.Id)
                    {
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Texto:
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtSimple = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "form-control",

                            };
                            txtSimple.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            txtSimple.Attributes["MaxLength"] = campo.TipoCampoMascara.LongitudMaxima.ToString();
                            txtSimple.Attributes["placeholder"] = campo.Descripcion;
                            _lstControles.Add(txtSimple);
                            createDiv.Controls.Add(txtSimple);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.TextoMultiLinea:
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtMultilinea = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "form-control",
                                TextMode = TextBoxMode.MultiLine,
                                Rows = 10
                            };
                            txtMultilinea.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            txtMultilinea.Attributes["MaxLength"] = campo.LongitudMaxima.ToString();
                            txtMultilinea.Attributes["placeholder"] = campo.Descripcion;
                            _lstControles.Add(txtMultilinea);
                            createDiv.Controls.Add(txtMultilinea);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.RadioBoton:
                            lbl.Attributes["for"] = "lstRadio" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            RadioButtonList lstRadio = new RadioButtonList
                            {
                                SelectedValue = "0",
                                ID = "lstRadio" + campo.NombreCampo,
                                Text = campo.Descripcion,
                                RepeatColumns = 5,
                                RepeatDirection = RepeatDirection.Horizontal
                            };
                            if (campo.EsArchivo)
                            {
                                foreach (DataRow row in _servicioCatalogos.ObtenerRegistrosArchivosCatalogo(int.Parse(campo.IdCatalogo.ToString())).Rows)
                                {
                                    lstRadio.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
                                }
                            }
                            else
                            {
                                if (campo.IdCatalogo != null)
                                    foreach (CatalogoGenerico cat in _servicioMascaras.ObtenerCatalogoCampoMascara((int)campo.IdCatalogo, false, true))
                                    {
                                        lstRadio.Items.Add(new ListItem(cat.Descripcion, cat.Id.ToString()));
                                    }
                            }
                            createDiv.Controls.Add(lstRadio);
                            _lstControles.Add(lstRadio);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.ListaDepledable:
                            lbl.Attributes["for"] = "ddl" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            DropDownList ddlCatalogo = new DropDownList
                            {
                                SelectedValue = "0",
                                ID = "ddl" + campo.NombreCampo,
                                Text = campo.Descripcion,
                                CssClass = "col-sm-10 form-control"
                            };
                            if (campo.EsArchivo)
                            {
                                foreach (DataRow row in _servicioCatalogos.ObtenerRegistrosArchivosCatalogo(int.Parse(campo.IdCatalogo.ToString())).Rows)
                                {
                                    ddlCatalogo.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
                                }
                            }
                            else
                            {
                                if (campo.IdCatalogo != null)
                                    foreach (CatalogoGenerico cat in _servicioMascaras.ObtenerCatalogoCampoMascara((int)campo.IdCatalogo, true, true))
                                    {
                                        ddlCatalogo.Items.Add(new ListItem(cat.Descripcion, cat.Id.ToString()));
                                    }
                            }
                            createDiv.Controls.Add(ddlCatalogo);
                            _lstControles.Add(ddlCatalogo);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.CasillaDeVerificación:
                            lbl.Attributes["for"] = "chklist" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            CheckBoxList chklist = new CheckBoxList
                            {
                                SelectedValue = "0",
                                ID = "chklist" + campo.NombreCampo,
                                Text = campo.Descripcion,
                                RepeatColumns = 5,
                                RepeatDirection = RepeatDirection.Horizontal
                            };
                            if (campo.EsArchivo)
                            {
                                foreach (DataRow row in _servicioCatalogos.ObtenerRegistrosArchivosCatalogo(int.Parse(campo.IdCatalogo.ToString())).Rows)
                                {
                                    chklist.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
                                }
                            }
                            else
                            {
                                if (campo.IdCatalogo != null)
                                    foreach (CatalogoGenerico cat in _servicioMascaras.ObtenerCatalogoCampoMascara((int)campo.IdCatalogo, false, true))
                                    {
                                        chklist.Items.Add(new ListItem(cat.Descripcion, cat.Id.ToString()));
                                    }
                            }
                            createDiv.Controls.Add(chklist);
                            _lstControles.Add(chklist);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.NúmeroDecimal:
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtDecimal = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                Text = campo.Descripcion,
                                CssClass = "form-control"
                            };
                            txtDecimal.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            txtDecimal.Attributes["placeholder"] = campo.Descripcion;
                            txtDecimal.Attributes["type"] = "number";
                            txtDecimal.Attributes["step"] = "0.01";
                            txtDecimal.Attributes["min"] = campo.ValorMinimo.ToString();
                            txtDecimal.Attributes["max"] = campo.ValorMaximo.ToString();
                            txtDecimal.Attributes["for"] = "DECIMAL";
                            createDiv.Controls.Add(txtDecimal);
                            _lstControles.Add(txtDecimal);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.NúmeroEntero:
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtEntero = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                Text = campo.NombreCampo,
                                CssClass = "form-control"
                            };
                            txtEntero.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            txtEntero.Attributes["placeholder"] = campo.NombreCampo;
                            txtEntero.Attributes["type"] = "number";
                            txtEntero.Attributes["step"] = "1";
                            txtEntero.Attributes["min"] = campo.ValorMinimo.ToString();
                            txtEntero.Attributes["max"] = campo.ValorMaximo.ToString();
                            createDiv.Controls.Add(txtEntero);
                            _lstControles.Add(txtEntero);
                            break;




                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Fecha:
                            lbl.Attributes["for"] = "FECHA";
                            createDiv.Controls.Add(lbl);
                            TextBox txtFecha = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                CssClass = "form-control"
                            };
                            txtFecha.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            txtFecha.Attributes["placeholder"] = campo.Descripcion;
                            txtFecha.Attributes["for"] = "FECHA";
                            txtFecha.Attributes["type"] = "date";
                            txtFecha.Attributes["step"] = "1";
                            createDiv.Controls.Add(txtFecha);
                            _lstControles.Add(txtFecha);
                            break;

                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.FechaRango:
                            lbl.Attributes["for"] = "FECHAINICIO";
                            createDiv.Controls.Add(lbl);

                            HtmlGenericControl createDivGrupoFechas = new HtmlGenericControl("DIV");
                            createDivGrupoFechas.ID = "createDivGrupoFechas" + campo.NombreCampo;
                            createDivGrupoFechas.Attributes["class"] = "form-group";

                            Label lblDe = new Label { Text = "De:", CssClass = "" };
                            lblDe.Attributes["for"] = "FECHAINICIO";
                            createDivGrupoFechas.Controls.Add(lblDe);

                            TextBox txtFechaInicio = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo + BusinessVariables.ParametrosMascaraCaptura.PrefijoFechaInicio,
                                CssClass = "form-control"
                            };
                            txtFechaInicio.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            txtFechaInicio.Attributes["placeholder"] = campo.Descripcion;
                            txtFechaInicio.Attributes["for"] = "FECHAINICIO";
                            txtFechaInicio.Attributes["type"] = "date";
                            txtFechaInicio.Attributes["step"] = "1";
                            createDivGrupoFechas.Controls.Add(txtFechaInicio);


                            Label lblHasta = new Label { Text = "De:", CssClass = "" };
                            lblHasta.Attributes["for"] = "FECHAFIN";
                            createDivGrupoFechas.Controls.Add(lblHasta);
                            TextBox txtFechaFin = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo + BusinessVariables.ParametrosMascaraCaptura.PrefijoFechaFin,
                                CssClass = "form-control"
                            };
                            txtFechaFin.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            txtFechaFin.Attributes["placeholder"] = campo.Descripcion;
                            txtFechaFin.Attributes["for"] = "FECHAFIN";
                            txtFechaFin.Attributes["type"] = "date";
                            txtFechaFin.Attributes["step"] = "1";
                            createDivGrupoFechas.Controls.Add(txtFechaFin);

                            HtmlGenericControl createDivFormFechas = new HtmlGenericControl("DIV");
                            createDivFormFechas.Attributes["class"] = "form-inline";
                            createDivFormFechas.Controls.Add(createDivGrupoFechas);
                            createDiv.Controls.Add(createDivFormFechas);

                            _lstControles.Add(txtFechaInicio);
                            _lstControles.Add(txtFechaFin);
                            break;

                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.Logico:
                            CheckBox chk = new CheckBox { ID = "chk" + campo.NombreCampo, Text = campo.Descripcion, ViewStateMode = ViewStateMode.Inherit };
                            _lstControles.Add(chk);
                            createDiv.Controls.Add(chk);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.ExpresiónRegular:
                            lbl.Attributes["for"] = "txt" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            TextBox txtMascara = new TextBox
                            {
                                ID = "txt" + campo.NombreCampo,
                                Text = campo.Descripcion,
                                CssClass = "form-control"
                            };
                            //txtMascara.Attributes["placeholder"] = campo.Descripcion;
                            txtMascara.Attributes["max"] = campo.ValorMaximo.ToString();
                            txtMascara.Attributes["for"] = "txt" + campo.Descripcion.Replace(" ", string.Empty);
                            MaskedEditExtender meeMascara = new MaskedEditExtender
                            {
                                ID = "mee" + campo.NombreCampo,
                                TargetControlID = txtMascara.ID,
                                InputDirection = MaskedEditInputDirection.LeftToRight,
                                Mask = campo.MascaraDetalle,
                                MaskType = MaskedEditType.Date,
                                AcceptAMPM = false,
                                AcceptNegative = MaskedEditShowSymbol.None,
                            };
                            txtMascara.Attributes.Add("onkeydown", "return (event.keyCode!=13 && event.keyCode!=27);");
                            createDiv.Controls.Add(meeMascara);
                            createDiv.Controls.Add(txtMascara);
                            _lstControles.Add(txtMascara);
                            break;
                        case (int)BusinessVariables.EnumeradoresKiiniNet.EnumTiposCampo.AdjuntarArchivo:
                            lbl.Attributes["for"] = "fu" + campo.NombreCampo;
                            createDiv.Controls.Add(lbl);
                            AsyncFileUpload asyncFileUpload = new AsyncFileUpload
                            {
                                ID = "fu" + campo.NombreCampo,
                                PersistFile = true,
                                UploaderStyle = AsyncFileUploaderStyle.Traditional,

                            };
                            asyncFileUpload.Attributes["style"] = "margin-top: 25px";
                            asyncFileUpload.UploadedComplete += asyncFileUpload_UploadedComplete;
                            createDiv.Controls.Add(asyncFileUpload);
                            _lstControles.Add(asyncFileUpload);
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

        void asyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            try
            {

                List<string> lstArchivo = Session["Files"] == null ? new List<string>() : (List<string>)Session["Files"];
                if (lstArchivo.Any(archivosCargados => archivosCargados.Split('_')[0] == e.FileName))
                    return;
                string extension = Path.GetExtension(e.FileName);
                if (extension == null) return;
                string filename = string.Format("{0}_{1}_{2}{3}{4}{5}{6}{7}{8}", e.FileName.Replace(extension, string.Empty), "ticketid", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, extension);
                AsyncFileUpload uploadControl = (AsyncFileUpload)sender;
                if (!Directory.Exists(BusinessVariables.Directorios.RepositorioTemporalMascara))
                    Directory.CreateDirectory(BusinessVariables.Directorios.RepositorioTemporalMascara);
                uploadControl.SaveAs(BusinessVariables.Directorios.RepositorioTemporalMascara + filename);
                Session[uploadControl.ID] = filename;
                lstArchivo.Add(filename);
                Session["Files"] = lstArchivo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
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
        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {

                //TODO: Cambiar id arbol por parametro
                List<HelperCampoMascaraCaptura> capturaMascara = ObtenerCapturaMascara();
                KiiniNet.Entities.Operacion.Tickets.Ticket result = _servicioTicket.CrearTicket(((Usuario)Session["UserData"]).Id, ((Usuario)Session["UserData"]).Id, ((ArbolAcceso)Session["ArbolAcceso"]).Id, capturaMascara, (int)BusinessVariables.EnumeradoresKiiniNet.EnumCanal.Portal, CampoRandom, true, false);
                lblNoTicket.Text = result.Id.ToString();
                if (CampoRandom)
                    lblRandom.Text = result.ClaveRegistro;
                if (Session["Files"] != null)
                    ConfirmaArchivos(result.Id);
                if (OnAceptarModal != null)
                    OnAceptarModal();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptOpen", "MostrarPopup(\"#modalExitoTicket\");", true);
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
        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (OnCancelarModal != null)
                    OnCancelarModal();
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
        protected void btnCerrarTicket_OnClick(object sender, EventArgs e)
        {
            try
            {
                //ucTicketPortal.Limpiar();
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptClose", "CierraPopup(\"#modal-new-ticket\");", true);
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