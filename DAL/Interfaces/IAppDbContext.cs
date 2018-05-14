using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAppDbContext
    {
        System.Data.Entity.DbSet<BO.Evenement> Evenements { get; set; }
        System.Data.Entity.DbSet<BO.Theme> Themes { get; set; }
        System.Data.Entity.DbSet<BO.Image> Images { get; set; }
        System.Data.Entity.DbSet<BO.Utilisateur> Utilisateurs { get; set; }
    }
}
