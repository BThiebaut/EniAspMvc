using BO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public override Evenement getById(int id)
        {
            throw new NotImplementedException();
        }

        public override Evenement insert(Evenement obj)
        {
            throw new NotImplementedException();
        }

        public override List<Evenement> listAll()
        {
            return dbContext.Evenements.ToList();
        }

        public override bool update(Evenement obj)
        {
            throw new NotImplementedException();
        }
    }
}
