using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.DataManager
{
    public abstract class MetaSqlCreator
    {
        public abstract string CreateTableSql(MetaClassDefine classDefine);

        public abstract string GetClassObjectSql(MetaClassDefine classDefine);
        
    }
}
