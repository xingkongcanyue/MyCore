using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.DataManager
{
    public class SqlServerCreator:MetaSqlCreator
    {
        private string CreateTableCol(MetaPropertyDefine property)
        { 
            string type=property.DataType.ToString();
            if(property.DataType==DataType.Varchar)
                type+="("+property.Length+")";

            return string.Format(" {0} {1} {2} {3}"
                , property.ColName
                , type
                , property.IsPrimaryKey ? "Primary key" : ""
                , property.IsCanNull ? "" : " not null"
                );
             
        }
        
        /// <summary>
        /// 获取建表sql
        /// </summary>
        /// <param name="tabDefine"></param>
        /// <returns></returns>
        public override string CreateTableSql(MetaClassDefine cls)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Create table ");
            sb.AppendLine(cls.Name);
            sb.AppendLine("(");
            for (int i = 0; i < cls.Properties.Count; i++)
            {
                sb.Append(CreateTableCol(cls.Properties[i]));
                if (i != cls.Properties.Count)
                    sb.AppendLine(",");
            }
            sb.Append(")");
            return sb.ToString();
        }

        /// <summary>
        /// 获取类的对象的查询sql
        /// </summary>
        /// <param name="cls"></param>
        /// <returns></returns>
        public override string GetClassObjectSql(MetaClassDefine cls)
        {
            StringBuilder sbCols = new StringBuilder();
            StringBuilder sbTabs = new StringBuilder();
            var tbs = new List<string>();
            sbTabs.Append("From ");
            sbTabs.AppendLine(cls.TableName);
            sbTabs.AppendLine(" t1");

            sbCols.Append("select ");
            tbs.Add(cls.TableName);
            for(var i=0;i<cls.Properties.Count;i++){
                var p = cls.Properties[i];
                if(p.IsUsing){
                    tbs.Add(p.UsingClass.TableName);
                    var curTbName = "t"+tbs.Count;

                    sbTabs.Append(" INNER JOIN ");
                    sbTabs.Append(p.UsingClass.TableName);
                    sbTabs.Append("  "+ curTbName);
                    sbTabs .AppendFormat(" on {0}.{1}={2}.{3}",
                        "t1",
                        p.Name,
                        curTbName, 
                        p.UsingProperty.Name
                        );
                    sbCols.AppendFormat(" {0}.{1} as {2}",curTbName,p.UsingProperty.ColName,p.UsingProperty.Name);
                    if (i != cls.Properties.Count)
                        sbCols.AppendLine(" , ");
                }else{
                    sbCols.AppendFormat(" t1.{0} as {1}",p.UsingProperty.ColName,p.Name);
                }
            
            }
            return sbCols.ToString() + sbTabs.ToString();
        }
    }
}
