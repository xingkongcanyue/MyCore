using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Mysoft.Core.Data
{
    public class Trans : IDisposable
    {
        private DbConnection conn;
        private DbTransaction dbTrans;
        public DbConnection DbConnection
        {
            get { return this.conn; }
        }
        public DbTransaction DbTrans
        {
            get { return this.dbTrans; }
        }

        /// <summary>
        /// 事物
        /// mysql表需要设置为alter table userinfo engine=InnoDB;
        /// </summary>
        public Trans()
        {
            conn = DbCommandMng.CreateConnection();
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        //public Trans(string connectionString)
        //{
        //    conn = CommandMng.CreateDbConnection();
        //    conn.Open();
        //    dbTrans = conn.BeginTransaction();
        //}
        public void Commit()
        {
            dbTrans.Commit();
            this.Colse();
        }

        public void RollBack()
        {
            dbTrans.Rollback();
            this.Colse();
        }

        public void Dispose()
        {
            this.Colse();
        }

        public void Colse()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
