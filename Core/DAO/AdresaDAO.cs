using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class AdresaDAO
    {
        private List<Adresa> _adrese;
        private Storage<Adresa> _storage;

        public AdresaDAO()
        {
            _storage = new Storage<Adresa>("adresa.csv");
            _adrese = _storage.Load();
        }

        private int GenerateID()
        {
            if (_adrese.Count == 0) return 0;
            return _adrese.Max(a => a.Id) + 1;
        }

        public Adresa AddAdresa(Adresa adresa)
        {
            adresa.Id = GenerateID();
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? UpdateAdresa(Adresa adresa)
        {
            Adresa? oldAdresa = GetAdresaByID(adresa.Id);
            if (oldAdresa == null) return null;

            oldAdresa.Ulica = adresa.Ulica;
            oldAdresa.Broj = adresa.Broj;
            oldAdresa.Grad = adresa.Grad;
            oldAdresa.Drzava = adresa.Drzava;

            _storage.Save(_adrese);
            return oldAdresa;
        }

        public Adresa? RemoveAdresa(int id)
        {
            Adresa? adresa = GetAdresaByID(id);
            if (adresa == null) return null;

            _adrese.Remove(adresa);
            _storage.Save(_adrese);
            return adresa;
        }

        public Adresa? GetAdresaByID(int id)
        {
            return _adrese.Find(a => a.Id == id);
        }

        public List<Adresa> GetAllAdresa()
        {
            return _adrese;
        }

        public void Reload()
        {
            _adrese = _storage.Load();
        }

        public List<Adresa> GetAllAdresa(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Adresa> adrese = _adrese;

            // sortiraj vehicles ukoliko je sortCriteria naveden
            switch (sortCriteria)
            {
                case "Id":
                    adrese = _adrese.OrderBy(ad => ad.Id);
                    break;
                case "Ulica":
                    adrese = _adrese.OrderBy(ad => ad.Ulica);
                    break;
                case "Broj":
                    adrese = _adrese.OrderBy(ad => ad.Broj);
                    break;
                case "Grad":
                    adrese = _adrese.OrderBy(ad => ad.Grad);
                    break;
                case "Drzava":
                    adrese = _adrese.OrderBy(ad => ad.Drzava);
                    break;
            }

            // promeni redosled ukoliko ima potrebe za tim
            if (sortDirection == SortDirection.Descending)
                adrese = adrese.Reverse();

            // paginacija
            adrese = adrese.Skip((page - 1) * pageSize).Take(pageSize);

            return adrese.ToList();
        }
    }
}
