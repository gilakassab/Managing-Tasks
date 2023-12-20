namespace BO;

public class Milestone
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreateAt { get; set; }
    public Status Status { get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? Deadline { get; set; }
    public DateTime? Complete { get; set; }
    public double CompletionPercentage { get; set; }
    public string Remarks { get; set; }
    public List<TaskInList> Dependencies { get; set; }
    public override string ToString() => this.ToStringProperty();
}
