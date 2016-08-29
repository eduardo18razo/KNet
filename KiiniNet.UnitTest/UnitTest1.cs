using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KiiniNet.UnitTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TesConsultas()
        {
            try
            {
                //new BusinessMascaras().GetDataMascara(1,1);
                //DataBaseModelContext db = new DataBaseModelContext();
                //var y = db.Ticket.Where(w=>w.EncuestaRespondida == false && w.IdEncuesta != null).ToList();
                //y = y.ToList();
                //SubRol
                //try
                //{

                //    if (!EventLog.SourceExists("KiiniNet"))
                //        EventLog.CreateEventSource("KiiniNet", "Service Send Notication");

                //    EventLog.WriteEntry("KiiniNet", "CerritosLicea");
                //    EventLog.WriteEntry("KiiniNet", "CerritosLicea",
                //        EventLogEntryType.Error, 234);
                //}
                //catch (Exception ex)
                //{
                //    //Log("KiiniNet", "Service Send Notication", ex.Message);
                //}
                //new BusinessDemonio().EnvioNotificacion();
                //while (true)
                //{
                //    Random obj = new Random();
                //string posibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                //int longitud = posibles.Length;
                //char letra;
                //int longitudnuevacadena = 4;
                //string nuevacadena = "";
                //for (int i = 0; i < longitudnuevacadena; i++)
                //{
                //    letra = posibles[obj.Next(longitud)];
                //    nuevacadena += letra.ToString();
                //}
                //Debug.WriteLine(nuevacadena);
                //}

                //while (true)
                //{
                //    int year = int.Parse(DateTime.Now.ToString("yy"));
                //    int seg = int.Parse(DateTime.Now.ToString("ss"));
                //    int dia = int.Parse(DateTime.Now.ToString("dd"));
                //    int mes = int.Parse(DateTime.Now.ToString("MM"));
                //    int hora = int.Parse(DateTime.Now.ToString("HH"));
                //    int min = int.Parse(DateTime.Now.ToString("mm"));
                //    int mil = int.Parse(DateTime.Now.ToString("fff"));
                //    int rnd = new Random().Next(100, 999);
                //    Thread.Sleep(2);
                //    string rndValue = rnd.ToString() + year + seg + dia + mes + hora + min + rnd + mil + int.Parse(DateTime.Now.ToString("fff"));

                //    Debug.WriteLine(rndValue);
                //}


                //DateTime.Now.ToString("Y");
                ////var test = new BusinessMascaras().ObtenerCatalogoCampoMascara("TipoUsuario");
                //DataBaseModelContext db = new DataBaseModelContext();
                //new BusinessTicket().ObtenerDetalleTicket(1);
                //bs.ObtenerMenuUsuario(3);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
