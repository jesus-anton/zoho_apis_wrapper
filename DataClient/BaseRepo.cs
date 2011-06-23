using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DataClient
{
    public class BaseRepo
    {
        public string ConnectionString { get; set; }
        public DbProviderFactory ProvFactory { get; set; }

        public DataTable Structure(string entityName)
        {
            DataTable ret = new DataTable(entityName);

            using (DbDataAdapter da = ProvFactory.CreateDataAdapter())
            {
                da.SelectCommand = ProvFactory.CreateCommand();
                da.SelectCommand.Connection = ProvFactory.CreateConnection();
                da.SelectCommand.Connection.ConnectionString = ConnectionString;
                da.SelectCommand.CommandText = "select * from " + entityName;
                da.FillSchema(ret, SchemaType.Source);
            }

            return ret;

        }

        public DataTable Select(string entityName)
        {
            return Select(entityName, "");
        }

        public DataTable Select(string entityName, string filter, params object[] values)
        {
            DataTable ret = new DataTable(entityName);

            using (DbDataAdapter da = ProvFactory.CreateDataAdapter())
            {
                da.SelectCommand = ProvFactory.CreateCommand();
                da.SelectCommand.Connection = ProvFactory.CreateConnection();
                da.SelectCommand.Connection.ConnectionString = ConnectionString;
                da.SelectCommand.CommandText = "select * from " + entityName + (!string.IsNullOrEmpty(filter) ? " where " + filter : "");
                foreach (object val in values)
                {
                    DbParameter prm = ProvFactory.CreateParameter();
                    prm.Value = val;
                    prm.DbType = DecodeType(val);
                    da.SelectCommand.Parameters.Add(prm);
                }
                da.FillSchema(ret, SchemaType.Source);
                da.Fill(ret);
            }

            return ret;
        }

        public void Update(DataTable tbl)
        {
            string entityName = tbl.TableName;
            using (DbDataAdapter da = ProvFactory.CreateDataAdapter())
            {
                da.SelectCommand = ProvFactory.CreateCommand();
                da.SelectCommand.Connection = ProvFactory.CreateConnection();
                da.SelectCommand.Connection.ConnectionString = ConnectionString;
                da.SelectCommand.CommandText = "select * from " + entityName + " where 1<>1";
                using (DbCommandBuilder bldr = ProvFactory.CreateCommandBuilder())
                {
                    bldr.DataAdapter = da;
                    da.DeleteCommand = bldr.GetDeleteCommand();
                    da.InsertCommand= bldr.GetInsertCommand();
                    da.UpdateCommand = bldr.GetUpdateCommand();

                    using (DataTable chg = tbl.GetChanges())
                    {
                        if (chg != null)
                        {
                            da.Update(chg);
                            tbl.Merge(chg);
                        }
                    }
                }
            }
        }

        private DbType DecodeType(object val)
        {
            if (val is string || val is char)
            {
                return DbType.String;
            }
            else if (val is byte)
            {
                return DbType.Byte;
            }
            else if (val is short)
            {
                return DbType.Int16;
            }
            else if (val is int)
            {
                return DbType.Int32;
            }
            else if (val is long)
            {
                return DbType.Int64;
            }
            else if (val is decimal)
            {
                return DbType.Decimal;
            }
            else if (val is float)
            {
                return DbType.Single;
            }
            else if (val is double)
            {
                return DbType.Double;
            }
            else if (val is DateTime)
            {
                return DbType.DateTime;
            }
            return DbType.Object;
        }
    }
}
