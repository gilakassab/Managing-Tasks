using DalApi;

namespace DalTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImlementation(); //stage 1
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1

        //Initialization.Do(s_dalDependency, s_dalEngineer, s_dalTask);
        }   
    }
}

