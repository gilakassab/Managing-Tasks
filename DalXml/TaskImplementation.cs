using DalApi;
using System.Xml.Serialization;

namespace Dal;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        //throw new NotImplementedException();
        const string tasksFile = @"..\xml\tasks.xml";
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
