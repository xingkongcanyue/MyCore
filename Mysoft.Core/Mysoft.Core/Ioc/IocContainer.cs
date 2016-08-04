using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Reflection;
namespace Mysoft.Core.Ioc
{
    public class IocContainer
    {
        private ContainerBuilder _builder = new ContainerBuilder();
        private IContainer _container;
        private IocContainer() { }
        public static IocContainer Instance { get; set; }

        static IocContainer() {
            Instance = new IocContainer();
        }


        public ContainerBuilder GetContainerBuilder()
        {
            return _builder;
        }

        public void SelfRegisterAll()
        {
            var basetype = typeof(IocRegistorBase);
            foreach (Assembly asm in AssemblyHelper.All) {
                var name = asm.GetName().Name;
                if (name.StartsWith("System") || name.StartsWith("Microsoft"))
                    continue;
                foreach (var type in asm.GetTypes()) {
                    if (basetype.IsAssignableFrom(type) && !type.IsAbstract && type.IsPublic)
                    {
                        var instance = (IocRegistorBase)Activator.CreateInstance(type);
                        instance.Register(_builder);
                    }
                }
            }
            Register_Over();
        }
        public void Register_Over()
        {
            _container = _builder.Build();
        }

        public TService GetService<TService>()
        {
           return  _container.Resolve<TService>();
        }
        public object GetService(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
