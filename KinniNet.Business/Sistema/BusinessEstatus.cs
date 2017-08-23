using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessEstatus : IDisposable
    {
        private readonly bool _proxy;
        public BusinessEstatus(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        {

        }

        public List<EstatusTicket> ObtenerEstatusTicket(bool insertarSeleccion)
        {
            List<EstatusTicket> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusTicket.Where(w => w.Habilitado).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new EstatusTicket
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public List<EstatusTicket> ObtenerEstatusTicketUsuario(int idUsuario, int idEstatusActual, bool esPropietario, bool insertarSeleccion)
        {
            List<EstatusTicket> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = (from etsrg in db.EstatusTicketSubRolGeneral
                        join et in db.EstatusTicket on etsrg.IdEstatusTicketAccion equals et.Id
                        join ug in db.UsuarioGrupo on etsrg.IdGrupoUsuario equals ug.IdGrupoUsuario
                        join sgu in db.SubGrupoUsuario on new { subgpo = ug.IdSubGrupoUsuario, sub = etsrg.IdSubRolSolicita } equals new { subgpo = (int?)sgu.Id, sub = (int?)sgu.IdSubRol }
                        where ug.IdUsuario == idUsuario && etsrg.IdEstatusTicketActual == idEstatusActual && etsrg.Propietario == esPropietario && etsrg.Habilitado
                        select et).Distinct().ToList();
                //result = (db.EstatusTicketSubRolGeneral.Join(db.EstatusTicket, easg => easg.IdEstatusTicketActual, et => et.Id, (easg, et) => new { easg, et })
                //    .Join(db.UsuarioGrupo, @t => @t.easg.IdGrupoUsuario, ug => ug.IdGrupoUsuario, (@t, ug) => new { @t, ug })
                //    .Where(@t => @t.ug.IdUsuario == idUsuario && @t.@t.easg.Habilitado && @t.@t.easg.Propietario == esPropietario)
                //    .OrderBy(o => o.t.easg.EstatusTicketAccion.Orden)
                //    .Select(@t => @t.@t.et)).Distinct().ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new EstatusTicket
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public List<EstatusAsignacion> ObtenerEstatusAsignacion(bool insertarSeleccion)
        {
            List<EstatusAsignacion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusAsignacion.Where(w => w.Habilitado).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new EstatusAsignacion
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public List<EstatusAsignacion> ObtenerEstatusAsignacionUsuario(int idUsuario, int estatusAsignacionActual, bool esPropietario, bool insertarSeleccion)
        {
            List<EstatusAsignacion> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = (from easg in db.EstatusAsignacionSubRolGeneral
                          join ea in db.EstatusAsignacion on easg.IdEstatusAsignacionActual equals ea.Id
                          join ea1 in db.EstatusAsignacion on easg.IdEstatusAsignacionAccion equals ea1.Id
                          join ug in db.UsuarioGrupo on easg.IdGrupoUsuario equals ug.IdGrupoUsuario
                          where ug.IdUsuario == idUsuario && easg.IdSubRol == ug.SubGrupoUsuario.IdSubRol &&
                                easg.TieneSupervisor == ug.GrupoUsuario.TieneSupervisor &&
                                easg.IdEstatusAsignacionActual == estatusAsignacionActual && easg.Habilitado && easg.Propietario == esPropietario
                          select ea1).Distinct().ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new EstatusAsignacion
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.ValueSeleccione,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione
                        });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public bool HasComentarioObligatorio(int idUsuario, int idSubRol, int estatusAsignacionActual, int estatusAsignar, bool esPropietario)
        {
            bool result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                result = (from easg in db.EstatusAsignacionSubRolGeneral
                          join ea in db.EstatusAsignacion on easg.IdEstatusAsignacionActual equals ea.Id
                          join ea1 in db.EstatusAsignacion on easg.IdEstatusAsignacionAccion equals ea1.Id
                          join ug in db.UsuarioGrupo on easg.IdGrupoUsuario equals ug.IdGrupoUsuario
                          where ug.IdUsuario == idUsuario && easg.IdSubRol == idSubRol &&
                                easg.IdEstatusAsignacionActual == estatusAsignacionActual && easg.IdEstatusAsignacionAccion == estatusAsignar && easg.Propietario == esPropietario
                          select easg.ComentarioObligado).First();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
    }
}
