using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Image : AbsEntity<Image>
    {
        [Required]
        public Evenement Evenement { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
