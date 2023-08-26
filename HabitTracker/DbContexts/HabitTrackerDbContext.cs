using HabitTracker.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.DbContexts;

public class HabitTrackerDbContext : IdentityDbContext<IdentityUser>
{
    public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) : base(options)
    {
    }
    
    public DbSet<Habit> Habits { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Habit>()
            .HasOne(h => h.User)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.UserId)
            .IsRequired();
    }
}