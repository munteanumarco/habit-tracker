using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.DbContexts;

public class HabitTrackerDbContext : IdentityDbContext<IdentityUser>
{
    public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) : base(options)
    {
    }
}