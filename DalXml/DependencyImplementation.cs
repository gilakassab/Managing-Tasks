﻿
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency
{
    const string filePath = @"dependencies";

    public int Create(Dependency item)
    {
        int id = Config.NextDependencyId;

        XElement dependenciesElement = XMLTools.LoadListFromXMLElement(filePath);

        XElement newDependencyElement = new XElement("Dependency",
             new XElement("Id", id),
             new XElement("DependentTask", item.DependentTask),
             new XElement("DependsOnTask", item.DependsOnTask)
         );

        dependenciesElement.Add(newDependencyElement);
        XMLTools.SaveListToXMLElement(dependenciesElement, filePath);
        return id;
    }

    public void Delete(int id)
    {
        XElement dependenciesElement = XMLTools.LoadListFromXMLElement(filePath);

        var dependencyToDelete = dependenciesElement.Elements("Dependency")
            .FirstOrDefault(d => (int)d.Element("Id") == id);

        if (dependencyToDelete != null)
        {
            dependencyToDelete.Remove();
            XMLTools.SaveListToXMLElement(dependenciesElement, filePath);
        }
    }

    public Dependency? Read(int id)
    {
        XElement rootElement = XMLTools.LoadListFromXMLElement(filePath);

        var query = from depElement in rootElement.Elements("Dependency")
                    where (int)depElement.Element("Id") == id
                    select new Dependency
                    {
                        Id = (int)depElement.Element("Id"),
                        DependentTask = (int)depElement.Element("DependentTask"),
                        DependsOnTask = (int)depElement.Element("DependsOnTask")
                    };

        return query.SingleOrDefault();
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement rootElement = XMLTools.LoadListFromXMLElement(filePath);

        var query = from depElement in rootElement.Elements("Dependency")
                    let dependency = new Dependency
                    {
                        Id = (int)depElement.Element("Id"),
                        DependentTask = (int)depElement.Element("DependentTask"),
                        DependsOnTask = (int)depElement.Element("DependsOnTask")
                    }
                    where filter(dependency)
                    select dependency;

        return query.SingleOrDefault();
    }

    public IEnumerable<Dependency?> ReadAll(Func<DO.Dependency, bool>? filter = null)
    {
        XElement rootElement = XMLTools.LoadListFromXMLElement(filePath);

        var query = from depElement in rootElement.Elements("Dependency")
                    let dependency = new Dependency
                    {
                        Id = (int)depElement.Element("Id"),
                        DependentTask = (int)depElement.Element("DependentTask"),
                        DependsOnTask = (int)depElement.Element("DependsOnTask")
                    }
                    where filter == null || filter(dependency)
                    select dependency;

        return query.ToList();
    }

    public void Update(Dependency item)
    {
        XElement rootElement = XMLTools.LoadListFromXMLElement(filePath);

        XElement depElement = (from d in rootElement.Elements("Dependency")
                               where (int)d.Element("Id") == item.Id
                               select d).SingleOrDefault()!;

        if (depElement != null)
        {
            depElement.Element("DependentTask").SetValue(item.DependentTask);
            depElement.Element("DependsOnTask").SetValue(item.DependsOnTask);
        }

        XMLTools.SaveListToXMLElement(rootElement, filePath);
    }

    public void Reset()
    {
        if (File.Exists(@"..\xml\dependencies.xml"))
        {
            File.Delete(@"..\xml\dependencies.xml");
        }
    }
}
