using KiiniNet.Entities.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using KinniNet.Data.Help;


namespace KinniNet.Core.Sistema
{
    public class BusinessPoliticas : IDisposable
    {
        private readonly bool _proxy;
        public BusinessPoliticas(bool proxy = false)
        {
            _proxy = proxy;
        }
        public void Dispose()
        {

        }
        public List<EstatusAsignacionSubRolGeneralDefault> GeneraEstatusAsignacionGrupoDefault()
        {
            List<EstatusAsignacionSubRolGeneralDefault> result = new List<EstatusAsignacionSubRolGeneralDefault>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusAsignacionSubRolGeneralDefault.ToList();
                if (result != null)
                {
                    foreach (EstatusAsignacionSubRolGeneralDefault data in result)
                    {
                        db.LoadProperty(data, "Rol");
                        db.LoadProperty(data, "SubRol");
                        db.LoadProperty(data, "EstatusAsignacionActual");
                        db.LoadProperty(data, "EstatusAsignacionAccion");

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public void HabilitarPoliticaAsignacion(int idAsignacion, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                EstatusAsignacionSubRolGeneralDefault inf = db.EstatusAsignacionSubRolGeneralDefault.SingleOrDefault(w => w.Id == idAsignacion);
                if (inf != null) inf.Habilitado = habilitado;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public List<EstatusTicketSubRolGeneralDefault> GeneraEstatusTicketSubRolGeneralDefault()
        {
            List<EstatusTicketSubRolGeneralDefault> result = new List<EstatusTicketSubRolGeneralDefault>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusTicketSubRolGeneralDefault.ToList();
                if (result != null)
                {
                    foreach (EstatusTicketSubRolGeneralDefault data in result)
                    {
                        db.LoadProperty(data, "EstatusTicketActual");
                        db.LoadProperty(data, "EstatusTicketAccion");
                        db.LoadProperty(data, "RolSolicita");
                        db.LoadProperty(data, "SubSolicita");
                        db.LoadProperty(data, "RolPertenece");
                        db.LoadProperty(data, "SubRolPertenece");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }
        //Modificar
        public void HabilitarPoliticaEstatus(int idAsignacion, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                EstatusTicketSubRolGeneralDefault inf = db.EstatusTicketSubRolGeneralDefault.SingleOrDefault(w => w.Id == idAsignacion);
                if (inf != null) inf.Habilitado = habilitado;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public List<SubRolEscalacionPermitida> GeneraSubRolEscalacionPermitida()
        {
            List<SubRolEscalacionPermitida> result = new List<SubRolEscalacionPermitida>();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.SubRolEscalacionPermitida.ToList();
                if (result != null)
                {
                    foreach (SubRolEscalacionPermitida data in result)
                    {
                        db.LoadProperty(data, "SubRol");
                        db.LoadProperty(data, "SubRolPermitido");
                        db.LoadProperty(data, "EstatusAsignacion");                    
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public void HabilitarPoliticaEscalacion(int idEscalacion, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                SubRolEscalacionPermitida inf = db.SubRolEscalacionPermitida.SingleOrDefault(w => w.Id == idEscalacion);
                if (inf != null) inf.Habilitado = habilitado;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

    }


}
