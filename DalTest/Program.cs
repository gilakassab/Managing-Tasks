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

