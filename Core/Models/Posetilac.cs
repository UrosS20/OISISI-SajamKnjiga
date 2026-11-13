using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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

        public Posetilac() { }

        public Posetilac(string brckarte, string ime, string prezime, DateOnly datrodjenja, string kontaktel, string email, int trenGodCla, StatusPosetioca status, double procena)
        {
            BrojClanskeKarte = brckarte;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datrodjenja;
            KontaktTelefon = kontaktel;
            Email = email;
            TrenutnaGodinaClanstva = trenGodCla;
            Status = status;
            ProsecnaOcenaRecenzije = procena;
        }

        public Posetilac(int ID, string brckarte, string ime, string prezime, DateOnly datrodjenja, string kontaktel, string email, int trenGodCla, StatusPosetioca status, double prosecnaocena)
        {
            Id = ID;
            BrojClanskeKarte = brckarte;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datrodjenja;
            KontaktTelefon = kontaktel;
            Email = email;
            TrenutnaGodinaClanstva = trenGodCla;
            Status = status;
            ProsecnaOcenaRecenzije = prosecnaocena;
        }
    }
}
