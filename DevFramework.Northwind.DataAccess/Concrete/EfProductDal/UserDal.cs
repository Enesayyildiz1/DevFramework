using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.Entities.DTOs;

namespace DevFramework.Northwind.DataAccess.Concrete.EfProductDal
{
    public class UserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<UserRoleDto> GetUserRoles(User user)
        {
            using (NorthwindContext db = new NorthwindContext())
            {
                var liste = from ur in db.UserRoles
                            join u in db.Users
                            on ur.UserId equals u.Id
                            join r in db.Roles
                           on ur.RoleId equals r.Id
                            where ur.UserId == user.Id
                            select new UserRoleDto { UserId = u.Id, UserName = u.UserName, RoleName = r.Name };
                return liste.ToList();
                            
                         

                           

            } 

        }
    }
}
