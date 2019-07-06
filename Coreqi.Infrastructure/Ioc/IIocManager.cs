/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：IIocManager                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/6 周六 12:18:04                        
 *└────────────────────────────────────────────────┘
 */

using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coreqi.Infrastructure.Ioc
{
    public interface IIocManager
    {
        IContainer Container { get; }
        bool IsRegistered(Type serviceType, ILifetimeScope scope = null);
        object Resolve(Type type, ILifetimeScope scope = null);
        T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class;
        T Resolve<T>(params Parameter[] parameters) where T : class;
        T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null);
        object ResolveOptional(Type serviceType, ILifetimeScope scope = null);
        object ResolveUnregistered(Type type, ILifetimeScope scope = null);
        T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class;
        ILifetimeScope Scope();
        bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance);
    }
}