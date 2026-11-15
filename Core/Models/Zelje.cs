using System;
using System.Collections.Generic;
using System.Text;
using Core.Storage.Serialization;

namespace Core.Models
{
    public class Zelje : ISerializable
    {
        public string BrojClanskeKarte { get; set; } = string.Empty;
        public string KnjigaId { get; set; } = string.Empty;

        public Zelje() { }

        public string[] ToCSV()
        {
            string[] v = { BrojClanskeKarte, KnjigaId };
            return v;
        }
        public void FromCSV(string[] v)
        {
            BrojClanskeKarte = v[0];
            KnjigaId = v[1];

        }
    }
}
