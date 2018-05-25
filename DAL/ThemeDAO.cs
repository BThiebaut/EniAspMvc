using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ThemeDAO : AbsDAO<Theme>
    {
        private static readonly ThemeDAO instance = new ThemeDAO();

        static ThemeDAO()
        {
        }

        private ThemeDAO()
        {
        }

        public static ThemeDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public override bool delete(Theme obj)
        {
            var isOk = true;
            try
            {
                var dbO = dbContext.Themes.Find(obj.Id);
                dbContext.Themes.Remove(dbO);
                dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public override Theme getById(int id)
        {
            return dbContext.Themes.Find(id);
        }

        public override Theme insert(Theme obj)
        {
            dbContext.Themes.Add(obj);
            dbContext.SaveChanges();
            return obj;
        }

        public override List<Theme> listAll()
        {
            return dbContext.Themes.Where(t => !t.Desactive).ToList();
        }

        public List<Theme> listFull()
        {
            return dbContext.Themes.OrderBy(t => t.Desactive).ToList();
        }

        public override bool update(Theme obj)
        {
            var isOk = true;
            try
            {
                var old = dbContext.Themes.Find(obj.Id);
                old.duplicate(obj);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public bool ToogleActive(int id)
        {
            
            var dbo = this.getById(id);
            dbo.Desactive = !dbo.Desactive;
            return this.update(dbo);
        }
    }
}
