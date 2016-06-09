using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using ApplicationClass = Microsoft.Office.Interop.Word.ApplicationClass;
using Page = System.Web.UI.Page;

namespace KiiniHelp.Funciones
{
    public static class Documentos
    {
        public static void MostrarDocumento(string nombrearchivo, Page page, string directorio)
        {
            string rutaHtml = ConfigurationManager.AppSettings["PathInformacionConsultaHtml"];

            string htmlFilePath = page.Server.MapPath(rutaHtml) + Path.GetFileNameWithoutExtension(nombrearchivo) + ".htm";
            string directoryPath = page.Server.MapPath(rutaHtml) + Path.GetFileNameWithoutExtension(nombrearchivo) + "_archivos";
            string directorioTemporal = directorio + Path.GetFileNameWithoutExtension(nombrearchivo) + "_archivos"; ;
            CopyFilesRecursively(new DirectoryInfo(directoryPath), new DirectoryInfo(directorio));
            byte[] bytes;
            using (FileStream fs = new FileStream(htmlFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(fs);
                bytes = reader.ReadBytes((int)fs.Length);
                fs.Close();
            }
            page.Response.BinaryWrite(bytes);
            page.Response.Flush();
            Thread.Sleep(2000);
            //foreach (string file in Directory.GetFiles(directorioTemporal))
            //{
            //    File.Delete(file);
            //}
            //Directory.Delete(directorioTemporal);
            //page.Response.End();
        }

        public static void EliminarTemporales()
        {
            
        }

        private static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            if (Directory.Exists(Path.Combine(target.FullName, source.Name))) return;
            target.CreateSubdirectory(source.Name);
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(Path.Combine(target.FullName, source.Name), file.Name));
            }
        }
    }
}