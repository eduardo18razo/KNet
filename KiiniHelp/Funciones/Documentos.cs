using System;
using System.Configuration;
using System.IO;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using ApplicationClass = Microsoft.Office.Interop.Word.ApplicationClass;
using Page = System.Web.UI.Page;

namespace KiiniHelp.Funciones
{
    public class Documentos : Page
    {
        public void MostrarWord(string nombrearchivo)
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

        private void MostrarExcel(string nombrearchivo)
        {
            object missingType = Type.Missing;
            object readOnly = true;
            object isVisible = false;
            object documentFormat = 8;
            string randomName = DateTime.Now.Ticks.ToString();
            object htmlFilePath = Server.MapPath("~/Uploads/") + randomName + ".htm";
            string directoryPath = Server.MapPath("~/Uploads/") + randomName + "_archivos";

            string fileName = @"C:\Users\Eduardo\Documents\Visual Studio 2013\Projects\KiiniNet\App\KinniNet\KiiniHelp\Uploads\Estimacion Consultas y tickets.xlsx";

            //Open the word document in background
            Microsoft.Office.Interop.Excel.ApplicationClass applicationclass = new Microsoft.Office.Interop.Excel.ApplicationClass();
            applicationclass.Workbooks.Open(fileName, Type.Missing, readOnly, missingType, missingType, missingType,
                                            missingType, missingType, missingType,
                                            missingType, missingType, isVisible,
                                            missingType, missingType, missingType);
            applicationclass.Visible = false;
            Workbook document = applicationclass.ActiveWorkbook;

            //Save the word document as HTML file
            document.SaveAs(htmlFilePath, XlFileFormat.xlHtml, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing);

            //Close the word document
            document.Close(missingType, missingType, missingType);

            //Delete the Uploaded Word File
            //File.Delete(Server.MapPath("~/Uploads/") + Path.GetFileName(fileName.ToString()));

            //Read the Html File as Byte Array and Display it on browser
            byte[] bytes;
            using (FileStream fs = new FileStream(htmlFilePath.ToString(), FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(fs);
                bytes = reader.ReadBytes((int)fs.Length);
                fs.Close();
            }
            Response.BinaryWrite(bytes);
            Response.Flush();

            //Delete the Html File
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