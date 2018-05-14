using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Evenement
    {
        public int Id { get; set; }
        public string Intitule { get; set; }
        public DateTime DateDebut  { get; set; }
        public DateTime DateFin { get; set; }
        public int Duree { get; set; }
        public DateTime HeureOuverture { get; set; }
        public DateTime HeureFermeture { get; set; }
        public List<Image> Images { get; set; }
        public Theme Theme { get; set; }
        public string Adresse { get; set; }
        public List<Utilisateur> Convives { get; set; }
        public StatutEvenement Statut { get; set; }

    }
}
