
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency
{
    public int Create(DO.Task item)
    {
        // הגדרת אוביקט= מכונה שיודעת להמיר אוביקטים מ ואל מחרוזת
        XmlSerializer serializer = new XmlSerializer(typeof(List<DO.Task>));
        // מצביע לקובץ שיודע לקרוא
        TextReader textReader = new StringReader(@"../xml/dependencies.xml");
        // 
        List<DO.Task> lst = (List<Task>?)serializer.Deserialize(textReader) ?? throw new Exception();
        // הוספת הפריט החדש
        lst.Add(item);

        using (TextWriter writer = new StreamWriter(@"../xml/dependencies.xml"))
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
