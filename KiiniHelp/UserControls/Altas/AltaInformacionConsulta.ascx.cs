using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI;
using AjaxControlToolkit;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceInformacionConsulta;
using KiiniHelp.ServiceSistemaTipoDocumento;
using KiiniHelp.ServiceSistemaTipoInformacionConsulta;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;
using Microsoft.Office.Interop.Word;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaInformacionConsulta : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        private readonly ServiceTipoInfConsultaClient _servicioCatalogosSistema = new ServiceTipoInfConsultaClient();
        private readonly ServiceTipoDocumentoClient _servicioSistemaTipoDocumento = new ServiceTipoDocumentoClient();

        private readonly ServiceInformacionConsultaClient _servicioInformacionConsulta =
            new ServiceInformacionConsultaClient();

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

        private void LlenaCombos()
        {
            try
            {
                Metodos.LlenaComboCatalogo(ddlTipoInformacion,
                    _servicioCatalogosSistema.ObtenerTipoInformacionConsulta(true));
                Metodos.LlenaComboCatalogo(ddlTipoDocumento, _servicioSistemaTipoDocumento.ObtenerTipoDocumentos(true));
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

        public void ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta tipoInformacion)
        {
            try
            {
                if (txtDescripcion.Text.Trim() == string.Empty)
                    throw new Exception("Debe especificar una descripción");
                switch (tipoInformacion)
                {
                    case BusinessVariables.EnumTiposInformacionConsulta.Texto:
                        if (txtEditor.Text.Trim() == string.Empty)
                            throw new Exception("Debe especificar un contenido");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        //if (!fuFile.HasFile)
                        //    throw new Exception("Debe especificar un documento");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                        if (txtDescripcionUrl.Text.Trim() == string.Empty)
                            throw new Exception("Debe especificar una url de pagina");
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            try
            {
                ddlTipoInformacion.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                ddlTipoInformacion_OnSelectedIndexChanged(ddlTipoInformacion, null);
                ddlTipoDocumento.SelectedIndex = BusinessVariables.ComboBoxCatalogo.Index;
                txtDescripcion.Text = String.Empty;
                txtEditor.Text = string.Empty;
                txtDescripcionUrl.Text = string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EsAlta
        {
            get { return Convert.ToBoolean(hfEsAlta.Value); }
            set { hfEsAlta.Value = value.ToString(); }
        }

        public int IdInformacionConsulta
        {
            get { return Convert.ToInt32(hfIdInformacionConsulta.Value); }
            set
            {
                InformacionConsulta info = _servicioInformacionConsulta.ObtenerInformacionConsultaById(value);
                if (info == null) throw new Exception("Error al obtener informacion");
                txtDescripcion.Text = info.Descripcion;
                ddlTipoInformacion.SelectedValue = info.IdTipoInfConsulta.ToString();
                ddlTipoInformacion_OnSelectedIndexChanged(ddlTipoInformacion, null);
                switch (info.IdTipoInfConsulta)
                {
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Texto:
                        txtEditor.Text = info.InformacionConsultaDatos.First().Descripcion;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:

                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                        txtDescripcionUrl.Text = info.InformacionConsultaDatos.First().Descripcion;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Servicio:
                        break;
                }
                hfIdInformacionConsulta.Value = value.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AlertaGeneral = new List<string>();
                if (!IsPostBack)
                {
                    LlenaCombos();
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

        protected void ddlTipoInformacion_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(ddlTipoInformacion.SelectedValue))
                {
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Texto:
                        divPropietrario.Visible = true;
                        divUploadDocumento.Visible = false;
                        divDocumento.Visible = false;
                        divUrl.Visible = false;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        divPropietrario.Visible = false;
                        divDocumento.Visible = true;
                        divUrl.Visible = false;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                        divPropietrario.Visible = false;
                        divDocumento.Visible = false;
                        divUploadDocumento.Visible = false;
                        divUrl.Visible = true;
                        break;
                    default:
                        divPropietrario.Visible = false;
                        divDocumento.Visible = false;
                        divUploadDocumento.Visible = false;
                        divUrl.Visible = false;
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
                AlertaGeneral = _lstError;
            }
        }

        protected void afuArchivo_OnUploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            try
            {
                TipoDocumento tipoDocto =
                    _servicioSistemaTipoDocumento.ObtenerTiposDocumentoId(int.Parse(ddlTipoDocumento.SelectedValue));
                bool fileOk = false;
                var path = Server.MapPath(ConfigurationManager.AppSettings["PathInformacionConsultaOriginal"]);
                if (afuArchivo.HasFile)
                {
                    string extension = Path.GetExtension(afuArchivo.FileName);
                    if (extension != null)
                    {
                        var fileExtension = extension.ToLower();
                        string[] allowedExtensions = tipoDocto.Extension.Split(',');
                        foreach (string t in allowedExtensions.Where(t => fileExtension == t))
                        {
                            fileOk = true;
                        }
                    }
                }

                if (fileOk)
                {
                    try
                    {
                        hfFileName.Value = afuArchivo.FileName;
                        afuArchivo.PostedFile.SaveAs(path + afuArchivo.FileName);
                        switch (int.Parse(ddlTipoDocumento.SelectedValue))
                        {
                            case (int)BusinessVariables.EnumTiposDocumento.Word:
                                ConvertirWord(afuArchivo.FileName);
                                break;
                            case (int)BusinessVariables.EnumTiposDocumento.PowerPoint:
                                //ConvertirExcel(afuArchivo.FileName);
                                break;
                            case (int)BusinessVariables.EnumTiposDocumento.Excel:
                                ConvertirExcel(afuArchivo.FileName);
                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
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

        protected void ddlTipoDocumento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                divUploadDocumento.Visible = true;
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

        private void ConvertirWord(string nombrearchivo)
        {
            try
            {
                string rutaHtml = ConfigurationManager.AppSettings["PathInformacionConsultaHtml"];
                string rutaArchivosCarga = ConfigurationManager.AppSettings["PathInformacionConsultaOriginal"];
                object missingType = Type.Missing;
                object readOnly = true;
                object isVisible = false;
                object documentFormat = 8;
                string randomName = DateTime.Now.Ticks.ToString();
                object htmlFilePath = Server.MapPath(rutaHtml) + Path.GetFileNameWithoutExtension(nombrearchivo) +
                                      ".htm";
                string directoryPath = Server.MapPath(rutaHtml) + Path.GetFileNameWithoutExtension(nombrearchivo) +
                                       "_archivos";
                object fileName = Server.MapPath(rutaArchivosCarga) + nombrearchivo;

                ApplicationClass applicationclass = new ApplicationClass();
                applicationclass.Documents.Open(ref fileName, ref readOnly, ref missingType, ref missingType,
                    ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType,
                    ref isVisible, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType);
                applicationclass.Visible = false;
                Document document = applicationclass.ActiveDocument;
                document.SaveAs(ref htmlFilePath, ref documentFormat, ref missingType, ref missingType, ref missingType,
                    ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType,
                    ref missingType, ref missingType, ref missingType, ref missingType, ref missingType);
                document.Close(ref missingType, ref missingType, ref missingType);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void ConvertirExcel(string nombrearchivo)
        {
            try
            {
                string rutaHtml = ConfigurationManager.AppSettings["PathInformacionConsultaHtml"];
                string rutaArchivosCarga = ConfigurationManager.AppSettings["PathInformacionConsultaOriginal"];
                object missingType = Type.Missing;
                //object readOnly = true;
                //object isVisible = false;
                object documentFormat = 8;
                string randomName = DateTime.Now.Ticks.ToString();
                object htmlFilePath = Server.MapPath(rutaHtml) + Path.GetFileNameWithoutExtension(nombrearchivo) +
                                      ".htm";
                string directoryPath = Server.MapPath(rutaHtml) + Path.GetFileNameWithoutExtension(nombrearchivo) +
                                       "_archivos";
                string fileName = Server.MapPath(rutaArchivosCarga) + nombrearchivo;

                Microsoft.Office.Interop.Excel.ApplicationClass xls =
                    new Microsoft.Office.Interop.Excel.ApplicationClass();
                xls.Workbooks.Open(fileName,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
                xls.Visible = false;
                Microsoft.Office.Interop.Excel.Workbook wb = xls.ActiveWorkbook;
                wb.SaveAs(htmlFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                wb.Close();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {
                InformacionConsulta informacion = new InformacionConsulta
                {
                    Descripcion = txtDescripcion.Text.Trim(),
                    Habilitado = true,
                    IdTipoInfConsulta = Convert.ToInt32(ddlTipoInformacion.SelectedValue),
                    InformacionConsultaDatos = new List<InformacionConsultaDatos>()
                };
                switch (Convert.ToInt32(ddlTipoInformacion.SelectedValue))
                {
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Texto:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.Texto);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos
                        {
                            Descripcion = txtEditor.Text,
                            Orden = 1
                        });
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.Documento);
                        informacion.IdTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos
                        {
                            Descripcion = afuArchivo.FileName,
                            Orden = 1
                        });
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos
                        {
                            Descripcion = txtDescripcionUrl.Text.Trim(),
                            Orden = 1
                        });
                        break;
                    default:
                        throw new Exception("Seleccione un tipo de información");
                }
                if (EsAlta)
                    _servicioInformacionConsulta.GuardarInformacionConsulta(informacion);
                else
                    _servicioInformacionConsulta.ActualizarInformacionConsulta(IdInformacionConsulta, informacion);
                LimpiarCampos();
                if (OnAceptarModal != null)
                    OnAceptarModal();
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

        protected void btnLimpiar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                if (OnLimpiarModal != null)
                    OnLimpiarModal();
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

        protected void btnCancelar_OnClick(object sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
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
                AlertaGeneral = _lstError;
            }
        }
    }
}