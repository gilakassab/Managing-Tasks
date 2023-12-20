namespace BO;

public class Task
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreateAt { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? Complete { get; set; }
    public string? Deliverables { get; set; }
    public TimeSpan RequiredEffortTime { get; set; }
    public string? Remarks { get; set; }
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperience Level { get; set; }
    public Status Status { get; set; }
    public MilestoneInTask? Milestone { get; set; }
    public List<TaskInList>? Dependencies { get; set; }
    public override string ToString() => this.ToStringProperty();
}
