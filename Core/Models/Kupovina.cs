using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Models
{
    internal class Kupovina : ISerializable
    {
        public int Id { get; set; }
        public Posetilac PosetilacKupuje { get; set; } = new Posetilac();
        public Knjiga KupljenaKnjiga { get; set; } = new Knjiga();
        public DateTime DatumKupovine { get; set; }
        public double Ocena { get; set; }
        public string Komentar { get; set; } = string.Empty;
        public string BrojClanskeKarte { get; set; } = string.Empty;
        
        public Kupovina() { }

        public Kupovina(int id, DateTime datum, double ocena, string komentar)
        {
            Id = id;
            DatumKupovine = datum;
            Ocena = ocena;
            Komentar = komentar;
        }
        public Kupovina(DateTime datum, double ocena, string komentar)
        {
            DatumKupovine = datum;
            Ocena = ocena;
            Komentar = komentar;
        }


    }
}
