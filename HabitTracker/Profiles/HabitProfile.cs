using AutoMapper;
using HabitTracker.Entities;
using HabitTracker.Models;

namespace HabitTracker.Profiles;

public class HabitProfile : Profile
{
    public HabitProfile()
    {
        CreateMap<Habit, HabitDto>();
        CreateMap<HabitDto, Habit>();
        CreateMap<HabitCreationDto, Habit>();
        CreateMap<Habit, HabitCreationDto>();
    }
}