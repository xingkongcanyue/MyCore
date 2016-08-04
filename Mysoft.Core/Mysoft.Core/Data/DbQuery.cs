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

         
        public int ExecuteNonQuery()
        {
            using (var comm = Command)
            {
                using (var conn = DbCommandMng.CreateConnection())
                {                    
                    comm.Connection = conn;
                    conn.Open();
                    int result = comm.ExecuteNonQuery();
                    conn.Close();
                    return result;
                }
            }
        }

        public object ExecuteScalar()
        {
            using (Command)
            {
                using (var conn = DbCommandMng.CreateConnection())
                {
                    Command.Connection = conn;
                    conn.Open();
                    object result = Command.ExecuteScalar();
                    conn.Close();
                    return result;
                }
            }
        
        }
    }

    public partial class DbQuery
    {
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
        public static int ExecuteNonQuery(string sql, params object[] paramters)
        {
            return From(sql, paramters).ExecuteNonQuery(); 
        }


         


    }
}
