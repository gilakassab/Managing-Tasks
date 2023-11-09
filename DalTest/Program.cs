using DalApi;
using Dal;
using DO;
using Microsoft.VisualBasic.FileIO;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace DalTest
{
    internal class Program
    { private static IDependency? s_dalDependency = new DependnecyImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1

        enum MainMenu{ EXIT=0,DEPENDENCY=1,ENGINEER=2,TASK=3}
        enum SubMenu { EXIT ,CREATE , READ, READALL ,UPDATE,DELETE }
        private static void SubMenuFunc(int currentEntity)
        {
            int chooseSubMenu = int.Parse(Console.ReadLine());
            while (chooseSubMenu != 0)
            {
                switch (currentEntity) {
                    case 1:
                     
                      switch (chooseSubMenu)
                    {
                        case 1:
                                Console.WriteLine("Enter details for all the characteristics");
                                int dependentTask, dependsOnTask;
                                dependentTask = int.Parse(Console.ReadLine());
                                dependsOnTask = int.Parse(Console.ReadLine());
                                s_dalDependency = new DependnecyImplementation();
                                Dependency newDependency = new(0, dependentTask, dependsOnTask);
                                s_dalDependency!.Create(newDependency);
                                break;
                        case 2:
                                int id;
                                Console.WriteLine("Enter id for reading");
                                id= int.Parse(Console.ReadLine());
                                s_dalDependency!.Read(id);
                                break;
                        case 3:
                                s_dalDependency!.ReadAll();break;
                        case 4:
                                int idUpdate, dependentTaskUpdate, dependsOnTaskUpdate;
                                Console.WriteLine("Enter id for reading");
                                idUpdate = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalDependency!.Read(idUpdate).ToString());
                                Console.WriteLine("Enter details to update");
                                dependentTaskUpdate = int.Parse(Console.ReadLine());
                                dependsOnTaskUpdate = int.Parse(Console.ReadLine());
                                Dependency newDependencyUpdate = new(idUpdate, dependentTaskUpdate, dependsOnTaskUpdate);
                                s_dalDependency!.Update(newDependencyUpdate);break;
                        case 5:
                                int idDelete;
                                Console.WriteLine("Enter id for deleting");
                                idDelete = int.Parse(Console.ReadLine());
                                s_dalDependency!.Delete(idDelete); break;
                        }
                    break;

                    case2:
                      switch (chooseSubMenu)
                        {
                         case 1:
                                Console.WriteLine("Enter id, name, email, cost and a number to choose experience");
                                int idEngineer,currentNum;
                                string nameEngineer, emailEngineer;
                                EngineerExperience levelEngineer;
                                double costEngineer;
                                idEngineer = int.Parse(Console.ReadLine());
                                nameEngineer = (Console.ReadLine());
                                emailEngineer = Console.ReadLine();
                                costEngineer = double.Parse(Console.ReadLine());
                                currentNum = int.Parse(Console.ReadLine());
                                switch (currentNum)
                                {
                                    case 1: levelEngineer = EngineerExperience.Expert;break;
                                    case 2: levelEngineer = EngineerExperience.Junior; break;
                                    case 3: levelEngineer = EngineerExperience.Rokkie; break;
                                }
                                s_dalEngineer = new EngineerImplementation();
                                Engineer newEngineer = new(idEngineer, nameEngineer, emailEngineer,levelEngineer,costEngineer;
                                s_dalEngineer!.Create(newEngineer);
                                break;
                            case 2:
                                int id;
                                Console.WriteLine("Enter id for reading");
                                id = int.Parse(Console.ReadLine());
                                s_dalEngineer!.Read(id);
                                break;
                            case 3:
                                s_dalEngineer!.ReadAll(); break;
                            case 4:
                                int idEngineerUpdate, currentNumUpdate;
                                string nameEngineerUpdate, emailEngineerUpdate;
                                EngineerExperience levelEngineerUpdate;
                                double costEngineerUpdate;
                                Console.WriteLine("Enter id for reading");
                                idEngineerUpdate = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalEngineer!.Read(idEngineerUpdate).ToString());
                                Console.WriteLine("Enter details to update");//if null to put the same details
                                nameEngineerUpdate = (Console.ReadLine());
                                emailEngineerUpdate = Console.ReadLine();
                                costEngineerUpdate = double.Parse(Console.ReadLine());
                                currentNumUpdate = int.Parse(Console.ReadLine());
                                switch (currentNum)
                                {
                                    case 1: levelEngineerUpdate = EngineerExperience.Expert; break;
                                    case 2: levelEngineerUpdate = EngineerExperience.Junior; break;
                                    case 3: levelEngineerUpdate = EngineerExperience.Rokkie; break;
                                }
                                Engineer newEngineerUpdate = new(idEngineerUpdate, nameEngineerUpdate, emailEngineerUpdate,levelEngineerUpdate,costEngineerUpdate);
                                s_dalEngineer!.Update(newEngineerUpdate); break;
                            case 5:
                                int idDelete;
                                Console.WriteLine("Enter id for deleting");
                                idDelete = int.Parse(Console.ReadLine());
                                s_dalEngineer!.Delete(idDelete); break;
                        }
                        break;

                    case 3:
                        switch (chooseSubMenu)
                        {
                            case 1:
                                Console.WriteLine("Enter  description, alias,deriverables, remarks,milestone, dates and task's level");
                                int taskEngineerId, currentTaskNum;
                                string taskDescription, taskAlias, taskDeliverables, taskRemarks;
                                bool taskMilestone;
                                DateTime taskCreateAt, taskStart, taskForecastDate, taskDeadline, taskComplete;
                                EngineerExperience taskLevel;
                                taskMilestone=bool.Parse(Console.ReadLine());
                                taskEngineerId = int.Parse(Console.ReadLine());
                                taskDescription = Console.ReadLine();
                                taskAlias = Console.ReadLine();
                                taskDeliverables = Console.ReadLine();
                                taskRemarks = Console.ReadLine();
                                taskCreateAt = DateTime.Parse(Console.ReadLine());
                                taskStart = DateTime.Parse(Console.ReadLine());
                                taskForecastDate = DateTime.Parse(Console.ReadLine());
                                taskDeadline = DateTime.Parse(Console.ReadLine());
                                taskComplete = DateTime.Parse(Console.ReadLine());
                                currentTaskNum = int.Parse(Console.ReadLine());
                                switch (taskLevel)
                                {
                                    case 1: levelEngineer = EngineerExperience.Expert; break;
                                    case 2: levelEngineer = EngineerExperience.Junior; break;
                                    case 3: levelEngineer = EngineerExperience.Rokkie; break;
                                }
                                s_dalTask = new TaskImplementation();
                                Task newTask = new(0,taskDescription,taskAlias, taskMilestone, taskCreateAt, taskStart, taskForecastDate,taskDeadline,taskComplete,taskDeliverables,taskRemarks,taskEngineerId,taskLevel;
                                s_dalTask!.Create(newTask);
                                break;
                            case 2:
                                int id;
                                Console.WriteLine("Enter id for reading");
                                id = int.Parse(Console.ReadLine());
                                s_dalTask!.Read(id);
                                break;
                            case 3:
                                s_dalTask!.ReadAll(); break;
                            case 4:
                                int idTaskUpdate, currentTaskNum;
                                string taskDescription, taskAlias, taskDeliverables, taskRemarks;
                                bool taskMilestone;
                                DateTime taskCreateAt, taskStart, taskForecastDate, taskDeadline, taskComplete;
                                EngineerExperience taskLevel;
                                Console.WriteLine("Enter id for reading");
                                idTaskUpdate = int.Parse(Console.ReadLine());
                                Console.WriteLine(s_dalTask!.Read(idTaskUpdate).ToString());
                                Console.WriteLine("Enter details to update");//if null to put the same details
                                taskMilestone = bool.Parse(Console.ReadLine());
                                taskEngineerId = int.Parse(Console.ReadLine());
                                taskDescription = Console.ReadLine();
                                taskAlias = Console.ReadLine();
                                taskDeliverables = Console.ReadLine();
                                taskRemarks = Console.ReadLine();
                                taskCreateAt = DateTime.Parse(Console.ReadLine());
                                taskStart = DateTime.Parse(Console.ReadLine());
                                taskForecastDate = DateTime.Parse(Console.ReadLine());
                                taskDeadline = DateTime.Parse(Console.ReadLine());
                                taskComplete = DateTime.Parse(Console.ReadLine());
                                currentTaskNum = int.Parse(Console.ReadLine());
                                switch (taskLevel)
                                {
                                    case 1: levelEngineer = EngineerExperience.Expert; break;
                                    case 2: levelEngineer = EngineerExperience.Junior; break;
                                    case 3: levelEngineer = EngineerExperience.Rokkie; break;
                                }
                                Task newTaskUpdate = new(idTaskUpdate, taskDescription, taskAlias, taskMilestone, taskCreateAt, taskStart, taskForecastDate, taskDeadline, taskComplete, taskDeliverables, taskRemarks, taskEngineerId, taskLevel;
                                s_dalTask!.Update(newTaskUpdate); break;
                            case 5:
                                int idDelete;
                                Console.WriteLine("Enter id for deleting");
                                idDelete = int.Parse(Console.ReadLine());
                                s_dalTask!.Delete(idDelete); break;
                        }
                        break;

                }

            }

        }
        }
        static void Main(string[] args)
        {
            int chooseEntity;
            chooseEntity = int.Parse(Console.ReadLine());
            
            while (chooseEntity != 0) {
                switch (chooseEntity) {
                    case 1:
                        SubMenuFunc(1);break;
                    case 2:
                        SubMenuFunc(2);break;
                    case 3:
                        SubMenuFunc(3);break;
                }
            }
           
            try
            {
                Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
            }
            catch (Exception ex) { 
            }

        }
    }
}

