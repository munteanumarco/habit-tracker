using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTracker.Entities;

public sealed class Habit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Habit Name is required")]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Related UserId is required")]
    [ForeignKey(nameof(ApplicationUser))]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}