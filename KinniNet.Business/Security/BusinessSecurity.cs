using System;
using System.Collections.Generic;
using System.Linq;
using KiiniNet.Entities.Cat.Arbol.Nodos;
using KiiniNet.Entities.Cat.Operacion;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Operacion.Usuarios;
using KinniNet.Business.Utils;
using KinniNet.Core.Operacion;
using KinniNet.Data.Help;

namespace KinniNet.Core.Security
{
    public class BusinessSecurity : IDisposable
    {
        public void Dispose()
        {

        }
        public class Autenticacion : IDisposable
        {
            private bool _proxy;
            public Autenticacion(bool proxy = false)
            {
                _proxy = proxy;
            }

            public void Dispose()
            {

            }

            public bool Autenticate(string user, string password)
            {

                bool result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    result = db.Usuario.Any(w => w.NombreUsuario == user && w.Password == password);
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

            public Usuario GetUserDataAutenticate(string user, string password)
            {
                Usuario result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    result = db.Usuario.SingleOrDefault(w => w.NombreUsuario == user && w.Password == password);
                    if (result != null)
                    {
                        db.LoadProperty(result, "CorreoUsuario");
                        db.LoadProperty(result, "TelefonoUsuario");
                        db.LoadProperty(result, "Organizacion");
                        db.LoadProperty(result, "Ubicacion");
                        db.LoadProperty(result, "TipoUsuario");
                        db.LoadProperty(result, "UsuarioGrupo");
                        foreach (UsuarioGrupo grupo in result.UsuarioGrupo)
                        {
                            db.LoadProperty(grupo, "GrupoUsuario");
                            if (grupo.GrupoUsuario != null)
                                db.LoadProperty(grupo.GrupoUsuario, "GrupoUsuarioInventarioArbol");
                            foreach (GrupoUsuarioInventarioArbol inventarioArbol in grupo.GrupoUsuario.GrupoUsuarioInventarioArbol)
                            {
                                db.LoadProperty(inventarioArbol, "InventarioArbolAcceso");
                                if (inventarioArbol.InventarioArbolAcceso != null)
                                    db.LoadProperty(inventarioArbol.InventarioArbolAcceso, "ArbolAcceso");
                            }
                        }
                        db.LoadProperty(result, "UsuarioRol");
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
        }

        public class Menus : IDisposable
        {
            private bool _proxy;
            public Menus(bool proxy = false)
            {
                _proxy = proxy;
            }
            public void Dispose()
            {

            }

            public List<Menu> ObtenerMenuUsuario(int idUsuario)
            {
                List<Menu> result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    result = ((IQueryable<Menu>)from ur in db.UsuarioRol
                                                join rtu in db.RolTipoUsuario on ur.IdRolTipoUsuario equals rtu.Id
                                                join rm in db.RolMenu on rtu.IdRol equals rm.IdRol
                                                join m in db.Menu on rm.IdMenu equals m.Id
                                                where ur.IdUsuario == idUsuario
                                                select m).OrderBy(o=>o.Id).Distinct().ToList();
                    foreach (Menu menu in result)
                    {
                        List<ArbolAcceso> lstArboles = null;
                        switch (menu.Id)
                        {
                            case (int)BusinessVariables.EnumMenu.Consultas:
                                lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByUsuarioTipoArbol(idUsuario, (int)BusinessVariables.EnumTipoArbol.Consultas).Distinct().ToList();
                                GeneraSubMenus(menu, lstArboles, db, "~/General/FrmNodoConsultas.aspx?IdArbol=");
                                break;
                            case (int)BusinessVariables.EnumMenu.Servicio:
                                lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByUsuarioTipoArbol(idUsuario, (int)BusinessVariables.EnumTipoArbol.Servicio).Distinct().ToList();
                                GeneraSubMenus(menu, lstArboles, db, "~/Ticket/FrmTicket.aspx?IdArbol=");
                                break;
                            case (int)BusinessVariables.EnumMenu.Incidentes:
                                lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByUsuarioTipoArbol(idUsuario, (int)BusinessVariables.EnumTipoArbol.Incidentes).Distinct().ToList();
                                GeneraSubMenus(menu, lstArboles, db, "~/Ticket/FrmTicket.aspx?IdArbol=");
                                break;
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

            private void GeneraSubMenus(Menu menu, List<ArbolAcceso> lstArboles, DataBaseModelContext db, string url)
            {
                try
                {
                    foreach (ArbolAcceso arbol in lstArboles)
                    {
                        if (arbol.Nivel1 != null)
                        {
                            if (menu.Menu1 == null)
                                menu.Menu1 = new List<Menu>();
                            Nivel1 n = db.Nivel1.SingleOrDefault(s => s.Id == arbol.Nivel1.Id);
                            if (n == null) continue;
                            if (!menu.Menu1.Any(a => a.Id == n.Id))
                            {
                                Menu menuNivel1 = new Menu
                                {
                                    Descripcion = n.Descripcion,
                                    Id = n.Id,
                                    Url = arbol.Nivel2 != null ? string.Empty : url + arbol.Id
                                };
                                menu.Menu1.Add(menuNivel1);
                            }
                        }

                        if (arbol.Nivel2 != null)
                        {
                            if (menu.Menu1[0].Menu1 == null)
                                menu.Menu1[0].Menu1 = new List<Menu>();
                            Nivel2 n = db.Nivel2.SingleOrDefault(s => s.Id == arbol.Nivel2.Id);
                            if (n == null) continue;
                            if (!menu.Menu1[0].Menu1.Any(a => a.Id == n.Id))
                            {
                                Menu menuNivel1 = new Menu
                                {
                                    Descripcion = n.Descripcion,
                                    Id = n.Id,
                                    Url = arbol.Nivel3 != null ? string.Empty : url + arbol.Id
                                };
                                menu.Menu1[0].Menu1.Add(menuNivel1);
                            }
                        }

                        if (arbol.Nivel3 != null)
                        {
                            if (menu.Menu1[0].Menu1[0].Menu1 == null)
                                menu.Menu1[0].Menu1[0].Menu1 = new List<Menu>();
                            Nivel3 n = db.Nivel3.SingleOrDefault(s => s.Id == arbol.Nivel3.Id);
                            if (n == null) continue;
                            if (!menu.Menu1[0].Menu1[0].Menu1.Any(a => a.Id == n.Id))
                            {
                                Menu menuNivel1 = new Menu
                                {
                                    Descripcion = n.Descripcion,
                                    Id = n.Id,
                                    Url = arbol.Nivel4 != null ? string.Empty : url + arbol.Id
                                };
                                menu.Menu1[0].Menu1[0].Menu1.Add(menuNivel1);
                            }
                        }

                        if (arbol.Nivel4 != null)
                        {
                            if (menu.Menu1[0].Menu1[0].Menu1[0].Menu1 == null)
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1 = new List<Menu>();
                            Nivel4 n = db.Nivel4.SingleOrDefault(s => s.Id == arbol.Nivel4.Id);
                            if (n == null) continue;
                            if (!menu.Menu1[0].Menu1[0].Menu1[0].Menu1.Any(a => a.Id == n.Id))
                            {
                                Menu menuNivel1 = new Menu
                                {
                                    Descripcion = n.Descripcion,
                                    Id = n.Id,
                                    Url = arbol.Nivel5 != null ? string.Empty : url + arbol.Id
                                };
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1.Add(menuNivel1);
                            }
                        }

                        if (arbol.Nivel5 != null)
                        {
                            if (menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1 == null)
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1 = new List<Menu>();
                            Nivel5 n = db.Nivel5.SingleOrDefault(s => s.Id == arbol.Nivel5.Id);
                            if (n == null) continue;
                            if (!menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1.Any(a => a.Id == n.Id))
                            {
                                Menu menuNivel1 = new Menu
                                {
                                    Descripcion = n.Descripcion,
                                    Id = n.Id,
                                    Url = arbol.Nivel6 != null ? string.Empty : url + arbol.Id
                                };
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1.Add(menuNivel1);
                            }
                        }

                        if (arbol.Nivel6 != null)
                        {
                            if (menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1 == null)
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1 = new List<Menu>();
                            Nivel6 n = db.Nivel6.SingleOrDefault(s => s.Id == arbol.Nivel6.Id);
                            if (n == null) continue;
                            if (!menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1.Any(a => a.Id == n.Id))
                            {
                                Menu menuNivel1 = new Menu
                                {
                                    Descripcion = n.Descripcion,
                                    Id = n.Id,
                                    Url = arbol.Nivel7 != null ? string.Empty : url + arbol.Id
                                };
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1.Add(menuNivel1);
                            }
                        }
                        if (arbol.Nivel7 != null)
                        {
                            if (menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1 == null)
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1 = new List<Menu>();
                            Nivel5 n = db.Nivel5.SingleOrDefault(s => s.Id == arbol.Nivel7.Id);
                            if (n == null) continue;
                            if (!menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1.Any(a => a.Id == n.Id))
                            {
                                Menu menuNivel1 = new Menu
                                {
                                    Descripcion = n.Descripcion,
                                    Id = n.Id,
                                    Url = url + arbol.Id
                                };
                                menu.Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1[0].Menu1.Add(menuNivel1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
