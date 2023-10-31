namespace DalTest;
using DalApi;
using DO;
#DEFINE MIN_ID 200000000
#DEFINE MAX_ID 400000000

public static class Initialization
{
    private static IDependency? s_dalDependency; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static ITask? s_dalTask; //stage 1

    private static readonly Random s_rand = new();

    private static void createEngineers()
    {
        string[] EngineersNames =
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
        ("Shir Yakov","shir92348@gmail.com"),
        };

        foreach (var _name in EngineersNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalEngineer!.Read(_id) != null);

            bool? _b = (_id % 2) == 0 ? true : false;
            Year _year = (Year)s_rand.Next((int)Year.FirstYear, (int)Year.ExtraYear + 1);

            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _bdt = start.AddDays(s_rand.Next(range));

            Engineer newEngineer = new(_id, _name, null, _b, _year, _bdt);

            s_dalStudent!.Create(newStu);
        }
    }
    private static void createStudents()
    {
        string[] studentNames =
        {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
        "Shira Israelof"
    };

        foreach (var _name in studentNames)
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

            Student newStu = new(_id, _name, null, _b, _year, _bdt);

            s_dalStudent!.Create(newStu);
        }
    }

    private static void createStudents()
    {
        string[] studentNames =
        {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
        "Shira Israelof"
    };

        foreach (var _name in studentNames)
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

            Student newStu = new(_id, _name, null, _b, _year, _bdt);

            s_dalStudent!.Create(newStu);
        }
    }
}
