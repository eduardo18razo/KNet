using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessTicket : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessTicket(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Guardar(int idUsuario, int idArbol, List<HelperCampoMascaraCaptura> lstCaptura)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Usuario usuario = new BusinessUsuarios().ObtenerUsuario(idUsuario);
                ArbolAcceso arbol = new BusinessArbolAcceso().ObtenerArbolAcceso(idArbol);
                Mascara mascara = new BusinessMascaras().ObtenerMascaraCaptura(arbol.InventarioArbolAcceso.First().IdMascara ?? 0);
                Encuesta encuesta = new BusinessEncuesta().ObtenerEncuesta(arbol.InventarioArbolAcceso.First().IdEncuesta ?? 0);
                Sla sla = new BusinessSla().ObtenerSla(arbol.InventarioArbolAcceso.First().IdSla ?? 0);
                Ticket ticket = new Ticket
                {
                    IdTipoUsuario = usuario.IdTipoUsuario,
                    IdTipoArbolAcceso = arbol.IdTipoArbolAcceso,
                    IdArbolAcceso = arbol.Id,
                    IdUsuario = usuario.Id,
                    IdOrganizacion = usuario.IdOrganizacion,
                    IdUbicacion = usuario.IdUbicacion,
                    IdMascara = mascara.Id,
                    IdEncuesta = encuesta.Id,
                    RespuestaEncuesta = new List<RespuestaEncuesta>()
                };
                ticket.RespuestaEncuesta.AddRange(encuesta.EncuestaPregunta.Select(pregunta => new RespuestaEncuesta {IdEncuesta = encuesta.Id,IdPregunta = pregunta.Id}));

                ticket.SlaEstimadoTicket = new SlaEstimadoTicket
                {
                    FechaInicio = DateTime.Now.Date,
                    FechaFin = DateTime.Now.Date, 
                    TiempoHoraProceso = sla.TiempoHoraProceso,
                    Terminado = false,
                    SlaEstimadoTicketDetalle = new List<SlaEstimadoTicketDetalle>()
                };
                ticket.SlaEstimadoTicket.SlaEstimadoTicketDetalle.AddRange(sla.SlaDetalle.Select(detalle => new SlaEstimadoTicketDetalle { IdSubRol = detalle.IdSubRol, TiempoProceso = detalle.TiempoProceso }));
                
                db.Ticket.AddObject(ticket);
                db.SaveChanges();
                string store = string.Format("{0} '{1}',", mascara.ComandoInsertar , ticket.Id);
                foreach (HelperCampoMascaraCaptura captura in lstCaptura)
                {
                    store += string.Format("'{0}',", captura.Valor);
                }
                store = store.Trim().TrimEnd(',');
                db.ExecuteStoreCommand(store);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
