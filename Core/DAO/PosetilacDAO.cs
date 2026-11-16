using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Core.DAO
{
    public class PosetilacDAO
    {
        private List<Posetilac> _posetioci;
        private Storage<Posetilac> _storage;

        public PosetilacDAO()
        {
            _storage = new Storage<Posetilac>("posetilac.csv");
            _posetioci = _storage.Load();
        }

        public int GenerateID()
        {
            if (_posetioci.Count == 0) return 0;
            return _posetioci.Max(p => p.Id) + 1;
        }

        public Posetilac AddPosetilac(Posetilac posetilac)
        {
            posetilac.Id = GenerateID();
            _posetioci.Add(posetilac);
            _storage.Save(_posetioci);
            return posetilac;
        }

        public Posetilac? UpdatePosetilac(Posetilac posetilac)
        {
            Posetilac? oldPosetialac = GetPosetilacByID(posetilac.Id);
            if (oldPosetialac == null) return null;

            oldPosetialac.Ime = posetilac.Ime;
            oldPosetialac.Prezime = posetilac.Prezime;
            oldPosetialac.DatumRodjenja = posetilac.DatumRodjenja;
            oldPosetialac.Adresa = posetilac.Adresa;
            oldPosetialac.KontaktTelefon = posetilac.KontaktTelefon;
            oldPosetialac.Email = posetilac.Email;
            oldPosetialac.BrojClanskeKarte = posetilac.BrojClanskeKarte;
            oldPosetialac.TrenutnaGodinaClanstva = posetilac.TrenutnaGodinaClanstva;
            oldPosetialac.Status = posetilac.Status;

            _storage.Save(_posetioci);
            return oldPosetialac;
        }

        public Posetilac? RemovePosetilac(int id)
        {
            Posetilac? posetilac = GetPosetilacByID(id);
            if (posetilac == null) return null;
            _posetioci.Remove(posetilac);
            _storage.Save(_posetioci);
            return posetilac;
        }

        public List<Posetilac> GetAllPosetioci()
        {
            return _posetioci;
        }

        public Posetilac? GetPosetilacByID(int id)
        {
            return _posetioci.Find(p => p.Id == id);
        }

        public void Reload()
        {
            _posetioci = _storage.Load();
        }
    }
}
