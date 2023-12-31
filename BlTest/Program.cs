using BO;
using DalApi;
using DalTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        enum MainMenu { EXIT, MILESTONE, ENGINEER, TASK }
        enum SubMenu { EXIT, CREATE, READ, READALL, UPDATE, DELETE }
         
        public static void MilestoneMenu()
        {

        }
        public static void EngineerMenu()
        {
            int chooseSubMenu;
            do
            {
                Console.WriteLine("enum SubMenu { EXIT ,CREATE , READ, READALL ,UPDATE,DELETE }");
                int.TryParse(Console.ReadLine() ?? throw new Exception("Enter a number please"), out chooseSubMenu);

                switch (chooseSubMenu)
                {
                    case 1:
                        Console.WriteLine("Enter id, name,isactive, email, level, cost and role");
                        int idEngineer,
                            idTask ;
                        string nameEngineer,
                               emailEngineer,
                               inputEE,
                               inputR; ;
                        DO.EngineerExperience levelEngineer;
                        DO.Roles role;
                        bool isActive;
                        double costEngineer;
                        idEngineer = int.Parse(Console.ReadLine());
                        nameEngineer = (Console.ReadLine());
                        isActive = Console.ReadLine() == "false" ? false : true;
                        emailEngineer = Console.ReadLine();
                        inputEE = Console.ReadLine();
                        inputR = Console.ReadLine();
                        levelEngineer = (DO.EngineerExperience)Enum.Parse(typeof(DO.EngineerExperience), inputEE);
                        costEngineer = double.Parse(Console.ReadLine());
                        role = (DO.Roles)Enum.Parse(typeof(DO.Roles), inputR);
                        idTask = int.Parse(Console.ReadLine());

                        BO.Engineer newEng = new BO.Engineer()
                        {
                            Id = idEngineer,
                            Name = nameEngineer,
                            IsActive = isActive,
                            Email = emailEngineer,
                            Level = (BO.EngineerExperience)levelEngineer,
                            Cost = costEngineer,
                            Role = (BO.Roles)role,
                            Task = new BO.TaskInEngineer()
                            {
                                Id = idTask,
                                Alias = s_bl.Task.Read(idTask).Alias
                            }
                        };
                        try
                        {
                            s_bl.Engineer.Create(newEng);
                        }
                        catch(Exception ex)
                        {
                            throw new BlFailedToCreate("Failed to build engineer ", ex);
                        }
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        try {
                            if (s_bl.Engineer!.Read(id) is null)
                                Console.WriteLine("no engineer found");
                            else
                            {
                                Console.WriteLine(s_bl.Engineer!.Read(id).ToString());
                            }
                            }
                        catch(Exception ex){
                                throw new BlFailedToRead("Failed to read engineer ", ex);
                            }
                        
                        break;
                    case 3:
                        try
                        {
                            s_bl.Engineer!.ReadAll()
                            .ToList()
                            .ForEach(engineer => Console.WriteLine(engineer.ToString()));
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToReadAll("Failed to readall engineer ", ex);
                        }
                        break;
                    case 4:
                        int idEngineerUpdate,
                            idTaskUpdate;
                        string nameEngineerUpdate,
                            emailEngineerUpdate, 
                            inputUpdateEE,
                            inputUpdateR;
                        EngineerExperience levelEngineerUpdate;
                        double costEngineerUpdate;
                        bool isActiveUpdate;
                        Console.WriteLine("Enter id for reading");
                        idEngineerUpdate = int.Parse(Console.ReadLine());
                        try
                        {
                            Engineer updatedEngineer = s_bl.Engineer.Read(idEngineerUpdate);
                            Console.WriteLine(updatedEngineer.ToString());
                            Console.WriteLine("Enter name, isactive,level,cost,role and id of task to update");//if null to put the same details
                            nameEngineerUpdate = Console.ReadLine() ?? updatedEngineer?.Name;
                            isActiveUpdate = Console.ReadLine() == "false" ? false : true;
                            emailEngineerUpdate = Console.ReadLine() ?? updatedEngineer?.Email;
                            inputUpdateEE = Console.ReadLine();
                            inputUpdateR = Console.ReadLine();
                            levelEngineerUpdate = string.IsNullOrWhiteSpace(inputUpdateEE) ? updatedEngineer.Level : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), inputUpdate);
                            costEngineerUpdate = double.Parse(Console.ReadLine());
                            role = (DO.Roles)Enum.Parse(typeof(DO.Roles), inputUpdateR);
                            idTaskUpdate = int.Parse(Console.ReadLine());
                            BO.Engineer newEngUpdate = new BO.Engineer()
                            {
                                Id = idEngineerUpdate,
                                Name = nameEngineerUpdate,
                                IsActive = isActiveUpdate,
                                Email = emailEngineerUpdate,
                                Level = (BO.EngineerExperience)levelEngineerUpdate,
                                Cost = costEngineerUpdate,
                                Role = (BO.Roles)role,
                                Task = new BO.TaskInEngineer()
                                {
                                    Id = idTaskUpdate,
                                    Alias = s_bl.Task.Read(idTaskUpdate).Alias
                                }
                            };
                            try
                            {
                                s_bl.Engineer.Update(newEngUpdate);
                            }
                            catch (Exception ex)
                            {
                                throw new BlFailedToCreate($"failed to update engineer id={idEngineerUpdate}");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new BlFailedToRead($"failed to read id: {idEngineerUpdate} of engineer", ex);
                        }
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        try {
                            s_bl.Engineer!.Delete(idDelete);
                        }
                        catch(Exception ex)
                        {
                            throw new BlFailedToDelete("failed to delete engineer", ex);
                        }
                        
                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }

        private static void TaskMenu()
        {
            int chooseSubMenu;
            do
            {
                Console.WriteLine("enum SubMenu { EXIT ,CREATE , READ, READALL ,UPDATE,DELETE }");
                chooseSubMenu = int.Parse(Console.ReadLine());
                switch (chooseSubMenu)
                {
                    case 1:
                        Console.WriteLine("Enter  description, alias,deriverables, remarks,milestone, dates and task's level");
                        int taskEngineerId,
                            milestoneInTaskId,
                            engineerInTaskId,
                            taskInListId;
                        string taskDescription,
                               taskAlias,
                               taskDeliverables,
                               taskRemarks,
                               inputEE,
                               inputS;
                        bool taskMilestone,
                             isActive;
                        TimeSpan requiredEffortTime;
                        DateTime taskCreateAt, 
                                 taskStart, 
                                 taskForecastDate, 
                                 taskDeadline, 
                                 taskComplete;
                        EngineerExperience taskLevel;
                        Status statusTask;
                        List<BO.TaskInList?>taskInList=null;
                        taskDescription = Console.ReadLine();
                        taskAlias = Console.ReadLine();
                        isActive = Console.ReadLine() == "false" ? false : true;
                        taskCreateAt = DateTime.Parse(Console.ReadLine());
                        //taskMilestone = bool.Parse(Console.ReadLine());
                        
                        


                        taskStart = DateTime.Parse(Console.ReadLine());
                        taskForecastDate = DateTime.Parse(Console.ReadLine());
                        taskDeadline = DateTime.Parse(Console.ReadLine());
                        taskComplete = DateTime.Parse(Console.ReadLine());
                        taskDeliverables = Console.ReadLine();
                        requiredEffortTime = TimeSpan.Zero;
                        taskRemarks = Console.ReadLine();
                        engineerInTaskId = int.Parse(Console.ReadLine());
                        inputEE = Console.ReadLine();
                        taskLevel = (EngineerExperience)Enum.Parse(typeof(EngineerExperience), inputEE);
                        taskEngineerId = int.Parse(Console.ReadLine());
                        inputS = Console.ReadLine();
                        statusTask = (Status)Enum.Parse(typeof(Status), inputS);
                        milestoneInTaskId = int.Parse(Console.ReadLine());
                        taskInListId = int.Parse(Console.ReadLine());
                        while (taskInListId != -1)
                        {
                            taskInListId = int.Parse(Console.ReadLine());
                            taskInList.Add(new BO.TaskInList()
                            {
                                try { }
                                Id = taskInListId,
                                Description = s_bl.Task.Read(taskInListId).Description,
                                Alias = s_bl.Task.Read(taskInListId).Alias,
                                Status = BO.Helper.Calculate(Status)
                            }); ;
                        }
                        try { s_bl.Task.Create() }
                        catch(Exception ex) { throw new BlFailedToCreate("failed to create task", ex); }
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dal.Task!.Read(t => t.Id == id) is null)
                            Console.WriteLine("no task found");
                        else
                            Console.WriteLine(s_dal.Task!.Read(t => t.Id == id).ToString());
                        break;
                    case 3:
                        s_dal.Task!.ReadAll()
        .ToList()
        .ForEach(task => Console.WriteLine(task.ToString()));
                        break;
                    case 4:
                        int idTaskUpdate, currentTaskNumUpdate, taskEngineerIdUpdate;
                        string taskDescriptionUpdate, taskAliasUpdate, taskDeliverablesUpdate, taskRemarksUpdate, inputUpdate;
                        bool taskMilestoneUpdate, isActiveUpdate;
                        DateTime taskCreateAtUpdate, taskStartUpdate, taskForecastDateUpdate, taskDeadlineUpdate, taskCompleteUpdate;
                        TimeSpan requiredEffortTimeUpdate;
                        EngineerExperience taskLevelUpdate;
                        Console.WriteLine("Enter id for reading");
                        idTaskUpdate = int.Parse(Console.ReadLine());
                        DO.Task updatedTask = s_dal.Task.Read(t => t.Id == idTaskUpdate);
                        Console.WriteLine(updatedTask.ToString());
                        Console.WriteLine("Enter details to update");//if null to put the same details
                        taskDescriptionUpdate = Console.ReadLine();
                        taskAliasUpdate = Console.ReadLine();
                        taskMilestoneUpdate = bool.Parse(Console.ReadLine());
                        requiredEffortTimeUpdate = TimeSpan.Zero;
                        inputUpdate = Console.ReadLine();
                        taskLevelUpdate = string.IsNullOrWhiteSpace(inputUpdate) ? updatedTask.Level : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), inputUpdate);
                        isActiveUpdate = Console.ReadLine() == "false" ? false : true;
                        taskCreateAtUpdate = DateTime.Parse(Console.ReadLine());
                        taskStartUpdate = DateTime.Parse(Console.ReadLine());
                        taskForecastDateUpdate = DateTime.Parse(Console.ReadLine());
                        taskDeadlineUpdate = DateTime.Parse(Console.ReadLine());
                        taskCompleteUpdate = DateTime.Parse(Console.ReadLine());
                        taskDeliverablesUpdate = Console.ReadLine();
                        taskRemarksUpdate = Console.ReadLine();
                        taskEngineerIdUpdate = int.Parse(Console.ReadLine());

                        s_dal.Task.Update(new DO.Task(idTaskUpdate, taskDescriptionUpdate, taskAliasUpdate, taskMilestoneUpdate, taskCreateAtUpdate, requiredEffortTimeUpdate, taskLevelUpdate, isActiveUpdate, taskStartUpdate, taskForecastDateUpdate, taskDeadlineUpdate, taskCompleteUpdate, taskDeliverablesUpdate, taskRemarksUpdate, taskEngineerIdUpdate)); break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        s_dal.Task!.Delete(idDelete);
                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }

        static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();
            try { int chooseEntity;
            do
            {
                Console.WriteLine("enum MainMenu {  EXIT, MILESTONE, ENGINEER, TASK }");
                chooseEntity = int.Parse(Console.ReadLine());
                switch (chooseEntity)
                {
                    case 1:
                        MilestoneMenu();
                        break;
                    case 2:
                        EngineerMenu();
                        break;
                    case 3:
                        TaskMenu();
                        break;
                }
            } while (chooseEntity >= 0 && chooseEntity < 4);}
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        } } }
    




