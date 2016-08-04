using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core.Ioc
{
    public abstract class IocTypeRegisterBase:IocRegistorBase
    {
        private ContainerBuilder _builder;
        public override void Register(Autofac.ContainerBuilder builder)
        {
            _builder = builder;
        }
        public virtual void RegistType<TFrom, TTo>() where TFrom:TTo
        {
            _builder.RegisterType<TFrom>().As<TTo>();
        }

        public virtual void RegistType(Type from,Type to)
        {
            _builder.RegisterType(from).As(to);
        }

        public abstract void Register();
         
    }
}
