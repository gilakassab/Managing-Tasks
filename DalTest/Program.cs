using DalApi;
using Dal;

namespace DalTest
{
    internal class Program
    { private static IDependency? s_dalDependency = new DependnecyImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1

        enum MainMenu{ }
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
            }
          catch (Exception ex) { 
            }

        }
    }
}

