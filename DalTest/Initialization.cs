namespace DalTest;
using DalApi;
using DO;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

public static class Initialization
{
    //private static IDependency? s_dalDependency; //stage 1
    //private static IEngineer? s_dalEngineer; //stage 1
    //private static ITask? s_dalTask; //stage 1
    private static IDal? s_dal; //stage 2
    private static readonly Random s_rand = new();
    
    //public static void Do(IDal dal) //stage 2
    public static void Do() //stage 4

    {
        //s_dalDependency = _s_dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = _s_dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalTask = _s_dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        s_dal = DalApi.Factory.Get; //stage 4

        createEngineers();
        createTasks();
        createDependencies();
    }

    private static void createEngineers()
    {
        //int min_id = 200000000, max_id = 400000000;
        //int _id;
        //string _name, _email;
        //EngineerExperience _level;
        //EngineerExperience[] _levels = new EngineerExperience[3];
        //_levels[0] = EngineerExperience.Expert;
        //_levels[1] = EngineerExperience.Rookie;
        //_levels[2] = EngineerExperience.Junior;

        //(string, string)[] EngineersDetails =
        //{
        //("Dani Levi","danil2290@gmail.com"),
        //("Eli Amar","eliamar@gmail.com"),
        //("Yair Cohen","yaircohen2004@gmail.com"),
        //("Ariela Levin","arielalevin@gmail.com"),
        //("Dina Klein","dk229012@gmail.com"),
        //("Shira Israelof ","israelof22@gmail.com"),
        //("Dan Totach","dan678@gmail.com"),
        //("Avital Wolden","aviwol@gmail.com"),
        //("Michal Shir","mic543@gmail.com"),
        //("Shir Yakov","shir92348@gmail.com")
        //};

        //for (int i = 0; i < 4; i++)
        //{
        //    foreach (var _details in EngineersDetails)
        //    {
        //        do
        //        {
        //            _id = s_rand.Next(min_id, max_id);
        //        }
        //        while (s_dal!.Engineer.Read(e => e.Id == _id) is not null);
        //        _name = _details.Item1;
        //        _email = _details.Item2;
        //        _level = _levels[s_rand.Next(0, 3)];
        //        Engineer newEngineer = new(_id, _name, _email, _level, s_rand.Next(2000, 9000));
        //        s_dal!.Engineer.Create(newEngineer);
        //    }
        //}

        //Stage 3
        s_dal.Engineer!.ReadAll()
      .Select(engineer => engineer.Id)
      .ToList()
      .ForEach(id => s_dal.Engineer.Delete(id));

        s_dal.Engineer.Create(new Engineer(248728764, "Rony Gilbert", true, "ronygil64@exam.com", EngineerExperience.Expert, 150.5));
        s_dal.Engineer.Create(new Engineer(982485477, "Meir Fuks", true, "meirfuks@exam.com", EngineerExperience.AdvancedBeginner, 130.5));
        s_dal.Engineer.Create(new Engineer(165324683, "Edi Green", true, "edi24683@exam.com", EngineerExperience.Advanced, 140.5));
        s_dal.Engineer.Create(new Engineer(934759393, "Shimmy Lipsin", true, "sl934759393@exam.com", EngineerExperience.Beginner, 100.5));
        s_dal.Engineer.Create(new Engineer(113634844, "Dudi Dlin", true, "dudidlin844@exam.com", EngineerExperience.Beginner, 100.5));
        //צריך עוד 4 וגם לסדר אותו לאמיתי
    }

