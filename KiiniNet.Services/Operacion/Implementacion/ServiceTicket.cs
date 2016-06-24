using System.Collections.Generic;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;
using KiiniNet.Services.Operacion.Interface;
using KinniNet.Core.Operacion;

namespace KiiniNet.Services.Operacion.Implementacion
{
    public class ServiceTicket : IServiceTicket
    {
        public void CrearTicket(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura)
        {
            using (BusinessTicket negocio = new BusinessTicket())
            {
                negocio.CrearTicket(idUsuario, idArbol, lstCaptura);
            }
        }

        public List<HelperTickets> ObtenerTickets(int idUsuario, int pageIndex, int pageSize)
        {
            using (BusinessTicket negocio = new BusinessTicket())
            {
                return negocio.ObtenerTickets(idUsuario,  pageIndex, pageSize);
            }
        }
    }
}
