using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace U5_W1_D3.Models
{
    public class Prodotti
    {
        public int IdArticolo { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public string imgPath { get; set; }
        public string imgAlt1 { get; set; }
        public string imgAlt2 { get; set; }
        public bool Visibile { get; set; }

    }
}