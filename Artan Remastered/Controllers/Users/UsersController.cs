using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System.IO;
using artan_gym.Models;
using Models.User;

namespace BackEnd.Areas.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> userManager;

        public UsersController(ApplicationDbContext context , SignInManager<UserModel> signInManager , UserManager<UserModel> _userManager)
        {
            appDbContext = context;
            this.signInManager = signInManager;
            userManager = _userManager;
        }


        [HttpGet]
        
        public IActionResult getUserDetail()
        {
            string userName = userManager.GetUserId(User);
            UserModel user = appDbContext.Users.Where(x => x.UserName == userName).FirstOrDefault();

            List<MealView> mealViews = appDbContext.MealView.Where(x => x.userid == user.Id).ToList();
            List<ExerciseView> exerciseViews = appDbContext.ExerciseView.Where(x => x.userid == user.Id).ToList();
            
            
            UserDataVW userDataVW = new UserDataVW();
            userDataVW.userExreciseView = exerciseViews;
            userDataVW.userMealView = mealViews;
            userDataVW.userInfo = user;

            return Ok(userDataVW);

        }
    }

}