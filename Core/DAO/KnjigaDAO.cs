using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class KnjigaDAO
    {
        private List<Knjiga> _knjige;
        private Storage<Knjiga> _storage;

        public KnjigaDAO()
        {
            _storage = new Storage<Knjiga>("knjiga.csv");
            _knjige = _storage.Load();
        }

        public int GenerateID()
        {
            if (_knjige.Count == 0) return 0;
            return _knjige.Max(k => k.Id) + 1;
        }

        public Knjiga AddKnjiga(Knjiga knjiga)
        {
            knjiga.Id = GenerateID();
            _knjige.Add(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }

        public Knjiga? UpdateKnjiga(Knjiga knjiga)
        {
            Knjiga? oldKnjiga = GetKnjigaByID(knjiga.Id);
            if (oldKnjiga == null) return null;

            oldKnjiga.ISBN = knjiga.ISBN;
            oldKnjiga.Naziv = knjiga.Naziv;
            oldKnjiga.ZanrKnjige = knjiga.ZanrKnjige;
            oldKnjiga.GodinaIzdanja = knjiga.GodinaIzdanja;
            oldKnjiga.Cena = knjiga.Cena;
            oldKnjiga.BrStrana = knjiga.BrStrana;
            oldKnjiga.IzdavacId = knjiga.IzdavacId;

            _storage.Save(_knjige);
            return oldKnjiga;
        }
        public Knjiga? RemoveKnjiga(int id)
        {
            Knjiga? knjiga = GetKnjigaByID(id);
            if (knjiga == null) return null;

            _knjige.Remove(knjiga);
            _storage.Save(_knjige);
            return knjiga;
        }

        public List<Knjiga> GetAllKnjige()
        {
            return _knjige;
        }

        public Knjiga? GetKnjigaByID(int id)
        {
            return _knjige.Find(k => k.Id == id);
        }

        public List<Knjiga> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<Knjiga>();

            return _knjige.Where(k =>
                k.ISBN.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                k.Naziv.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                k.ZanrKnjige.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public List<Knjiga> GetAllKnjige(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Knjiga> knjige = _knjige;

            switch (sortCriteria)
            {
                case "Id":
                    knjige = _knjige.OrderBy(k => k.Id);
                    break;
                case "ISBN":
                    knjige = _knjige.OrderBy(k => k.ISBN);
                    break;
                case "Naziv":
                    knjige = _knjige.OrderBy(k => k.Naziv);
                    break;
                case "ZanrKnjige":
                    knjige = _knjige.OrderBy(k => k.ZanrKnjige);
                    break;
                case "GodinaIzdanja":
                    knjige = _knjige.OrderBy(k => k.GodinaIzdanja);
                    break;
                case "Cena":
                    knjige = _knjige.OrderBy(k => k.Cena);
                    break;
                case "IzdavacId":
                    knjige = _knjige.OrderBy(k => k.IzdavacId);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
                knjige = knjige.Reverse();

            // paginacija
            knjige = knjige.Skip((page - 1) * pageSize).Take(pageSize);

            return knjige.ToList();
        }
    }
}
