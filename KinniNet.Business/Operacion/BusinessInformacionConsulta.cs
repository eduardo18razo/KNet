using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessInformacionConsulta : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessInformacionConsulta(bool proxy = false)
        {
            _proxy = proxy;
        }

        public List<InformacionConsulta> ObtenerInformacionConsulta(BusinessVariables.EnumTiposInformacionConsulta tipoinfoConsulta, bool insertarSeleccion)
        {
            List<InformacionConsulta> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.InformacionConsulta.Where(w => w.IdTipoInfConsulta == (int)tipoinfoConsulta && w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new InformacionConsulta
                        {
                            Id = BusinessVariables.ComboBoxCatalogo.Value,
                            Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion
                        });
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public void GuardarInformacionConsulta(InformacionConsulta informacion)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                informacion.Descripcion = informacion.Descripcion.Trim().ToUpper();
                switch (informacion.IdTipoInfConsulta)
                {
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Texto:

                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:

                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                        if (!informacion.InformacionConsultaDatos.First().Descripcion.StartsWith("http://"))
                            informacion.InformacionConsultaDatos.First().Descripcion = "http://" + informacion.InformacionConsultaDatos.First().Descripcion;
                        break;
                    default:
                        throw new Exception("Seleccione un tipo de información");
                }
                //TODO: Cambiar habilitado por el embebido
                informacion.Habilitado = true;
                if (informacion.Id == 0)
                    db.InformacionConsulta.AddObject(informacion);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public void ActualizarInformacionConsulta(int idInformacionConsulta, InformacionConsulta informacion)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.LazyLoadingEnabled = true;
                InformacionConsulta info = db.InformacionConsulta.SingleOrDefault(s => s.Id == idInformacionConsulta);
                if (info == null) return;
                info.Descripcion = informacion.Descripcion.Trim().ToUpper();
                info.IdTipoInfConsulta = informacion.IdTipoInfConsulta;
                info.IdTipoDocumento = informacion.IdTipoDocumento;
                //TODO: Cambiar habilitado por el embebido
                info.Habilitado = true;
                switch (informacion.IdTipoInfConsulta)
                {
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Texto:
                        for (int i = 0; i < info.InformacionConsultaDatos.Count; i++)
                        {
                            info.InformacionConsultaDatos[i].Descripcion = informacion.InformacionConsultaDatos[i].Descripcion;
                        }
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.Documento:
                        for (int i = 0; i < info.InformacionConsultaDatos.Count; i++)
                        {
                            info.InformacionConsultaDatos[i].Descripcion = informacion.InformacionConsultaDatos[i].Descripcion;
                        }
                        break;
                    case (int)BusinessVariables.EnumTiposInformacionConsulta.PaginaHtml:
                        for (int i = 0; i < info.InformacionConsultaDatos.Count; i++)
                        {
                            if (!informacion.InformacionConsultaDatos.First().Descripcion.StartsWith("http://"))
                                info.InformacionConsultaDatos.First().Descripcion = "http://" + informacion.InformacionConsultaDatos.First().Descripcion;
                        }
                        break;
                    default:
                        throw new Exception("Seleccione un tipo de información");
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public List<InformacionConsulta> ObtenerInformacionConsultaArbol(int idArbol)
        {
            List<InformacionConsulta> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.InventarioArbolAcceso.Join(db.InventarioInfConsulta, iaa => iaa.Id, iic => iic.IdInventario,
                        (iaa, iic) => new { iaa, iic })
                        .Join(db.InformacionConsulta, @t => @t.iic.IdInfConsulta, ic => ic.Id, (@t, ic) => new { @t, ic })
                        .Where(@t => @t.@t.iaa.IdArbolAcceso == idArbol)
                        .Select(@t => @t.ic).ToList();
                foreach (InformacionConsulta informacionConsulta in result)
                {
                    db.LoadProperty(informacionConsulta, "TipoInfConsulta");
                }
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public InformacionConsulta ObtenerInformacionConsultaById(int idInformacion)
        {
            InformacionConsulta result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.InformacionConsulta.SingleOrDefault(w => w.Id == idInformacion);
                if (result != null)
                {
                    db.LoadProperty(result, "TipoInfConsulta");
                    db.LoadProperty(result, "InformacionConsultaDatos");
                }
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public List<InformacionConsulta> ObtenerInformacionConsulta(int? idTipoInformacionConsulta, int? idTipoDocumento)
        {
            List<InformacionConsulta> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<InformacionConsulta> qry = db.InformacionConsulta;
                if (idTipoInformacionConsulta != null)
                    qry = qry.Where(w => w.IdTipoInfConsulta == idTipoInformacionConsulta);
                if (idTipoDocumento != null)
                    qry = qry.Where(w => w.IdTipoDocumento == idTipoDocumento);
                result = qry.ToList();
                foreach (InformacionConsulta consulta in result)
                {
                    db.LoadProperty(consulta, "TipoInfConsulta");
                    db.LoadProperty(consulta, "TipoDocumento");
                }
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }

        public void GuardarHit(int idArbol, int idUsuario)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                HitConsulta hit = new HitConsulta
                {
                    IdTipoArbolAcceso = new BusinessArbolAcceso().ObtenerArbolAcceso(idArbol).IdTipoArbolAcceso,
                    IdArbolAcceso = idArbol,
                    IdUsuario = idUsuario,
                    IdUbicacion = new BusinessUbicacion().ObtenerUbicacionUsuario(new BusinessUsuarios().ObtenerUsuario(idUsuario).IdUbicacion).Id,
                    IdOrganizacion = new BusinessOrganizacion().ObtenerOrganizacionUsuario(new BusinessUsuarios().ObtenerUsuario(idUsuario).IdOrganizacion).Id,
                    HitGrupoUsuario = new List<HitGrupoUsuario>(),
                    FechaHoraAlta = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), "yyyy-MM-dd HH:mm:ss:fff", CultureInfo.InvariantCulture)
                };
                foreach (GrupoUsuarioInventarioArbol guia in new BusinessArbolAcceso().ObtenerGruposUsuarioArbol(idArbol))
                {
                    hit.HitGrupoUsuario.Add(new HitGrupoUsuario
                    {
                        IdRol = guia.IdRol,
                        IdGrupoUsuario = guia.IdGrupoUsuario,
                        IdSubGrupoUsuario = guia.IdSubGrupoUsuario
                    });
                }

                if (hit.Id == 0)
                    db.HitConsulta.AddObject(hit);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public void HabilitarInformacion(int idInformacion, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                InformacionConsulta inf = db.InformacionConsulta.SingleOrDefault(w => w.Id == idInformacion);
                if (inf != null) inf.Habilitado = habilitado;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
        }

    }
}
