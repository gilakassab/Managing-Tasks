using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string filePath = @"engineers";
    public int Create(Engineer item)
    {
        int id = item.Id;

        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(filePath);

        if (engineers.Any(e => e.Id == id))
            throw new DalAlreadyExistsException($"Engineer with ID={id} already exists");

        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers,filePath);
        return id;
    }

    public void Delete(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (Read(e => e.Id == id) is null)
            throw new DalDoesNotExistException($"An object of type engineer with ID {id} doesnt exists");
        engineersList.RemoveAll(t => t.Id == id);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return XMLTools.LoadListFromXMLSerializer<Engineer>(filePath).FirstOrDefault<Engineer>(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        return filter == null ? XMLTools.LoadListFromXMLSerializer<Engineer>(filePath).Select(item => item) : XMLTools.LoadListFromXMLSerializer<Engineer>(filePath).Where(filter!);
    }

    public void Update(Engineer item)
    {
        var existingEngineer = Read(e => e.Id == item.Id);
        if (existingEngineer is null)
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exist");

        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(filePath);
        engineers.Remove(existingEngineer);
        engineers.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineers, filePath);
    }

    public void Reset()
    {
        //List<Engineer> engineers = new List<Engineer>();
        //XMLTools.SaveListToXMLSerializer(engineers, filePath);

        if (File.Exists(@"..\xml\engineers.xml"))
        {
            File.Delete(@"..\xml\engineers.xml");
        }
    }
}
