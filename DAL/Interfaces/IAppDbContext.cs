using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAppDbContext : IDisposable, IObjectContextAdapter
    {
        System.Data.Entity.DbSet<BO.Evenement> Evenements { get; set; }
        System.Data.Entity.DbSet<BO.Theme> Themes { get; set; }
        System.Data.Entity.DbSet<BO.Image> Images { get; set; }
        System.Data.Entity.DbSet<BO.Utilisateur> Utilisateurs { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
