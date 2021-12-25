using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace artan_gym.Models
{
    public class MealView
    {
        public int Id { get; set; }//Meal Type id
        public int mealtype { get; set; }
        public int userid { get; set; }
        public float mealcount { get; set; }
        public string description { get; set; }
        public int MealNumber { get; set; }
        public string Active { get; set; }
        public string mealName { get; set; }
        public string Mealunit { get; set; }


    }
}
