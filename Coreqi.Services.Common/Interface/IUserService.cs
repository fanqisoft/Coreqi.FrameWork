using Coreqi.ViewModels.User;
using Panda.DynamicWebApi;
using Panda.DynamicWebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coreqi.Services.Common.Interface
{
    [DynamicWebApi]
    public interface IUserService:IDynamicWebApi
    {
        void CreateUser(CreateStudentInput user);
    }
}
