using DalApi;
using DO;

namespace Dal;

internal class TaskImplementation : ITask
{
    const string filePath = @"../xml/tasks.xml";

    public int Create(DO.Task item)
    {
        int id = Config.NextTaskId;
        DO.Task copy = item with { Id = id };
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath);
        tasks.Add(copy);
        XMLTools.SaveListToXMLSerializer<DO.Task>(tasks, filePath);
        return id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"Task is indelible entity");
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath).FirstOrDefault(filter!);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return filter == null ? XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath).Select(item => item) : XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath).Where(filter!);
    }

    public void Update(DO.Task item)
    {
        var existingTask = Read(t => t.Id == item.Id);
        if (existingTask is null)
            throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist");

        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath);
        tasks.Remove(existingTask);
        tasks.Add(item);
        XMLTools.SaveListToXMLSerializer<DO.Task>(tasks, filePath);
    }
}
