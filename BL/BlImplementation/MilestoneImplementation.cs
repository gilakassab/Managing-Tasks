using BlApi;
using BO;

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
        throw new NotImplementedException();
    }
}
