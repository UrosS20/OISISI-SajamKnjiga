using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Models
{
    public class Adresa : ISerializable
    {
        public int Id {  get; set; }
        public string Ulica { get; set; } = string.Empty;
        public string Broj { get; set; } = string.Empty;
        public string Grad { get; set; } = string.Empty;
        public string Drzava { get; set; } = string.Empty;

        public Adresa() { }

        public Adresa(int id, string ulica, string broj, string grad, string drzava)
        {
            Id = id;
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            Drzava = drzava;
        }

        public override string ToString()
        {
            return $"{Ulica},{Broj},{Grad},{Drzava}";
        }

        public string[] ToCSV() 
        {
            string[] csvValues =
            {
                Id.ToString(),
                Ulica,
                Broj,
                Grad,
                Drzava
            };
            return csvValues;
        }
        public void FromCSV(string[] s)
        {
            Id = int.Parse(s[0]);
            Ulica = s[1];
            Broj = s[2];
            Grad = s[3];
            Drzava = s[4];
        }
    }
}
