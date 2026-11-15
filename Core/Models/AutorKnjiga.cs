using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Models
{
    public class AutorKnjiga : ISerializable
    {
        public int AutorId { get; set; }
        public int KnjigaId { get; set; }
        public AutorKnjiga() { }

        public AutorKnjiga(int aid, int kid)
        {
            AutorId = aid;
            KnjigaId = kid;
        }
        public string[] ToCSV()
        {
            return new string[] { AutorId.ToString(), KnjigaId.ToString() };
        }
        public void FromCSV(string[] v)
        {
            AutorId = int.Parse(v[0]);
            KnjigaId = int.Parse(v[1]);
        }
    }
}
