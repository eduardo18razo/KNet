using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceInformacionConsulta;
using KiiniHelp.ServiceParametrosSistema;
using KiiniHelp.ServiceSistemaTipoDocumento;
using KiiniHelp.ServiceSistemaTipoInformacionConsulta;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Parametros;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaInformacionConsulta : UserControl, IControllerModal
    {
        public event DelegateAceptarModal OnAceptarModal;
        public event DelegateLimpiarModal OnLimpiarModal;
        public event DelegateCancelarModal OnCancelarModal;

        private readonly ServiceTipoInfConsultaClient _servicioCatalogosSistema = new ServiceTipoInfConsultaClient();
        private readonly ServiceTipoDocumentoClient _servicioSistemaTipoDocumento = new ServiceTipoDocumentoClient();
        private readonly ServiceParametrosClient _servicioParametros = new ServiceParametrosClient();
        private readonly ServiceInformacionConsultaClient _servicioInformacionConsulta = new ServiceInformacionConsultaClient();

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
                ParametrosGenerales generales = _servicioParametros.ObtenerParametrosGenerales();
                List<int> uploads = new List<int>();
                for (int i = 1; i <= int.Parse(generales.NumeroArchivo); i++)
                {
                    uploads.Add(i);
                }
                rptDonloads.DataSource = uploads;
                rptDonloads.DataBind();
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
                    case BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido:
                        if (txtEditor.Text.Trim() == string.Empty)
                            throw new Exception("Debe especificar un contenido");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.DocumentoOffice:
                        //if (!fuFile.HasFile)
                        //    throw new Exception("Debe especificar un documento");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.DireccionWeb:
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
                ddlTipoInformacion.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                ddlTipoInformacion_OnSelectedIndexChanged(ddlTipoInformacion, null);
                ddlTipoDocumento.SelectedIndex = BusinessVariables.ComboBoxCatalogo.IndexSeleccione;
                txtDescripcion.Text = String.Empty;
                txtEditor.Text = string.Empty;
                txtDescripcionUrl.Text = string.Empty;
                Session["FileSize"] = 0;
                Session["FileList"] = new List<string>();
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
        public int IdTipoInformacion
        {
            get { return int.Parse(ddlTipoInformacion.SelectedValue); }
            set
            {
                ddlTipoInformacion.SelectedValue = value.ToString();
                ddlTipoInformacion_OnSelectedIndexChanged(ddlTipoInformacion, null);
            }
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
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido:
                        txtEditor.Text = info.InformacionConsultaDatos.First().Descripcion;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.DocumentoOffice:

                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.DireccionWeb:
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
                    Session["FileSize"] = 0;
                    Session["FileList"] = new List<string>();
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
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido:
                        divPropietrario.Visible = true;
                        divUploadDocumento.Visible = false;
                        divDocumento.Visible = false;
                        divUrl.Visible = false;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.DocumentoOffice:
                        divPropietrario.Visible = false;
                        divDocumento.Visible = true;
                        divUrl.Visible = false;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.DireccionWeb:
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
                List<string> archivos = (List<string>)Session["FileList"];

                TipoDocumento tipoDocto = _servicioSistemaTipoDocumento.ObtenerTiposDocumentoId(int.Parse(ddlTipoDocumento.SelectedValue));
                bool fileOk = false;
                var path = BusinessVariables.Directorios.RepositorioTemporalInformacionConsulta;
                if (!Directory.Exists(BusinessVariables.Directorios.RepositorioTemporalInformacionConsulta))
                    Directory.CreateDirectory(BusinessVariables.Directorios.RepositorioTemporalInformacionConsulta);
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
                        //switch (int.Parse(ddlTipoDocumento.SelectedValue))
                        //{
                        //    case (int)BusinessVariables.EnumTiposDocumento.Word:
                        //        ConvertirWord(afuArchivo.FileName);
                        //        break;
                        //    case (int)BusinessVariables.EnumTiposDocumento.PowerPoint:
                        //        //ConvertirExcel(afuArchivo.FileName);
                        //        break;
                        //    case (int)BusinessVariables.EnumTiposDocumento.Excel:
                        //        ConvertirExcel(afuArchivo.FileName);
                        //        break;
                        //}
                        archivos.Add(afuArchivo.FileName);
                        Session["FileList"] = archivos;

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
                List<string> lstArchivos = (List<string>)Session["FileList"];
                switch (Convert.ToInt32(ddlTipoInformacion.SelectedValue))
                {
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.EditorDeContenido);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos
                        {
                            Descripcion = txtEditor.Text,
                            Orden = 1
                        });
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.DocumentoOffice:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.DocumentoOffice);
                        informacion.IdTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos
                        {
                            Descripcion = afuArchivo.FileName,
                            Orden = 1
                        });
                        lstArchivos.Remove(afuArchivo.FileName);
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.DireccionWeb:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.DireccionWeb);
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
                    _servicioInformacionConsulta.GuardarInformacionConsulta(informacion, lstArchivos);
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

        protected void afDosnload_OnUploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            try
            {
                List<string> archivos = (List<string>)Session["FileList"];
                AsyncFileUpload uploadControl = (AsyncFileUpload)sender;
                ParametrosGenerales generales = _servicioParametros.ObtenerParametrosGenerales();
                Int64 sumaArchivos = Int64.Parse(Session["FileSize"].ToString());
                sumaArchivos += int.Parse(e.FileSize);
                if (!Directory.Exists(BusinessVariables.Directorios.RepositorioTemporalInformacionConsulta))
                    Directory.CreateDirectory(BusinessVariables.Directorios.RepositorioTemporalInformacionConsulta);
                if ((sumaArchivos / 1024) > int.Parse(generales.TamanoDeArchivo))
                    throw new Exception(string.Format("El tamaño maximo de carga es de {0}MB", generales.TamanoDeArchivo));
                uploadControl.SaveAs(BusinessVariables.Directorios.RepositorioTemporalInformacionConsulta + e.FileName);
                archivos.Add(e.FileName);
                Session["FileList"] = archivos;
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