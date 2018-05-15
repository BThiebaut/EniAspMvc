using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;

namespace EniProjetMvc.Utils
{
    public class EnumerationUtil
    {
        private Dictionary<StatutEvenement, string> tableauStatut = new Dictionary<StatutEvenement, string>();
        
        public static EnumerationUtil Instance => new EnumerationUtil();

        public EnumerationUtil()
        {
            tableauStatut.Add(StatutEvenement.ANNULE, "ANNULÉ");
            tableauStatut.Add(StatutEvenement.ARCHIVE, "ARCHIVÉ");
            tableauStatut.Add(StatutEvenement.A_VENIR, "À VENIR");
            tableauStatut.Add(StatutEvenement.EN_COURS, "EN COURS");
        }

        public string getLabelStatut(StatutEvenement statut)
        {
            return tableauStatut.First(s => s.Key == statut).Value;
        }
    }
}