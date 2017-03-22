using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace KinniNet.Business.Utils
{
    public static class BusinessFile
    {
        public static void CopiarArchivoDescarga(string rutaOrigen, string nombreArchivo, string rutaDestino)
        {
            try
            {
                if (!File.Exists(rutaDestino + nombreArchivo))
                    File.Copy(rutaOrigen + nombreArchivo, rutaDestino + nombreArchivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void MoverTemporales(string folderOrigen, string folderDestino, List<string> archivos)
        {
            try
            {
                foreach (string archivo in archivos)
                {
                    if (!Directory.Exists(folderDestino))
                        Directory.CreateDirectory(folderDestino);
                    if (File.Exists(folderDestino + archivo))
                        File.Delete(folderDestino + archivo);
                    File.Move(folderOrigen + archivo, folderDestino + archivo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void LimpiarRepositorioTemporal(List<string> archivos)
        {
            try
            {
                foreach (string archivo in archivos)
                {
                    File.Delete(BusinessVariables.Directorios.RepositorioTemporal + archivo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void LimpiarTemporales(List<string> archivos)
        {
            try
            {
                foreach (string archivo in archivos)
                {
                    File.Delete(BusinessVariables.Directorios.RepositorioTemporalInformacionConsulta + archivo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static class Imagenes
        {
            public static byte[] ImageToByteArray(string image)
            {
                byte[] data = null;
                image = BusinessVariables.Directorios.RepositorioTemporal + image;
                FileInfo fInfo = new FileInfo(image);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(image, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);
                return data;
            }

            public static Image ByteArrayToImage(byte[] byteArrayIn)
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            } 
        }
        public static void ConvertirWord(string nombrearchivo)
        {
            string logFile = BusinessVariables.Directorios.RepositorioRepositorio + @"LogArchivosWord.txt";
            if (!File.Exists(logFile))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(logFile))
                {
                    sw.WriteLine("Inicio Log" + DateTime.Now.ToString());
                }
            }
            Word._Application winWord = null;
            using (StreamWriter file = new StreamWriter(logFile))
            {
                try
                {
                    file.WriteLine("Inicia Proceso");
                    string htmlFilePath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + ".htm";
                    string fileName = BusinessVariables.Directorios.RepositorioInformacionConsulta + nombrearchivo;
                    //nuevo codigo
                    Object oMissing = System.Reflection.Missing.Value;

                    Object oTemplatePath = "D:\\MyTemplate.dotx";
                    Word.Application wordApp = new Word.Application();
                    Word.Document wordDoc = new Word.Document();
                    file.WriteLine("Inicia Proceso abre documento nuevo codigo");
                    wordDoc = wordApp.Documents.Add(fileName);
                    file.WriteLine("Inicia Proceso abrio documento nuevo codigo");
                    file.WriteLine("Inicia Proceso Guarda html nuevo codigo");
                    wordDoc.SaveAs(htmlFilePath, Word.WdSaveFormat.wdFormatHTML);
                    file.WriteLine("Inicia Proceso guardo html nuevo codigo");
                    //wordApp.Documents.Open("myFile.doc");
                    wordApp.Application.Quit();
                    //Finnuevo codigo


                    //file.WriteLine("Inicia aplicacion");
                    //winWord = new Word.ApplicationClass();
                    //winWord.Visible = true;
                    //file.WriteLine("Abre Archivo");
                    //winWord.Documents.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //file.WriteLine("Abrio Archivo");
                    //file.WriteLine("Activa documento de trabajo");
                    //Word.Document doc = winWord.ActiveDocument;
                    //file.WriteLine("Activo documento de trabajo");
                    //file.WriteLine(htmlFilePath);

                    //doc.SaveAs(htmlFilePath, Word.WdSaveFormat.wdFormatHTML, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    //file.WriteLine("Guardo nuevo formato");
                    //doc.Close();
                    //file.WriteLine("cerro documento");
                    //winWord.Quit();
                    //file.WriteLine("Cerro Word");
                }
                catch (Exception e)
                {
                    if (winWord != null)
                    {
                        winWord.Quit();
                    }
                    throw new Exception(e.Message);
                }
                finally
                {
                    GC.Collect();
                }
            }
        }
        public static void ConvertirExcel(string nombrearchivo)
        {
            string logFile = BusinessVariables.Directorios.RepositorioRepositorio + @"LogArchivosExcel.txt";
            if (!File.Exists(logFile))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(logFile))
                {
                    sw.WriteLine("Inicio Log" + DateTime.Now.ToString());
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(logFile))
            {
                try
                {
                    file.WriteLine("Inicia Proceso");
                    string htmlFilePath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + ".htm";
                    string directoryPath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + "_archivos";
                    string fileName = BusinessVariables.Directorios.RepositorioInformacionConsulta + nombrearchivo;
                    file.WriteLine("Inicia aplicacion");
                    Excel._Application xls = new Excel.ApplicationClass();
                    xls.Visible = false;
                    file.WriteLine("Abre Archivo");
                    xls.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    file.WriteLine("Abrio Archivo");
                    file.WriteLine("Activa hoja de trabajo");
                    Excel.Workbook wb = xls.ActiveWorkbook;
                    file.WriteLine("Activo hoja de trabajo");
                    file.WriteLine(htmlFilePath);

                    wb.SaveAs(htmlFilePath, Excel.XlFileFormat.xlHtml, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    file.WriteLine("Guardo nuevo formato");
                    wb.Close();
                    file.WriteLine("cerro libro de trabajo");
                    xls.Quit();
                    file.WriteLine("Cerro excel");

                }
                catch (Exception e)
                {
                    file.WriteLine("error proceso \n" + e.InnerException.Message + " Error gral\n" + e.Message);
                    throw new Exception(e.Message);

                }
                finally
                {
                    GC.Collect();
                }
            }
        }
        public static void ConvertirPowerPoint(string nombrearchivo)
        {
            string logFile = BusinessVariables.Directorios.RepositorioRepositorio + @"LogArchivosWord.txt";
            if (!File.Exists(logFile))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(logFile))
                {
                    sw.WriteLine("Inicio Log" + DateTime.Now.ToString());
                }
            }
            PowerPoint._Application winWord = null;
            using (StreamWriter file = new StreamWriter(logFile))
            {
                try
                {

                    file.WriteLine("Inicia Proceso");
                    string htmlFilePath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + ".htm";
                    string fileName = BusinessVariables.Directorios.RepositorioInformacionConsulta + nombrearchivo;

                    PowerPoint.Application ppApp = new PowerPoint.Application();
                    ppApp.Visible = MsoTriState.msoTrue;
                    PowerPoint.Presentations ppPresens = ppApp.Presentations;
                    PowerPoint.Presentation objPres = ppPresens.Open(fileName, MsoTriState.msoFalse, MsoTriState.msoTrue, MsoTriState.msoTrue);
                    file.WriteLine("Abrio Presentacion");
                    file.WriteLine("Guardara Nueva Presentacion nuevo formato");
                    file.WriteLine(htmlFilePath);
                    objPres.SaveAs(htmlFilePath, PowerPoint.PpSaveAsFileType.ppSaveAsHTML, MsoTriState.msoCTrue);
                    file.WriteLine("Guardo nuevo formato");
                    PowerPoint.Slides objSlides = objPres.Slides;
                    //PowerPoint.SlideShowWindows objSSWs; 
                    //PowerPoint.SlideShowSettings objSSS;
                    ////Run the Slide show
                    //objSSS = objPres.SlideShowSettings;
                    //objSSS.Run();
                    //objSSWs = ppApp.SlideShowWindows;
                    //while (objSSWs.Count >= 1)
                    //    System.Threading.Thread.Sleep(100);
                    ////Close the presentation without saving changes and quit PowerPoint
                    //objPres.Close();
                    ppApp.Quit();



                    //file.WriteLine("Inicia aplicacion");
                    //winWord = new PowerPoint.ApplicationClass();
                    //file.WriteLine("Abre Archivo");
                    //PowerPoint.Presentation prsPres = winWord.Presentations.Open(fileName, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                    //file.WriteLine("Abrio Archivo");
                    //file.WriteLine(htmlFilePath);
                    //file.WriteLine("GuardaraNuevo Documento nuevo formato");
                    //prsPres.SaveAs(htmlFilePath, PowerPoint.PpSaveAsFileType.ppSaveAsHTML);
                    //file.WriteLine("Guardo nuevo formato");
                    //prsPres.Close();
                    //file.WriteLine("cerro documento");
                    //winWord.Quit();
                    //file.WriteLine("Cerro Word");
                }
                catch (Exception e)
                {
                    if (winWord != null)
                    {
                        winWord.Quit();
                    }
                    throw new Exception(e.Message);
                }
                finally
                {
                    GC.Collect();
                }
            }
        }
        public static class ExcelManager
        {
            public static DataTable ObtenerHojasExcel(string nombreArchivo)
            {
                OleDbConnection myConnection = null;
                DataTable result;
                try
                {
                    myConnection = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + nombreArchivo + "';Extended Properties=Excel 12.0;");
                    myConnection.Open();
                    result = myConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (myConnection != null && myConnection.State == ConnectionState.Open)
                    {
                        myConnection.Close();
                    }
                }
                return result;
            }

            public static DataSet LeerHojaExcel(string archivo, string hoja)
            {
                OleDbConnection excelCoon = null;
                DataSet dtSet;
                try
                {
                    excelCoon = new OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + BusinessVariables.Directorios.RepositorioTemporal + archivo + " ';Extended Properties=Excel 12.0;");
                    OleDbDataAdapter cmd = new OleDbDataAdapter("select * from [" + hoja + "]", excelCoon);
                    excelCoon.Open();
                    cmd.TableMappings.Add("Table", "tablaPaso");
                    dtSet = new DataSet();
                    cmd.Fill(dtSet);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (excelCoon != null && excelCoon.State == ConnectionState.Open)
                    {
                        excelCoon.Close();
                    }
                }
                return dtSet;
            }
        }
    }
}
