using System;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using KiiniNet.Entities.Cat.Mascaras;
using KiiniNet.Entities.Helper;
using KiiniNet.Entities.Operacion.Tickets;
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

        private bool CrearEstructuraMascaraBaseDatos(Mascara mascara)
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
                                                      (mascara.Random ? ", " + BusinessVariables.ParametrosMascaraCaptura.CampoRandom + " \n" : string.Empty) +
                                                      "CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED \n" +
                                                      "( \n" +
                                                      "\t[Id] ASC \n" +
                                                      ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] \n" +
                                                      ") ON [PRIMARY] \n" +
                                                      "ALTER TABLE [dbo].[{0}]  WITH CHECK ADD  CONSTRAINT [FK_{0}_Ticket] FOREIGN KEY([IdTicket]) \n" +
                                                      "REFERENCES [dbo].[Ticket] ([Id])\n" +
                                                      "ALTER TABLE [dbo].[{0}] CHECK CONSTRAINT [FK_{0}_Ticket]\n" +
                                                      "ALTER TABLE [dbo].[{0}] ADD  CONSTRAINT [DF_{0}_habilitado]  DEFAULT ((1)) FOR [Habilitado]", mascara.NombreTabla, queryCamposTabla);
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
                    TipoCampoMascara tmpTipoCampoMascara =
                        db.TipoCampoMascara.SingleOrDefault(f => f.Id == campoMascara.IdTipoCampoMascara);
                    if (tmpTipoCampoMascara == null) continue;
                    switch (tmpTipoCampoMascara.TipoDatoSql)
                    {
                        case "NVARCHAR":
                            queryParametros += String.Format("@{0} {1}({2})", campoMascara.NombreCampo,
                                tmpTipoCampoMascara.TipoDatoSql, campoMascara.LongitudMaxima);
                            break;
                        default:
                            queryParametros += String.Format("@{0} {1}", campoMascara.NombreCampo,
                                tmpTipoCampoMascara.TipoDatoSql);
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

                if (mascara.Random)
                {
                    queryParametros += String.Format(", @{0} {1}", BusinessVariables.ParametrosMascaraCaptura.NombreCampoRandom, BusinessVariables.ParametrosMascaraCaptura.TipoCampoRandom);
                    queryCampos += ", " + BusinessVariables.ParametrosMascaraCaptura.NombreCampoRandom;
                    queryValues += String.Format(", @{0}", BusinessVariables.ParametrosMascaraCaptura.NombreCampoRandom);
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
                EliminarObjetoBaseDeDatos(mascara.NombreTabla, BusinessVariables.EnumTipoObjeto.Tabla);
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

                if (mascara.Random)
                {
                    queryParametros += String.Format(", @{0} {1}", BusinessVariables.ParametrosMascaraCaptura.NombreCampoRandom, BusinessVariables.ParametrosMascaraCaptura.TipoCampoRandom);
                    queryCamposValues += String.Format(", \n {0} = @{0}", BusinessVariables.ParametrosMascaraCaptura.NombreCampoRandom);
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
                EliminarObjetoBaseDeDatos(mascara.NombreTabla, BusinessVariables.EnumTipoObjeto.Tabla);
                EliminarObjetoBaseDeDatos(mascara.ComandoInsertar, BusinessVariables.EnumTipoObjeto.Store);
                throw new Exception((ex.InnerException).Message);
            }
            finally
            {
                db.Dispose();
            }
            return true;
        }

        private void EliminarObjetoBaseDeDatos(string nombreObjeto, BusinessVariables.EnumTipoObjeto objeto)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                string query = "DROP ";
                switch (objeto)
                {
                    case BusinessVariables.EnumTipoObjeto.Tabla:
                        query += "TABLE " + nombreObjeto;
                        break;
                    case BusinessVariables.EnumTipoObjeto.Store:
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
                    campoMascara.TipoCampoMascara = null;
                }
                mascara.NombreTabla = (BusinessVariables.ParametrosMascaraCaptura.PrefijoTabla + mascara.Descripcion).Replace(" ", string.Empty);
                mascara.ComandoInsertar = (BusinessVariables.ParametrosMascaraCaptura.PrefijoComandoInsertar + mascara.Descripcion).Replace(" ", string.Empty);
                mascara.ComandoActualizar = (BusinessVariables.ParametrosMascaraCaptura.PrefijoComandoActualizar + mascara.Descripcion).Replace(" ", string.Empty);
                mascara.Habilitado = true;

                ExisteMascara(mascara);
                CrearEstructuraMascaraBaseDatos(mascara);
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

        public Mascara ObtenerMascaraCapturaByIdTicket(int idTicket)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            Mascara result = null;
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                Ticket ticket = db.Ticket.Single(s => s.Id == idTicket);
                if (ticket != null)
                {
                    db.LoadProperty(ticket, "Mascara");
                    result = ticket.Mascara;
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

        public List<Mascara> Consulta(string descripcion)
        {
            List<Mascara> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                IQueryable<Mascara> qry = db.Mascara;
                if (descripcion.Trim() != string.Empty)
                    qry = qry.Where(w => w.Descripcion.Contains(descripcion));
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

        public void HabilitarMascara(int idMascara, bool habilitado)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Mascara mascara = db.Mascara.SingleOrDefault(w => w.Id == idMascara);
                if (mascara != null) mascara.Habilitado = habilitado;
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

        public List<HelperMascaraData> ObtenerDatosMascara(int idMascara, int idTicket)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<HelperMascaraData> result = null;
            try
            {
                Mascara mascara = db.Mascara.SingleOrDefault(s => s.Id == idMascara);
                if (mascara != null)
                {
                    db.LoadProperty(mascara, "CampoMascara");
                    string campos = mascara.CampoMascara.Aggregate(string.Empty, (current, campoMascara) => current + (campoMascara.NombreCampo + ", "));
                    if (mascara.Random)
                        campos += BusinessVariables.ParametrosMascaraCaptura.NombreCampoRandom;
                    else
                        campos = campos.Trim().TrimEnd(',');

                    DataSet retVal = new DataSet();
                    EntityConnection entityConn = (EntityConnection)db.Connection;
                    SqlConnection sqlConn = (SqlConnection)entityConn.StoreConnection;
                    SqlCommand cmdReport = new SqlCommand(string.Format("select {0} from {1} where IdTicket = {2}", campos, mascara.NombreTabla, idTicket), sqlConn);
                    SqlDataAdapter daReport = new SqlDataAdapter(cmdReport);
                    using (cmdReport)
                    {
                        cmdReport.CommandType = CommandType.Text;
                        daReport.Fill(retVal);
                    }

                    if (retVal.Tables.Count > 0)
                    {
                        if (retVal.Tables[0].Rows.Count > 0)
                        {
                            result = new List<HelperMascaraData>();
                            foreach (DataRow row in retVal.Tables[0].Rows)
                            {
                                foreach (DataColumn column in retVal.Tables[0].Columns)
                                {
                                    HelperMascaraData data = new HelperMascaraData();
                                    data.Campo = column.ColumnName;
                                    data.Value = row[column.ColumnName].ToString();
                                    result.Add(data);
                                }
                                break;
                            }
                        }
                            
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

        
    }
}
