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
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> _userManager;

        public AdminController(ApplicationDbContext context , SignInManager<UserModel> signInManager , UserManager<UserModel> userManager)
        {
            this._context = context;
            this.signInManager = signInManager;
            _userManager = userManager;
        }
        SetMealView BindMealData(int userid)
        {
            SetMealView meal = new SetMealView();

            meal.mealTypes = _context.MealTypes.ToList();

            meal.UserName = _context.UsersCoacheVW.Where(i => i.Id == userid).FirstOrDefault().UserName;

            meal.meal = _context.Meals.Where(i => i.userid == userid).ToList();

            meal.mealnumbers = _context.Meals.Where(m => m.userid == userid).Select(m => m.MealNumber.ToString()) .ToList();

            meal.mealnumbers = meal.mealnumbers.Distinct().ToList();

            return meal;
        }

        async Task<SetExcerciseView>  BindExcerciseData (int userid , int planid)
        {
            SetExcerciseView setexcercise = new SetExcerciseView();
            setexcercise.exercises = await _context.Exercises.Where(Ex => Ex.userid == userid && Ex.PlanId == planid).ToListAsync();
            setexcercise.exerciseTypes = await _context.ExerciseTypes.ToListAsync();
            setexcercise.username = _context.UsersCoacheVW.Where(u => u.Id == userid).FirstOrDefault().UserName;
            int CountPlanid = _context.Exercises.Where(x => x.PlanId == planid).Count();

            if (CountPlanid > 0)
            {
                setexcercise.PlanId = _context.Exercises.Where(x => x.userid == userid).Select(x => x.PlanId).Distinct().ToList();
            }
            else
            {
                setexcercise.PlanId = _context.Exercises.Where(x => x.userid == userid).Select(x => x.PlanId).Distinct().ToList();
                setexcercise.PlanId.Add(planid);
            }

            setexcercise.SetId = _context.Exercises.Where(x => x.userid == userid && x.PlanId == planid).Select(x => x.setid).Distinct().ToList();
            return setexcercise;
        }

        [HttpGet]
        public IActionResult checkPermision()
        {
            return Ok();
        }

