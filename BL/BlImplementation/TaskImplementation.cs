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

        List<BO.TaskInList> tasksList = null;
        BO.MilestoneInTask? milestone = null;

        int milestoneId = _dal.Dependency.Read(d => d.DependentTask == doTask.Id)!.Id;
        DO.Task? milestoneAsATask = _dal.Task.Read(t => t.Id == milestoneId && t.Milestone);
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
            tasksList = Helper.CalculateList(id);
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
            Dependencies = tasksList
        };
    }

    public IEnumerable<Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        Func<BO.Task, bool> filter1 = filter != null ? filter! : item => true;
        List<BO.Task> boTasks = null;
        foreach (DO.Task doTask in _dal.Task.ReadAll(filter1))
        {
            List<BO.TaskInList> tasksList = null;
            BO.MilestoneInTask? milestone = null;

            int milestoneId = _dal.Dependency.Read(d => d.DependentTask == doTask.Id)!.Id;
            DO.Task? milestoneAsATask = _dal.Task.Read(t => t.Id == milestoneId && t.Milestone);
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
                tasksList = Helper.CalculateList(id);
            }
            boTasks.Add(new BO.Task()
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
                Dependencies = tasksList
            });
        }
        return boTasks;
    }

    public void Update(BO.Task item)
    {
        Helper.ValidatePositiveNumber(item.Id, nameof(item.Id));
        Helper.ValidateNonEmptyString(item.Alias, nameof(item.Alias));

        try
        {
            DO.Task doTask = new DO.Task(item.Id, item.Description, item.Alias, false, item.CreateAt, item.RequiredEffortTime, (DO.EngineerExperience)item.Level, item.IsActive, item.Start, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.Engineer.Id);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boTask.Id} not exists", ex);
        }
    }
}