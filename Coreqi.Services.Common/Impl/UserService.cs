/*
 *┌────────────────────────────────────────────────┐
 *│　描   述：UserService                                                    
 *│　作   者：fanqi                                              
 *│　版   本：1.0                                              
 *│　创建时间：2019/7/6 周六 20:43:59                        
 *└────────────────────────────────────────────────┘
 */

using Autofac.Annotation;
using Coreqi.Repository.Common.Interface;
using Coreqi.Services.Common.Interface;
using Coreqi.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coreqi.Services.Common.Impl
{
    [Bean(typeof(IUserService))]
    public class UserService : IUserService
    {
        [Autowired]
        public IUserRepository UserRepository { get; set; }

        [HttpPost]
        public void CreateUser(CreateStudentInput user)
        {
            UserRepository.CreateUser(user);
        }
    }
}