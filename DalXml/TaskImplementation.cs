using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class TaskImplementation : ITask
{
    const string filePath = @"tasks";

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
        //List<DO.Task> tasksList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        //if (Read(t => t.Id == id) is null)
        //    throw new DalDoesNotExistException($"An object of type task with ID {id} doesnt exists");
        //tasksList.RemoveAll(t => t.Id == id);
        //XMLTools.SaveListToXMLSerializer<DO.Task>(tasksList, "tasks");

        List<Dependency> lstDependency = XMLTools.LoadListFromXMLSerializer<Dependency>("dependencies");
        List<DO.Task> lst = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        DO.Task? task = lst.FirstOrDefault(task => task?.Id == id);
        if (task is null)
            throw new DalDoesNotExistException($"Task with ID={id} is not exist");
        if (Config.startProject >= DateTime.Now)
            throw new DalDeletionImpossible("Task cannot be deleted because the project already began");
        foreach (var dep in lstDependency)
        {
            if (dep.DependsOnTask == id)
            {
                throw new Exception($"Task with ID ={id} cannot be deleted");
            }
        }
        foreach (var dep in lstDependency)
        {
            if (dep.DependentTask == id)
            {
                lstDependency.Remove(dep);
            }
        }
        lst.Remove(task);
        XMLTools.SaveListToXMLSerializer<DO.Task>(lst, "tasks");
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath).SingleOrDefault(filter!);
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

    public void Reset()
    {
        //List<DO.Task> tasks = new List<DO.Task>();
        //XMLTools.SaveListToXMLSerializer(tasks, filePath);

        if (File.Exists(@"..\xml\tasks.xml"))
        {
            File.Delete(@"..\xml\tasks.xml");
        }

        string configFile = "data-config";
        XElement configElement = XMLTools.LoadListFromXMLElement(configFile);
        configElement.Element("NextTaskId")?.SetValue("0");
        XMLTools.SaveListToXMLElement(configElement, configFile);
    }
}
