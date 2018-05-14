using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EniProjetMvc.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BO.Evenement> Evenements { get; set; }
        public System.Data.Entity.DbSet<BO.Theme> Themes { get; set; }
        public System.Data.Entity.DbSet<BO.Image> Images { get; set; }
        public System.Data.Entity.DbSet<BO.Utilisateur> Utilisateurs { get; set; }
    }
}
