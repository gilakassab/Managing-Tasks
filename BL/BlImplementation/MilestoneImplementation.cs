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
            Status = CalculateStatus(t.Start, t.ForecastDate, t.Deadline, t.Complete)
        }).ToList();

        return new BO.Milestone()
        {
            Id = doTaskMilestone.Id,
            Description = doTaskMilestone.Description,
            Alias = doTaskMilestone.Alias,
            CreateAt = doTaskMilestone.CreateAt,
            Status = CalculateStatus(doTaskMilestone.Start, doTaskMilestone.ForecastDate, doTaskMilestone.Deadline, doTaskMilestone.Complete),
            ForecastDate = doTaskMilestone.ForecastDate,
            Deadline = doTaskMilestone.Deadline,
            Complete = doTaskMilestone.Complete,
            CompletionPercentage = (tasksInList.Count(t => t.Status == Status.OnTrack) / (double)tasksInList.Count) * 100,
            Remarks = doTaskMilestone.Remarks,
            Dependencies = tasksInList
        };
    }

    public void Update(BO.Milestone boMilestone)
    {
        // קריאה לאבן דרך ממקור הנתונים
        DO.Milestone doMilestone = _dal.Milestone.Read(boMilestone.Id);
        if (doMilestone == null)
        {
            throw new BO.BlDoesNotExistException($"Milestone with ID={boMilestone.Id} does not exist");
        }

        // ביצוע העדכון על האבן דרך
        // (בדוגמה זו, העדכון מתבצע רק על השדות שניתן לעדכן: כינוי, תיאור, הערות)
        doMilestone.Alias = boMilestone.Alias;
        doMilestone.Description = boMilestone.Description;
        doMilestone.Remarks = boMilestone.Remarks;

        // ביצוע ניסיון לעדכון בשכבת הנתונים
        try
        {
            _dal.Milestone.Update(doMilestone);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Milestone with ID={boMilestone.Id} already exists", ex);
        }

        // שליפת הגרסה העדכנית של האבן דרך ממקור הנתונים
        DO.Milestone updatedDoMilestone = _dal.Milestone.Read(boMilestone.Id);

        // יצירת אובייקט מסוג Milestone מעודכן לפי המידע ששולף ממקור הנתונים
        BO.Milestone updatedBoMilestone = new BO.Milestone
        {
            Id = updatedDoMilestone.Id,
            Description = updatedDoMilestone.Description,
            Alias = updatedDoMilestone.Alias,
            CreateAt = updatedDoMilestone.CreateAt,
            // וכן הלאה - שאר השדות של Milestone
            // ...
        };

        return updatedBoMilestone;
    }

    public Status CalculateStatus(DateTime? start, DateTime? forecastDate, DateTime? deadline, DateTime? complete)
    {
        if (start == null && deadline == null)
            return Status.Unscheduled;

        if (start != null && deadline != null && complete == null)
            return Status.Scheduled;

        if (start != null && complete != null && complete <= forecastDate)
            return Status.OnTrack;

        if (start != null && complete != null && complete > forecastDate)
            return Status.InJeopardy;

        return Status.Unscheduled;
    }
}
