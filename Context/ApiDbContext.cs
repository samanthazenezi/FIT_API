using Fit_API.src.Models;
using Microsoft.EntityFrameworkCore;

namespace Fit_API.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<TrainingModel> Trainings { get; set; }
    public DbSet<ExercisesModel> Exercises { get; set; }
}
