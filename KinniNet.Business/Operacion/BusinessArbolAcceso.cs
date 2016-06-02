using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Arbol.Nodos;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Usuario;
using KiiniNet.Entities.Operacion;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessArbolAcceso : IDisposable
    {
        private bool _proxy;
        public void Dispose()
        {

        }
        public BusinessArbolAcceso(bool proxy = false)
        {
            _proxy = proxy;
        }
        public List<Nivel1> ObtenerNivel1(int idTipoArbol, int idTipoUsuario, bool insertarSeleccion)
        {
            List<Nivel1> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.Where(w => w.IdTipoArbolAcceso == idTipoArbol && w.IdTipoUsuario == idTipoUsuario).SelectMany(nivel => db.Nivel1.Where(w => w.Id == nivel.IdNivel1)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Nivel1 { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Nivel2> ObtenerNivel2(int idTipoArbol, int idTipoUsuario, int idNivel1, bool insertarSeleccion)
        {
            List<Nivel2> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.Where(w => w.IdTipoArbolAcceso == idTipoArbol && w.IdTipoUsuario == idTipoUsuario && w.IdNivel1 == idNivel1 && w.Habilitado).SelectMany(nivel => db.Nivel2.Where(w => w.Id == nivel.IdNivel2)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Nivel2 { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Nivel3> ObtenerNivel3(int idTipoArbol, int idTipoUsuario, int idNivel2, bool insertarSeleccion)
        {
            List<Nivel3> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.Where(w => w.IdTipoArbolAcceso == idTipoArbol && w.IdTipoUsuario == idTipoUsuario && w.IdNivel2 == idNivel2 && w.Habilitado).SelectMany(nivel => db.Nivel3.Where(w => w.Id == nivel.IdNivel3)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Nivel3 { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Nivel4> ObtenerNivel4(int idTipoArbol, int idTipoUsuario, int idNivel3, bool insertarSeleccion)
        {
            List<Nivel4> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.Where(w => w.IdTipoArbolAcceso == idTipoArbol && w.IdTipoUsuario == idTipoUsuario && w.IdNivel3 == idNivel3 && w.Habilitado).SelectMany(nivel => db.Nivel4.Where(w => w.Id == nivel.IdNivel4)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Nivel4 { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Nivel5> ObtenerNivel5(int idTipoArbol, int idTipoUsuario, int idNivel4, bool insertarSeleccion)
        {
            List<Nivel5> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.Where(w => w.IdTipoArbolAcceso == idTipoArbol && w.IdTipoUsuario == idTipoUsuario && w.IdNivel4 == idNivel4 && w.Habilitado).SelectMany(nivel => db.Nivel5.Where(w => w.Id == nivel.IdNivel5)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Nivel5 { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Nivel6> ObtenerNivel6(int idTipoArbol, int idTipoUsuario, int idNivel5, bool insertarSeleccion)
        {
            List<Nivel6> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.Where(w => w.IdTipoArbolAcceso == idTipoArbol && w.IdTipoUsuario == idTipoUsuario && w.IdNivel5 == idNivel5 && w.Habilitado).SelectMany(nivel => db.Nivel6.Where(w => w.Id == nivel.IdNivel6)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Nivel6 { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public List<Nivel7> ObtenerNivel7(int idTipoArbol, int idTipoUsuario, int idNivel6, bool insertarSeleccion)
        {
            List<Nivel7> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.Where(w => w.IdTipoArbolAcceso == idTipoArbol && w.IdTipoUsuario == idTipoUsuario && w.IdNivel6 == idNivel6 && w.Habilitado).SelectMany(nivel => db.Nivel7.Where(w => w.Id == nivel.IdNivel7)).Distinct().OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Nivel7 { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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
        public bool EsNodoTerminal(int idTipoUsuario, int idTipoArbol, int nivel1, int? nivel2, int? nivel3, int? nivel4, int? nivel5, int? nivel6, int? nivel7)
        {
            bool result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                var qry = db.ArbolAcceso.Where(w => w.IdTipoUsuario == idTipoUsuario && w.IdTipoArbolAcceso == idTipoArbol && w.IdNivel1 == nivel1);
                qry = nivel2.HasValue ? qry.Where(w => w.IdNivel2 == nivel2) : qry.Where(w => w.IdNivel2 == null);
                qry = nivel3.HasValue ? qry.Where(w => w.IdNivel3 == nivel3) : qry.Where(w => w.IdNivel3 == null);
                qry = nivel4.HasValue ? qry.Where(w => w.IdNivel4 == nivel4) : qry.Where(w => w.IdNivel4 == null);
                qry = nivel5.HasValue ? qry.Where(w => w.IdNivel5 == nivel5) : qry.Where(w => w.IdNivel5 == null);
                qry = nivel6.HasValue ? qry.Where(w => w.IdNivel6 == nivel6) : qry.Where(w => w.IdNivel6 == null);
                qry = nivel7.HasValue ? qry.Where(w => w.IdNivel7 == nivel7) : qry.Where(w => w.IdNivel7 == null);
                result = qry.Any(a => a.EsTerminal);
            }
            catch (Exception)
            {
                throw new Exception("Error al Obtener Organizacion");
            }
            finally
            {
                db.Dispose();
            }
            return result;
        }
        public void GuardarArbol(ArbolAcceso arbol)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                //TODO: Cambiar habilitado por el embebido
                arbol.Habilitado = true;
                List<GrupoUsuario> lstGrupoUsuario = null;
                if (arbol.Nivel1 != null)
                {
                    arbol.Nivel1.Descripcion = arbol.Nivel1.Descripcion.ToUpper();
                    arbol.Nivel1.Habilitado = arbol.Nivel1.Habilitado;
                    if (!arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                        lstGrupoUsuario = new BusinessGrupoUsuario().ObtenerGruposUsuarioSistema();
                }
                if (arbol.Nivel2 != null)
                {
                    arbol.Nivel2.Descripcion = arbol.Nivel2.Descripcion.ToUpper();
                    arbol.Nivel2.Habilitado = arbol.Nivel2.Habilitado;
                    if (!arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                        lstGrupoUsuario = new BusinessGrupoUsuario().ObtenerGruposUsuarioNivel(arbol.IdTipoArbolAcceso, arbol.IdNivel1, null, null, null, null, null, null);
                }
                if (arbol.Nivel3 != null)
                {
                    arbol.Nivel3.Descripcion = arbol.Nivel3.Descripcion.ToUpper();
                    arbol.Nivel3.Habilitado = arbol.Nivel3.Habilitado;
                    if (!arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                        lstGrupoUsuario = new BusinessGrupoUsuario().ObtenerGruposUsuarioNivel(arbol.IdTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, null, null, null, null, null);
                }
                if (arbol.Nivel4 != null)
                {
                    arbol.Nivel4.Descripcion = arbol.Nivel4.Descripcion.ToUpper();
                    arbol.Nivel4.Habilitado = arbol.Nivel4.Habilitado;
                    if (!arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                        lstGrupoUsuario = new BusinessGrupoUsuario().ObtenerGruposUsuarioNivel(arbol.IdTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, null, null, null, null);
                }
                if (arbol.Nivel5 != null)
                {
                    arbol.Nivel5.Descripcion = arbol.Nivel5.Descripcion.ToUpper();
                    arbol.Nivel5.Habilitado = arbol.Nivel5.Habilitado;
                    if (!arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                        lstGrupoUsuario = new BusinessGrupoUsuario().ObtenerGruposUsuarioNivel(arbol.IdTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, arbol.IdNivel4, null, null, null);
                }
                if (arbol.Nivel6 != null)
                {
                    arbol.Nivel6.Descripcion = arbol.Nivel6.Descripcion.ToUpper();
                    arbol.Nivel6.Habilitado = arbol.Nivel6.Habilitado;
                    if (!arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                        lstGrupoUsuario = new BusinessGrupoUsuario().ObtenerGruposUsuarioNivel(arbol.IdTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, arbol.IdNivel4, arbol.IdNivel5, null, null);
                }
                if (arbol.Nivel7 != null)
                {
                    arbol.Nivel7.Descripcion = arbol.Nivel7.Descripcion.ToUpper();
                    arbol.Nivel7.Habilitado = arbol.Nivel7.Habilitado;
                    if (!arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                        lstGrupoUsuario = new BusinessGrupoUsuario().ObtenerGruposUsuarioNivel(arbol.IdTipoArbolAcceso, arbol.IdNivel1, arbol.IdNivel2, arbol.IdNivel3, arbol.IdNivel4, arbol.IdNivel5, arbol.IdNivel6, null);
                }
                if (arbol.InventarioArbolAcceso != null && !arbol.InventarioArbolAcceso.First().GrupoUsuarioInventarioArbol.Any())
                {
                    if (lstGrupoUsuario != null)
                        foreach (GrupoUsuario grupo in lstGrupoUsuario)
                        {
                            if (grupo.SubGrupoUsuario.Any())
                            {
                                foreach (SubGrupoUsuario subGrupo in grupo.SubGrupoUsuario)
                                {
                                    arbol.InventarioArbolAcceso[0].GrupoUsuarioInventarioArbol.Add(new GrupoUsuarioInventarioArbol
                                    {
                                        IdGrupoUsuario = grupo.Id,
                                        IdRol = subGrupo.SubRol.IdRol,
                                        IdSubGrupoUsuario = subGrupo.Id
                                    });
                                }
                            }
                            else
                            {
                                arbol.InventarioArbolAcceso[0].GrupoUsuarioInventarioArbol.Add(new GrupoUsuarioInventarioArbol
                                {
                                    IdGrupoUsuario = grupo.Id,
                                    IdRol = grupo.TipoGrupo.RolTipoGrupo.First().IdRol,
                                    IdSubGrupoUsuario = null
                                });
                            }
                        }
                }

                if (arbol.Id == 0)
                    db.ArbolAcceso.AddObject(arbol);
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
        public List<ArbolAcceso> ObtenerArbolesAccesoByUsuarioTipoArbol(int idUsuario, int idTipoArbol)
        {
            List<ArbolAcceso> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<ArbolAcceso> qry = from ac in db.ArbolAcceso
                                              join iac in db.InventarioArbolAcceso on ac.Id equals iac.IdArbolAcceso
                                              join guia in db.GrupoUsuarioInventarioArbol on iac.Id equals guia.IdInventarioArbolAcceso
                                              join ug in db.UsuarioGrupo on new { guia.IdRol, guia.IdGrupoUsuario, guia.IdSubGrupoUsuario } equals new { ug.IdRol, ug.IdGrupoUsuario, ug.IdSubGrupoUsuario }
                                              where ug.IdUsuario == idUsuario && ac.IdTipoArbolAcceso == idTipoArbol
                                              select ac;
                var m = qry.ToList();
                result = qry.ToList();
                foreach (ArbolAcceso arbol in result)
                {
                    db.LoadProperty(arbol, "Nivel1");
                    db.LoadProperty(arbol, "Nivel2");
                    db.LoadProperty(arbol, "Nivel3");
                    db.LoadProperty(arbol, "Nivel4");
                    db.LoadProperty(arbol, "Nivel5");
                    db.LoadProperty(arbol, "Nivel6");
                    db.LoadProperty(arbol, "Nivel7");
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
        public ArbolAcceso ObtenerArbolAcceso(int idArbol)
        {
            ArbolAcceso result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.ArbolAcceso.SingleOrDefault(w => w.Habilitado && w.Id == idArbol);
                if (result == null) return null;
                db.LoadProperty(result, "Nivel1");
                db.LoadProperty(result, "Nivel2");
                db.LoadProperty(result, "Nivel3");
                db.LoadProperty(result, "Nivel4");
                db.LoadProperty(result, "Nivel5");
                db.LoadProperty(result, "Nivel6");
                db.LoadProperty(result, "Nivel7");
                db.LoadProperty(result, "InventarioArbolAcceso");
                foreach (InventarioArbolAcceso inventarioArbol in result.InventarioArbolAcceso)
                {
                    db.LoadProperty(inventarioArbol, "InventarioInfConsulta");
                    foreach (InventarioInfConsulta inventarioInformacion in inventarioArbol.InventarioInfConsulta)
                    {
                        db.LoadProperty(inventarioInformacion, "InformacionConsulta");
                    }
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

        public List<GrupoUsuarioInventarioArbol> ObtenerGruposUsuarioArbol(int idArbol)
        {
            List<GrupoUsuarioInventarioArbol> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<GrupoUsuarioInventarioArbol> qry = from ac in db.ArbolAcceso
                                                              join iac in db.InventarioArbolAcceso on ac.Id equals iac.IdArbolAcceso
                                                              join guia in db.GrupoUsuarioInventarioArbol on iac.Id equals guia.IdInventarioArbolAcceso
                                                              select guia;
                result = qry.Distinct().ToList();
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
    }
}
