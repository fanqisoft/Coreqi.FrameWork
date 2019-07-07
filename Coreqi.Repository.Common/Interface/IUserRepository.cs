using Coreqi.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coreqi.Repository.Common.Interface
{
    public interface IUserRepository
    {
        void createUser(UserInsertDto user);
    }
}
