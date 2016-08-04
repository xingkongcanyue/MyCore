using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.Ioc
{
    public abstract class IocRegistorBase
    {
        
        public  abstract void Register(ContainerBuilder builder);
    }
}
