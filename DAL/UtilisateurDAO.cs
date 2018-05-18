using BO;
using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UtilisateurDAO : AbsDAO<Utilisateur>
    {
        private static readonly UtilisateurDAO instance = new UtilisateurDAO();

        static UtilisateurDAO()
        {
        }

        private UtilisateurDAO()
        {
        }

        public static UtilisateurDAO Instance
        {
            get
            {
                return instance;
            }
        }

        public override bool delete(Utilisateur obj)
        {
            var isOk = true;
            try
            {
                var dbO = dbContext.Utilisateurs.Find(obj.Id);
                dbContext.Utilisateurs.Remove(dbO);
                dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public override Utilisateur getById(int id)
        {
            return dbContext.Utilisateurs.Find(id);
        }

        public override Utilisateur insert(Utilisateur obj)
        {
            dbContext.Utilisateurs.Add(obj);
            dbContext.SaveChanges();
            return obj;
        }

        public override List<Utilisateur> listAll()
        {
            return dbContext.Utilisateurs.ToList();
        }

        public override bool update(Utilisateur obj)
        {
            var isOk = true;
            try
            {
                var old = dbContext.Utilisateurs.Find(obj.Id);
                old.duplicate(obj);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                isOk = false;
            }
            return isOk;
        }

        public bool HasInscription(Evenement evenement, int idUtilisateur)
        {
            var isok = dbContext.Utilisateurs.Where(u => u.Id == idUtilisateur && u.Evenements.Where(e => e.Id == evenement.Id).FirstOrDefault() != null).FirstOrDefault();
            return isok != null;
        }
    }
}
