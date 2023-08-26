using Microsoft.AspNetCore.Identity;

namespace HabitTracker.Entities;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Habit> Habits { get; set; }
}