using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Models
{

    public class Autor : ISerializable
    {
        public int Id { get; set; }
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateOnly DatumRodjenja { get; set; }
        public Adresa Adresa { get; set; } = new Adresa();
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BrojLicneKarte { get; set; } = string.Empty;
        public int GodineIskustva { get; set; }
        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

        public Autor() {}

        public Autor(int id, string ime, string prezime, DateOnly datumRodjenja, Adresa adresa, string telefon, string email, string brojLicneKarte, int godineIskustva, List<Knjiga> knjige)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            BrojLicneKarte = brojLicneKarte;
            GodineIskustva = godineIskustva;
            Knjige = knjige;
        }

        

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Ime,
                Prezime,
                DatumRodjenja.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                string.Join(";", Adresa.ToCSV()),
                Telefon, 
                Email,
                BrojLicneKarte,
                GodineIskustva.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] s)
        {
            Id = int.Parse(s[0]);
            Ime = s[1];
            Prezime = s[2];
            DatumRodjenja = DateOnly.ParseExact(
                s[3],
                new[] { "yyyy-MM-dd", "M/d/yyyy" },
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);

            Adresa = new Adresa();
            Adresa.FromCSV(s[4].Split(';'));

            Telefon = s[5];
            Email = s[6];
            BrojLicneKarte = s[7];
            GodineIskustva = int.Parse(s[8]);
        }

        public override string ToString()
        {
            string knjigeIds = Knjige.Count == 0
                ? "nema"
                : string.Join(", ", Knjige.Select(k => k.Id));

            return
                $"ID: {Id}\n" +
                $"Ime: {Ime}\n" +
                $"Prezime: {Prezime}\n" +
                $"Datum rođenja: {DatumRodjenja:yyyy-MM-dd}\n" +
                $"Adresa: {Adresa}\n" +
                $"Telefon: {Telefon}\n" +
                $"Email: {Email}\n" +
                $"Broj lične karte: {BrojLicneKarte}\n" +
                $"Godine iskustva: {GodineIskustva}\n" +
                $"Knjige (ID): {knjigeIds}\n";
        }
    }
}
