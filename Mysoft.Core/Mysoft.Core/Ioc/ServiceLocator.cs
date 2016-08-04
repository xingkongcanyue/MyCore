using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.Ioc
{
    public class ServiceLocator
    {
        private ServiceLocator() { }
        private static ServiceLocator _instance = new ServiceLocator();
        public static ServiceLocator Instance { get { return _instance; } }

        public TService GetService<TService>()
        {
            return IocContainer.Instance.GetService<TService>();
        }
    }
}
