using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Models
{
    public class Kupovina : ISerializable
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

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"Posetilac: {PosetilacKupuje}\n" +
                   $"Knjiga: {KupljenaKnjiga}\n" +
                   $"Datum: {DatumKupovine:yyyy-MM-dd}\n" +
                   $"Ocena: {Ocena}\n" +
                   $"Komentar: {Komentar}";
        }

        public string[] ToCSV()
        {
            string[] v =
            {
                Id.ToString(),
                PosetilacKupuje.BrojClanskeKarte,
                KupljenaKnjiga.Id.ToString(),
                DatumKupovine.ToString("yyyy-MM-dd"),
                Ocena.ToString(),
                Komentar
            };
            return v;
        }
        public void FromCSV(string[] v)
        {
            Id = int.Parse(v[0]);
            BrojClanskeKarte = v[1];
            PosetilacKupuje = new Posetilac();
            PosetilacKupuje.BrojClanskeKarte = v[1];
            KupljenaKnjiga = new Knjiga()
            {
                Id = int.Parse(v[2])
            };

            DatumKupovine = DateOnly.ParseExact(v[3], "yyyy-MM-dd");
            Ocena = int.Parse(v[4]);
            Komentar = v[5];
        }
    }
}
