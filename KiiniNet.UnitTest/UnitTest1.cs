using System;
using KinniNet.Core.Operacion;
using KinniNet.Core.Security;
using KinniNet.Data.Help;
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
                //var test = new BusinessMascaras().ObtenerCatalogoCampoMascara("TipoUsuario");
                DataBaseModelContext db = new DataBaseModelContext();
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
