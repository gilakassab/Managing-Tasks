using BlApi;
using System.Collections.Generic;
namespace BlImplementation;



internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Task boTask)
    {
        DO.Task doTask = new DO.Task
        (boTask.Id,
        boTask.Description,
        boTask.Alias,
        boTask.Milestone,
        boTask.RequiredEffortTime,
        (DO.EngineerExperience)boTask.Level,
        boTask.IsActive,
        boTask.CreateAt,
        boTask.Start,
        boTask.ForecastDate,
        boTask.Deadline,
        boTask.Complete,
        boTask.Deliverables,
        boTask.Remarks,
        boTask.Engineer);
        try
        {
            int id = _dal.Task.Create(doTask);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boTask.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
