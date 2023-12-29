using BlApi;
using BO;
using System.Collections.Generic;
namespace BlImplementation;



internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(BO.Task item)
    {
        Helper.ValidatePositiveNumber(item.Id, nameof(item.Id));
        Helper.ValidateNonEmptyString(item.Alias, nameof(item.Alias));

        DO.Task doTask = new DO.Task
        (item.Id, item.Description, item.Alias, false, item.CreateAt, item.RequiredEffortTime, (DO.EngineerExperience)item.Level, item.IsActive, item.Start, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.Engineer.Id);
        try
        {
            int newId = _dal.Task.Create(doTask);

            var dependenciesToCreate = item.Dependencies
            .Select(task => new DO.Dependency
            {
                DependentTask = item.Id,
                DependsOnTask = task.Id
            })
            .ToList();
            dependenciesToCreate.ForEach(dependency => _dal.Dependency.Create(dependency));

            return newId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(t => t.Id == id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        int milestoneId = _dal.Dependency.Read(d => d.DependentTask == doTask.Id)!.Id;
        DO.Task? milestoneAsATask = _dal.Task.Read(t => t.Id == milestoneId && t.Milestone);
        BO.MilestoneInTask? milestone = null;
        if (milestoneAsATask != null)
        {
            string aliasOfMilestone = milestoneAsATask.Alias;
            milestone = new BO.MilestoneInTask()
            {
                Id = milestoneId,
                Alias = aliasOfMilestone
            };
        }
        else
        {
            var aaa=
        }

        return new BO.Task()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            Milestone = milestone,
            RequiredEffortTime = doTask.RequiredEffortTime,
            Level = (BO.EngineerExperience)doTask.Level,
            IsActive = doTask.IsActive,
            CreateAt = doTask.CreateAt,
            Start = doTask.Start,
            ForecastDate = doTask.ForecastDate,
            Deadline = doTask.Deadline,
            Complete = doTask.Complete,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Dependencies = null
        };
    }

    public IEnumerable<Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        Func<BO.Task, bool> filter1 = filter != null ? filter! : item => true;
        return (from DO.Task doTask in _dal.Task.ReadAll()
                select new BO.Task
                {
                    Id = doTask.Id,
                    Description = doTask.Description,
                    Alias = doTask.Alias,
                    Milestone = milstone,
                    RequiredEffortTime = doTask.RequiredEffortTime,
                    Level = (BO.EngineerExperience)doTask.Level,
                    IsActive = doTask.IsActive,
                    CreateAt = doTask.CreateAt,// לשנות בDO לבלי סימן שאלה
                    Start = doTask.Start,
                    ForecastDate = doTask.ForecastDate,
                    Deadline = doTask.Deadline,
                    Complete = doTask.Complete,
                    Deliverables = doTask.Deliverables,
                    Remarks = doTask.Remarks
                }).Where(filter1);
    }
    public void Update(BO.Task boTask)
    {
        DO.Task doTask = _dal.Task.Read(e => e.Id == boTask.Id);
        if (doTask is null)
            throw new BO.BlDoesNotExistException($"Task with ID={boTask.Id} does not exist");


        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boTask.Id} not exists", ex);
        }
    }
}






