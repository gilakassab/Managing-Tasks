using BlApi;
using System.Collections.Generic;
namespace BO;



internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(Task item)
    {
        Tools.ValidatePositiveNumber(item.Id, nameof(item.Id));
        Tools.ValidateNonEmptyString(item.Alias, nameof(item.Alias));

        DO.Task doTask = new DO.Task
        (item.Id, item.Description, item.Alias, false, item.CreateAt, item.RequiredEffortTime, (DO.EngineerExperience)item.Level!, item.IsActive, item.Start, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.Engineer!.Id);
        try
        {
            var dependenciesToCreate = item.Dependencies!
                .Select(task => new DO.Dependency
                {
                    DependentTask = item.Id,
                    DependsOnTask = task.Id
                })
                .ToList();
            dependenciesToCreate.ForEach(dependency => _dal.Dependency.Create(dependency));

            int newId = _dal.Task.Create(doTask);

            return newId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(t => t.Id == id);
        if (doTask == null)
            throw new BlDoesNotExistException($"Task with ID={id} does Not exist");

        List<TaskInList>? tasksList = null;
        MilestoneInTask? milestone = null;

        DO.Dependency checkMilestone = _dal.Dependency.Read(d => d.DependsOnTask == doTask.Id);
        if (checkMilestone != null)
        {
            int milestoneId = checkMilestone.DependentTask;
            DO.Task? milestoneAsATask = _dal.Task.Read(t => t.Id == milestoneId && t.Milestone);
            if (milestoneAsATask != null)
            {
                string aliasOfMilestone = milestoneAsATask.Alias;
                milestone = new MilestoneInTask()
                {
                    Id = milestoneId,
                    Alias = aliasOfMilestone
                };
            }
            else
            {
                tasksList = Tools.CalculateList(id);
            }
        }
        else
        {
            tasksList = Tools.CalculateList(id);
        }

        var engineer = _dal.Engineer.Read(e => e.Id == doTask.EngineerId);
        EngineerInTask? engineerInTask = null;
        if (engineer != null)
        {
            engineerInTask = new EngineerInTask()
            {
                Id = engineer.Id,
                Name = engineer.Name
            };
        }


        return new Task()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            Milestone = milestone,
            CreateAt = doTask.CreateAt,
            RequiredEffortTime = doTask.RequiredEffortTime,
            Level = (EngineerExperience)doTask.Level,
            IsActive = doTask.IsActive,
            Start = doTask.Start,
            ForecastDate = doTask.ForecastDate,
            Deadline = doTask.Deadline,
            Complete = doTask.Complete,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Dependencies = tasksList!,
            Engineer = engineerInTask
        };
    }

    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null)
    {
        Func<Task, bool>? filter1 = filter != null ? filter! : item => true;
        List<Task>? boTasks = new List<Task>();

        foreach (DO.Task? doTask in _dal.Task.ReadAll())
        {
            List<TaskInList>? tasksList = null;
            MilestoneInTask? milestone = null;


            var checkMilestone = _dal.Dependency.Read(d => d.DependentTask == doTask.Id);
            if (checkMilestone != null)
            {
                int milestoneId = checkMilestone.Id;
                DO.Task? milestoneAsATask = _dal.Task.Read(t => t.Id == milestoneId && t.Milestone);
                if (milestoneAsATask != null)
                {
                    string aliasOfMilestone = milestoneAsATask.Alias;
                    milestone = new MilestoneInTask()
                    {
                        Id = milestoneId,
                        Alias = aliasOfMilestone
                    };
                }
                else
                {
                    tasksList = Tools.CalculateList(doTask.Id);
                }
            }
            else
            {
                tasksList = Tools.CalculateList(doTask.Id);
            }

            var engineer = _dal.Engineer.Read(e => e.Id == doTask.EngineerId);
            EngineerInTask? engineerInTask = null;
            if (engineer != null)
            {
                engineerInTask = new EngineerInTask()
                {
                    Id = engineer.Id,
                    Name = engineer.Name
                };
            }

            boTasks!.Add(new Task()
            {
                Id = doTask!.Id,
                Description = doTask.Description,
                Alias = doTask.Alias,
                Milestone = milestone,
                RequiredEffortTime = doTask.RequiredEffortTime,
                Level = (EngineerExperience)doTask.Level!,
                IsActive = doTask.IsActive,
                CreateAt = doTask.CreateAt,
                Start = doTask.Start,
                ForecastDate = doTask.ForecastDate,
                Deadline = doTask.Deadline,
                Complete = doTask.Complete,
                Deliverables = doTask.Deliverables,
                Remarks = doTask.Remarks,
                Dependencies = tasksList,
                Engineer = engineerInTask
            });

            Console.WriteLine(doTask.Id);
        }
        return boTasks!.Where(filter1).ToList();
    }

    public void Update(Task item)
    {
        Tools.ValidatePositiveNumber(item.Id, nameof(item.Id));
        Tools.ValidateNonEmptyString(item.Alias, nameof(item.Alias));

        try
        {
            if (item.Milestone != null)
            {
                _dal.Dependency.ReadAll(d => d.DependentTask == item.Id);
                if (item.Dependencies != null)
                {
                    foreach (TaskInList doDependency in item.Dependencies)
                    {
                        DO.Dependency doDepend = new DO.Dependency(0, item.Id, doDependency.Id);
                        int idDependency = _dal.Dependency.Create(doDepend);
                    }
                }
            }

            DO.Task doTask = new DO.Task(item.Id, item.Description, item.Alias, false, item.CreateAt, item.RequiredEffortTime, (DO.EngineerExperience)item.Level, item.IsActive, item.Start, item.ForecastDate, item.Deadline, item.Complete, item.Deliverables, item.Remarks, item.Engineer.Id);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BlAlreadyExistsException($"Engineer with ID={item.Id} not exists", ex);
        }
    }
}