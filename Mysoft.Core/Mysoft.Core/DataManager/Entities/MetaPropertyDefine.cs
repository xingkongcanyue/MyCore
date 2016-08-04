using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.DataManager
{
    public class MetaPropertyDefine
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string ChnName { get; set; }
        public string ClassId { get; set; }

        public DataType DataType { get; set; }

        public int Length { get; set; }

        public bool IsPrimaryKey { get; set; }

        public bool IsCanNull { get; set; }

        public bool IsReadOnly { get; set; }

        public bool Visiable { get; set; }

        public object DefaultValue { get; set; }

        public ShowType ShowType { get; set; }

        public bool IsUsing { get; set; }

        public string UsingClassId { get; set; }

        public MetaClassDefine UsingClass { get; set; }

        public MetaPropertyDefine UsingProperty { get; set; }
        public string UsingPropertyId { get; set; }

       // public MetaTableColDefine TableCol { get; set; }
        public string ColName { get; set; }
    }
}
