//using DalApi;
//using DO;
//using System.Xml.Linq;

//namespace Dal;

//internal class EngineerImplementation : IEngineer
//{
//    //public int Create(Engineer item)
//    //{
//    //    int id = item.Id;
//    //    if (DataSource.Engineers.Any(e => e.Id == id))
//    //        throw new DalAlreadyExistsException($"Engineer with ID={id} already exists");

//    //    DataSource.Engineers.Add(item);
//    //    return id;
//    //}

//    public int Create(Engineer item)
//    {
//        int id = item.Id;

//        // הוסף את המהלך שמשמש ליצירת ה-XElement והוספתו לקובץ XML
//        XElement engineersElement = XElement.Load("path/to/engineers.xml");

//        if (engineersElement.Elements("Engineer").Any(e => (int)e.Element("Id") == id))
//            throw new DalAlreadyExistsException($"Engineer with ID={id} already exists");

//        XElement newEngineerElement = new XElement("Engineer",
//            new XElement("Id", item.Id),
//            new XElement("Name", item.Name),
//            new XElement("Email", item.Email)
//        // הוסף כאן את שאר המאפיינים של Engineer
//        );

//        engineersElement.Add(newEngineerElement);
//        engineersElement.Save("path/to/engineers.xml");
//        return id;
//    }

//    public void Delete(int id)
//    {
//        throw new DalDeletionImpossible($"Engineer is indelible entity");
//    }

//    public Engineer? Read(Func<Engineer, bool> filter)
//    {
//        return DataSource.Engineers.FirstOrDefault(filter!);
//    }

//    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
//    {
//        return filter == null ? DataSource.Engineers.Select(item => item) : DataSource.Engineers.Where(filter!);
//    }

//    public void Update(Engineer item)
//    {
//        var existingEngineer = Read(e => e.Id == item.Id);
//        if (existingEngineer is null)
//            throw new DalDoesNotExistException($"Engineer with ID={item.Id} does not exist");

//        DataSource.Engineers.Remove(existingEngineer);
//        DataSource.Engineers.Add(item);
//    }
//}
