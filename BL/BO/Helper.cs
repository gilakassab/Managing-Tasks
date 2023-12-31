
using DO;

namespace BlImplementation;

internal static class Helper
{
    public static void ValidatePositiveId(int? id, string paramName)
    {
        if (id <= 0)
            throw new BO.BlInvalidDataException($"Invalid {paramName} ID. Must be a positive number.");
    }

    public static void ValidateNonEmptyString(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BO.BlInvalidDataException($"{paramName} cannot be empty.");
    }

    public static void ValidatePositiveNumber(double? number, string paramName)
    {
        if (number <= 0)
            throw new BO.BlInvalidDataException($"Invalid {paramName}. Must be a positive number.");
    }

    public static void ValidateEmail(string email?, string paramName)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            if (addr.Address != email)
                throw new BO.BlInvalidDataException($"Invalid email address for {paramName}.");
        }
        catch
        {
            throw new BO.BlInvalidDataException($"Invalid email address for {paramName}.");
        }
    }

    public static Status CalculateStatus(DateTime? start, DateTime? forecastDate, DateTime? deadline, DateTime? complete)
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

        //if (startDate == null && baselineStartDate == null)
        //    return Status.Unscheduled;

        //if (startDate != null && baselineStartDate != null && scheduledStartDate != null)
        //    return Status.Scheduled;

        //if (startDate != null && completeDate != null && completeDate <= forecastDate)
        //    return Status.OnTrack;

        //if (startDate != null && completeDate != null && deadlineDate != null && completeDate <= forecastDate)
        //    return Status.InJeopardy;

        //return Status.Unscheduled;
    }

    public static List<BO.TaskInList>? CalculateList(int taskId)
    {
        List<BO.TaskInList> tasksList = null;
        _dal.Dependency.ReadAll(d => d.DependentTask == taskId)
                           .Select(d => _dal.Task.Read(d.DependsOnTask))
                           .ToList()
                           .ForEach(task =>
                           {
                               tasksList.Add(new BO.TaskInList()
                               {
                                   Id = task.Id,
                                   Alias = task.Alias,
                                   Description = task.Description,
                                   Status = Helper.CalculateStatus(task.Start, task.ForecastDate, task.Deadline, task.Complete)
                               });
                           }
       return tasksList;
    }
}