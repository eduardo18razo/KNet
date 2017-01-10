using System;
using System.Linq;
using KinniNet.Core.Demonio;
using KinniNet.Core.Sistema;
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
                new BusinessDemonio().ActualizaSla();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