#region "Index Page"

        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _context.UsersCoacheVW.ToListAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        public IActionResult Meals()
        {
            try
            {
                return Ok(_context.MealTypes.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        public IActionResult Excercise()
        {
            try
            {
                return Ok(_context.ExerciseTypes.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> Coaches()
        {
            return Ok(await _context.Coaches.ToListAsync());
        }

        public IActionResult Setmeal(int id)
        {
            try
            {
                return Ok(BindMealData(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        public async Task<IActionResult> Coachprofile(int? id)
        {
            return Ok(await BindCouchProfile(id));
        }

#endregion

#region "MealType Page"

        [HttpPut]
        public IActionResult AddMealType([FromBody]MealTypes inputmeal)
        {
            try
            {
                _context.MealTypes.Add(inputmeal);
                _context.SaveChanges();

                return Ok(_context.MealTypes.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public IActionResult SaveMealType([FromBody]List<MealTypes> inputmeal)
        {
            try
            {
                _context.MealTypes.UpdateRange(inputmeal);
                _context.SaveChanges();

                return Ok(_context.MealTypes.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }
        [HttpDelete]
        public IActionResult RemoveMealType(int MealId)
        {
            try
            {
                _context.MealTypes.Remove(_context.MealTypes.Find(MealId));
                _context.SaveChanges();

                return Ok(_context.MealTypes.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }
#endregion

#region "ExType Page"

        [HttpPost]
        public async Task<IActionResult> UploadGif([FromForm]IFormCollection GifFile)
        {
            try
            {
                if(GifFile.Files.Count > 0)
                {
                    var file = GifFile.Files[0];

                    if (file != null)
                    {
                        if (file.Length > 0)
                        {
                            string path = Path.Combine(Request.Host.Value, "FrontEnd/testfrontend/build/Gifs");

                            var fs = new FileStream(Path.Combine("FrontEnd/testfrontend/build/Gifs", file.FileName), FileMode.Create);
                            await file.CopyToAsync(fs);

                            return Ok();
                        }
                    }
                }
                return Ok("فایلی برا آپلود انتخاب نشده است");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> AddExcerciseType([FromBody]ExerciseType inputmodel)
        {
            try
            {
                _context.ExerciseTypes.Add(inputmodel);
                await _context.SaveChangesAsync();

                List<ExerciseType> extypes = _context.ExerciseTypes.ToList();

                return Ok(extypes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        public IActionResult SaveExcerciseType([FromBody]List<ExerciseType> inputmodel)
        {
            try
            {
                _context.ExerciseTypes.UpdateRange(inputmodel);
                _context.SaveChanges();

                return Ok(_context.ExerciseTypes.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpDelete]
        public IActionResult RemoveExcerciseType(int ExId)
        {
            try
            {
                _context.ExerciseTypes.Remove(_context.ExerciseTypes.Find(ExId));
                _context.SaveChanges();

                return Ok(_context.ExerciseTypes.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }
#endregion

#region "SetMeal Page"

       [HttpPut]
        public IActionResult AddUserMeal([FromBody]Meal inputmodel)
        {
            try
            {
                if (inputmodel.MealNumber == 0)
                {
                    inputmodel.MealNumber = _context.Meals.Where(m => m.userid == inputmodel.userid).OrderBy(o => o.MealNumber).ToList().Select(m => m.MealNumber).LastOrDefault() + 1;
                }

                _context.Meals.Add(inputmodel);
                _context.SaveChanges();

                return Ok(BindMealData(inputmodel.userid));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public IActionResult SaveMeal([FromBody]SetMealView inputmodel , int UserId)
        {
            try
            {
                inputmodel.meal.ForEach(m => m.userid = UserId);

                _context.UpdateRange(inputmodel.meal);
                _context.SaveChanges();
                return Ok(BindMealData(UserId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpDelete]
        public IActionResult DeleteMeal(int mealid)
        {
            try
            {
                Meal mealdelete = _context.Meals.Find(mealid);

                _context.Meals.Remove(mealdelete);
                _context.SaveChanges();

                return Ok(BindMealData(mealdelete.userid));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> AddUserToCouch([FromBody]CouchprofieView form)
        {
            try
            {
                List<UserModel> userlist = new List<UserModel>();

                for (int i = 0;i < form.UsersCoacheVWswithoutcouch.Count;i++)
                {
                    if (form.selected[i])
                    {
                        UserModel user = _context.Users.Where(u => u.Id == form.UsersCoacheVWswithoutcouch[i].Id).FirstOrDefault();
                        user.couchid = form.couchid;
                        userlist.Add(user);
                    }
                }

                _context.Users.UpdateRange(userlist);
                await _context.SaveChangesAsync();

                return Ok(await BindCouchProfile(form.couchid));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

 
#endregion

#region "SetEx Page"

        public async Task <IActionResult> SetExcercise(int Id , int planid)
        {
            try
            {
            //BindExcerciseData(Id)
                return Ok(await BindExcerciseData(Id , planid));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }


        [HttpPut]
        public async Task<IActionResult> AddUserExcercise([FromBody]Exercise inputmodel , int UserId)
        {
            try
            {
                inputmodel.userid = UserId;
                inputmodel.Active = "true";
                _context.Exercises.Add(inputmodel);
                _context.SaveChanges();
                return Ok(await BindExcerciseData(inputmodel.userid , inputmodel.PlanId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveUserExcercise([FromBody]List<Exercise> inputmodel)
        {
            try
            {
                _context.Exercises.UpdateRange(inputmodel);
                _context.SaveChanges();
                return Ok(await BindExcerciseData(inputmodel[0].userid , inputmodel[0].PlanId));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserExcercise(int ExId)
        {
            try
            {
                int userid = _context.Exercises.Find(ExId).userid;
                int planid = _context.Exercises.Find(ExId).PlanId;
                _context.Exercises.Remove(_context.Exercises.Find(ExId));
                _context.SaveChanges();
                return Ok(await BindExcerciseData(userid , planid));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserPlan(int PlanId , int UserId)
        {
            try
            {
                _context.Exercises.RemoveRange(_context.Exercises.Where(x => x.PlanId == PlanId && x.userid == UserId));
                _context.SaveChanges();
                return Ok(await BindExcerciseData(UserId , 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
                throw;
            }
        }
#endregion

#region "Couch Page"
        [HttpDelete]
        public async Task<IActionResult> RemmoveUserFromCouche(int? id , int? userid)
        {
            UserModel user = _context.Users.Where(u => u.Id == userid).FirstOrDefault();
            user.couchid = 0;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
#endregion

        async Task<CouchprofieView> BindCouchProfile(int? id)
        {
            Coachlist couchprof = _context.Coaches.Where(i => i.Id == id).FirstOrDefault();

            ViewData["Couchname"] = couchprof.Coachname;
            ViewData["Address"] = couchprof.Address;
            ViewData["Phonenumber"] = couchprof.Phonenumber;
            ViewData["Email"] = couchprof.Email;

            CouchprofieView couchprofieView = new CouchprofieView();

            couchprofieView.UsersCoacheVWs = await _context.UsersCoacheVW.Where(u => u.couchid == id).ToListAsync();
            couchprofieView.UsersCoacheVWswithoutcouch = await _context.UsersCoacheVW.Where(u => u.couchid == 0).ToListAsync();
            couchprofieView.selected = new List<bool>();
            for (int i = 0; i < couchprofieView.UsersCoacheVWswithoutcouch.Count; i++)
            {
                couchprofieView.selected.Add(false);
            }

            couchprofieView.couchid = (int)id;

            return couchprofieView;
        }

    }

    public class SetExcerciseView
    {
        public List<Exercise> exercises { get; set; }
        public List<int> PlanId { get; set; }
        public List<int> SetId { get; set; }
        public List<ExerciseType> exerciseTypes { get; set; }
        public string username { get; set; }
    }

    public class SetMealView
    {
        public List<Meal> meal { get; set; }
        public Meal addmeal { get; set; }
        public List<string> mealnumbers { get; set; }
        public List<MealTypes> mealTypes { get; set; }
        public string UserName { get; set; }
    }

    public class CouchprofieView
    {
        public List<UsersCoacheVW> UsersCoacheVWs { get; set; }
        public List<UsersCoacheVW> UsersCoacheVWswithoutcouch { get; set; }
        public List<bool> selected { get; set; }
        public int couchid { get; set; }
    }
}