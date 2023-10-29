﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        if (Read(id) is not null)
            throw new Exception($"Task with ID={id} does not exist");
        DataSource.Tasks.Remove(Read(id));
    }

    public Task? Read(int id)
    {
        Task? task = DataSource.Tasks.FirstOrDefault(t => t.Id == id);
        if (task is not null)
            return task;
        return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    {
        if (Read(item.Id) is not null)
            throw new Exception($"Task with ID={item.Id} does not exist");
        Delete(item.Id);
        DataSource.Tasks.Add(item);
    }
}
