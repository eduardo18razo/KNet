using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Helper;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessCatalogos : IDisposable
    {
        private bool _proxy;
        public BusinessCatalogos(bool proxy = false)
        {
            _proxy = proxy;
        }

        public void Dispose()
        { }

        public List<Catalogos> ObtenerCatalogos(bool insertarSeleccion)
        {
            List<Catalogos> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Catalogos.OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Catalogos
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

        public List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion)
        {
            List<Catalogos> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Catalogos.Where(w => w.EsMascaraCaptura && w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index,
                        new Catalogos
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

        public bool ExisteMascara(string nombreTabla)
        {
            bool result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                var pTableName = new SqlParameter { ParameterName = "@TABLENAME", Value = nombreTabla };
                var pResult = new SqlParameter { ParameterName = "@OUTER", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                db.ExecuteStoreCommand("exec ExisteTablaCatalogo @TABLENAME, @OUTER output", pTableName, pResult);
                result = (int)pResult.Value == 1;
                if (result)
                    throw new Exception("Ya Existe");

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

        private bool CreaEstructuraBaseDatos(string nombreTabla)
        {
            try
            {
                if (CreaTabla(nombreTabla)) ;
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            return true;
        }
        private bool CreaTabla(string nombreCatalogo)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                string qryCrearTablas = String.Format("CREATE TABLE {0} ( \n" +
                                                      "Id int IDENTITY(1,1) NOT NULL, \n" +
                                                      "[Descripcion] [nvarchar](MAX) NOT NULL," +
                                                      "Habilitado BIT \n" +
                                                      "CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED \n" +
                                                      "( \n" +
                                                      "\t[Id] ASC \n" +
                                                      ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] \n" +
                                                      ") ON [PRIMARY] \n " +
                                                      "ALTER TABLE [dbo].[{0}] ADD  CONSTRAINT [DF_{0}_habilitado]  DEFAULT ((1)) FOR [Habilitado]", nombreCatalogo);

                db.ExecuteStoreCommand(qryCrearTablas);
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return true;
        }

        public void CrearCatalogo(string nombreCatalogo, bool esMascara)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                nombreCatalogo = nombreCatalogo.Trim().ToUpper();
                Catalogos catalogo = new Catalogos { Descripcion = nombreCatalogo };
                catalogo.Tabla = (BusinessVariables.ParametrosCatalogo.PrefijoTabla + nombreCatalogo).Replace(" ", string.Empty);
                catalogo.EsMascaraCaptura = esMascara;
                catalogo.Habilitado = true;
                ExisteMascara(catalogo.Tabla);
                CreaEstructuraBaseDatos(catalogo.Tabla);
                db.Catalogos.AddObject(catalogo);
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

        public List<Catalogos> ObtenerCatalogoConsulta(int? idCatalogo)
        {
            List<Catalogos> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Catalogos> qry = db.Catalogos;
                if (idCatalogo != null)
                    qry = qry.Where(w => w.Id == idCatalogo);
                result = qry.ToList();
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

        public void Habilitar(int idCatalogo, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Catalogos catalogo = db.Catalogos.SingleOrDefault(w => w.Id == idCatalogo);
                if (catalogo != null) catalogo.Habilitado = habilitado;
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

        public void AgregarRegistro(int idCatalogo, string descripcion)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Catalogos catalogo = db.Catalogos.Single(w => w.Id == idCatalogo);
                string store = string.Format("{0} '{1}', '{2}'", BusinessVariables.ParametrosCatalogo.PrefijoComandoInsertar, catalogo.Tabla, descripcion.Trim().ToUpper());
                db.ExecuteStoreCommand(store);
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

        public List<CatalogoGenerico> ObtenerRegistrosCatalogo(int idCatalogo)
        {
            List<CatalogoGenerico> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Catalogos cat = db.Catalogos.Single(s => s.Id == idCatalogo);
                result = db.ExecuteStoreQuery<CatalogoGenerico>("ObtenerCatalogoSistema '" + cat.Tabla + "'").ToList();
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
}
