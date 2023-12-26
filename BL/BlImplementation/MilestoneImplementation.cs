
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
        throw new NotImplementedException();
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
