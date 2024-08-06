using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MizeApi.Helper;
using System.Linq;
using System.Threading.Tasks;

namespace MizeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MizePortalDB _mizePortalDB;

        public UserController(MizePortalDB mizePortalDB)
        {
            _mizePortalDB = mizePortalDB;
        }

        [HttpPost("Login")]
        public async Task<User> ChechLogin(User userModel, string application)
        {
            var user = _mizePortalDB.Users.Where(r => r.UserName == userModel.UserName && r.Password == userModel.Password).Where(t => t.Applications.Contains(application) || t.Roles.Contains("SuperAdmin")).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
                return null;
        }

        [HttpPost("Register")]
        public async Task<User> Register(User userModel)
        {
           var newUser =  _mizePortalDB.Users.InsertWithIdentity(() => new User
            {
                UserName = userModel.UserName,
                Password = userModel.Password,
                Applications = "",
                IsActive = false,
                Roles = ""
            });
            return _mizePortalDB.Users.FirstOrDefault(c=>c.Id == newUser.ObjToInt(0));
        }
    }
}
