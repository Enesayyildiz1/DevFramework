using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.Concrete;
using DevFramework.Northwind.DataAccess.Concrete.EfProductDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
      //  GET: Account
        //private IUserService _userService;

        //public AccountController(IUserService userService)
        //{
        //    _userService = userService;
        //}
      UserManager _userService = new UserManager(new UserDal());

        public string Login(string userName,string password)
        {
            var user = _userService.GetByUserNameAndPassword(userName,password);
            if (user!=null)
            {
  AuthenticationHelper.CreateAuthCookie(
                new Guid(),
                user.UserName,
                user.EMail,
               
                DateTime.Now.AddDays(15),
                _userService.GetUserRoles(user).Select(u=>u.RoleName).ToArray(),
                false,
                user.FirstName,
               user.LastName);
            return "User is authenticated";
            }
            return "User is not authenticated";
        }
    }
}