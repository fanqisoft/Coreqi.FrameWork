/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：ConfigHelper                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/6 周六 16:18:44                        
 *└────────────────────────────────────────────────┘
 */

using Coreqi.Infrastructure.Ioc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coreqi.Configuration
{
    public static class ConfigHelper
    {
        private static readonly IConfiguration Configuration = IocManager.Instance.Resolve<IConfiguration>();
        public static T GetConfig<T>(string key, T defaultValue)
        {
            try
            {
                var result = Configuration[key];
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception)
            {
                if (defaultValue != null)
                {
                    return defaultValue;
                }
                return default(T);
            }
        }

        public static T GetConfig<T>(string key)
        {
            try
            {
                var result = Configuration[key];
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception)
            {
                throw new Exception($"没有在配置文件中的appSettings中找到{key}的配置，请检查配置文件配置！");
            }
        }
    }
}