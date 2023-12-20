
using BlApi;
using BO;

namespace BlImplementation;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.Id, boEngineer.Name,  boEngineer.IsActive, boEngineer.Email, (DO.EngineerExperience)boEngineer.Level, boEngineer.Cost, (DO.Roles)boEngineer.Role);
        try
        {
            int id = _dal.Engineer.Create(doEngineer);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={boEngineer.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer> ReadAll(Roles role)
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
