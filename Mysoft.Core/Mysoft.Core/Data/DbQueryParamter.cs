using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Mysoft.Core.Data
{
    public class DbQueryParamter
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }
        public DbType DbType { get; set; }
        public int Size { get; set; }

    }
}
