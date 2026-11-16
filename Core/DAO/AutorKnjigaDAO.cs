using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class AutorKnjigaDAO
    {
        private readonly List<AutorKnjiga> _veze;
        private readonly Storage<AutorKnjiga> _storage;

        public AutorKnjigaDAO()
        {
            _storage = new Storage<AutorKnjiga>("autor_knjiga.txt");
            _veze = _storage.Load();
        }

        public void AddVeza(int autorId, int knjigaId)
        {
            AutorKnjiga veza = new AutorKnjiga(autorId, knjigaId);
            _veze.Add(veza);
            _storage.Save(_veze);
        }

        public void RemoveVeza(int autorId, int knjigaId)
        {
            AutorKnjiga? veza = _veze.Find(v => v.AutorId == autorId && v.KnjigaId == knjigaId);
            if (veza != null)
            {
                _veze.Remove(veza);
                _storage.Save(_veze);
            }
        }

        public List<AutorKnjiga> GetAllAutoKnjigaVeze()
        {
            return _veze;
        }
    }
}
