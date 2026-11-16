using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAO
{
    public class IzdavacDAO
    {
        private readonly Storage<Izdavac> _storage;
        private List<Izdavac> _izdavaci;

        public IzdavacDAO()
        {
            _storage = new Storage<Izdavac>("izdavaci.csv");
            _izdavaci = _storage.Load();
        }

        public List<Izdavac> GetAll() => _izdavaci;

        public Izdavac? GetById(int id) => _izdavaci.Find(i => i.Id == id);

        public void Add(Izdavac izdavac)
        {
            int noviId = _izdavaci.Count == 0 ? 1 : _izdavaci.Max(i => i.Id) + 1;
            izdavac.Id = noviId;
            _izdavaci.Add(izdavac);
            _storage.Save(_izdavaci);
        }

        public void Update(Izdavac izdavac)
        {
            int index = _izdavaci.FindIndex(i => i.Id == izdavac.Id);
            if (index == -1) return;

            _izdavaci[index] = izdavac;
            _storage.Save(_izdavaci);
        }

        public void Delete(int id)
        {
            _izdavaci.RemoveAll(i => i.Id == id);
            _storage.Save(_izdavaci);
        }
    }
}
