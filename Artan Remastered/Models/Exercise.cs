using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace artan_gym.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public int userid { get; set; }
        public int ExerciseType { get; set; }
        public float count { get; set; }
        public int setid { get; set; }
        public int PlanId { get; set; }
        public string Active { get; set; }
        public int Repeat { get; set; }
        public float Rest { get; set; }
        
    }
}
