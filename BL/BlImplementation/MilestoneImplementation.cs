using BlApi;
using BO;
using DO;
using System.Numerics;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
    
    public int Create()
    {
        throw new NotImplementedException();
    }

    public Milestone? Read(int id)
    {
        try
        {
            DO.Task? doTaskMilestone = _dal.Task.Read(t => t.Id == id && t.Milestone);
            if (doTaskMilestone == null)
                throw new BO.BlDoesNotExistException($"Milstone with ID={id} does Not exist");

            var tasksId = _dal.Dependency.ReadAll(d => d.DependsOnTask == doTaskMilestone.Id)
                                         .Select(d => d.DependentTask);
            var tasks = _dal.Task.ReadAll(t => tasksId.Contains(t.Id)).ToList();

            var tasksInList = tasks.Select(t => new BO.TaskInList
            {
                Id = t.Id,
                Description = t.Description,
                Alias = t.Alias,
                Status = Helper.CalculateStatus(t.Start, t.ForecastDate, t.Deadline, t.Complete)
            }).ToList();

            return new BO.Milestone()
            {
                Id = doTaskMilestone.Id,
                Description = doTaskMilestone.Description,
                Alias = doTaskMilestone.Alias,
                CreateAt = doTaskMilestone.CreateAt,
                Status = Helper.CalculateStatus(doTaskMilestone.Start, doTaskMilestone.ForecastDate, doTaskMilestone.Deadline, doTaskMilestone.Complete),
                ForecastDate = doTaskMilestone.ForecastDate,
                Deadline = doTaskMilestone.Deadline,
                Complete = doTaskMilestone.Complete,
                CompletionPercentage = (tasksInList.Count(t => t.Status == Status.OnTrack) / (double)tasksInList.Count) * 100,
                Remarks = doTaskMilestone.Remarks,
                Dependencies = tasksInList
            };
        }
        catch (Exception ex)
        {
            throw new BlFailedToRead("Failed to build milestone ", ex);
        }
    }

    public void Update(BO.Milestone item)
    {
        Helper.ValidatePositiveNumber(item.Id, nameof(item.Id));
        Helper.ValidateNonEmptyString(item.Alias, nameof(item.Alias));

        try
        {
            DO.Task doTask = new DO.Task(item.Id, item.Description, item.Alias, false, item.CreateAt, (TimeSpan)(item.ForecastDate - item.Deadline), null, true, item.Start, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.Engineer.Id);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Milsetone with ID={item.Id} not exists", ex);
        }
    }
}