using BO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EvenementDAO : AbsDAO<Evenement>
    {

        public EvenementDAO()
        {
        }

        public override bool delete(Evenement obj)
        {
            var isOk = true;
            try
            {
                var dbO = dbContext.Evenements.Find(obj.Id);
                dbContext.Evenements.Remove(dbO);
                dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public override Evenement getById(int id)
        {
            return dbContext.Evenements.SingleOrDefault(e => e.Id == id);
        }

        public override Evenement insert(Evenement obj)
        {
            dbContext.Evenements.Add(obj);
            dbContext.SaveChanges();
            return obj;
        }

        public override List<Evenement> listAll()
        {
            return dbContext.Evenements.ToList();
        }

        public override bool update(Evenement obj)
        {
            var isOk = true;
            try
            {
                var old = dbContext.Evenements.Find(obj.Id);
                old.duplicate(obj);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }
    }
}
