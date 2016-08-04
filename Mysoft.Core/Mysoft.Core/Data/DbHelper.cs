using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
namespace Mysoft.Core.Data
{
    internal class DbHelper
    {
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal T ExecuteScalar<T>(DbCommand cmd)
        { 
            return  ExecuteScalar(cmd).To<T>();
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal T ExecuteScalar<T>(DbCommand cmd, Trans trans)
        {
            return ExecuteScalar(cmd, trans).To<T>();
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal object ExecuteScalar(DbCommand cmd)
        {
            using (cmd.Connection = DbCommandMng.CreateConnection())
            {
                cmd.Connection.Open();
                object ret = cmd.ExecuteScalar();
                cmd.Connection.Close();
                return ret;
            }

        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal object ExecuteScalar(DbCommand cmd, Trans trans)
        {  
            cmd.Connection = trans.DbConnection;
            cmd.Transaction = trans.DbTrans;
            object ret = cmd.ExecuteScalar();
            return ret;
        }


        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal int ExecuteNonQuery(DbCommand cmd)
        {
            using (cmd.Connection = DbCommandMng.CreateConnection())
            {
                cmd.Connection.Open();
                int ret = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return ret;
            }
        }

        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal int ExecuteNonQuery(DbCommand cmd, Trans trans )
        {
            cmd.Connection = trans.DbConnection;
            cmd.Transaction = trans.DbTrans;
            int ret = cmd.ExecuteNonQuery();
            return ret; 
        }

        /// <summary>
        /// 执行SQL，返回datatable
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal DataTable ExecuteDataTable(DbCommand cmd)
        {
            using (cmd.Connection = DbCommandMng.CreateConnection())
            {
                DbDataAdapter dbDataAdapter = DbCommandMng.CreateDataAdapter();
                dbDataAdapter.SelectCommand = cmd;
                DataTable dataTable = new DataTable();
                dbDataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        /// <summary>
        /// 执行SQL，返回datatable
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal DataTable ExecuteDataTable(DbCommand cmd, Trans trans)
        {
            cmd.Connection = trans.DbConnection;
            cmd.Transaction = trans.DbTrans;
            DbDataAdapter dbDataAdapter = DbCommandMng.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// 执行SQL，返回dataSet
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal DataSet ExecuteDataSet(DbCommand cmd)
        {
            using (cmd.Connection = DbCommandMng.CreateConnection())
            {
                DbDataAdapter dbDataAdapter = DbCommandMng.CreateDataAdapter();
                dbDataAdapter.SelectCommand = cmd;
                DataSet dataset = new DataSet();
                dbDataAdapter.Fill(dataset);
                return dataset;
            }
        }

        /// <summary>
        /// 执行SQL，返回dataSet
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal DataSet ExecuteDataSet(DbCommand cmd, Trans trans )
        {
            cmd.Connection = trans.DbConnection;
            cmd.Transaction = trans.DbTrans;
            DbDataAdapter dbDataAdapter = DbCommandMng.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet dataset = new DataSet();
            dbDataAdapter.Fill(dataset);
            return dataset;
        }

        /// <summary>
        /// 返回第一行，并转化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal T ToSingle<T>(DbCommand cmd) where T : class, new()
        {
            using (cmd.Connection = DbCommandMng.CreateConnection())
            {
                cmd.Connection.Open();
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    reader.Read();
                    var item = FillModel<T>(reader);
                    //if (cmd.Connection.State == ConnectionState.Open)
                    //    cmd.Connection.Close();
                    return item;
                }
            }
        }

        /// <summary>
        /// 返回第一行，并转化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal T ToSingle<T>(DbCommand cmd, Trans trans) where T : class, new()
        {
            cmd.Connection = trans.DbConnection;
            cmd.Transaction = trans.DbTrans;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                return FillModel<T>(reader);
            } 
        }
         
      
        /// <summary>
        /// 查询所有集合，返回实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal List<T> ToList<T>(DbCommand cmd) where T : class, new()
        {
            List<T> list = new List<T>();
            using (cmd.Connection = DbCommandMng.CreateConnection())
            {

                cmd.Connection.Open();
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        list.Add(FillModel<T>(reader));
                    }
                    //if (cmd.Connection.State == ConnectionState.Open)
                    //    cmd.Connection.Close();

                    cmd.Connection.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// 查询所有集合，返回实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal List<T> ToList<T>(DbCommand cmd, Trans trans ) where T : class, new()
        {
            cmd.Connection = trans.DbConnection;
            cmd.Transaction = trans.DbTrans;
            List<T> list = new List<T>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(FillModel<T>(reader));
                }
                return list;
            }

        }



        /// <summary>
        /// 查询第一列集合，返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal List<T> ExecuteScalarList<T>(DbCommand cmd)
        {
            using (cmd.Connection = DbCommandMng.CreateConnection())
            {
                List<T> list = new List<T>();
                cmd.Connection.Open();
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        list.Add((reader[0].To<T>()));
                    }
                    //if(cmd.Connection.State==ConnectionState.Open)
                    //    cmd.Connection.Close();
                    return list;
                }
            }
        }


        /// <summary>
        /// 查询第一列集合，返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal List<T> ExecuteScalarList<T>(DbCommand cmd, Trans trans)
        {
            cmd.Connection = trans.DbConnection;
            cmd.Transaction = trans.DbTrans;
            List<T> list = new List<T>();
            //    DbDataReader reader = cmd.ExecuteReader();
            using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    list.Add(reader[0].To<T>());
                }
                return list;
            }
        }

        /// <summary>
        /// 用DbDataReader填充实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T FillModel<T>( DbDataReader dr) where T : class, new()
        {
            if (dr == null || !dr.HasRows)
            {
                return default(T);
            }
            T model = new T();
            var properties = model.GetType().GetProperties();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                var property = properties.FirstOrDefault(x => x.Name.IsSame(dr.GetName(i)));
                if (property != null && dr[i] != DBNull.Value)
                {
                    var value = dr[i].ToType(property.PropertyType);
                    // property.SetValue(model, value, null);                     
                    property.FastSetValue(model, value);
                }
            }
            return model;
        }
    }
}
