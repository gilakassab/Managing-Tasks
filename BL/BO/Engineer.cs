using System.Runtime.CompilerServices;
namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }
    public EngineerExperience Level { get; init; }
    public double Cost { get; set; }
    public int CurrentTaskId { get; set; }
    public string CurrentTaskDescription { get; set; }

    public override string ToString() => this.ToStringProperty();
}
