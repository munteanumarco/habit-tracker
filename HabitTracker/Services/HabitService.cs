using AutoMapper;
using HabitTracker.Entities;
using HabitTracker.Models;
using HabitTracker.Repositories.Interfaces;
using HabitTracker.Services.Interfaces;

namespace HabitTracker.Services;

public class HabitService : IHabitService
{
    private readonly IHabitTrackerRepository _repository;
    private readonly IMapper _mapper;
    
    public HabitService(IHabitTrackerRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<HabitDto>> GetHabitsAsync()
    {
        var habits = await _repository.GetHabitsAsync();
        return _mapper.Map<IEnumerable<HabitDto>>(habits);
    }

    public async Task<HabitDto?> GetHabitAsync(int id)
    {
        var habit = await _repository.GetHabitAsync(id);
        return habit != null ? _mapper.Map<HabitDto>(habit) : null;
    }

    public async Task<HabitDto?> CreateHabitAsync(HabitCreationWithUserDto habitCreationWithoutUserDto)
    {
        return await _repository.CreateHabitAsync(habitCreationWithoutUserDto);
    }

    public async Task<bool> DeleteHabitAsync(int id)
    {
        var habitToDelete = await _repository.GetHabitAsync(id);
        
        if (habitToDelete == null) return false;
        
        _repository.DeleteHabitAsync(habitToDelete);
        
        return true;
    }
}