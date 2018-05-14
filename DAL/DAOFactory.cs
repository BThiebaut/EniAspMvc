using BO.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAOFactory<T>
    {
        public static AbsDAO<T> GetRepository<T>(IAppDbContext dbContext) where T : IIdentifiable
        {
            var strName = "DAL." + typeof(T).Name + "DAO";
            try
            {
                var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                var wrappedInstance = Activator.CreateInstance(assemblyName, strName);
                var inst = wrappedInstance.Unwrap() as AbsDAO<T>;
                inst.SetDbContext(dbContext);
                return inst;
            }catch(Exception e)
            {
                throw new Exception("La classe " + strName + " n'existe pas");
            }
        }
        
    }
}
