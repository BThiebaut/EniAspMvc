using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Theme : IIdentifiable
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
    }
}
