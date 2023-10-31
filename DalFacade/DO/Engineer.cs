namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level"></param>
/// <param name="Cost"></param>
public record Engineer
(
    int Id,
    string Name,
    string? Email = null,
    EngineerExperience Level = EngineerExperience.Expert,
    double? Cost = null
)
{
}