namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    //public int Create(Engineer item)
    //{
    //    int id = item.Id;

    //    XElement engineersElement = XElement.Load(filePath);

    //    if (engineersElement.Elements("Engineer").Any(e => (int)e.Element("Id") == id))
    //        throw new DalAlreadyExistsException($"Engineer with ID={id} already exists");

    //    XElement newEngineerElement = new XElement("Engineer",
    //        new XElement("Id", item.Id),
    //        new XElement("Name", item.Name),
    //        new XElement("Email", item.Email),
    //        new XElement("Level", item.Level),
    //        new XElement("Cost", item.Cost),
    //        new XElement("IsActive", item.IsActive)
    //    );

    //    engineersElement.Add(newEngineerElement);
    //    engineersElement.Save(filePath);
    //    return id;
    //}

    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id; ;
    }

    public void Delete(int id)
    {
        DataSource.Dependencies.Remove(Read(e => e.Id == id));
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(filter!);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        return filter == null ? DataSource.Dependencies.Select(item => item) : DataSource.Dependencies.Where(filter!);
    }

    public void Update(Dependency item)
    {
        var existingDependency = Read(d => d.Id == item.Id);
        if (existingDependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does not exist");

        if (existingDependency.DependsOnTask != 0)
            throw new DalDeletionImpossible($"Dependency with ID={item.Id} is indelible entity");

        DataSource.Dependencies.Remove(existingDependency);
        DataSource.Dependencies.Add(item);
    }
}