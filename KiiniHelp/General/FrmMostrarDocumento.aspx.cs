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
                //UcMostrarArchivo.NombreDocumento = nombreDocto;
                //UcMostrarArchivo.TipoInformacion = tipoInformacion;

                if (!IsPostBack)
                {
                    Documentos doctos = new Documentos();
                    switch (tipoInformacion)
                    {
                        case (int)BusinessVariables.EnumTiposDocumento.Word:
                            MostrarWord(nombreDocto);
                            break;
                        case (int)BusinessVariables.EnumTiposDocumento.PowerPoint:
                            break;
                    }
                }
            }
        }

        private void MostrarWord(string nombrearchivo)
        {
            string rutaTemporales = ConfigurationManager.AppSettings["temporalyFilesInformacionConsulta"];
            string rutaArchivosCarga = ConfigurationManager.AppSettings["PathInformacionConsulta"];
            object missingType = Type.Missing;
            object readOnly = true;
            object isVisible = false;
            object documentFormat = 8;
            string randomName = DateTime.Now.Ticks.ToString();
            object htmlFilePath = Server.MapPath(rutaTemporales) + randomName + ".htm";
            string directoryPath = Server.MapPath(rutaTemporales) + randomName + "_archivos";
            object fileName = rutaArchivosCarga + nombrearchivo;

            ApplicationClass applicationclass = new ApplicationClass();
            applicationclass.Documents.Open(ref fileName, ref readOnly, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref  missingType, ref missingType, ref missingType, ref isVisible, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType);
            applicationclass.Visible = false;
            Document document = applicationclass.ActiveDocument;
            document.SaveAs(ref htmlFilePath, ref documentFormat, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType);
            document.Close(ref missingType, ref missingType, ref missingType);
            byte[] bytes;
            using (FileStream fs = new FileStream(htmlFilePath.ToString(), FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(fs);
                bytes = reader.ReadBytes((int)fs.Length);
                fs.Close();
            }
            Response.BinaryWrite(bytes);
            Response.Flush();
            Thread.Sleep(2000);
            File.Delete(htmlFilePath.ToString());
            foreach (string file in Directory.GetFiles(directoryPath))
            {
                File.Delete(file);
            }
            Directory.Delete(directoryPath);
            Response.End();
        }
    }
}