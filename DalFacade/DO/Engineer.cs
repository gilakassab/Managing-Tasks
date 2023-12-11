using System.Runtime.CompilerServices;

namespace DO;

/// <summary>
/// Engineers
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level"></param>
/// <param name="Cost"></param>
/// <param name="isActive"></param>
public record Engineer
(
    int Id,
    string Name,
    bool IsActive = false,
    string? Email = null,
    EngineerExperience Level = EngineerExperience.Expert,
    double? Cost = null
)
{
    public Engineer() : this(0, "", false) { }
}
