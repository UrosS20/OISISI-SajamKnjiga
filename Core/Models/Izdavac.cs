using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    internal class Izdavac : ISerializable
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

    }
}
