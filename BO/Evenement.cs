using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Evenement : AbsEntity<Evenement>
    {
        
        [Required]
        public string Intitule { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateDebut { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateFin { get; set; }

        [Required]
        public int Duree { get; set; }

        [Required]
        public string HeureOuverture { get; set; }

        [Required]
        public string HeureFermeture { get; set; }
        public List<Image> Images { get; set; }

        [Required]
        public Theme Theme { get; set; }

        [Required]
        public string Adresse { get; set; }
        public List<Utilisateur> Convives { get; set; }

        [Required]
        public StatutEvenement Statut { get; set; }
        
    }
}
