using System;
using System.Collections.Generic;
using Core.Storage.Serialization;
using System.Text;

namespace Core.Models
{
    public class Posetilac : ISerializable
    {
        public int Id { get; set; }
        public string BrojClanskeKarte { get; set; } = string.Empty;
        public string Ime { get; set; } = string.Empty;
        public string Prezime { get; set; } = string.Empty;
        public DateOnly DatumRodjenja { get; set; }
        public Adresa Adresa { get; set; } = new Adresa();
        public string KontaktTelefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TrenutnaGodinaClanstva { get; set; }
        public StatusPosetioca Status { get; set; }
        public double ProsecnaOcenaRecenzije { get; set; }

        public List<Kupovina> Kupovine { get; set; } = new List<Kupovina>();
        public List<Knjiga> ListaZelja { get; set; } = new List<Knjiga>();

        public Posetilac() { }

        public Posetilac(string brcKarte, string ime, string prezime, DateOnly datRodjenja, string kontaktTel, string email, int trenGodCla, StatusPosetioca status, double prOcena)
        {
            BrojClanskeKarte = brcKarte;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datRodjenja;
            KontaktTelefon = kontaktTel;
            Email = email;
            TrenutnaGodinaClanstva = trenGodCla;
            Status = status;
            ProsecnaOcenaRecenzije = prOcena;
        }

        public Posetilac(int ID, string brcKarte, string ime, string prezime, DateOnly datRodjenja, string kontaktTel, string email, int trenGodCla, StatusPosetioca status, double prosecnaOcena)
        {
            Id = ID;
            BrojClanskeKarte = brcKarte;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datRodjenja;
            KontaktTelefon = kontaktTel;
            Email = email;
            TrenutnaGodinaClanstva = trenGodCla;
            Status = status;
            ProsecnaOcenaRecenzije = prosecnaOcena;
        }

        public override string ToString()
        {
            string kupovineIds = Kupovine.Count == 0 ? "nema" : string.Join(", ", Kupovine.Select(k => k.Id));

            string listaZeljaIds =  ListaZelja.Count == 0 ? "nema" : string.Join(", ", ListaZelja.Select(k => k.Id));

            return $"BrojClanskeKarte: {BrojClanskeKarte}\n" +
                   $"Ime: {Ime}\n" +
                   $"Prezime: {Prezime}\n" +
                   $"DatumRodjenja: {DatumRodjenja:yyyy-MM-dd}\n" +
                   $"Adresa: {Adresa}\n" +
                   $"Telefon: {KontaktTelefon}\n" +
                   $"Email: {Email}\n" +
                   $"TrenutnaGodinaClanstva: {TrenutnaGodinaClanstva}\n" +
                   $"Status: {Status}\n" +
                   $"ProsecnaOcenaRecenzije: {ProsecnaOcenaRecenzije:F2}\n" +
                   $"Kupovine(IDs): [{kupovineIds}]\n" +
                   $"ListaZelja (knjige IDs): [{listaZeljaIds}]\n";
        }

        public string[] ToCSV()
        {
            string adresaCsv = string.Join(";", Adresa.ToCSV());

            string[] v =
            {
                Id.ToString(),
                BrojClanskeKarte,
                Ime,
                Prezime,
                DatumRodjenja.ToString("yyyy-MM-dd"),
                adresaCsv,
                KontaktTelefon,
                Email,
                TrenutnaGodinaClanstva.ToString(),
                Status.ToString(),
                ProsecnaOcenaRecenzije.ToString("F2")
            };
            return v;
        }

        public void FromCSV(string[] v)
        {
            Id = int.Parse(v[0]);
            BrojClanskeKarte = v[1];
            Ime = v[2];
            Prezime = v[3];
            DatumRodjenja = DateOnly.ParseExact(v[4], "yyyy-MM-dd");

            Adresa = new Adresa();
            Adresa.FromCSV(v[5].Split(';'));

            KontaktTelefon = v[6];
            Email = v[7];
            TrenutnaGodinaClanstva = int.Parse(v[8]);
            Status = Enum.Parse<StatusPosetioca>(v[9]);
            ProsecnaOcenaRecenzije = double.Parse(v[10]);
        }
    }
}
