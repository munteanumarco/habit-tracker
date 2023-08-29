using HabitTracker.Entities;
using HabitTracker.Models;

namespace HabitTracker.Repositories.Interfaces;

public interface IHabitTrackerRepository
{
    Task<IEnumerable<Habit>> GetHabitsAsync();
    Task<Habit?> GetHabitAsync(int id);
    Task<HabitDto> CreateHabitAsync(HabitCreationWithUserDto habitCreationWithoutUserDto);
    void DeleteHabitAsync(Habit habit);
}