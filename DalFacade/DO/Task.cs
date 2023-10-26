

using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DO;
/// <summary>
/// Task management
/// </summary>
/// <param name="uniqueId">Unique ID number</param>
/// <param name="description">Description of the task</param>
/// <param name="nickname">nickname's task</param>
/// <param name="milestone"></param>
/// <param name="productionDate">the production date</param>
/// <param name="startDate">the start date</param>
/// <param name="estimatedCompletionDate">the estimated completion date</param>
/// <param name="finalDateForCompletion">the final date for completion</param>
/// <param name="actualEndDate">the actual end date</param>
/// <param name="product">description of the product </param>
/// <param name="remarks">remarks</param>
/// <param name="engineerIdAssignedToTheTask">The engineer ID assigned to the task</param>
/// <param name="difficulty">difficulty</param>
public record Task
    (
    int uniqueId,
    string description,
    string nickname,
    bool? milestone ,
    DateTime? productionDate = null,
    DateTime? startDate = null,
    DateTime? estimatedCompletionDate = null,
    DateTime? finalDateForCompletion = null,
    DateTime? actualEndDate = null,
    string product,
    string? remarks = null,
    int engineerIdAssignedToTheTask,
    string difficulty
    )
{
}
