using BO.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAOFactory
    {
        public static AbsDAO<T> GetRepository<T>(IAppDbContext dbContext) where T : IIdentifiable
        {
            var strName = "DAL." + typeof(T).Name + "DAO";
            try
            {
                var inst = Type.GetType(strName).GetProperty("Instance").GetValue(null, null) as AbsDAO<T>;
                inst.SetDbContext(dbContext);
                return inst;
            }catch(Exception e)
            {
                throw new Exception("La classe " + strName + " n'existe pas");
            }
        }
        
    }
}
