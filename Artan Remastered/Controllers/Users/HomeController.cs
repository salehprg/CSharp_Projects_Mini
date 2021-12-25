using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using artan_gym.Models;
using Models.User;
using artan_gym.Helper;

namespace BackEnd.Areas.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly SignInManager<UserModel> signInManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly UserManager<UserModel> userManager;

        public HomeController(ApplicationDbContext context , 
                            SignInManager<UserModel> _signInManager , 
                            UserManager<UserModel> _userManager,
                            RoleManager<IdentityRole<int>> _roleManager)
        {
            appDbContext = context;
            signInManager = _signInManager;
            roleManager = _roleManager;
            userManager = _userManager;
        }

        public class LoginModel
        {
            public string Username {get;set;}
            public string Password {get;set;}
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            bool result = (await signInManager.PasswordSignInAsync(loginModel.Username , loginModel.Password , true , false)).Succeeded;

            if(result)
            {
                var userRoleNames = new List<string>();

                UserModel userModel = appDbContext.Users.Where(x => x.UserName == loginModel.Username).FirstOrDefault();

                userRoleNames = userManager.GetRolesAsync(userModel).Result.ToList();

                List<Claim> authClaims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userModel.UserName),
                    new Claim(JwtRegisteredClaimNames.GivenName, userModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),          
                };

                foreach (var item in userRoleNames)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, item)); // Add Users role
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Secret));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    expires: DateTime.UtcNow.AddDays(1),
                    claims: authClaims,  // Add user Roles to token
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    ) ;

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return BadRequest();
        }
    

        [HttpPut]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            try
            {
                UserModel adminUser = await userManager.FindByNameAsync("Admin");
                if(adminUser == null)
                {
                    //When admin account not available it means this block doesn't call yet
                    try
                    {
                        adminUser = new UserModel{Email = "Admin@info.com" , FirstName = "Admin" , LastName = "Admin" , UserName = "Admin"};
                        await userManager.CreateAsync(adminUser , "Admin-1234");
                        
                        IdentityRole<int> adminRole = new IdentityRole<int>{Name = "Admin"};
                        await roleManager.CreateAsync(adminRole);
                        
                        IdentityRole<int> coachRole = new IdentityRole<int>{Name = "Coach"};
                        await roleManager.CreateAsync(coachRole);

                        IdentityRole<int> userRole = new IdentityRole<int>{Name = "User"};
                        await roleManager.CreateAsync(userRole);


                        await userManager.AddToRoleAsync(adminUser , "Admin");
                    }
                    catch
                    {}
                }
            
                bool result = (await userManager.CreateAsync(userModel , userModel.Password)).Succeeded;

                if(result)
                {
                    await userManager.AddToRoleAsync(userModel , "User");

                    return Ok();
                }

                return BadRequest("نام کاربری وارد شده موجود است");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
    }

}