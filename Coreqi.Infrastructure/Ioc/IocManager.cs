/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：IocManager                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/6 周六 12:19:51                        
 *└────────────────────────────────────────────────┘
 */

using Autofac;
using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Autofac.Extensions.DependencyInjection;

namespace Coreqi.Infrastructure.Ioc
{
    public class IocManager:IIocManager
    {
        public static IocManager Instance { get; } = new IocManager();
        public IContainer Container { get; private set; }
    /// <summary>
    /// Ioc容器初始化
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
        public IServiceProvider Initialize(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(Instance).As<IIocManager>().SingleInstance();
            builder.Populate(services);

            // 注册autofac打标签模式
            builder.RegisterModule(new Autofac.Annotation.AutofacAnnotationModule(AppDomain.CurrentDomain.GetAssemblies()).SetAllowCircularDependencies(true).InstancePerLifetimeScope());

            Container = builder.Build();
            var serviceProvider = new AutofacServiceProvider(Container);
            return serviceProvider;
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<T>();
            }
            return scope.ResolveKeyed<T>(key);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual T Resolve<T>(params Autofac.Core.Parameter[] parameters) where T : class
        {
            var scope = Scope();
            return scope.Resolve<T>(parameters);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.Resolve(type);
        }

        /// <summary>
        /// Resolve all
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">key</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved services</returns>
        public virtual T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<IEnumerable<T>>().ToArray();
            }
            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class
        {
            return ResolveUnregistered(typeof(T), scope) as T;
        }

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual object ResolveUnregistered(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType, scope);
                        if (service == null) throw new Exception("Unknown dependency");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (Exception)
                {

                }
            }
            throw new Exception("No constructor  was found that had all the dependencies satisfied.");
        }

        /// <summary>
        /// Try to resolve srevice
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <param name="instance">Resolved service</param>
        /// <returns>Value indicating whether service has been successfully resolved</returns>
        public virtual bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// Check whether some service is registered (can be resolved)
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Result</returns>
        public virtual bool IsRegistered(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.IsRegistered(serviceType);
        }

        /// <summary>
        /// Resolve optional
        /// </summary>
        /// <param name="serviceType">Type</param>
        /// <param name="scope">Scope; pass null to automatically resolve the current scope</param>
        /// <returns>Resolved service</returns>
        public virtual object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.ResolveOptional(serviceType);
        }

        /// <summary>
        /// 获取当前生命周期
        /// </summary>
        /// <returns></returns>
        public virtual ILifetimeScope Scope()
        {
            try
            {
                //当返回这样的生命周期范围时，你应该确保它一旦被使用就会被处理掉（例如在计划任务中）
                return Container.BeginLifetimeScope();  //创建新的生命周期范围
            }
            catch (Exception)
            {
                //如果已经处理了RequestLifetimeScope，我们可以在这里得到一个例外
                //例如，在“Application_EndRequest”处理程序中或之后请求
                //但请注意，通常它永远不会发生
                //当返回这样的生命周期范围时，你应该确保它一旦被使用就会被处理掉（例如在计划任务中）
                return Container.BeginLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag);   //创建请求生存期范围
            }
        }
    }
}