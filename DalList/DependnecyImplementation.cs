﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependnecyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencies.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        Dependency? dependency = DataSource.Dependencies.FirstOrDefault(d => d.Id == id);
        if (dependency is not null)
            return dependency;
        return null;
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}