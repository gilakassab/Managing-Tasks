namespace DalTest;
using DalApi;
using DO;
using DalFacade;
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
    //private static readonly Random s_rand = new();
    
    //public static void Do(IDal dal) //stage 2
    public static void Do() //stage 4
    {
        //s_dalDependency = _s_dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = _s_dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalTask = _s_dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        s_dal = Factory.Get; //stage 4

        s_dal.Reset();
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

        //s_dal.Engineer!.ReadAll()
        //.ToList()
        //.ForEach(engineer => Console.WriteLine(engineer.ToString()));

        s_dal!.Engineer.Create(new Engineer(248728764, "Rony Gilbert", true, "ronygil64@exam.com", EngineerExperience.Expert, 150.5, Roles.TeamLeader));
        s_dal.Engineer.Create(new Engineer(982485477, "Meir Fuks", true, "meirfuks@exam.com", EngineerExperience.AdvancedBeginner, 130.5, Roles.GraphicArtist));
        s_dal.Engineer.Create(new Engineer(165324683, "Edi Green", true, "edi24683@exam.com", EngineerExperience.Advanced, 140.5, Roles.Programmer));
        s_dal.Engineer.Create(new Engineer(934759393, "Shimmy Lipsin", true, "sl934759393@exam.com", EngineerExperience.Beginner, 100.5, Roles.Programmer));
        s_dal.Engineer.Create(new Engineer(113634844, "Dudi Dlin", true, "dudidlin844@exam.com", EngineerExperience.Beginner, 100.5, Roles.Programmer));

        //Console.WriteLine("\n*************************\n");
        //s_dal.Engineer!.ReadAll()
        //.ToList()
        //.ForEach(engineer => Console.WriteLine(engineer.ToString()));
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


//        s_dal.Task!.ReadAll()
//.ToList()
//.ForEach(engineer => Console.WriteLine(engineer.ToString()));

        s_dal!.Task.Create(new Task(0, "Decide what is the next project", "Alias1", false, new DateTime(2024,1,1), TimeSpan.Zero, EngineerExperience.Expert, true,null,null,null,null,null,null,248728764));
        s_dal.Task.Create(new Task(0, "Check the requirements", "Alias2", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, null, null, null, null, null, null, 982485477));
        s_dal.Task.Create(new Task(0, "Choose the most convenient way", "Alias3", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, null, null, null, null, null, null, 982485477));
        s_dal.Task.Create(new Task(0, "Decide on the location of the feature", "Alias4", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, null, null, null, null, null, null, 982485477));
        s_dal.Task.Create(new Task(0, "Decide on the shape of the feature", "Alias5", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, null, null, null, null, null, null, 982485477));
        s_dal.Task.Create(new Task(0, "Work on the design", "Alias6", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.AdvancedBeginner, true, null, null, null, null, null, null, 982485477));
        s_dal.Task.Create(new Task(0, "stage 0 in the programming", "P0", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "stage 1 in the programming", "P1", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "stage 2 in the programming", "P2", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true , null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "stage 3 in the programming", "P3", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "stage 4 in the programming", "P4", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "stage 5 in the programming", "P5", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "stage 6 in the programming", "P6", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "stage 7 in the programming", "P6", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Advanced, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "Run the code", "Alias15", false,new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Beginner, true, null, null, null, null, null, null, 934759393));
        s_dal.Task.Create(new Task(0, "Find errors in the code ", "Alias16", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Beginner, true, null, null, null, null, null, null, 934759393));
        s_dal.Task.Create(new Task(0, "Get permission from the programmer and pass the code on", "Alias17", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Beginner, true, null, null, null, null, null, null, 934759393));
        s_dal.Task.Create(new Task(0, "Bring confirmation to the software tester that the code is correct", "Alias18", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Beginner, true, null, null, null, null, null, null, 165324683));
        s_dal.Task.Create(new Task(0, "Send the feature to the advertising team", "Alias19", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Beginner, true, null, null, null, null, null, null, 934759393));
        s_dal.Task.Create(new Task(0, "Update all platforms", "Alias20", false, new DateTime(2024, 1, 1), TimeSpan.Zero, EngineerExperience.Beginner, true, null, null, null, null, null, null, 113634844));
        //Console.WriteLine("\n*************************\n");

    //    s_dal.Task!.ReadAll()
    //.ToList()
    //.ForEach(engineer => Console.WriteLine(engineer.ToString()));
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


//        s_dal.Dependency!.ReadAll()
//.ToList()
//.ForEach(engineer => Console.WriteLine(engineer.ToString()));

        s_dal!.Dependency.Create(new Dependency(0, 1, 0));
        s_dal.Dependency.Create(new Dependency(0, 2,1));
        s_dal.Dependency.Create(new Dependency(0, 3, 2));
        s_dal.Dependency.Create(new Dependency(0, 4, 3));
        s_dal.Dependency.Create(new Dependency(0, 6, 4));
        s_dal.Dependency.Create(new Dependency(0, 7, 6));
        s_dal.Dependency.Create(new Dependency(0, 8, 7));
        s_dal.Dependency.Create(new Dependency(0, 9, 10));
        s_dal.Dependency.Create(new Dependency(0, 11, 10));
        s_dal.Dependency.Create(new Dependency(0, 12, 11));
        s_dal.Dependency.Create(new Dependency(0, 13, 12));
        s_dal.Dependency.Create(new Dependency(0, 13, 14));
        s_dal.Dependency.Create(new Dependency(0, 14, 13));
        s_dal.Dependency.Create(new Dependency(0, 15, 13));
        s_dal.Dependency.Create(new Dependency(0, 19, 17));
        s_dal.Dependency.Create(new Dependency(0, 19, 18));
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
//        Console.WriteLine("\n*************************\n");

//        s_dal.Dependency!.ReadAll()
//.ToList()
//.ForEach(engineer => Console.WriteLine(engineer.ToString()));
    }
}




