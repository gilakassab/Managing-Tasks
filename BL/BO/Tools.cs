using BlImplementation;
using System.Reflection;

namespace BO;

public static class Tools
{
    //public static string ToStringProperty<T>(this T obj)
    //{
    //    PropertyInfo[] properties = typeof(T).GetProperties();

    //    string result = string.Join(", ", properties.Select(property =>
    //    {
    //        object? value = property.GetValue(obj);
    //        string valueString = (value != null) ? value.ToString() : "null";
    //        return $"{property.Name}: {valueString}";
    //    }));

    //    return result;
    //}

    public static string ToStringProperty<T>(this T obj)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        string result = string.Join(", ", properties.Select(property =>
        {
            object? value = property.GetValue(obj);
            string valueString;

            if (value == null)
            {
                valueString = "null";
            }
            else if (value is IEnumerable<object> enumerableValue)
            {
                valueString = string.Join(", ", enumerableValue.Select(item => item.ToString()));
            }
            else
            {
                valueString = value.ToString();
            }

            return $"{property.Name}: {valueString}";
        }));

        return result;
    }

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

    public static void ValidateEmail(string? email, string paramName)
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
        DalApi.IDal _dal = Factory.Get;

        List<BO.TaskInList> tasksList = new List<TaskInList>();
        _dal.Dependency.ReadAll(d => d.DependentTask == taskId)
                           .Select(d => _dal.Task.Read(d1 => d1.Id == d.DependsOnTask))
                           .ToList()
                           .ForEach(task =>
                           {
                               tasksList.Add(new BO.TaskInList()
                               {
                                   Id = task.Id,
                                   Alias = task.Alias,
                                   Description = task.Description,
                                   Status = (BO.Status)Tools.CalculateStatus(task.Start, task.ForecastDate, task.Deadline, task.Complete)
                               });
                           });
        return tasksList;
    }

    public static void EnterStartDateProject(DateTime startDate)
    {
        DalApi.IDal _dal = Factory.Get;
        _dal.startProject = startDate;
    }

    public static void EnterDeadLineDateProject(DateTime deadlineProject)
    {
        DalApi.IDal _dal = Factory.Get;
        _dal.deadlineProject = deadlineProject;
    }
}
