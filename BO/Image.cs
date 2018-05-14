using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Image : IIdentifiable
    {
        public int Id { get; set; }
        public Evenement Evenement { get; set; }
        public string Url { get; set; }
    }
}
