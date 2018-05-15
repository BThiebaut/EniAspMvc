using BO;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EniProjetMvc.Models
{
    public class EvenementVM
    {
        public Evenement Evenement { get; set; }
        public List<Theme> Themes { get; set; }
        public int? selectedTheme { get; set; }
        public List<string> ImagesUrls { get; set; }

        public EvenementVM(IAppDbContext db)
        {
            Themes = DAOFactory<Theme>.GetRepository<Theme>(db).listAll();
        }

        public EvenementVM()
        {
            Themes = new List<Theme>();
        }

    }
}