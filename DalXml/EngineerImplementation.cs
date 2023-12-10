using DalApi;
using DO;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    const string filePath = @"../xml/engineers.xml";
    public int Create(Engineer item)
    {
        int id = item.Id;

        XElement engineersElement = XElement.Load(filePath);

        if (engineersElement.Elements("Engineer").Any(e => (int)e.Element("Id") == id))
            throw new DalAlreadyExistsException($"Engineer with ID={id} already exists");

        XElement newEngineerElement = new XElement("Engineer",
            new XElement("Id", item.Id),
            new XElement("Name", item.Name),
            new XElement("Email", item.Email),
            new XElement("Level", item.Level),
            new XElement("Cost", item.Cost),
            new XElement("IsActive", item.IsActive)
        );

        engineersElement.Add(newEngineerElement);
        engineersElement.Save(filePath);
        return id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"Engineer is indelible entity");
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

        DataSource.Engineers.Remove(existingEngineer);
        DataSource.Engineers.Add(item);
    }
}
