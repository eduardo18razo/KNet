using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        public static void ConvertirWord(string nombrearchivo)
        {
            try
            {
                object missingType = Type.Missing;
                object readOnly = true;
                object isVisible = false;
                object documentFormat = 8;
                string htmlFilePath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + ".htm";
                string directoryPath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + "_archivos";
                object fileName = BusinessVariables.Directorios.RepositorioInformacionConsulta + nombrearchivo;

                Word._Application applicationclass = new Word.ApplicationClass();
                applicationclass.Documents.Open(ref fileName, ref readOnly, ref missingType, ref missingType,
                    ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType,
                    ref isVisible, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType);
                applicationclass.Visible = false;
                Word.Document document = applicationclass.ActiveDocument;
                document.SaveAs(htmlFilePath, ref documentFormat, ref missingType, ref missingType, ref missingType,
                    ref missingType, ref missingType, ref missingType, ref missingType, ref missingType, ref missingType,
                    ref missingType, ref missingType, ref missingType, ref missingType, ref missingType);
                document.Close(ref missingType, ref missingType, ref missingType);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void ConvertirExcel(string nombrearchivo)
        {
            try
            {
                string htmlFilePath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + ".htm";
                string directoryPath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + "_archivos";
                string fileName = BusinessVariables.Directorios.RepositorioInformacionConsulta + nombrearchivo;

                Excel._Application xls = new Excel.ApplicationClass();
                xls.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xls.Visible = false;
                Excel.Workbook wb = xls.ActiveWorkbook;
                wb.SaveAs(htmlFilePath, Excel.XlFileFormat.xlHtml, Type.Missing, Type.Missing, false, false, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                wb.Close();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public static void ConvertirPowerPoint(string nombrearchivo)
        {
            string fileName = BusinessVariables.Directorios.RepositorioInformacionConsulta + nombrearchivo;
            //Give the name and path of the HTML file to be generated
            string htmlFilePath = BusinessVariables.Directorios.RepositorioInformacionConsultaHtml + Path.GetFileNameWithoutExtension(nombrearchivo) + ".htm";
            //Create a PowerPoint Application Object
            PowerPoint.Application ppApp = new PowerPoint.Application();

            //Create a PowerPoint Presentation object
            PowerPoint.Presentation prsPres = ppApp.Presentations.Open(fileName, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
            //Call the SaveAs method of Presentaion object and specify the format as HTML
            prsPres.SaveAs(htmlFilePath, PowerPoint.PpSaveAsFileType.ppSaveAsHTML, MsoTriState.msoTrue);

            //Close the Presentation object
            prsPres.Close();
            //Close the Application object
            ppApp.Quit();
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
