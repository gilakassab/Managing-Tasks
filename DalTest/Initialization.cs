namespace DalTest;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

public static class Initialization
{
    private static IDependency? s_dalDependency; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static ITask? s_dalTask; //stage 1

    private static readonly Random s_rand = new();

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
                while (s_dalEngineer!.Read(_id) != null);
                    _name = _details.Item1;
                    _email = _details.Item2;
                    _level = _levels[s_rand.Next(0, 3)];
                Engineer newEngineer = new(_id, _name, _email, _level, s_rand.Next(2000, 9000));
                    s_dalEngineer!.Create(newEngineer);
            }
        }
    }
}
    private static void createDependencies()
    {        
    //for (int i = 0;i < 250;i++) {
    //    _dependentTask=ReaderWriterLock()
    //    Dependency neWDependency = new( _dependentTask, _DependsOnTask);

    ////    s_dalDependency!.Create(neWDependency);
    //    }}
       
    }

    private static void createTasks()
    {
    bool _milestone = false;
        string[] TasksNames =
        {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
        "Shira Israelof"
    };

        foreach (var _Taskname in TasksNames)
        { 
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalStudent!.Read(_id) != null);

            bool? _b = (_id % 2) == 0 ? true : false;
            Year _year =
            (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);

            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _bdt = start.AddDays(s_rand.Next(range));
        
            Task newTask = new(_id, _description, _alias, _milestone ,/* _createAt*/ null,/* _start=*/null, /*_forecastDate*/ null, /*_deadline*/ null, /*_complete*/ null , /*_deliverables*/ null,/*_remarks*/ null, /*_engineerId*/ null,_level);

            s_dalStudent!.Create(newStu);
        }
    }
}
/* int Id,
    string Description,
    string Alias,
    bool? Milestone,
    DateTime? CreateAt = null,
    DateTime? Start = null,
    DateTime? ForecastDate = null,
    DateTime? Deadline = null,
    DateTime? Complete = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null,
    EngineerExperience Level = EngineerExperience.Expert*/
//bool? _b = (_id % 2) == 0 ? true : false;
//Year _year = (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);

//DateTime start = new DateTime(1995, 1, 1);
//int range = (DateTime.Today - start).Days;
//DateTime _bdt = start.AddDays(s_rand.Next(range));
