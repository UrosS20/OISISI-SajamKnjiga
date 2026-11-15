using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Models
{
    public class Izdavac : ISerializable
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = string.Empty;
        public Autor? Sef { get; set; }
        public List<Autor> Autori { get; set; } = new List<Autor>();
        public List<Knjiga> Knjige { get; set; } = new List<Knjiga>();

        public Izdavac() { }
        public Izdavac(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
        public Izdavac(string naziv)
        {

            Naziv = naziv;
        }

        public override string ToString()
        {
            string sefText = Sef != null ? $"{Sef.Ime} {Sef.Prezime}" : "Nema sefa";

            return $"Sifra: {Id}\n" + $"Naziv: {Naziv}\n" + $"Sef: {sefText}\n";
        }
        public string[] ToCSV()
        {
            string[] v =
            {  Id.ToString(), Naziv, Sef != null ? Sef.Id.ToString() : "0" };
            return v;
        }
        public void FromCSV(string[] v)
        {
            Id = int.Parse(v[0]);
            Naziv = v[1];
            int sefId = int.Parse(v[2]);

            if (sefId == 0)
            {
                Sef = null;
            }
            else
            {
                Sef = new Autor();
                Sef.Id = sefId;
            }

        }
    }
}
