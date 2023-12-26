using DalApi;
using System;

namespace Dal;

//sealed internal class DalXml : IDal
//{
//    public static IDal Instance { get; } = new DalXml();
//    private DalXml() { }
//    public IDependency Dependency =>  new DependencyImplementation();

//    public IEngineer Engineer =>  new EngineerImplementation();

//    public ITask Task =>  new TaskImplementation();
//}

sealed internal class DalXml : IDal
{
    private static readonly Lazy<DalXml> lazyInstance = new Lazy<DalXml>(() => new DalXml(), true);

    private static readonly IDependency dependencyInstance = new DependencyImplementation();
    private static readonly IEngineer engineerInstance = new EngineerImplementation();
    private static readonly ITask taskInstance = new TaskImplementation();

    static DalXml() { }

    public IDependency Dependency => dependencyInstance;
    public IEngineer Engineer => engineerInstance;
    public ITask Task => taskInstance;

    public static IDal Instance => lazyInstance.Value;

    public DateTime? startProject { get => Config.startProject; set => Config.startProject = value; }

    public DateTime? deadlineProject { get => Config.deadlineProject; set => Config.deadlineProject = value; }

    private DalXml() { }

    public void Reset()
    {
        Task.Reset();
        Engineer.Reset();
        Dependency.Reset();
    }
}
