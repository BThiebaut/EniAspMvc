using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EniProjetMvc.Extensions
{
    public class BtnNav
    {
        public string Url { get; set; }
        public string Icone { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public bool AdminOnly { get; set; }

        public BtnNav()
        {
            AdminOnly = false;
        }

    }
}
