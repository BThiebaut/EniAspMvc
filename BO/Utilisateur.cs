﻿using BO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Utilisateur : IIdentifiable
    {
        public int Id { get; set; }
        public string Adresse { get; set; }
    }
}
