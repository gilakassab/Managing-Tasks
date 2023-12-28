using BlApi;
using System.Data;
using System.Collections.Generic;
using BO;
using DO;
using System.Xml.Linq;
using System.Reflection.Emit;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {

        Helper.ValidatePositiveId(boEngineer.Id, nameof(boEngineer.Id));
        Helper.ValidateNonEmptyString(boEngineer.Name, nameof(boEngineer.Name));
        Helper.ValidateEmail(boEngineer.Name, nameof(boEngineer.Name));
        Helper.ValidatePositiveNumber(boEngineer.Cost, nameof(boEngineer.Cost));

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
            int newId = _dal.Engineer.Create(doEngineer);
            return newId;
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

        return CreateBOFromDO(doEngineer);
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        Func<BO.Engineer, bool> filter1 = filter != null ? filter! : item => true;
        return (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                let boEngineer = CreateBOFromDO(doEngineer)
                select boEngineer).Where(filter1);
    }

    public void Update(BO.Engineer boEngineer)
    {
        Helper.ValidatePositiveId(boEngineer.Id, nameof(boEngineer.Id));
        Helper.ValidateNonEmptyString(boEngineer.Name, nameof(boEngineer.Name));
        Helper.ValidateEmail(boEngineer.Email, nameof(boEngineer.Email));
        Helper.ValidatePositiveNumber(boEngineer.Cost, nameof(boEngineer.Cost));

        DO.Engineer newDoEngineer = new DO.Engineer
           (boEngineer.Id,
           boEngineer.Name,
        boEngineer.IsActive,
        boEngineer.Email,
           (DO.EngineerExperience)boEngineer.Level,
           boEngineer.Cost,
           (DO.Roles)boEngineer.Role);

        try
        {
            _dal.Engineer.Update(newDoEngineer);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} not exists", ex);
        }
    }

    private BO.Engineer CreateBOFromDO(DO.Engineer doEngineer)
    {
        var doTasks = _dal.Task.ReadAll(t => t.EngineerId == doEngineer.Id && Helper.CalculateStatus(t.Start, t.ForecastDate, t.Deadline, t.Complete) == BO.Status.OnTrack).FirstOrDefault();
        TaskInEngineer taskInEngineer = null;
        if (doTasks != null)
        {
            taskInEngineer = new BO.TaskInEngineer
            {
                Id = doTasks.Id,
                Alias = doTasks.Alias
            };
        }
        
        return new BO.Engineer()
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            IsActive = doEngineer.IsActive,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level,
            Cost = doEngineer.Cost ?? 0,
            Role = (BO.Roles)doEngineer.Role,
            Task = taskInEngineer
        }; ;
    }
}