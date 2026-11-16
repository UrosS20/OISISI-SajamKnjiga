using Core.Models;
using Core.Storage;

namespace Core.DAO
{
    public class ZeljeDAO
    {
        private readonly List<Zelje> _zelje;
        private readonly Storage<Zelje> _storage;

        public ZeljeDAO()
        {
            _storage = new Storage<Zelje>("zelje.csv");
            _zelje = _storage.Load();
        }

        public Zelje AddZelja(string brojClanskeKarte, int knjigaId)
        {
            if (GetZelja(brojClanskeKarte, knjigaId) != null)
                throw new InvalidOperationException("Knjiga je već na listi želja.");

            Zelje zelja = new()
            {
                BrojClanskeKarte = brojClanskeKarte,
                KnjigaId = knjigaId.ToString()
            };

            _zelje.Add(zelja);
            _storage.Save(_zelje);
            return zelja;
        }

        public Zelje? RemoveZelja(string brojClanskeKarte, int knjigaId)
        {
            Zelje? zelja = GetZelja(brojClanskeKarte, knjigaId);
            if (zelja == null) return null;

            _zelje.Remove(zelja);
            _storage.Save(_zelje);
            return zelja;
        }

        public Zelje? GetZelja(string brojClanskeKarte, int knjigaId)
        {
            return _zelje.Find(z =>
                z.BrojClanskeKarte == brojClanskeKarte &&
                z.KnjigaId == knjigaId.ToString());
        }

        public List<Zelje> GetAllZelje()
        {
            return _zelje;
        }

        public List<Zelje> GetZeljeZaPosetioca(string brojClanskeKarte)
        {
            return _zelje
                .Where(z => z.BrojClanskeKarte == brojClanskeKarte)
                .ToList();
        }
    }
}
