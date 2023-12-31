using AutoMapper;
using HabitTracker.DbContexts;
using HabitTracker.Entities;
using HabitTracker.Models;
using HabitTracker.Repositories.Interfaces;
using HabitTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Repositories;

public class HabitTrackerRepository : IHabitTrackerRepository
{
    private readonly HabitTrackerDbContext _context;
    private readonly IMapper _mapper;

    public HabitTrackerRepository(HabitTrackerDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<Habit>> GetHabitsAsync()
    {
        return await _context.Habits.ToListAsync();
    }

    public async Task<Habit?> GetHabitAsync(int id)
    {
        return await _context.Habits.FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<HabitDto> CreateHabitAsync(HabitCreationWithUserDto habitCreationWithoutUserDto)
    {
        var habitEntity = _mapper.Map<Habit>(habitCreationWithoutUserDto);

        await _context.Habits.AddAsync(habitEntity);

        await _context.SaveChangesAsync();

        return _mapper.Map<HabitDto>(habitEntity);
    }

    public async void DeleteHabitAsync(Habit habit)
    {
        _context.Habits.Remove(habit);
        await _context.SaveChangesAsync();
    }
}