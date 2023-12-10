namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

internal class TaskImplementation : ITask
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
        throw new DalDeletionImpossible($"Task is indelible entity");
    }

    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter!);
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        return filter == null ? DataSource.Tasks.Select(item => item) : DataSource.Tasks.Where(filter!);
    }

    public void Update(Task item)
    {
        var existingTask = Read(t => t.Id == item.Id);
        if (existingTask is null)
            throw new DalDoesNotExistException($"Task with ID={item.Id} does not exist");

        DataSource.Tasks.Remove(existingTask);
        DataSource.Tasks.Add(item);
    }
}