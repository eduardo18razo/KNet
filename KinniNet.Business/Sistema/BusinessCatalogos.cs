﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using KiiniNet.Entities.Cat.Sistema;
using KiiniNet.Entities.Helper;
using KinniNet.Business.Utils;
using KinniNet.Data.Help;

namespace KinniNet.Core.Sistema
{
    public class BusinessCatalogos : IDisposable
    {
        private readonly bool _proxy;
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
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new Catalogos
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

        public List<Catalogos> ObtenerCatalogosMascaraCaptura(bool insertarSeleccion)
        {
            List<Catalogos> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Catalogos.Where(w => w.EsMascaraCaptura && w.Habilitado).OrderBy(o => o.Descripcion).ToList();
                if (insertarSeleccion)
                    result.Insert(BusinessVariables.ComboBoxCatalogo.IndexSeleccione,
                        new Catalogos
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
            return true;
        }

        public Catalogos ObtenerCatalogo(int idCatalogo)
        {
            Catalogos result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {

                db.ContextOptions.ProxyCreationEnabled = _proxy;
                result = db.Catalogos.SingleOrDefault(s => s.Id == idCatalogo);
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

        public void CrearCatalogo(string nombreCatalogo, bool esMascara, List<string> registros)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                db.ContextOptions.ProxyCreationEnabled = _proxy;
                nombreCatalogo = nombreCatalogo.Trim().ToUpper();
                Catalogos catalogo = new Catalogos
                {
                    Descripcion = nombreCatalogo,
                    Tabla = BusinessCadenas.Cadenas.FormatoBaseDatos((BusinessVariables.ParametrosCatalogo.PrefijoTabla + nombreCatalogo).Replace(" ", string.Empty)),
                    EsMascaraCaptura = esMascara,
                    Archivo = false,
                    Habilitado = true
                };
                ExisteMascara(catalogo.Tabla);
                CreaEstructuraBaseDatos(catalogo.Tabla);
                db.Catalogos.AddObject(catalogo);
                db.SaveChanges();
                if (registros.Count <= 0) return;
                foreach (string registro in registros)
                {
                    AgregarRegistro(catalogo.Id, registro);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }

        public List<CatalogoGenerico> ObtenerRegistrosSistemaCatalogo(int idCatalogo, bool insertarSeleccion)
        {
            List<CatalogoGenerico> result;
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                Catalogos cat = db.Catalogos.Single(s => s.Id == idCatalogo);
                result = db.ExecuteStoreQuery<CatalogoGenerico>("ObtenerCatalogoSistema '" + cat.Tabla + "'," + Convert.ToInt32(insertarSeleccion)).ToList();
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

        public DataTable ObtenerRegistrosArchivosCatalogo(int idCatalogo)
        {
            DataTable result = new DataTable();
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                int lenght = 15;

                Catalogos cat = db.Catalogos.Single(s => s.Id == idCatalogo);
                List<CamposCatalogo> lstCampos = ObtenerCamposCatalogo(idCatalogo);
                string sql = string.Format(" SELECT {0} Id, '{1}' as Descripcion \nUNION \nSELECT Id, ", BusinessVariables.ComboBoxCatalogo.ValueSeleccione, BusinessVariables.ComboBoxCatalogo.DescripcionSeleccione);
                foreach (CamposCatalogo campo in lstCampos.Where(w => w.Descripcion != "Id" && w.Descripcion != "Habilitado"))
                {
                    switch (campo.TipoDato)
                    {
                        case "int":
                        case "float":
                            sql += string.Format("REPLACE(STR([{0}], {1}), SPACE(1), '0') + ' | ' +", campo.Descripcion, lenght);
                            break;
                        case "nvarchar":
                            sql += string.Format("SUBSTRING(left([{0}] + SPACE(15) , {1}), 1, {1}) + ' | ' +", campo.Descripcion, lenght);
                            break;
                    }
                }

                sql = sql.TrimEnd('+').TrimEnd('\'').Trim().TrimEnd('|');
                sql = sql.TrimEnd('|').TrimEnd('\'').Trim().TrimEnd('|');
                sql = sql.TrimEnd('+').TrimEnd('\'').Trim().TrimEnd('|');
                sql = sql.TrimEnd('+').TrimEnd('\'').Trim().TrimEnd('|');
                sql = sql.TrimEnd('+').TrimEnd('\'').Trim().TrimEnd('|');
                sql += " as Descripcion \nFROM " + cat.Tabla;

                SqlConnection sqlConn = new SqlConnection(string.Format("Server={0};Database={1};Trusted_Connection=True", db.Connection.DataSource, (((System.Data.EntityClient.EntityConnection)(db.Connection)).StoreConnection).Database));
                SqlCommand cmdSql = new SqlCommand(sql, sqlConn);
                //cmdSql.Parameters.Add("@TABLENAME", SqlDbType.VarChar).Value = cat.Tabla;
                SqlDataAdapter da = new SqlDataAdapter(cmdSql);
                cmdSql.CommandType = CommandType.Text;
                DataSet ds = new DataSet();
                da.Fill(ds, cat.Tabla);

                DataTable dt = ds.Tables[cat.Tabla];
                result = ds.Tables[cat.Tabla];
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

        #region excel

        private void RollBackActualizacion(DataSet dsOriginal, string nombreTabla, SqlConnection sqlCon)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(string.Format("select * from {0} order by Id", nombreTabla), sqlCon);
                SqlDataAdapter daRollBack = new SqlDataAdapter(cmd);
                daRollBack.TableMappings.Add("Table", nombreTabla);
                DataSet dsRollBack = new DataSet();
                daRollBack.Fill(dsRollBack);
                for (int row = 0; row < dsOriginal.Tables[nombreTabla].Rows.Count; row++)
                {
                    for (int col = 1; col < dsOriginal.Tables[nombreTabla].Columns.Count; col++)
                    {
                        dsRollBack.Tables[nombreTabla].Rows[row][col] = dsOriginal.Tables[nombreTabla].Rows[row][col];
                    }
                }
                new SqlCommandBuilder(daRollBack);
                daRollBack.Update(dsRollBack.Tables[nombreTabla]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ActualizarCatalogoExcel(int idCatalogo, bool esMascara, string archivo, string hoja)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            DataSet dsOriginales = null;
            SqlConnection sqlConn = null;
            string nombreTabla = null;
            try
            {
                Catalogos catalogo = db.Catalogos.SingleOrDefault(s => s.Id == idCatalogo);
                if (catalogo != null)
                {
                    nombreTabla = catalogo.Tabla;
                    sqlConn = new SqlConnection(string.Format("Server={0};Database={1};Trusted_Connection=True", db.Connection.DataSource, (((System.Data.EntityClient.EntityConnection)(db.Connection)).StoreConnection).Database));
                    sqlConn.Open();
                    SqlCommand cmdSql = new SqlCommand(string.Format("select * from {0} order by Id", catalogo.Tabla), sqlConn);
                    SqlDataAdapter daOriginal = new SqlDataAdapter(cmdSql);
                    daOriginal.TableMappings.Add("Table", catalogo.Tabla);
                    dsOriginales = new DataSet();
                    daOriginal.Fill(dsOriginales);

                    DataSet dtExcel = BusinessFile.ExcelManager.LeerHojaExcel(archivo, hoja);
                    SqlCommand cmdDesabilita = new SqlCommand(string.Format("update {0} set Habilitado = 0", catalogo.Tabla), sqlConn);
                    cmdDesabilita.ExecuteNonQuery();
                    cmdSql = new SqlCommand(string.Format("select * from {0}", catalogo.Tabla), sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter(cmdSql);
                    da.TableMappings.Add("Table", catalogo.Tabla);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    foreach (DataRow row in dtExcel.Tables["tablaPaso"].Rows)
                    {

                        DataRow[] foundRows = ds.Tables[catalogo.Tabla].Select(dtExcel.Tables["tablaPaso"].Columns[0].ColumnName + " = '" + row[0] + "'");
                        if (foundRows.Any())
                        {
                            foreach (DataRow dataRowDb in foundRows)
                            {
                                for (int col = 2; col < ds.Tables[catalogo.Tabla].Columns.Count - 1; col++)
                                {
                                    dataRowDb[col] = row[ds.Tables[catalogo.Tabla].Columns[col].ColumnName];
                                }
                                dataRowDb["Habilitado"] = true;
                            }
                        }
                        else
                        {
                            DataRow dr = ds.Tables[catalogo.Tabla].NewRow();
                            foreach (DataColumn column in dtExcel.Tables["tablaPaso"].Columns)
                            {
                                dr[column.ColumnName] = row[column.ColumnName].ToString();
                            }
                            dr["Habilitado"] = true;
                            ds.Tables[catalogo.Tabla].Rows.Add(dr);
                        }
                    }
                    new SqlCommandBuilder(da);
                    da.Update(ds.Tables[catalogo.Tabla]);

                }
            }
            catch (Exception ex)
            {
                if (sqlConn != null && dsOriginales != null && nombreTabla != string.Empty)
                    RollBackActualizacion(dsOriginales, nombreTabla, sqlConn);
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
        }
        public void CrearCatalogoExcel(string nombreCatalogo, bool esMascara, string archivo, string hoja)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            try
            {
                nombreCatalogo = nombreCatalogo.ToUpper();
                Catalogos catalogo = new Catalogos
                {
                    Descripcion = nombreCatalogo,
                    Tabla = (BusinessVariables.ParametrosCatalogo.PrefijoTabla + nombreCatalogo).Replace(" ", string.Empty),
                    EsMascaraCaptura = esMascara,
                    Archivo = true,
                    Habilitado = true
                };
                ExisteMascara(catalogo.Tabla);
                DataSet dtExcel = BusinessFile.ExcelManager.LeerHojaExcel(archivo, hoja);
                string sqltable = CreateSqlTableFromDataTable(catalogo.Tabla, dtExcel.Tables["tablaPaso"]);
                db.ExecuteStoreCommand(sqltable);
                SqlConnection sqlConn = new SqlConnection(string.Format("Server={0};Database={1};Trusted_Connection=True", db.Connection.DataSource, (((System.Data.EntityClient.EntityConnection)(db.Connection)).StoreConnection).Database));
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(string.Format("select * from {0}", catalogo.Tabla), sqlConn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.TableMappings.Add("Table", catalogo.Tabla);
                DataSet ds = new DataSet();
                da.Fill(ds);
                List<CampoCatalogo> lstCampos = (from DataColumn column in dtExcel.Tables["tablaPaso"].Columns select new CampoCatalogo { Campo = column.ColumnName, TipoCampo = SqlGetType(column) }).ToList();
                catalogo.CampoCatalogo = lstCampos;
                foreach (DataRow row in dtExcel.Tables["tablaPaso"].Rows)
                {
                    DataRow dr = ds.Tables[catalogo.Tabla].NewRow();
                    foreach (DataColumn column in dtExcel.Tables["tablaPaso"].Columns)
                    {
                        dr[column.ColumnName] = row[column.ColumnName].ToString();
                    }
                    dr["Habilitado"] = true;
                    ds.Tables[catalogo.Tabla].Rows.Add(dr);
                }
                new SqlCommandBuilder(da);
                da.Update(ds.Tables[catalogo.Tabla]);
                db.Catalogos.AddObject(catalogo);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                EliminarObjetoBaseDeDatos(nombreCatalogo, BusinessVariables.EnumTipoObjeto.Tabla);
                throw new Exception(ex.Message);
            }
            finally { db.Dispose(); }
        }

        public static string CreateSqlTableFromDataTable(string tableName, DataTable table)
        {

            string sql = "CREATE TABLE [" + tableName + "] (\n";
            sql += "Id int IDENTITY(1,1) NOT NULL, \n";
            sql = table.Columns.Cast<DataColumn>().Aggregate(sql, (current, column) => current + ("[" + column.ColumnName + "] " + SqlGetType(column) + ",\n"));
            sql += "Habilitado BIT \n";
            sql = sql.TrimEnd(new char[] { ',', '\n' }) + "\n";
            sql += "CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED (";
            sql += "[Id] ASC \n" +
                   ") WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] \n" +
                   ") ON [PRIMARY] \n " +
                   "ALTER TABLE [dbo].[" + tableName + "] ADD  CONSTRAINT [DF_{" + tableName + "}_habilitado]  DEFAULT ((1)) FOR [Habilitado]";
            return sql;
        }

        public static string SqlGetType(DataColumn column)
        {
            return GetSqlType(column.DataType, column.MaxLength, 10, 2);
        }

        public static string GetSqlType(object type, int columnSize, int numericPrecision, int numericScale)
        {
            switch (type.ToString())
            {
                case "System.Byte[]":
                    return "VARBINARY(MAX)";
                case "System.Boolean":
                    return "BIT";
                case "System.DateTime":
                    return "DATETIME";
                case "System.DateTimeOffset":
                    return "DATETIMEOFFSET";
                case "System.Decimal":
                    if (numericPrecision != -1 && numericScale != -1)
                        return "DECIMAL(" + numericPrecision + "," + numericScale + ")";
                    else
                        return "DECIMAL";
                case "System.Double":
                    return "FLOAT";
                case "System.Single":
                    return "REAL";
                case "System.Int64":
                    return "BIGINT";
                case "System.Int32":
                    return "INT";
                case "System.Int16":
                    return "SMALLINT";
                case "System.String":
                    return "NVARCHAR(" + ((columnSize == -1 || columnSize > 8000) ? "MAX" : columnSize.ToString()) + ")";
                case "System.Byte":
                    return "TINYINT";
                case "System.Guid":
                    return "UNIQUEIDENTIFIER";
                default:
                    throw new Exception(type.ToString() + " not implemented.");
            }
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
                throw new Exception(ex.Message);
            }
            finally
            {
                db.Dispose();
            }
        }
        #endregion excel
        public List<CamposCatalogo> ObtenerCamposCatalogo(int idCatalogo)
        {
            DataBaseModelContext db = new DataBaseModelContext();
            List<CamposCatalogo> result = new List<CamposCatalogo>();
            try
            {
                Catalogos catalogo = db.Catalogos.SingleOrDefault(w => w.Id == idCatalogo);
                if (catalogo != null)
                {
                    SqlConnection sqlConn = new SqlConnection(string.Format("Server={0};Database={1};Trusted_Connection=True", db.Connection.DataSource, (((System.Data.EntityClient.EntityConnection)(db.Connection)).StoreConnection).Database));
                    SqlCommand cmdSchema = new SqlCommand(string.Format("select COLUMN_NAME, DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME ='{0}'", catalogo.Tabla), sqlConn);
                    SqlDataAdapter daSchema = new SqlDataAdapter(cmdSchema);
                    daSchema.TableMappings.Add("Table", catalogo.Tabla);
                    DataSet dsSchema = new DataSet();
                    daSchema.Fill(dsSchema);
                    result = (from DataRow row in dsSchema.Tables[catalogo.Tabla].Rows select new CamposCatalogo { Descripcion = row[0].ToString(), TipoDato = row[1].ToString() }).ToList();
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

        public class CamposCatalogo
        {
            public string Descripcion { get; set; }
            public string TipoDato { get; set; }
        }
    }
}
