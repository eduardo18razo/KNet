using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;
using KinniNet.Core.Demonio;
using KinniNet.Core.Operacion;
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
                Area area = new BusinessArea().ObtenerAreaById(1);

                List<InfoClass> contenClass = ObtenerPropiedadesObjeto(area);

                string formatoArea = NamedFormat.Format("El Area {Descripcion} tiene el identificador {Id}", area);
                new BusinessTicketMailService().RecibeCorreos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<InfoClass> ObtenerPropiedadesObjeto(object obj)
        {
            List<InfoClass> result;
            try
            {
                var propertiesArea = GetProperties(obj);
                result = (from info in propertiesArea
                          where info.PropertyType.Name == "String" || info.PropertyType.Name == "Int32" || info.PropertyType.Name == "DateTime"
                          select new InfoClass
                          {
                              Name = info.Name,
                              Type = info.PropertyType.Name
                          }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        private IEnumerable<PropertyInfo> GetProperties(object obj)
        {
            return obj.GetType().GetProperties().ToList();
        }

        public class InfoClass
        {
            public string Name { get; set; }
            public string Type { get; set; }

        }
    }
}
