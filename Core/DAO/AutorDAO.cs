using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class AutorDAO
    {
        private List<Autor> _autori;
        private Storage<Autor> _storage;

        public AutorDAO()
        {
            _storage = new Storage<Autor>("autor.csv");
            _autori = _storage.Load();
        }

        public int GenerateID()
        {
            if (_autori.Count == 0) return 0;
            return _autori.Max(a => a.Id) + 1;
        }

        public Autor AddAutor(Autor autor)
        {
            autor.Id = GenerateID();
            _autori.Add(autor);
            _storage.Save(_autori);
            return autor;
        }

        public Autor? UpdateAutor(Autor autor)
        {
            Autor? oldAutor = GetAutorByID(autor.Id);
            if (oldAutor == null) return null;

            oldAutor.Ime = autor.Ime;
            oldAutor.Prezime = autor.Prezime;
            oldAutor.DatumRodjenja = autor.DatumRodjenja;
            oldAutor.Adresa = autor.Adresa;
            oldAutor.Telefon = autor.Telefon;
            oldAutor.Email = autor.Email;
            oldAutor.BrojLicneKarte = autor.BrojLicneKarte;
            oldAutor.GodineIskustva = autor.GodineIskustva;

            _storage.Save(_autori);
            return oldAutor;
        }

        public Autor? RemoveAutor(int id)
        {
            Autor? autor = GetAutorByID(id);
            if (autor == null) return null;

            _autori.Remove(autor);
            _storage.Save(_autori);
            return autor;
        }

        public List<Autor> GetAllAutor()
        {
            return _autori;
        }

        public Autor? GetAutorByID(int id)
        {
            return _autori.Find(a => a.Id == id);
        }

        public List<Autor> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<Autor>();

            return _autori.Where(a =>
                a.Ime.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                a.Prezime.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                a.Telefon.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                a.Email.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                a.BrojLicneKarte.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        public void Reload()
        {
            _autori = _storage.Load();
        }

        public List<Autor> GetAllAutor(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Autor> autors = _autori;

            // sortiraj vehicles ukoliko je sortCriteria naveden
            switch (sortCriteria)
            {
                case "Id":
                    autors = _autori.OrderBy(a => a.Id);
                    break;
                case "Ime":
                    autors = _autori.OrderBy(a => a.Ime);
                    break;
                case "Prezime":
                    autors = _autori.OrderBy(a => a.Prezime);
                    break;
                case "DatumRodjenja":
                    autors = _autori.OrderBy(a => a.DatumRodjenja);
                    break;
                case "Telefon":
                    autors = _autori.OrderBy(a => a.Telefon);
                    break;
                case "Email":
                    autors = _autori.OrderBy(a => a.Email);
                    break;
                case "BrojLicneKarte":
                    autors = _autori.OrderBy(a => a.BrojLicneKarte);
                    break;
                case "GodineIskustva":
                    autors = _autori.OrderBy(a => a.GodineIskustva);
                    break;
            }

            // promeni redosled ukoliko ima potrebe za tim
            if (sortDirection == SortDirection.Descending)
                autors = autors.Reverse();

            // paginacija
            autors = autors.Skip((page - 1) * pageSize).Take(pageSize);

            return autors.ToList();
        }
    }
}
