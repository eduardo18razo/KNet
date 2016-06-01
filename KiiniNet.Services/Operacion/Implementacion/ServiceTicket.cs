using System.Collections.Generic;
using KiiniNet.Entities.Helper;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceTicket : IServiceTicket
    {
        public void Guardar(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura)
        {
            using (BusinessTicket negocio = new BusinessTicket())
            {
                negocio.Guardar(idUsuario, idArbol, lstCaptura);
            }
        }
    }
}
