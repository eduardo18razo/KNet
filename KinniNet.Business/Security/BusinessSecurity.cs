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
                    if (db.Usuario.Count(w => w.NombreUsuario == user && w.Password == password) > 1)
                        throw new Exception("Error al obtener informacion consulte a su Administrador");
                    result = db.Usuario.SingleOrDefault(w => w.NombreUsuario == user && w.Password == password);
                    if (result != null)
                    {
                        db.LoadProperty(result, "Organizacion");
                        db.LoadProperty(result, "Ubicacion");
                        db.LoadProperty(result, "TipoUsuario");
                        db.LoadProperty(result, "UsuarioRol");
                        foreach (UsuarioRol rol in result.UsuarioRol)
                        {
                            db.LoadProperty(rol, "RolTipoUsuario");
                        }
                        db.LoadProperty(result, "UsuarioGrupo");
                        foreach (UsuarioGrupo grupo in result.UsuarioGrupo)
                        {
                            db.LoadProperty(grupo, "SubGrupoUsuario");
                        }

                    }
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

            public Usuario GetUserInvitadoDataAutenticate(int idTipoUsuario)
            {
                Usuario result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    if (db.Usuario.Count(w => w.IdTipoUsuario == idTipoUsuario) > 1)
                        throw new Exception("Error al obtener informacion consulte a su Administrador");
                    result = db.Usuario.SingleOrDefault(w => w.IdTipoUsuario == idTipoUsuario);
                    if (result != null)
                    {
                        db.LoadProperty(result, "Organizacion");
                        db.LoadProperty(result, "Ubicacion");
                        db.LoadProperty(result, "TipoUsuario");
                        db.LoadProperty(result, "UsuarioRol");
                        foreach (UsuarioRol rol in result.UsuarioRol)
                        {
                            db.LoadProperty(rol, "RolTipoUsuario");
                        }
                        db.LoadProperty(result, "UsuarioGrupo");
                        foreach (UsuarioGrupo grupo in result.UsuarioGrupo)
                        {
                            db.LoadProperty(grupo, "SubGrupoUsuario");
                        }

                    }
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

            public List<Menu> ObtenerMenuUsuario(int idUsuario, int idArea, bool arboles)
            {
                List<Menu> result = new List<Menu>();
                DataBaseModelContext db = new DataBaseModelContext();
                const string menuPadre = "Menu2";
                const string menuHijo = "Menu1";
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    IQueryable<Menu> qry = from ur in db.UsuarioRol
                                           join rtu in db.RolTipoUsuario on ur.IdRolTipoUsuario equals rtu.Id
                                           join rm in db.RolMenu on rtu.IdRol equals rm.IdRol
                                           join m in db.Menu on rm.IdMenu equals m.Id
                                           where ur.IdUsuario == idUsuario
                                           select m;
                    foreach (Menu menu in qry.Where(w => w.IdPadre == null).Distinct())
                    {
                        result.Add(menu);
                        db.LoadProperty(menu, menuHijo);
                        if (menu.Menu1 != null && menu.Menu1.Count == 0) menu.Menu1 = null;
                        if (menu.Menu1 == null) continue;
                        foreach (Menu menu1 in menu.Menu1)
                        {
                            db.LoadProperty(menu1, menuHijo);
                            if (menu1.Menu1 != null && menu1.Menu1.Count == 0) menu1.Menu1 = null;
                            if (menu1.Menu1 == null) continue;
                            foreach (Menu menu2 in menu1.Menu1)
                            {
                                db.LoadProperty(menu2, menuHijo);
                                if (menu2.Menu1 != null && menu2.Menu1.Count == 0) menu2.Menu1 = null;
                                if (menu2.Menu1 == null) continue;
                                foreach (Menu menu3 in menu2.Menu1)
                                {
                                    db.LoadProperty(menu3, menuHijo);
                                    if (menu3.Menu1 != null && menu3.Menu1.Count == 0) menu3.Menu1 = null;
                                }
                            }
                        }
                    }

                    foreach (Menu menu in qry.Where(w => w.IdPadre != null).Distinct())
                    {
                        db.LoadProperty(menu, menuPadre);
                        if (menu.Menu2 != null)
                        {
                            db.LoadProperty(menu.Menu2, menuPadre);
                            if (menu.Menu2.Menu2 != null)
                            {
                                db.LoadProperty(menu.Menu2.Menu2, menuPadre);
                                if (menu.Menu2.Menu2.Menu2 != null)
                                {
                                    result.Add(menu.Menu2.Menu2.Menu2);
                                }
                                else
                                result.Add(menu.Menu2.Menu2);

                            }
                            else
                                result.Add(menu.Menu2);
                        }
                        else
                            result.Add(menu);
                    }

                    result = result.Distinct().ToList();

                    if (arboles)
                        foreach (Menu menu in result)
                        {

                            List<ArbolAcceso> lstArboles = null;
                            switch (menu.Id)
                            {
                                case (int)BusinessVariables.EnumMenu.Consultas:
                                    lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByUsuarioTipoArbol(idUsuario, (int)BusinessVariables.EnumTipoArbol.Consultas, idArea).Distinct().ToList();
                                    GeneraSubMenus(menu, lstArboles, db, "~/Users/General/FrmNodoConsultas.aspx?IdArbol=");
                                    break;
                                case (int)BusinessVariables.EnumMenu.Servicio:
                                    lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByUsuarioTipoArbol(idUsuario, (int)BusinessVariables.EnumTipoArbol.Servicio, idArea).Distinct().ToList();
                                    GeneraSubMenus(menu, lstArboles, db, "~/Users/Ticket/FrmTicket.aspx?IdArbol=");
                                    break;
                                case (int)BusinessVariables.EnumMenu.Incidentes:
                                    lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByUsuarioTipoArbol(idUsuario, (int)BusinessVariables.EnumTipoArbol.Incidentes, idArea).Distinct().ToList();
                                    GeneraSubMenus(menu, lstArboles, db, "~/Users/Ticket/FrmTicket.aspx?IdArbol=");
                                    break;
                            }
                        }
                    else
                    {
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Consultas));
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Servicio));
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Incidentes));
                    }

                    Menu menus = result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Consultas);
                    if (menus != null && menus.Menu1 == null)
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Consultas));
                    menus = result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Servicio);
                    if (menus != null && menus.Menu1 == null)
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Servicio));

                    menus = result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Incidentes);
                    if (menus != null && menus.Menu1 == null)
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Incidentes));
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

            public List<Menu> ObtenerMenuPublico(int idTipoUsuario, int idArea, bool arboles)
            {
                List<Menu> result;
                DataBaseModelContext db = new DataBaseModelContext();
                try
                {
                    db.ContextOptions.ProxyCreationEnabled = _proxy;
                    IQueryable<Menu> qry = from rtu in db.RolTipoUsuario
                                           join rm in db.RolMenu on rtu.IdRol equals rm.IdRol
                                           join m in db.Menu on rm.IdMenu equals m.Id
                                           where rtu.IdTipoUsuario == idTipoUsuario
                                           select m;
                    result = qry.OrderBy(o => o.Id).Distinct().ToList();
                    foreach (Menu menu in result)
                    {
                        db.LoadProperty(menu, "Menu1");
                        if (menu.Menu1.Count == 0) menu.Menu1 = null;
                        if (menu.Menu1 == null) continue;
                        foreach (Menu menu1 in menu.Menu1)
                        {
                            db.LoadProperty(menu1, "Menu1");
                            if (menu1.Menu1.Count == 0) menu1.Menu1 = null;
                            if (menu1.Menu1 == null) continue;
                            foreach (Menu menu2 in menu1.Menu1)
                            {
                                db.LoadProperty(menu2, "Menu1");
                                if (menu2.Menu1.Count == 0) menu2.Menu1 = null;
                                if (menu2.Menu1 == null) continue;
                                foreach (Menu menu3 in menu2.Menu1)
                                {
                                    db.LoadProperty(menu3, "Menu1");
                                    if (menu3.Menu1.Count == 0) menu3.Menu1 = null;
                                }
                            }
                        }
                    }
                    if (arboles)
                        foreach (Menu menu in result)
                        {

                            List<ArbolAcceso> lstArboles = null;
                            switch (menu.Id)
                            {
                                case (int)BusinessVariables.EnumMenu.Consultas:
                                    lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByTipoUsuarioTipoArbol(idTipoUsuario, (int)BusinessVariables.EnumTipoArbol.Consultas, idArea).Distinct().ToList();
                                    GeneraSubMenus(menu, lstArboles, db, "~/Users/General/FrmNodoConsultas.aspx?IdArbol=");
                                    break;
                                case (int)BusinessVariables.EnumMenu.Servicio:
                                    lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByTipoUsuarioTipoArbol(idTipoUsuario, (int)BusinessVariables.EnumTipoArbol.Servicio, idArea).Distinct().ToList();
                                    GeneraSubMenus(menu, lstArboles, db, "~/Users/Ticket/FrmTicket.aspx?IdArbol=");
                                    break;
                                case (int)BusinessVariables.EnumMenu.Incidentes:
                                    lstArboles = new BusinessArbolAcceso().ObtenerArbolesAccesoByTipoUsuarioTipoArbol(idTipoUsuario, (int)BusinessVariables.EnumTipoArbol.Incidentes, idArea).Distinct().ToList();
                                    GeneraSubMenus(menu, lstArboles, db, "~/Users/Ticket/FrmTicket.aspx?IdArbol=");
                                    break;
                            }
                        }
                    else
                    {
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Consultas));
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Servicio));
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Incidentes));
                    }

                    Menu menus = result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Consultas);
                    if (menus != null && menus.Menu1 == null)
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Consultas));
                    menus = result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Servicio);
                    if (menus != null && menus.Menu1 == null)
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Servicio));

                    menus = result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Incidentes);
                    if (menus != null && menus.Menu1 == null)
                        result.Remove(result.SingleOrDefault(s => s.Id == (int)BusinessVariables.EnumMenu.Incidentes));
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
}
