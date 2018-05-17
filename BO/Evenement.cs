using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Intitulé")]
        [Required]
        public string Intitule { get; set; }

        [DisplayName("Date de début")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateDebut { get; set; }

        [DisplayName("Date de fin")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DateFin { get; set; }

        [DisplayName("Durée")]
        [Required]
        public int Duree { get; set; }

        [DisplayName("Heure d'ouverture")]
        [Required]
        public string HeureOuverture { get; set; }

        [DisplayName("Heure de fermeture")]
        [Required]
        public string HeureFermeture { get; set; }
        public List<Image> Images { get; set; }

        [Required]
        public Theme Theme { get; set; }

        [DisplayName("Adresse")]
        [Required]
        public string Adresse { get; set; }
        public List<Utilisateur> Convives { get; set; }

        [DisplayName("Statut de l'événement")]
        [Required]
        public StatutEvenement Statut { get; set; }
        
    }
}
