
using BlApi;
using System.Data;
using System.Collections.Generic;
namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
    public int Create(BO.Engineer boEngineer)
    {
       DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.Id,
       boEngineer.Name,
       boEngineer.IsActive,
       boEngineer.Email,
       (DO.EngineerExperience)boEngineer.Level,
       boEngineer.Cost,
       (DO.Roles)boEngineer.Role);
        try
        {
            int id = _dal.Engineer.Create(doEngineer);
            return id;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new BO.BlDeletionImpossible($"Engineer is indelible entity");
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(e => e.Id == id);
        if (doEngineer == null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        return new BO.Engineer()
        {
            Id = id,
            Name = doEngineer.Name,
            IsActive = doEngineer.IsActive,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost??0,
            Role = (BO.Roles)doEngineer.Role
        };
    }
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        Func<BO.Engineer, bool>  filter1 = filter != null ? filter! : item => true;
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    Id = doEngineer.Id,
                    Name = doEngineer.Name,
                    IsActive = doEngineer.IsActive,
                    Email = doEngineer.Email,
                    Level = (BO.EngineerExperience)doEngineer.Level,
                    Cost = doEngineer.Cost ?? 0,
                    Role = (BO.Roles)doEngineer.Role
                }).Where(filter1);
    }

    public void Update(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = _dal.Engineer.Read(e => e.Id == boEngineer.Id);
        if (doEngineer is null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={boEngineer.Id} does not exist");


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

