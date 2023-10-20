using HabitTracker.Entities;
using HabitTracker.Models;

namespace HabitTracker.Services.Interfaces;

public interface IHabitService
{
    Task<IEnumerable<HabitDto>> GetHabitsAsync();
    Task<HabitDto?> GetHabitAsync(int id);
    Task<HabitDto?> CreateHabitAsync(HabitCreationWithoutUserDto habitCreationWithoutUserDto, string userId);
    Task<bool> DeleteHabitAsync(int id);
}