using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace artan_gym.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public int mealtype { get; set; }
        public int userid { get; set; }
        public float mealcount { get; set; }
        public string description { get; set; }
        public int MealNumber { get; set; }
        public string Active { get; set; }

    }
}
