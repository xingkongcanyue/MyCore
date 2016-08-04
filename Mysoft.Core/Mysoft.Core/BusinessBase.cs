using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public abstract class BusinessBase<T> where T:BusinessBase<T>
    {
        public abstract int Save();
    }
}
