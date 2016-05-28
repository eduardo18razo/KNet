using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;
using KiiniHelp.Funciones;
using KiiniHelp.ServiceInformacionConsulta;
using KiiniHelp.ServiceSistemaTipoDocumento;
using KiiniHelp.ServiceSistemaTipoInformacionConsulta;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;

namespace KiiniHelp.UserControls.Altas
{
    public partial class AltaInformacionConsulta : UserControl
    {
        readonly ServiceTipoInfConsultaClient _servicioCatalogosSistema = new ServiceTipoInfConsultaClient();
        readonly ServiceTipoDocumentoClient _servicioSistemaTipoDocumento = new ServiceTipoDocumentoClient();
        readonly ServiceInformacionConsultaClient _servicioInformacionConsulta = new ServiceInformacionConsultaClient();

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
                Metodos.LlenaComboCatalogo(ddlTipoInformacion, _servicioCatalogosSistema.ObtenerTipoInformacionConsulta(true));
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
                    case BusinessVariables.EnumTiposInformacionConsulta.Propietario:
                        if (txtEditor.Text.Trim() == string.Empty)
                            throw new Exception("Debe especificar un contenido");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        if (!fuFile.HasFile)
                            throw new Exception("Debe especificar un documento");
                        break;
                    case BusinessVariables.EnumTiposInformacionConsulta.Paginahtml:
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
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Propietario:
                        divPropietrario.Visible = true;
                        divDocumento.Visible = false;
                        divUrl.Visible = false;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        divPropietrario.Visible = false;
                        divDocumento.Visible = true;
                        divUrl.Visible = false;
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Paginahtml:
                        divPropietrario.Visible = false;
                        divDocumento.Visible = false;
                        divUrl.Visible = true;
                        break;
                    default:
                        divPropietrario.Visible = false;
                        divDocumento.Visible = false;
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
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Propietario:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.Propietario);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos { Descripcion = txtEditor.Text, Orden = 1 });
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.Documento);
                        informacion.IdTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                        afuArchivo_OnUploadedComplete(null, null);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos { Descripcion = fuFile.PostedFile.FileName, Orden = 1 });
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Paginahtml:
                        ValidaCaptura(BusinessVariables.EnumTiposInformacionConsulta.Paginahtml);
                        informacion.InformacionConsultaDatos.Add(new InformacionConsultaDatos { Descripcion = txtDescripcionUrl.Text.Trim(), Orden = 1 });
                        break;
                    default:
                        throw new Exception("Seeleccione un tipo de información");
                }
                _servicioInformacionConsulta.GuardarInformacionConsulta(informacion);
                LimpiarCampos();
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
                TipoDocumento tipoDocto = _servicioSistemaTipoDocumento.ObtenerTiposDocumentoId(int.Parse(ddlTipoDocumento.SelectedValue));
                string[] validFileTypes = tipoDocto.Extension.Split(',');
                string ext = Path.GetExtension(fuFile.PostedFile.FileName);
                bool isValidFile = validFileTypes.Any(t => ext.ToLower() == t.ToLower());
                if (!isValidFile)
                {
                    throw new Exception("Archivo con formato Invalido");
                }
                else
                {
                    string path = Server.MapPath("~/Uploads/") + fuFile.PostedFile.FileName;
                    fuFile.SaveAs(path);
                }
                upInfo.Update();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}