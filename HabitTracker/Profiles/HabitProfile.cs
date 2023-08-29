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
        CreateMap<HabitCreationWithoutUserDto, Habit>();
        CreateMap<Habit, HabitCreationWithoutUserDto>();
        CreateMap<HabitCreationWithUserDto, Habit>();
        CreateMap<Habit, HabitCreationWithUserDto>();
    }
}