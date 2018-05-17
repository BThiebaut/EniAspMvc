﻿using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Theme : AbsEntity<Theme>
    {
        [DisplayName("Théme")]
        [Required]
        public string Libelle { get; set; }
        public string Description { get; set; }
    }
}
