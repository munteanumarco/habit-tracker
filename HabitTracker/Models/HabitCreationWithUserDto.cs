namespace HabitTracker.Models;

public class HabitCreationWithUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}