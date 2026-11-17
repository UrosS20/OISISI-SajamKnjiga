using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Models
{
    public class Knjiga : ISerializable
    {   
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Naziv { get; set; } = string.Empty;

        public string ZanrKnjige { get; set; } = string.Empty;

        public int GodinaIzdanja { get; set; }
        public double Cena { get; set; }
        public int BrStrana { get; set; }
        public int IzdavacId { get; set; }

        public Izdavac? Izdavac { get; set; }

        public List<Autor> Autori { get; set; }
        public List<Posetilac> Kupci { get; set; }
        public List<Posetilac> PosetiociNaListiZelja { get; set; }

        public Knjiga()
        {
            Autori = new List<Autor>();
            Kupci = new List<Posetilac>();
            PosetiociNaListiZelja = new List<Posetilac>();
        }

        public Knjiga(int id, string isbn, string naziv, string zanr, int godinaIzdanja, double cena, int brStrana, int izdavacId)
        {
            Id = id;
            ISBN = isbn;
            Naziv = naziv;
            ZanrKnjige = zanr;
            GodinaIzdanja = godinaIzdanja;
            Cena = cena;
            BrStrana = brStrana;
            IzdavacId = izdavacId;

            Autori = new List<Autor>();
            Kupci = new List<Posetilac>();
            PosetiociNaListiZelja = new List<Posetilac>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(CultureInfo.InvariantCulture),
                ISBN,
                Naziv,
                ZanrKnjige,
                GodinaIzdanja.ToString(CultureInfo.InvariantCulture),
                Cena.ToString(CultureInfo.InvariantCulture),
                BrStrana.ToString(CultureInfo.InvariantCulture),
                IzdavacId.ToString(CultureInfo.InvariantCulture)
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0], CultureInfo.InvariantCulture);
            ISBN = values[1];
            Naziv = values[2];
            ZanrKnjige = values[3];
            GodinaIzdanja = int.Parse(values[4], CultureInfo.InvariantCulture);
            Cena = double.Parse(values[5], CultureInfo.InvariantCulture);
            BrStrana = int.Parse(values[6], CultureInfo.InvariantCulture);
            IzdavacId = int.Parse(values[7], CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"ISBN: {ISBN}, ");
            sb.Append($"NAZIV: {Naziv}, ");
            sb.Append($"ŽANR: {ZanrKnjige}, ");
            sb.Append($"CENA: {Cena} RSD, ");
            sb.Append($"AUTORI: ");
            sb.AppendJoin(", ", Autori.Select(a => $"{a.Ime} {a.Prezime}"));
            return sb.ToString();
        }
    }
}
