using HabitTracker.Entities;
using HabitTracker.Models;

namespace HabitTracker.Services.Interfaces;

public interface IHabitTrackerRepository
{
    Task<IEnumerable<Habit>> GetHabitsAsync();
    Task<Habit?> GetHabitAsync(int id);
    Task<HabitDto> CreateHabitAsync(HabitCreationDto habitCreationDto);
    void DeleteHabitAsync(Habit habit);
}