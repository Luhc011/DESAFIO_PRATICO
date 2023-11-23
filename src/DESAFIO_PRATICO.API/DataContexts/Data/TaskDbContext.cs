using DESAFIO_PRATICO.API.DataContexts.Data.Mapping;
using DESAFIO_PRATICO.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO_PRATICO.API.DataContexts.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<UserModel>? Users { get; set; } = null;
        public DbSet<TaskModel>? Tasks { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
        }
    }
}
