using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T> where T : class
    {
        int Create<T>(); //Creates new entity object in DAL
        Dependency? Read<T>(); //Reads entity object by its ID 
        List<Dependency> ReadAll(); //stage 1 only, Reads all entity objects
        void Update(< item); //Updates entity object
        void Delete(int id); //Deletes an object by its Id
    }
}
