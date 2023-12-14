using DalApi;
using System;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IDependency Dependency =>  new DependencyImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task =>  new TaskImplementation();
}

//using System;

//namespace DalApi
//{
//    public interface IDal
//    {
//        IDependency Dependency { get; }
//        IEngineer Engineer { get; }
//        ITask Task { get; }
//    }

//    internal sealed class DalXml : IDal
//    {
//        // השימוש ב-Lazy<T> עם הבנייה הנרמלית
//        private static readonly Lazy<DalXml> lazyInstance = new Lazy<DalXml>(() => new DalXml());

//        // פרופרטי Instance ישמש את ה-Value של ה-Lazy כדי להבטיח Thread Safety ו-Lazy Initialization
//        public static IDal Instance => lazyInstance.Value;

//        private DalXml() { }

//        public IDependency Dependency => new DependencyImplementation();
//        public IEngineer Engineer => new EngineerImplementation();
//        public ITask Task => new TaskImplementation();
//    }

//    // הממשקים המקוריים
//    public interface IDependency { }
//    public interface IEngineer { }
//    public interface ITask { }

//    // ממשקי המנוע והמשימה המקוריים
//    internal class DependencyImplementation : IDependency { }
//    internal class EngineerImplementation : IEngineer { }
//    internal class TaskImplementation : ITask { }
//}
//השימוש ב-Lazy מבטיח שהאובייקט יתחיל להתצפות(Lazy Initialization) רק כאשר התכנית מגיעה פעם ראשונה לקרוא ל-Instance.השימוש ב-readonly מבטיח שהתחביר יישאר Thread Safe, אפילו בסביבה מרובה לתהליכים.

//אם יש צורך לעבור למצב רב-תהליכי (multi-threading) ואתה רוצה למנוע הפקעות לא רצוניות נוספות, כדאי להשתמש במימוש עם lock כמו שהוסבר בדוגמה ב-Java.