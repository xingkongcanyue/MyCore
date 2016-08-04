using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Mysoft.Core.Data
{
    public class DbCommandMng
    {
        private static string _connectionString;
        private static string _dbName;
        private static string _providerName;
        private static DbProviderFactory _dbProvider;

        static DbCommandMng() {
            _dbName = ConfigurationManager.AppSettings["DbName"];
            _connectionString = ConfigurationManager.ConnectionStrings[_dbName].ConnectionString;
            _providerName = ConfigurationManager.ConnectionStrings[_dbName].ProviderName;
            _dbProvider = GetDbProviderFactory(_providerName);
        }

        internal static DbProviderFactory DbProvider {
            get { return _dbProvider; } 
        }


        internal static string DbConectionString 
        {
            get { return _connectionString; }
        }

        //internal static DbCommand CreateCommand(string sql, List<DbQueryParamter> paramters)
        internal static DbCommand CreateCommand()
        {
            var command = DbProvider.CreateCommand();
            //command.CommandText = sql;
            //paramters.ForEach(x =>
            //{
            //    var param = command.CreateParameter();
            //    param.ParameterName = x.ParameterName;
            //    param.Value = x.Value;
            //    param.Size = x.Size;
            //    param.DbType = x.DbType;
            //    command.Parameters.Add(param);
            //});
            return command;
        }

        internal static DbConnection CreateConnection()
        {
            return DbProvider.CreateConnection();
        }
        internal static DbProviderFactory GetDbProviderFactory(string provideName)
        {
            return DbProviderFactories.GetFactory(provideName);
        }

         



    }
}
