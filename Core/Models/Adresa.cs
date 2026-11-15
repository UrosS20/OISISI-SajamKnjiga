using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Models
{
    public class Adresa : ISerializable
    {
        public string Ulica { get; set; } = string.Empty;
        public string Broj { get; set; } = string.Empty;
        public string Grad { get; set; } = string.Empty;
        public string Drzava { get; set; } = string.Empty;

        public Adresa() { }

        public Adresa(string ulica, string broj, string grad, string drzava)
        {
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
           => new[] { Ulica, Broj, Grad, Drzava };
        public void FromCSV(string[] s)
        {
            if (s == null || s.Length < 4)
            {
                Ulica = Broj = Grad = Drzava = string.Empty; return;
            }
            Ulica = s[0].Trim();
            Broj = s[1].Trim();
            Grad = s[2].Trim();
            Drzava = s[3].Trim();
        }
    }
}
