using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create()
    {
        throw new NotImplementedException();
    }

    public Milestone? Read(int id)
    {
        DO.Task? doTaskMilestone = _dal.Task.Read(t => t.Id == id && t.Milestone);
        if (doTaskMilestone == null)
            throw new BO.BlDoesNotExistException($"Milstone with ID={id} does Not exist");

        var dependencies = _dal.Dependency.ReadAll(d => d.DependsOnTask == doTaskMilestone.Id);
        foreach ( var d in dependencies ) { 
        
        }


        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            IsActive = doEngineer.IsActive,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost ?? 0,
            Role = (BO.Roles)doEngineer.Role
        };
    }

    public void Update(Milestone item)
    {
        DO.Task doTask = _dal.Task.Read(e => e.Id == boTask.Id);
        if (doEngineer is null)
            throw new DalDoesNotExistException($"Engineer with ID={boEngineer.Id} does not exist");


        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} not exists", ex);
        }
    }
}
