using BO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class EvenementDAO : AbsDAO<Evenement>
    {
        private static readonly EvenementDAO instance = new EvenementDAO();

        static EvenementDAO()
        {
        }

        private EvenementDAO()
        {
        }

        public static EvenementDAO Instance
        {
            get
            {
                return instance;
            }
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
            return dbContext.Evenements
                            .Include(e => e.Images)
                            .Include(e => e.Theme)
                            .Include(e => e.Convives)
                            .SingleOrDefault(e => e.Id == id);
        }

        public List<Evenement> getByStatut(StatutEvenement statut, int limit)
        {
            return dbContext.Evenements
                            .Where(e => e.Statut == statut)
                            .Take(limit)
                            .Include(e => e.Images)
                            .Include(e => e.Theme)
                            .Include(e => e.Convives)
                            .ToList();
        }

        public override Evenement insert(Evenement obj)
        {
            dbContext.Evenements.Add(obj);
            dbContext.SaveChanges();
            return obj;
        }

        public override List<Evenement> listAll()
        {
            return dbContext.Evenements
                            .Include(e => e.Images)
                            .Include(e => e.Theme)
                            .Include(e => e.Convives)
                            .OrderBy(e => e.Statut)
                            .ToList();
        }

        public List<Evenement> listAll(Utilisateur utilisateur)
        {
            var events = utilisateur.Evenements.Select(o => o.Id).ToList();
            return dbContext.Evenements.Include(e => e.Images).Where(e => events.Contains(e.Id)).OrderBy(e => e.Statut).ToList();
        }

        public List<Evenement> listAll(Utilisateur utilisateur = null, string search = null, Theme theme = null, StatutEvenement? statut = null)
        {
            var query = dbContext.Evenements
                                 .Include(e => e.Images)
                                 .Include(e => e.Theme)
                                 .Include(e => e.Convives);

            if (utilisateur != null)
            {
                var events = utilisateur.Evenements.Select(o => o.Id).ToList();
                query = query.Where(e => events.Contains(e.Id));
            }

            if (search != null && search != "")
            {
                search = search.Trim().ToLower();
                query = query.Where(e => e.Intitule.Trim().ToLower().Contains(search));
            }
            if (theme != null && theme.Id != 0)
            {
                query = query.Where(e => e.Theme.Id == theme.Id);
            }
            if (statut != null && statut.HasValue)
            {
                query = query.Where(e => e.Statut == statut.Value);
            }

            return query.OrderBy(e => e.Statut).ToList();
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

        public override bool cancel(Evenement obj)
        {
            if (obj.Statut != StatutEvenement.ARCHIVE)
            {
                obj.Statut = StatutEvenement.ANNULE;
            }
            return this.update(obj);
        }

        public void AutoUpdateStatut()
        {
            var events = this.listAll();
            var today = DateTime.Now;
            foreach (var item in events)
            {
                if (item.DateFin < today && item.Statut != StatutEvenement.ANNULE)
                {
                    item.Statut = StatutEvenement.ARCHIVE;
                }
                else if (item.DateDebut < today && item.DateFin > today && item.Statut != StatutEvenement.ANNULE)
                {
                    item.Statut = StatutEvenement.EN_COURS;
                }
            }
            dbContext.SaveChanges();
        }

    }
}