    private static void createTasks()
    {
        //EngineerExperience _level;
        //EngineerExperience[] _levels = new EngineerExperience[3];
        //_levels[0] = EngineerExperience.Expert;
        //_levels[1] = EngineerExperience.Rookie;
        //_levels[2] = EngineerExperience.Junior;
        //int _id = 0;
        //bool _milestone = false;
        //IEnumerable<Engineer?> myEngineers = s_dal!.Engineer.ReadAll();
        //int maxEngineer = myEngineers.Count();
        //for (int i = 0; i < 100; i++)
        //{
        //    string _description = "Task " + (i + 1).ToString();
        //    string _alias = (i + 1).ToString();
        //    _level = _levels[s_rand.Next(0, 3)];
        //    var nonNullEngineers = myEngineers.Where(e => e != null).ToList();
        //    int currentEngineerId = nonNullEngineers[s_rand.Next(0, nonNullEngineers.Count)].Id;

        //    //Year _year = (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);

        //    //    DateTime start = new DateTime(1995, 1, 1);
        //    //    int range = (DateTime.Today - start).Days;
        //    //    DateTime _bdt = start.AddDays(s_rand.Next(range));

        //    Task newTask = new(_id, _description, _alias, _milestone,/* _createAt*/ DateTime.Today,/* _start=*/null, /*_forecastDate*/ null, /*_deadline*/ null, /*_complete*/ null, /*_deliverables*/ null,/*_remarks*/ null, currentEngineerId, _level);
        //    s_dal!.Task.Create(newTask);
        //}

        //Stage 3
        s_dal.Task!.ReadAll()
      .Select(task => task.Id)
      .ToList()
      .ForEach(id => s_dal.Task.Delete(id)); 

        s_dal.Task.Create(new Task(0, "Decide what is the next project", "Alias1", false, TimeSpan.Zero, EngineerExperience.Expert, true,,,,,,, "248728764"));
        s_dal.Task.Create(new Task(0, "Check the requirements", "Alias2", false, TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, "982485477"));
        s_dal.Task.Create(new Task(0, "Choose the most convenient way", "Alias3", false, TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, "982485477"));//צריך עוד 19 וגם לסדר אותו לאמיתי
        s_dal.Task.Create(new Task(0, "Decide on the location of the feature", "Alias4", false, TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, "982485477"));
        s_dal.Task.Create(new Task(0, "Decide on the shape of the feature", "Alias5", false, TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, "982485477"));
        s_dal.Task.Create(new Task(0, "Work on the design", "Alias6", false, TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, "982485477"));
        s_dal.Task.Create(new Task(0, "stage 0 in the programming", "Alias7", false, TimeSpan.Zero, EngineerExperience.Advanced, true, "165324683"));
        s_dal.Task.Create(new Task(0, "stage 1 in the programming", "Alias8", false, TimeSpan.Zero, EngineerExperience.Advanced, true, "165324683"));
        s_dal.Task.Create(new Task(0, "stage 2 in the programming", "Alias9", false, TimeSpan.Zero, EngineerExperience.Advanced, true , "165324683"));
        s_dal.Task.Create(new Task(0, "stage 3 in the programming", "Alias10", false, TimeSpan.Zero, EngineerExperience.Advanced, true, "165324683"));
        s_dal.Task.Create(new Task(0, "stage 4 in the programming", "Alias11", false, TimeSpan.Zero, EngineerExperience.Advanced, true, "165324683"));
        s_dal.Task.Create(new Task(0, "stage 5 in the programming", "Alias12", false, TimeSpan.Zero, EngineerExperience.Advanced, true, "165324683"));
        s_dal.Task.Create(new Task(0, "stage 6 in the programming", "Alias13", false, TimeSpan.Zero, EngineerExperience.Advanced, true, "165324683"));
        s_dal.Task.Create(new Task(0, "stage 7 in the programming", "Alias14", false, TimeSpan.Zero, EngineerExperience.Advanced, true, "165324683"));
        s_dal.Task.Create(new Task(0, "Run the code", "Alias15", false, TimeSpan.Zero, EngineerExperience.Beginner, true, "934759393"));
        s_dal.Task.Create(new Task(0, "Find errors in the code ", "Alias16", false, TimeSpan.Zero, EngineerExperience.Beginner, true, "934759393"));
        s_dal.Task.Create(new Task(0, "Get permission from the programmer and pass the code on", "Alias17", false, TimeSpan.Zero, EngineerExperience.Beginner, true, "934759393"));
        s_dal.Task.Create(new Task(0, "Bring confirmation to the software tester that the code is correct", "Alias18", false, TimeSpan.Zero, EngineerExperience.934759393, true, "165324683"));
        s_dal.Task.Create(new Task(0, "Send the feature to the advertising team", "Alias19", false, TimeSpan.Zero, EngineerExperience.Beginner, true, "934759393");
        s_dal.Task.Create(new Task(0, "Update all platforms", "Alias20", false, TimeSpan.Zero, EngineerExperience.Beginner, true, "113634844"));
    }

    private static void createDependencies()
    {
        //IEnumerable<Task?> myTasks = s_dal!.Task.ReadAll();
        //int maxTask = myTasks.Count(), _dependentTask, _DependsOnTask;
        //for (int i = 0; i < 250; i++)
        //{
        //    var nonNullTasks = myTasks.Where(e => e != null).ToList();
        //    _dependentTask = nonNullTasks[s_rand.Next(0, nonNullTasks.Count)].Id;
        //    _DependsOnTask = nonNullTasks[s_rand.Next(0, nonNullTasks.Count)].Id;
        //    Dependency neWDependency = new(0, _dependentTask, _DependsOnTask);
        //    s_dal!.Dependency.Create(neWDependency);
        //}

        //Stage 3
        s_dal.Dependency!.ReadAll()
      .Select(dependency => dependency.Id)
      .ToList()
      .ForEach(id => s_dal.Dependency.Delete(id));

        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));
        s_dal.Dependency.Create(new Dependency(0, 1, 2));


    }
}




