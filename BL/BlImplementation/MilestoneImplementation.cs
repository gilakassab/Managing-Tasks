using BlApi;
using BO;
using DO;
using System.Numerics;
using System.Threading.Tasks;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;

    public IEnumerable<DO.Dependency> Create()
    {
        var groupedDependencies = _dal.Dependency.ReadAll()
            .GroupBy(d => d.DependentTask)
            .OrderBy(group => group.Key)
            .Select(group => (group.Key, group.Select(d => d!.DependsOnTask).ToList()))
            .ToList();

        var uniqueLists = groupedDependencies
            .Select(group => group.Item2.Distinct().ToList())
            .ToList();

        int milestoneAlias = 1;

        List<DO.Dependency> dependencies = new List<DO.Dependency>();

        foreach (var tasksList in uniqueLists)
        {
            if (tasksList != null)
            {
                DO.Task doTask = new DO.Task
                    (0,
                    $"a milestone with Id: {milestoneAlias}",
                    $"M{milestoneAlias}",
                    true,
                    DateTime.Now,
                    null, null, true, null, null, null, null, null, null, null);
                try
                {
                    int milestoneId = _dal.Task.Create(doTask);

                    foreach (var taskId in tasksList)
                    {
                        dependencies.Add(new DO.Dependency
                        {
                            DependentTask = milestoneId,
                            DependsOnTask = taskId
                        });
                    }

                    foreach (var dependencyGroup in groupedDependencies)
                    {
                        if (dependencyGroup.Item2.SequenceEqual(tasksList))
                        {
                            foreach (var dependentOnTaskId in dependencyGroup.Item2)
                            {
                                dependencies.Add(new DO.Dependency
                                {
                                    DependentTask = dependentOnTaskId,
                                    DependsOnTask = milestoneId
                                });
                            }
                        }
                    }

                    milestoneAlias++;
                }
                catch (DO.DalAlreadyExistsException ex)
                {
                    throw new BO.BlFailedToCreate($"failed to create Milestone with Alias = M{milestoneAlias}", ex);
                }
            }
        }

        //משימות שלא תלויות בשום משימה
        var independentOnTasks = _dal.Task.ReadAll()
    .Where(task => !dependencies.Any(d => d.DependentTask == task!.Id))
    .Select(task => task!.Id)
    .ToList();

        DO.Task startMilestone = new DO.Task
               (0,
               $"a milestone with Id: {0}",
               $"Start",
               true,
               DateTime.Now,
               null, null, true, null, null, null, null, null, null, null);

        //משימות ששום משימה לא תלויה בהן
        var independentTasks = _dal.Task.ReadAll()
    .Where(task => !dependencies.Any(d => d.DependsOnTask == task!.Id))
    .Select(task => task!.Id)
    .ToList();

        DO.Task endMilestone = new DO.Task
               (0,
               $"a milestone with Id: {milestoneAlias}",
               $"End",
               true,
               DateTime.Now,
               null, null, true, null, null, null, null, null, null, null);

        try
        {
            int startMilestoneId = _dal.Task.Create(startMilestone);
            int endMilestoneId = _dal.Task.Create(endMilestone);

            foreach (var task in independentOnTasks)
            {
                dependencies.Add(new DO.Dependency
                {
                    DependentTask = task,
                    DependsOnTask = startMilestoneId
                });
            }

            foreach (var task in independentTasks)
            {
                dependencies.Add(new DO.Dependency
                {
                    DependentTask = endMilestoneId,
                    DependsOnTask = task
                });
            }
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlFailedToCreate("Failed to create END or START milestone", ex);
        }

        _dal.Dependency.ReadAll().ToList().ForEach(d => _dal.Dependency.Delete(d!.Id));
        dependencies.ToList().ForEach(d => _dal.Dependency.Create(d));
        return _dal.Dependency.ReadAll()!;
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
                Status = Tools.CalculateStatus(t.Start, t.ForecastDate, t.Deadline, t.Complete)
            }).ToList();

            return new BO.Milestone()
            {
                Id = doTaskMilestone.Id,
                Description = doTaskMilestone.Description,
                Alias = doTaskMilestone.Alias,
                CreateAt = doTaskMilestone.CreateAt,
                Status = Tools.CalculateStatus(doTaskMilestone.Start, doTaskMilestone.ForecastDate, doTaskMilestone.Deadline, doTaskMilestone.Complete),
                ForecastDate = doTaskMilestone.ForecastDate,
                Deadline = doTaskMilestone.Deadline,
                Complete = doTaskMilestone.Complete,
                CompletionPercentage = (tasksInList.Count(t => t.Status == Status.OnTrack) / tasksInList.Count * 0.1) * 100,
                Remarks = doTaskMilestone.Remarks,
                Dependencies = tasksInList!
            };
        }
        catch (Exception ex)
        {
            throw new BlFailedToRead("Failed to build milestone ", ex);
        }
    }

    public void Update(BO.Milestone item)
    {
        Tools.ValidatePositiveNumber(item.Id, nameof(item.Id));
        Tools.ValidateNonEmptyString(item.Alias, nameof(item.Alias));

        try
        {
            DO.Task oldDoTask = _dal.Task.Read(t=>t.Id== item.Id)!;
            DO.Task doTask = new DO.Task(item.Id, item.Description, item.Alias, false, item.CreateAt, (TimeSpan)(item.ForecastDate - item.Deadline)!, null, true, oldDoTask.Start, item.ForecastDate, item.Deadline, item.Complete, oldDoTask.Deliverables, item.Remarks, null);
            _dal.Task.Update(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Milsetone with ID={item.Id} not exists", ex);
        }
    }
}