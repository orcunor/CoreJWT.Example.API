using CoreJWT.Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreJWT.Example.IServices
{
    public interface IUserInfoService
    {
        UserInfo Authenticate(string username, string password);
        IEnumerable<UserInfo> GetAll();
        void SignUp(UserInfo user);
    }
}
