
using DalApi;
namespace Dal
{
    sealed public class DalXml : IDal
    {

        public IDependency Dependency => new DependencyImplementation();

        public IEngineer Engineer => new EngineerImplementation();

        public ITask Task => new TaskImplementation();

    }
}
