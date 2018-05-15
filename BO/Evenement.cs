using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Evenement : AbsEntity<Evenement>
    {
        public string Intitule { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateDebut { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateFin { get; set; }
        public int Duree { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime HeureOuverture { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime HeureFermeture { get; set; }
        public List<Image> Images { get; set; }
        public Theme Theme { get; set; }
        public string Adresse { get; set; }
        public List<Utilisateur> Convives { get; set; }
        public StatutEvenement Statut { get; set; }
        
    }
}
