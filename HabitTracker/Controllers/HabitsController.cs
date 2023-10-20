using AutoMapper;
using HabitTracker.Entities;
using HabitTracker.Models;
using HabitTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitTracker.Controllers;

[ApiController]
[Route("api/habits")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class HabitsController : ControllerBase
{
    private readonly IHabitService _habitService;

    public HabitsController(IHabitService habitService, IMapper mapper)
    {
        _habitService = habitService ?? throw new ArgumentNullException(nameof(habitService));
    }
    
    [HttpGet("up")]
    public IActionResult Up()
    {
        return Ok("Habits controller is up.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetHabits()
    {
        return Ok(await _habitService.GetHabitsAsync());
    }
    
    [HttpGet("{id}", Name = "GetHabit")]
    public async Task<ActionResult<HabitDto>> GetHabit([FromRoute] int id)
    {
        var habit = await _habitService.GetHabitAsync(id);
        
        if (habit == null) return BadRequest($"Habit with id {id} not found.");

        return Ok(habit);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHabit([FromBody] HabitCreationWithoutUserDto habitCreationWithoutUserDto)
    {
        if (HttpContext.Items["userId"] is not string userId) return BadRequest("UserId not present in the JWT");

        var habit = await _habitService.CreateHabitAsync(habitCreationWithoutUserDto, userId);
        
        if (habit == null) return BadRequest("Habit not created");

        return CreatedAtRoute("GetHabit", new {id = habit.Id }, habit);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHabit([FromRoute] int id)
    {
        var status = await _habitService.DeleteHabitAsync(id);
        if (status == false) return BadRequest($"No habit with id {id}");
        return NoContent();
    }
}