using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.User;

namespace artan_gym.Models
{
    public class ApplicationDbContext : IdentityDbContext<UserModel , IdentityRole<int> , int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Coachlist> Coaches {get; set;}
        public DbSet<Exercise> Exercises {get; set;}
        public DbSet<ExerciseType> ExerciseTypes {get; set;}
        public DbSet<Meal> Meals {get; set;}
        public DbSet<MealTypes> MealTypes {get; set;}
        public DbSet<UsersCoacheVW> UsersCoacheVW {get; set;}


        public DbSet<MealView> MealView {get; set;}
        public DbSet<ExerciseView> ExerciseView {get; set;}
        


        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     base.OnModelCreating(builder);

        // }
    }
}