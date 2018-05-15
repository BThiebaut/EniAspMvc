﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UtilisateurDAO : AbsDAO<Utilisateur>
    {
        public UtilisateurDAO()
        {
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
    }
}
