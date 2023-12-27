using BO;
using DalApi;
using DalTest;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        Console.WriteLine("Enter id, name, email, cost and a number to choose experience");
                        int idEngineer, currentNum;
                        string nameEngineer, emailEngineer, input;
                        DO.EngineerExperience levelEngineer;
                        DO.Roles role;
                        bool isActive;
                        double costEngineer;
                        idEngineer = int.Parse(Console.ReadLine());
                        nameEngineer = (Console.ReadLine());
                        isActive = Console.ReadLine() == "false" ? false : true;
                        emailEngineer = Console.ReadLine();
                        input = Console.ReadLine();
                        levelEngineer = (DO.EngineerExperience)Enum.Parse(typeof(DO.EngineerExperience), input);
                        costEngineer = double.Parse(Console.ReadLine());
                        role = (DO.Roles)Enum.Parse(typeof(DO.Roles), input);

                        s_bl.Engineer.Create(new Engineer(idEngineer, nameEngineer, isActive, emailEngineer, levelEngineer, costEngineer, role, null));
                        break;
                    case 2:
                        int id;
                        Console.WriteLine("Enter id for reading");
                        id = int.Parse(Console.ReadLine());
                        if (s_dal.Engineer!.Read(e => e.Id == id) is null)
                            Console.WriteLine("no engineer found");
                        else
                            Console.WriteLine(s_dal.Engineer!.Read(e => e.Id == id).ToString());
                        break;
                    case 3:
                        s_dal.Engineer!.ReadAll()
         .ToList()
         .ForEach(engineer => Console.WriteLine(engineer.ToString()));
                        break;
                    case 4:
                        int idEngineerUpdate, currentNumUpdate;
                        string nameEngineerUpdate, emailEngineerUpdate, inputUpdate;
                        EngineerExperience levelEngineerUpdate;
                        double costEngineerUpdate;
                        bool isActiveUpdate;
                        Console.WriteLine("Enter id for reading");
                        idEngineerUpdate = int.Parse(Console.ReadLine());
                        Engineer updatedEngineer = s_dal.Engineer.Read(e => e.Id == idEngineerUpdate);
                        Console.WriteLine(updatedEngineer.ToString());
                        Console.WriteLine("Enter details to update");//if null to put the same details
                        nameEngineerUpdate = Console.ReadLine() ?? updatedEngineer?.Name;
                        isActiveUpdate = Console.ReadLine() == "false" ? false : true;
                        emailEngineerUpdate = Console.ReadLine() ?? updatedEngineer?.Email;
                        inputUpdate = Console.ReadLine();
                        levelEngineerUpdate = string.IsNullOrWhiteSpace(inputUpdate) ? updatedEngineer.Level : (EngineerExperience)Enum.Parse(typeof(EngineerExperience), inputUpdate);
                        costEngineerUpdate = double.Parse(Console.ReadLine());


                        s_dal.Engineer.Update(new Engineer(idEngineerUpdate, nameEngineerUpdate, isActiveUpdate, emailEngineerUpdate, levelEngineerUpdate, costEngineerUpdate));
                        break;
                    case 5:
                        int idDelete;
                        Console.WriteLine("Enter id for deleting");
                        idDelete = int.Parse(Console.ReadLine());
                        s_dal.Engineer!.Delete(idDelete);
                        break;
                    default: return;
                }
            } while (chooseSubMenu > 0 && chooseSubMenu < 6);
        }
        public static void TaskMenu()
        {

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
    




