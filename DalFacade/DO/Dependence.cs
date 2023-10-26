

namespace DO;
/// <summary>
/// Dependencies between tasks
/// </summary>
/// <param name="uniqueId">Unique ID number</param>
/// <param name="pendingTaskId">ID number of pending task</param>
/// <param name="previousAssignmentId">Previous assignment ID number</param>
public record Dependence
    
    (
    int uniqueId,
    int pendingTaskId,
    int previousAssignmentId
    )
{
}
