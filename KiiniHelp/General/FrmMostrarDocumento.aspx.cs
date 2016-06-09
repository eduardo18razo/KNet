using System;
using System.Configuration;
using System.IO;
using System.Threading;
using KiiniHelp.Funciones;
using KinniNet.Business.Utils;
using Microsoft.Office.Interop.Word;
using Page = System.Web.UI.Page;

namespace KiiniHelp.General
{
    public partial class FrmMostrarDocumento : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nombreDocto = Request.QueryString["NombreDocumento"];
                int tipoInformacion = Convert.ToInt32(Request.QueryString["TipoDocumento"]);
                string directorio = Server.MapPath("~/General/");
                if (!IsPostBack)
                {
                    switch (tipoInformacion)
                    {
                        case (int)BusinessVariables.EnumTiposDocumento.Word:
                            Documentos.MostrarDocumento(nombreDocto, this, directorio);
                            break;
                        case (int)BusinessVariables.EnumTiposDocumento.PowerPoint:
                            Documentos.MostrarDocumento(nombreDocto, this, directorio);
                            break;
                        case (int)BusinessVariables.EnumTiposDocumento.Excel:
                            Documentos.MostrarDocumento(nombreDocto, this, directorio);
                            break;
                    }
                }
            }
        }

       
    }
}