using BO.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class AbsDAO<T> where T : IIdentifiable
    {
        public IAppDbContext dbContext;

        public void SetDbContext(IAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract List<T> listAll();
        public abstract T getById(int id);
        public abstract T insert(T obj);
        public abstract bool update(T obj);
        public abstract bool delete(T obj);
        
    }
}
