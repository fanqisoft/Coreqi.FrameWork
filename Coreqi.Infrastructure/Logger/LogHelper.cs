/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：LogHelper                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/9 周二 9:12:31                        
 *└────────────────────────────────────────────────┘
 */

using Autofac;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Coreqi.Infrastructure.Logger
{
    public static class LogHelper
    {
        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        static LogHelper()
        {

        }

        public static void ConfigLogger(ILoggerFactory loggerFactory)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            loggerFactory.AddNLog();

            //loggerFactory.AddNLog(new NLogProviderOptions
            //{
            //    CaptureMessageTemplates = true,
            //    CaptureMessageProperties = true
            //});
            //var nlogConfig = new LoggingConfiguration();
            //var traceTarget = new TraceTarget("trace")
            //{
            //    Layout =
            //        "${date}|${level:uppercase=true}|${message} ${exception}|${logger}|${all-event-properties}"
            //};
            //nlogConfig.AddTarget(traceTarget);
            //nlogConfig.AddRuleForAllLevels(traceTarget, "*");
            //LogManager.Configuration = nlogConfig;
        }

        public static ContainerBuilder CreateServiceProviderWithNLogAndAutofac(ContainerBuilder builder)
        {
            builder.RegisterType<LoggerFactory>()
                            .As<ILoggerFactory>()
                            .SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>))
                            .As(typeof(ILogger<>))
                            .SingleInstance();
            return builder;
        }

        public static void Error(string title, Exception exception, [CallerMemberName]string methodName = "")
        {
            logger.Error(exception, $"{methodName}方法出现错误！");
        }

        public static void Error(string title, string msg, [CallerMemberName]string methodName = "")
        {
            logger.Error($"{methodName}方法出现错误！，标题：{title}，附加消息：{msg}");
        }

        public static void Warn(string title, Exception exception, [CallerMemberName]string methodName = "")
        {
            logger.Warn(exception, $"{methodName}方法出现警告！");
        }

        public static void Warn(string title, string msg, [CallerMemberName]string methodName = "")
        {
            logger.Warn($"{methodName}方法出现警告！，标题：{title}，附加消息：{msg}");
        }

        public static void Warn(string title, string msg, Exception exception, [CallerMemberName]string methodName = "")
        {
            logger.Warn(exception, $"{methodName}方法出现警告！，标题：{title}，附加消息：{msg}");

        }
        public static void Warn(string title, string msg, Dictionary<string, string> addInfo, [CallerMemberName]string methodName = "")
        {

        }
        public static void Warn(string title, Exception ex, Dictionary<string, string> addInfo, [CallerMemberName]string methodName = "")
        {
        }

        /// <summary>
        /// 内部接口出错 或者 打Debug日志 用Info
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="addInfo"></param>
        /// <param name="methodName"></param>
        public static void Info(string title, string message, Dictionary<string, string> addInfo = null, [CallerMemberName]string methodName = "")
        {

        }
        public static void Info(string title, Exception exception, [CallerMemberName]string methodName = "")
        {

        }
        public static void Debug(string title, string message = "", Dictionary<string, string> addInfo = null, [CallerMemberName]string methodName = "")
        {

        }
        public static void Debug(string title, Exception exception, [CallerMemberName]string methodName = "")
        {
        }
    }
}

////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//            佛祖保佑       永不宕机     永无BUG                  //
////////////////////////////////////////////////////////////////////