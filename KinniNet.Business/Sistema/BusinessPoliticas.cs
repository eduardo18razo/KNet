﻿using KiiniNet.Entities.Parametros;
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

        public List<EstatusAsignacionSubRolGeneralDefault> EstatusAsignacionSubRolGeneralDefault()
        {
            List<EstatusAsignacionSubRolGeneralDefault> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusAsignacionSubRolGeneralDefault.ToList();
                foreach (EstatusAsignacionSubRolGeneralDefault data in result)
                {
                    db.LoadProperty(data, "Rol");
                    db.LoadProperty(data, "SubRol");
                    db.LoadProperty(data, "EstatusAsignacionActual");
                    db.LoadProperty(data, "EstatusAsignacionAccion");

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public List<EstatusTicketSubRolGeneralDefault> EstatusTicketSubRolGeneralDefault()
        {
            List<EstatusTicketSubRolGeneralDefault> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusTicketSubRolGeneralDefault.ToList();
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public List<EstatusAsignacionSubRolGeneral> EstatusAsignacionSubRolGeneral()
        {
            List<EstatusAsignacionSubRolGeneral> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusAsignacionSubRolGeneral.ToList();
                foreach (EstatusAsignacionSubRolGeneral data in result)
                {
                    db.LoadProperty(data, "GrupoUsuario");
                    db.LoadProperty(data, "Rol");
                    db.LoadProperty(data, "SubRol");
                    db.LoadProperty(data, "EstatusAsignacionActual");
                    db.LoadProperty(data, "EstatusAsignacionAccion");

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally { db.Dispose(); }
            return result;
        }

        public List<EstatusTicketSubRolGeneral> EstatusTicketSubRolGeneral()
        {
            List<EstatusTicketSubRolGeneral> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.EstatusTicketSubRolGeneral.ToList();
                foreach (EstatusTicketSubRolGeneral data in result)
                {
                    db.LoadProperty(data, "GrupoUsuario");
                    db.LoadProperty(data, "EstatusTicketActual");
                    db.LoadProperty(data, "EstatusTicketAccion");
                    db.LoadProperty(data, "RolSolicita");
                    db.LoadProperty(data, "SubSolicita");
                    db.LoadProperty(data, "RolPertenece");
                    db.LoadProperty(data, "SubRolPertenece");
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
    }


}
