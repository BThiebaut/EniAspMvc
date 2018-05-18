using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Utilisateur : AbsEntity<Utilisateur>
    {
        [DisplayName("Adresse")]
        [Required]
        public string Adresse { get; set; }

        public List<Evenement> Evenements { get; set; }

        public IApplicationUser ApplicationUser { get; set; }

        public Utilisateur()
        {
            Evenements = new List<Evenement>();
        }
    }
}
