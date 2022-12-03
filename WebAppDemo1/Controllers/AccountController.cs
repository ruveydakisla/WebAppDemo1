using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using WebAppDemo1.Entities;
using WebAppDemo1.Models;



namespace WebAppDemo1.Controllers
{
    //[Route("[controller]/[action]")]
    //[ApiController]
    public class AccountController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IConfiguration _configuration;
        public AccountController(DataBaseContext dataBaseContext, IConfiguration configuration)
        {
            this._dataBaseContext = dataBaseContext;
            this._configuration = configuration;
        }

      
        [HttpPost]
        //[Route("Login")]
        
        public IActionResult Login(LoginViewModel model)
        {
            


            if (ModelState.IsValid)
            {
                string md5salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = md5salt + model.Password;
                string hashedPassword = saltedPassword.MD5();

            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if(_dataBaseContext.Users.Any(x=>x.UserName.ToLower()==model.UserName.ToLower())) {
                ModelState.AddModelError("", "Username is already exist.");
                return View(model);
            
            }
            string md5salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
            string saltedPassword = md5salt + model.Password;
            string hashedPassword = saltedPassword.MD5();
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    Password = hashedPassword,
                    UserName = model.UserName,
                };
                _dataBaseContext.Users.Add(user);

                int effectedRowCount=_dataBaseContext.SaveChanges();
                if(effectedRowCount == 0) {
                    ModelState.AddModelError("", "User can not be added");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
