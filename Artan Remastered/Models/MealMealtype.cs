using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace artan_gym.Models
{
    public class MealMealtype
    {
        public List<Meal> meal { get; set; }
        public Meal addmeal { get; set; }
        public List<MealTypes> mealTypes { get; set; }
        public List<UsersCoacheVW> userinfo { get; set; }
    }
}   
