using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.DataManager
{
    public class MetaClassDefine
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ChnName { get; set; }

        public string Description { get; set; }

        //public MetaTableDefine Table { get; set; }

        public string TableName { get; set; }
        public virtual List<MetaPropertyDefine> Properties { get; set; }
    }
}
