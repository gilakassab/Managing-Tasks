namespace DO;

/// <summary>
/// Task management
/// </summary>
/// <param name="Id"></param>
/// <param name="Description"></param>
/// <param name="Alias"></param>
/// <param name="Milestone"></param>
/// <param name="CreateAt"></param>
/// <param name="Start"></param>
/// <param name="ForecastDate"></param>
/// <param name="Deadline"></param>
/// <param name="Complete"></param>
/// <param name="Deliverables"></param>
/// <param name="Remarks"></param>
/// <param name="EngineerId"></param>
/// <param name="Level"></param>
public record Task
    (
    int Id,
    string Description,
    string Alias,
    bool Milestone,
    DateTime? CreateAt = null,
    DateTime? Start = null,
    DateTime? ForecastDate = null,
    DateTime? Deadline = null,
    DateTime? Complete = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null,
    EngineerExperience Level = EngineerExperience.Expert,
       bool isActive = false
 )
{
}
