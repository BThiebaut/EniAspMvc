using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Utilisateur : AbsEntity<Utilisateur>
    {
        public string Adresse { get; set; }
    }
}
