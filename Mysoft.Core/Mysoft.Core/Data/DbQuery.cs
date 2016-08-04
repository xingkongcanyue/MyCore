using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
namespace Mysoft.Core.Data
{
    public partial class DbQuery
    {
        private static DbHelper _dbHelper = new DbHelper();
        private DbQuery(string sql)
        {
            Command = DbCommandMng.CreateCommand();                
            CommandText = sql;
        }
        internal DbCommand Command { get; set; }

        public DbParameterCollection Paramters
        {
            get { return Command.Parameters; }
        }


        public string CommandText
        {
            get { return Command.CommandText; }
            set { Command.CommandText = value; }
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DbQuery AddParam(string name, object value)
        {
            var param = Command.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            Paramters.Add(param);
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="paramter"></param>
        /// <returns></returns>
        public DbQuery AddParam(DbParameter paramter)
        {
            if (paramter != null)
                Paramters.Add(paramter);
            return this;
        }

        #region 实例构建

        public static DbQuery From(string sql)
        {
            return new DbQuery(sql);
        }

        public static DbQuery From(string sql, object[] paramters)
        {
            var i = 0;
            var paramNames = paramters.Select(x => "@p" + i++).ToArray();
            sql = string.Format(sql, paramNames);
            var query = DbQuery.From(sql);
            for (i = 0; i < paramters.Length; i++)
            {
                query.AddParam(paramNames[i], paramters[i]);
            }
            return query;

        }

        #endregion
    }

    public partial class DbQuery
    {
        #region sql查询方法 
        /// <summary>
        ///  执行SQL，返回影响行数
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            return _dbHelper.ExecuteNonQuery(Command);
        }
        /// <summary>
        ///  执行SQL，返回影响行数
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(Trans trans)
        {
            return _dbHelper.ExecuteNonQuery(Command, trans);
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary> 
        /// <returns></returns>
        public object ExecuteScalar()
        {
            return _dbHelper.ExecuteScalar(Command);
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary> 
        /// <returns></returns>
        public object ExecuteScalar(Trans trans)
        {
            return _dbHelper.ExecuteScalar(Command, trans);
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary> 
        /// <returns></returns>
        public T ExecuteScalar<T>()
        {
            return _dbHelper.ExecuteScalar<T>(Command);
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary> 
        /// <returns></returns>
        public T ExecuteScalar<T>(Trans trans)
        {
            return _dbHelper.ExecuteScalar<T>(Command, trans);
        }


        /// <summary>
        /// 执行SQL，返回datatable
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteDataTable()
        {
            return _dbHelper.ExecuteDataTable(Command);
        }
        /// <summary>
        /// 执行SQL，返回datatable
        /// </summary>
        /// <returns></returns>
        public DataTable ExecuteDataTable(Trans trans)
        {
            return _dbHelper.ExecuteDataTable(Command, trans);
        }

        /// <summary>
        /// 执行SQL，返回dataSet
        /// </summary> 
        /// <returns></returns>
        public DataSet ExecuteDataSet()
        {
            return _dbHelper.ExecuteDataSet(Command);
        }
        /// <summary>
        /// 执行SQL，返回dataSet
        /// </summary> 
        /// <returns></returns>
        public DataSet ExecuteDataSet(Trans trans)
        {
            return _dbHelper.ExecuteDataSet(Command, trans);
        }


        /// <summary>
        /// 返回第一行，并转化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        /// <returns></returns>
        public T ToSingle<T>() where T : class, new()
        {
            return _dbHelper.ToSingle<T>(Command);
        }
        /// <summary>
        /// 返回第一行，并转化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        /// <returns></returns>
        public T ToSingle<T>(Trans trans) where T : class, new()
        {
            return _dbHelper.ToSingle<T>(Command, trans);
        }

        /// <summary>
        /// 查询所有集合，返回实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ToList<T>() where T : class, new()
        {
            return _dbHelper.ToList<T>(Command);
        }
        /// <summary>
        /// 查询所有集合，返回实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ToList<T>(Trans trans) where T : class, new()
        {
            return _dbHelper.ToList<T>(Command, trans);
        }

        /// <summary>
        /// 查询第一列集合，返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ExecuteScalarList<T>()
        {
            return _dbHelper.ExecuteScalarList<T>(Command);
        }
        /// <summary>
        /// 查询第一列集合，返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ExecuteScalarList<T>(Trans trans)
        {
            return _dbHelper.ExecuteScalarList<T>(Command, trans);
        }
        #endregion

        #region Sql静态查询方法
        public static int ExecuteNonQuery(string sql, params object[] paramters)        
        {
            return DbQuery.From(sql, paramters).ExecuteNonQuery();
        }

        public static object ExecuteScalar(string sql, params object[] paramters)
        {
            return DbQuery.From(sql, paramters).ExecuteScalar();
        }
        public static T ExecuteScalar<T>(string sql, params object[] paramters)
        {
            return DbQuery.From(sql, paramters).ExecuteScalar<T>();
        }
        public static DataTable ExecuteDataTable(string sql, params object[] paramters)
        {
            return DbQuery.From(sql, paramters).ExecuteDataTable();
        }
        public static DataSet ExecuteDataSet(string sql, params object[] paramters)
        {
            return DbQuery.From(sql, paramters).ExecuteDataSet();
        }

        public static T GetSingle<T>(string sql, params object[] paramters) where T : class, new()
        {
            return DbQuery.From(sql, paramters).ToSingle<T>();
        }
        public static List<T> GetList<T>(string sql, params object[] paramters) where T : class, new()
        {
            return DbQuery.From(sql, paramters).ToList<T>();
        }
        public static List<T> ExecuteScalarList<T>(string sql, params object[] paramters) 
        {
            return DbQuery.From(sql, paramters).ExecuteScalarList<T>();
        }

        #endregion





    }
}
