using System;
using System.ComponentModel;
using System.Linq;
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
                DataBaseModelContext db = new DataBaseModelContext();
                BusinessSecurity.Menus bs = new BusinessSecurity.Menus();
                bs.ObtenerMenuUsuario(3);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
