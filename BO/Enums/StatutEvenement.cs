using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum StatutEvenement
    {
        A_VENIR,
        EN_COURS,
        ANNULE,
        ARCHIVE
    }

    public class StatutEvenementUtil
    {
        public static string GetStatutEvenementColor(StatutEvenement statut)
        {
            switch (statut)
            {
                case StatutEvenement.ANNULE:
                    return "warning";
                case StatutEvenement.ARCHIVE:
                    return "default";
                case StatutEvenement.EN_COURS:
                    return "success";
                default:
                    return "info";
            }
        }

        private Dictionary<StatutEvenement, string> tableauStatut = new Dictionary<StatutEvenement, string>();

        public static StatutEvenementUtil Instance => new StatutEvenementUtil();

        public StatutEvenementUtil()
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
