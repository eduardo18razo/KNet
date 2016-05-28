using System;
using System.Web.UI;
using Page = Microsoft.Office.Interop.Word;

namespace KiiniHelp.TestUsControl
{
    public partial class FrmTest : System.Web.UI.Page
    {
        public int IdMascara = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DoctoWord();
                //DoctoExce();
                ////Set the appropriate ContentType.
                //Response.ContentType = "application/vnd.ms-word";
                ////Get the physical path to the file.
                //string FilePath = MapPath("~/Uploads/Pantallas Alta y asignacion de grupos.docx");
                ////Write the file directly to the HTTP content output stream.
                //Response.WriteFile(FilePath);
                //Response.End();
                UcMascaraCaptura.IdMascara = 1;

            }
        }

        protected void btnModalOrganizacion_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "Script", "MostrarPopup(\"#modalTest\");", true);
        }

        protected void OnClick(object sender, EventArgs e)
        {
        }

        //private void DoctoWord()
        //{
        //    object missingType = Type.Missing;
        //    object readOnly = true;
        //    object isVisible = false;
        //    object documentFormat = 8;
        //    string randomName = DateTime.Now.Ticks.ToString();
        //    object htmlFilePath = Server.MapPath("~/TestUsControl/") + randomName + ".htm";
        //    string directoryPath = Server.MapPath("~/TestUsControl/") + randomName + "_archivos";

        //    object fileName = @"C:\Users\Eduardo\Documents\Visual Studio 2013\Projects\KiiniNet\App\KinniNet\KiiniHelp\Uploads\Pantallas Alta y asignacion de grupos.docx";

        //    //Open the word document in background
        //    Microsoft.Office.Interop.Word.ApplicationClass applicationclass = new Microsoft.Office.Interop.Word.ApplicationClass();
        //    applicationclass.Documents.Open(ref fileName,
        //                                    ref readOnly,
        //                                    ref missingType, ref missingType, ref missingType,
        //                                    ref missingType, ref missingType, ref  missingType,
        //                                    ref missingType, ref missingType, ref isVisible,
        //                                    ref missingType, ref missingType, ref missingType,
        //                                    ref missingType, ref missingType);
        //    applicationclass.Visible = false;
        //    Microsoft.Office.Interop.Word.Document document = applicationclass.ActiveDocument;

        //    //Save the word document as HTML file
        //    document.SaveAs(ref htmlFilePath, ref documentFormat, ref missingType,
        //                    ref missingType, ref missingType, ref missingType,
        //                    ref missingType, ref missingType, ref missingType,
        //                    ref missingType, ref missingType, ref missingType,
        //                    ref missingType, ref missingType, ref missingType,
        //                    ref missingType);

        //    //Close the word document
        //    document.Close(ref missingType, ref missingType, ref missingType);

        //    //Delete the Uploaded Word File
        //    //File.Delete(Server.MapPath("~/Uploads/") + Path.GetFileName(fileName.ToString()));

        //    //Read the Html File as Byte Array and Display it on browser
        //    byte[] bytes;
        //    using (FileStream fs = new FileStream(htmlFilePath.ToString(), FileMode.Open, FileAccess.Read))
        //    {
        //        BinaryReader reader = new BinaryReader(fs);
        //        bytes = reader.ReadBytes((int)fs.Length);
        //        fs.Close();
        //    }
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Thread.Sleep(2000);
        //    //Delete the Html File
        //    File.Delete(htmlFilePath.ToString());
        //    foreach (string file in Directory.GetFiles(directoryPath))
        //    {
        //        File.Delete(file);
        //    }
        //    Directory.Delete(directoryPath);
        //    Response.End();
        //}

        //private void DoctoExce()
        //{
        //    object missingType = Type.Missing;
        //    object readOnly = true;
        //    object isVisible = false;
        //    object documentFormat = 8;
        //    string randomName = DateTime.Now.Ticks.ToString();
        //    object htmlFilePath = Server.MapPath("~/Uploads/") + randomName + ".htm";
        //    string directoryPath = Server.MapPath("~/Uploads/") + randomName + "_archivos";

        //    string fileName = @"C:\Users\Eduardo\Documents\Visual Studio 2013\Projects\KiiniNet\App\KinniNet\KiiniHelp\Uploads\Estimacion Consultas y tickets.xlsx";

        //    //Open the word document in background
        //    Microsoft.Office.Interop.Excel.ApplicationClass applicationclass = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    applicationclass.Workbooks.Open(fileName, Type.Missing, readOnly, missingType, missingType, missingType,
        //                                    missingType, missingType, missingType,
        //                                    missingType, missingType, isVisible,
        //                                    missingType, missingType, missingType);
        //    applicationclass.Visible = false;
        //    Workbook document = applicationclass.ActiveWorkbook;

        //    //Save the word document as HTML file
        //    document.SaveAs(htmlFilePath, XlFileFormat.xlHtml, Type.Missing, Type.Missing, Type.Missing,
        //        Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        //        Type.Missing);

        //    //Close the word document
        //    document.Close(missingType, missingType, missingType);

        //    //Delete the Uploaded Word File
        //    //File.Delete(Server.MapPath("~/Uploads/") + Path.GetFileName(fileName.ToString()));

        //    //Read the Html File as Byte Array and Display it on browser
        //    byte[] bytes;
        //    using (FileStream fs = new FileStream(htmlFilePath.ToString(), FileMode.Open, FileAccess.Read))
        //    {
        //        BinaryReader reader = new BinaryReader(fs);
        //        bytes = reader.ReadBytes((int)fs.Length);
        //        fs.Close();
        //    }
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();

        //    //Delete the Html File
        //    File.Delete(htmlFilePath.ToString());
        //    foreach (string file in Directory.GetFiles(directoryPath))
        //    {
        //        File.Delete(file);
        //    }
        //    Directory.Delete(directoryPath);
        //    Response.End();
        //}

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
        }
    }
}