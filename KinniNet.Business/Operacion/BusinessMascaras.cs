using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using KiiniNet.Entities.Cat.Mascaras;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Operacion
{
    public class BusinessMascaras : IDisposable
    {
        private bool _proxy;
        public BusinessMascaras(bool proxy = false)
        {
            _proxy = proxy;
        }

        private bool CrearEstrcturaMascaraBaseDatos(Mascara mascara)
        {
            try
            {
                if (CreaTabla(mascara))
                    if (CrearInsert(mascara))
                        CreaUpdate(mascara);
            }
            catch (Exception ex)
            {
                throw new Exception((ex.InnerException).Message);
            }
            return true;
        }

        private bool CreaTabla(Mascara mascara)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                string queryCamposTabla = string.Empty;
                foreach (CampoMascara campoMascara in mascara.CampoMascara)
                {

                    TipoCampoMascara tmpTipoCampoMascara = db.TipoCampoMascara.SingleOrDefault(f => f.Id == campoMascara.IdTipoCampoMascara);
                    if (tmpTipoCampoMascara == null) continue;
                    switch (tmpTipoCampoMascara.TipoDatoSql)
                    {
                        case "NVARCHAR":
                            queryCamposTabla += String.Format("{0} {1}({2}) {3},\n", campoMascara.NombreCampo, tmpTipoCampoMascara.TipoDatoSql, campoMascara.LongitudMaxima, campoMascara.Requerido ? "NOT NULL" : "NULL");
                            break;
                        default:
                            queryCamposTabla += String.Format("{0} {1} {2},\n", campoMascara.NombreCampo, tmpTipoCampoMascara.TipoDatoSql, campoMascara.Requerido ? "NOT NULL" : "NULL");
                            break;
                    }

                }
                string qryCrearTablas = String.Format("CREATE TABLE {0} ( \n" +
                                                      "Id int IDENTITY(1,1) NOT NULL, \n" +
                                                      "IdTicket int NOT NULL," +
                                                      "{1}" +
                                                      "Habilitado BIT \n" +
                                                      "CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED \n" +
                                                      "( \n" +
                                                      "\t[Id] ASC \n" +
                                                      ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] \n" +
                                                      ") ON [PRIMARY] \n" +
                                                      "ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [FK_{0}_Ticket] FOREIGN KEY([IdTicket]) \n" +
                                                      "REFERENCES [dbo].[Ticket] ([Id])\n" +
                                                      "ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [FK_{0}_Ticket]\n", mascara.NombreTabla, queryCamposTabla);
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

        private bool CrearInsert(Mascara mascara)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                string queryParametros = "@IDTICKET int, ";
                string queryCampos = "IDTICKET, ";
                string queryValues = "@IDTICKET, ";
                int paramsCount = mascara.NoCampos;
                int contadorParametros = 0;
                foreach (CampoMascara campoMascara in mascara.CampoMascara)
                {
                    contadorParametros++;
                    TipoCampoMascara tmpTipoCampoMascara = db.TipoCampoMascara.SingleOrDefault(f => f.Id == campoMascara.IdTipoCampoMascara);
                    if (tmpTipoCampoMascara == null) continue;
                    switch (tmpTipoCampoMascara.TipoDatoSql)
                    {
                        case "NVARCHAR":
                            queryParametros += String.Format("@{0} {1}({2})", campoMascara.NombreCampo, tmpTipoCampoMascara.TipoDatoSql, campoMascara.LongitudMaxima);
                            break;
                        default:
                            queryParametros += String.Format("@{0} {1}", campoMascara.NombreCampo, tmpTipoCampoMascara.TipoDatoSql);
                            break;
                    }
                    
                    queryCampos += String.Format("{0}", campoMascara.NombreCampo);
                    queryValues += String.Format("@{0}", campoMascara.NombreCampo);
                    if (contadorParametros < paramsCount)
                    {
                        queryParametros += ", \n";
                        queryCampos += ", \n";
                        queryValues += ", \n";
                    }
                }

                string queryStore = string.Format("Create  PROCEDURE {0}( \n" +
                                                  "{1}" +
                                                  ") \n" +
                                                  "AS \n" +
                                                  "BEGIN \n" +
                                                  "INSERT INTO {2}({3}) \n" +
                                                  "VALUES({4}) \n" +
                                                  "END", mascara.ComandoInsertar, queryParametros, mascara.NombreTabla, queryCampos, queryValues);
                db.ExecuteStoreCommand(queryStore);
            }
            catch (Exception ex)
            {
                EliminarObjetoBaseDeDatos(mascara.NombreTabla, Objeto.Tabla);
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return true;
        }

        private bool CreaUpdate(Mascara mascara)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                string queryParametros = string.Empty;
                string queryCamposValues = string.Empty;
                string queryWhereValues = "Id = @ID";
                int paramsCount = mascara.NoCampos;
                int contadorParametros = 0;
                foreach (CampoMascara campoMascara in mascara.CampoMascara)
                {
                    contadorParametros++;
                    TipoCampoMascara tmpTipoCampoMascara = db.TipoCampoMascara.SingleOrDefault(f => f.Id == campoMascara.IdTipoCampoMascara);
                    if (tmpTipoCampoMascara == null) continue;
                    queryParametros += String.Format("@{0} {1}", campoMascara.NombreCampo, tmpTipoCampoMascara.TipoDatoSql);
                    queryCamposValues += String.Format("{0} = @{0}", campoMascara.NombreCampo);

                    if (contadorParametros < paramsCount)
                    {
                        queryParametros += ", \n";
                        queryCamposValues += ", \n";
                    }
                }

                string queryStore = string.Format("Create  PROCEDURE {0}( \n" +
                                                  "@ID INT, \n" +
                                                  "{1}" +
                                                  ") \n" +
                                                  "AS \n" +
                                                  "BEGIN \n" +
                                                  "UPDATE {2} \n" +
                                                  "SET {3} \n" +
                                                  "WHERE {4} \n" +
                                                  "END", mascara.ComandoActualizar, queryParametros, mascara.NombreTabla, queryCamposValues, queryWhereValues);
                db.ExecuteStoreCommand(queryStore);
            }
            catch (Exception ex)
            {
                EliminarObjetoBaseDeDatos(mascara.NombreTabla, Objeto.Tabla);
                EliminarObjetoBaseDeDatos(mascara.ComandoInsertar, Objeto.Store);
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return true;
        }

        private void EliminarObjetoBaseDeDatos(string nombreObjeto, Objeto objeto)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                string query = "DROP ";
                switch (objeto)
                {
                    case Objeto.Tabla:
                        query += "TABLE " + nombreObjeto;
                        break;
                    case Objeto.Store:
                        query += "PROCEDURE " + nombreObjeto;
                        break;
                }
                db.ExecuteStoreCommand(query);
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

        public void CrearMascara(Mascara mascara)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                mascara.NoCampos = mascara.CampoMascara.Count;
                foreach (CampoMascara campoMascara in mascara.CampoMascara)
                {
                    campoMascara.Descripcion = campoMascara.Descripcion.Trim().ToUpper();
                    campoMascara.NombreCampo = campoMascara.Descripcion.Trim().ToUpper().Replace(" ", "");
                    campoMascara.SimboloMoneda = campoMascara.SimboloMoneda == null ? null : campoMascara.SimboloMoneda.Trim().ToUpper();
                }
                mascara.NombreTabla = (BusinessVariables.ParametrosMascaraCaptura.PrefijoTabla + mascara.Descripcion).Replace(" ", string.Empty);
                mascara.ComandoInsertar = (BusinessVariables.ParametrosMascaraCaptura.PrefijoComandoInsertar + mascara.Descripcion).Replace(" ", string.Empty);
                mascara.ComandoActualizar = (BusinessVariables.ParametrosMascaraCaptura.PrefijoComandoActualizar + mascara.Descripcion).Replace(" ", string.Empty);
                mascara.Habilitado = true;

                ExisteMascara(mascara);
                CrearEstrcturaMascaraBaseDatos(mascara);
                db.Mascara.AddObject(mascara);
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

        public bool ExisteMascara(Mascara mascara)
        {
            bool result = false;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                var pTableName = new SqlParameter { ParameterName = "@TABLENAME", Value = mascara.NombreTabla };
                var pResult = new SqlParameter { ParameterName = "@OUTER", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int };
                db.ExecuteStoreCommand("exec ExisteTablaMascara @TABLENAME, @OUTER output", pTableName, pResult);
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

        public void Dispose()
        {
        }

        private enum Objeto
        {
            Tabla = 1,
            Store = 2
        }

        public List<Mascara> ObtenerMascarasAcceso(bool insertarSeleccion)
        {
            List<Mascara> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Mascara.Where(w => w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.Index, new Mascara { Id = BusinessVariables.ComboBoxCatalogo.Value, Descripcion = BusinessVariables.ComboBoxCatalogo.Descripcion, Habilitado = BusinessVariables.ComboBoxCatalogo.Habilitado });
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

        public Mascara ObtenerMascaraCaptura(int idMascara)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            Mascara result;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Mascara.SingleOrDefault(s => s.Id == idMascara);
                if (result != null)
                {
                    db.LoadProperty(result, "CampoMascara");
                    foreach (CampoMascara campoMascara in result.CampoMascara)
                    {
                        db.LoadProperty(campoMascara, "TipoCampoMascara");
                        db.LoadProperty(campoMascara, "Catalogos");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public List<CatalogoGenerico> ObtenerCatalogoCampoMascara(string tabla)
        {
            List<CatalogoGenerico> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                result = db.ExecuteStoreQuery<CatalogoGenerico>("ObtenerCatalogoSistema '" + tabla + "'").ToList();
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

        public class CatalogoGenerico
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
        }
    }
}
