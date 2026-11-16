using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{
    public class KupovinaDAO
    {
        private readonly List<Kupovina> _kupovine;
        private readonly Storage<Kupovina> _storage;

        public KupovinaDAO()
        {
            _storage = new Storage<Kupovina>("kupovina.csv");
            _kupovine = _storage.Load();
        }

        public int GenerateID()
        {
            if (_kupovine.Count == 0) return 0;
            return _kupovine.Max(k => k.Id) + 1;
        }

        public Kupovina AddKupovina(Kupovina kupovina)
        {
            kupovina.Id = GenerateID();
            _kupovine.Add(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public Kupovina? UpdateKupovina(Kupovina kupovina)
        {
            Kupovina? oldKupovina = GetKupovinaByID(kupovina.Id);
            if (oldKupovina == null) return null;

            oldKupovina.Id = kupovina.Id;
            oldKupovina.DatumKupovine = kupovina.DatumKupovine;
            oldKupovina.Ocena = kupovina.Ocena;
            oldKupovina.Komentar = kupovina.Komentar;

            _storage.Save(_kupovine);
            return oldKupovina;
        }

        public Kupovina? RemoveKupovina(int id)
        {
            Kupovina? kupovina = GetKupovinaByID(id);
            if (kupovina == null) return null;
            _kupovine.Remove(kupovina);
            _storage.Save(_kupovine);
            return kupovina;
        }

        public List<Kupovina> GettAllKupovine()
        {
            return _kupovine;
        }

        public Kupovina? GetKupovinaByID(int id)
        {
            return _kupovine.Find(k => k.Id == id);
        }
    }
}
