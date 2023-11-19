namespace DalTest;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

public static class Initialization
{
    //private static IDependency? s_dalDependency; //stage 1
    //private static IEngineer? s_dalEngineer; //stage 1
    //private static ITask? s_dalTask; //stage 1
    private static IDal? s_dal; //stage 2
    private static readonly Random s_rand = new();
    public static void Do(IDal dal)
    {
        //s_dalDependency = _s_dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = _s_dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalTask = _s_dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        createEngineers();
        createTasks();
        createDependencies();
    }

    private static void createEngineers()
    {
        int min_id = 200000000, max_id = 400000000;
        int _id;
        string _name, _email;
        EngineerExperience _level;
        EngineerExperience[] _levels = new EngineerExperience[3];
        _levels[0] = EngineerExperience.Expert;
        _levels[1] = EngineerExperience.Rookie;
        _levels[2] = EngineerExperience.Junior;

        (string, string)[] EngineersDetails =
        {
        ("Dani Levi","danil2290@gmail.com"),
        ("Eli Amar","eliamar@gmail.com"),
        ("Yair Cohen","yaircohen2004@gmail.com"),
        ("Ariela Levin","arielalevin@gmail.com"),
        ("Dina Klein","dk229012@gmail.com"),
        ("Shira Israelof ","israelof22@gmail.com"),
        ("Dan Totach","dan678@gmail.com"),
        ("Avital Wolden","aviwol@gmail.com"),
        ("Michal Shir","mic543@gmail.com"),
        ("Shir Yakov","shir92348@gmail.com")
        };

        for (int i = 0; i < 4; i++)
        {
            foreach (var _details in EngineersDetails)
            {
                do
                {
                    _id = s_rand.Next(min_id, max_id);
                }
                while (s_dal!.Engineer.Read(_id) is not null) ;
                _name = _details.Item1;
                _email = _details.Item2;
                _level = _levels[s_rand.Next(0, 3)];
                Engineer newEngineer = new(_id, _name, _email, _level, s_rand.Next(2000, 9000));
                s_dal!.Engineer.Create(newEngineer);
            }
        }

    }

    private static void createTasks()
    {
        EngineerExperience _level;
        EngineerExperience[] _levels = new EngineerExperience[3];
        _levels[0] = EngineerExperience.Expert;
        _levels[1] = EngineerExperience.Rookie;
        _levels[2] = EngineerExperience.Junior;
        int _id = 0;
        bool _milestone = false;
        List<Engineer?> myEngineers = s_dal!.Engineer.ReadAll();
        int maxEngineer = myEngineers.Count();
        for (int i = 0; i < 100; i++)
        {
            string _description = "Task " + (i + 1).ToString();
            string _alias = (i + 1).ToString();
            _level = _levels[s_rand.Next(0, 3)];
            int currentEngineerId = myEngineers[s_rand.Next(0, maxEngineer)].Id;
            //Year _year = (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);

            //    DateTime start = new DateTime(1995, 1, 1);
            //    int range = (DateTime.Today - start).Days;
            //    DateTime _bdt = start.AddDays(s_rand.Next(range));

            Task newTask = new(_id, _description, _alias, _milestone,/* _createAt*/ DateTime.Today,/* _start=*/null, /*_forecastDate*/ null, /*_deadline*/ null, /*_complete*/ null, /*_deliverables*/ null,/*_remarks*/ null, currentEngineerId, _level);
            s_dal!.Task.Create(newTask);
        }   
    }

    private static void createDependencies()
    {
        List<Task?> myTasks = s_dal!.Task.ReadAll();
        int maxTask = myTasks.Count(), _dependentTask, _DependsOnTask;
        for (int i = 0; i < 250; i++)
        {
            _dependentTask = myTasks[s_rand.Next(0, maxTask)].Id;
            _DependsOnTask = myTasks[s_rand.Next(0, maxTask)].Id;
            Dependency neWDependency = new(0, _dependentTask, _DependsOnTask);
            s_dal!.Dependency.Create(neWDependency);
        }
    }
}