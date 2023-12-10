using DalApi;
using System.Xml.Serialization;

namespace Dal;

internal class TaskImplementation : ITask
{ 
    public int Create(Task item)
    {
        // הגדרת אוביקט= מכונה שיודעת להמיר אוביקטים מ ואל מחרוזת
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Task>));
        // מצביע לקובץ שיודע לקרוא
        TextReader textReader = new StringReader(@"../xml/tasks.xml");
        // 
        List<Task> lst = (List<Task>?)serializer.Deserialize(textReader) ?? throw new Exception();
        // הוספת הפריט החדש
        lst.Add(item);

        using (TextWriter writer = new StreamWriter(@"../xml/tasks.xml"))
        {
            serializer.Serialize(writer, lst);
        }

        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
