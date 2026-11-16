using System.Globalization;
using Core.DAO;
using Core.Models;

var adresaDao = new AdresaDAO();
var autorDao = new AutorDAO();
var izdavacDao = new IzdavacDAO();
var knjigaDao = new KnjigaDAO();
var posetilacDao = new PosetilacDAO();
var kupovinaDao = new KupovinaDAO();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== SAJAM KNJIGA ===");
    Console.WriteLine("1. Adrese");
    Console.WriteLine("2. Autori");
    Console.WriteLine("3. Izdavači");
    Console.WriteLine("4. Knjige");
    Console.WriteLine("5. Posetioci");
    Console.WriteLine("6. Kupovine");
    Console.WriteLine("0. Izlaz");

    switch (ReadText("Izbor: "))
    {
        case "1": AddressMenu(); break;
        case "2": AuthorMenu(); break;
        case "3": PublisherMenu(); break;
        case "4": BookMenu(); break;
        case "5": VisitorMenu(); break;
        case "6": PurchaseMenu(); break;
        case "0": return;
        default: Pause("Nepoznata opcija."); break;
    }
}

void AddressMenu()
{
    while (true)
    {
        ShowCrudMenu("ADRESE");
        switch (ReadText("Izbor: "))
        {
            case "1": adresaDao.AddAdresa(ReadAddress()); Pause("Adresa je dodata."); break;
            case "2":
                int id = ReadInt("ID adrese: ");
                Adresa? address = adresaDao.GetAdresaByID(id);
                if (address == null) { Pause("Adresa ne postoji."); break; }
                address = ReadAddress(id);
                Pause(adresaDao.UpdateAdresa(address) == null ? "Adresa ne postoji." : "Adresa je izmenjena.");
                break;
            case "3": Pause(adresaDao.RemoveAdresa(ReadInt("ID adrese: ")) == null ? "Adresa ne postoji." : "Adresa je obrisana."); break;
            case "4": PrintAll(adresaDao.GetAllAdresa()); break;
            case "0": return;
            default: Pause("Nepoznata opcija."); break;
        }
    }
}

void AuthorMenu()
{
    while (true)
    {
        ShowCrudMenu("AUTORI");
        switch (ReadText("Izbor: "))
        {
            case "1": autorDao.AddAutor(ReadAuthor()); Pause("Autor je dodat."); break;
            case "2":
                int id = ReadInt("ID autora: ");
                if (autorDao.GetAutorByID(id) == null) { Pause("Autor ne postoji."); break; }
                Pause(autorDao.UpdateAutor(ReadAuthor(id)) == null ? "Autor ne postoji." : "Autor je izmenjen.");
                break;
            case "3": Pause(autorDao.RemoveAutor(ReadInt("ID autora: ")) == null ? "Autor ne postoji." : "Autor je obrisan."); break;
            case "4": PrintAll(autorDao.GetAllAutor()); break;
            case "0": return;
            default: Pause("Nepoznata opcija."); break;
        }
    }
}

void PublisherMenu()
{
    while (true)
    {
        ShowCrudMenu("IZDAVAČI");
        switch (ReadText("Izbor: "))
        {
            case "1": izdavacDao.Add(new Izdavac(ReadText("Naziv: "))); Pause("Izdavač je dodat."); break;
            case "2":
                int id = ReadInt("ID izdavača: ");
                if (izdavacDao.GetById(id) == null) { Pause("Izdavač ne postoji."); break; }
                izdavacDao.Update(new Izdavac(id, ReadText("Novi naziv: ")));
                Pause("Izdavač je izmenjen.");
                break;
            case "3": izdavacDao.Delete(ReadInt("ID izdavača: ")); Pause("Brisanje je izvršeno."); break;
            case "4": PrintAll(izdavacDao.GetAll()); break;
            case "0": return;
            default: Pause("Nepoznata opcija."); break;
        }
    }
}

void BookMenu()
{
    while (true)
    {
        ShowCrudMenu("KNJIGE");
        switch (ReadText("Izbor: "))
        {
            case "1": knjigaDao.AddKnjiga(ReadBook()); Pause("Knjiga je dodata."); break;
            case "2":
                int id = ReadInt("ID knjige: ");
                if (knjigaDao.GetKnjigaByID(id) == null) { Pause("Knjiga ne postoji."); break; }
                Pause(knjigaDao.UpdateKnjiga(ReadBook(id)) == null ? "Knjiga ne postoji." : "Knjiga je izmenjena.");
                break;
            case "3": Pause(knjigaDao.RemoveKnjiga(ReadInt("ID knjige: ")) == null ? "Knjiga ne postoji." : "Knjiga je obrisana."); break;
            case "4": PrintAll(knjigaDao.GetAllKnjige()); break;
            case "0": return;
            default: Pause("Nepoznata opcija."); break;
        }
    }
}

void VisitorMenu()
{
    while (true)
    {
        ShowCrudMenu("POSEТIOCI");
        switch (ReadText("Izbor: "))
        {
            case "1": posetilacDao.AddPosetilac(ReadVisitor()); Pause("Posetilac je dodat."); break;
            case "2":
                int id = ReadInt("ID posetioca: ");
                if (posetilacDao.GetPosetilacByID(id) == null) { Pause("Posetilac ne postoji."); break; }
                Pause(posetilacDao.UpdatePosetilac(ReadVisitor(id)) == null ? "Posetilac ne postoji." : "Posetilac je izmenjen.");
                break;
            case "3": Pause(posetilacDao.RemovePosetilac(ReadInt("ID posetioca: ")) == null ? "Posetilac ne postoji." : "Posetilac je obrisan."); break;
            case "4": PrintAll(posetilacDao.GetAllPosetioci()); break;
            case "0": return;
            default: Pause("Nepoznata opcija."); break;
        }
    }
}

void PurchaseMenu()
{
    while (true)
    {
        ShowCrudMenu("KUPOVINE");
        switch (ReadText("Izbor: "))
        {
            case "1": kupovinaDao.AddKupovina(ReadPurchase()); Pause("Kupovina je dodata."); break;
            case "2":
                int id = ReadInt("ID kupovine: ");
                if (kupovinaDao.GetKupovinaByID(id) == null) { Pause("Kupovina ne postoji."); break; }
                Pause(kupovinaDao.UpdateKupovina(ReadPurchase(id)) == null ? "Kupovina ne postoji." : "Kupovina je izmenjena.");
                break;
            case "3": Pause(kupovinaDao.RemoveKupovina(ReadInt("ID kupovine: ")) == null ? "Kupovina ne postoji." : "Kupovina je obrisana."); break;
            case "4": PrintAll(kupovinaDao.GettAllKupovine()); break;
            case "0": return;
            default: Pause("Nepoznata opcija."); break;
        }
    }
}

Adresa ReadAddress(int id = 0) => new(
    id,
    ReadText("Ulica: "),
    ReadText("Broj: "),
    ReadText("Grad: "),
    ReadText("Država: "));

Autor ReadAuthor(int id = 0) => new(
    id,
    ReadText("Ime: "),
    ReadText("Prezime: "),
    ReadDate("Datum rođenja (yyyy-MM-dd): "),
    ReadAddress(),
    ReadText("Telefon: "),
    ReadText("Email: "),
    ReadText("Broj lične karte: "),
    ReadInt("Godine iskustva: "),
    new List<Knjiga>());

Knjiga ReadBook(int id = 0) => new(
    id,
    ReadText("ISBN: "),
    ReadText("Naziv: "),
    ReadText("Žanr: "),
    ReadInt("Godina izdanja: "),
    ReadDouble("Cena: "),
    ReadInt("Broj strana: "),
    ReadInt("ID izdavača: "));

Posetilac ReadVisitor(int id = 0) => new(
    id,
    ReadText("Broj članske karte: "),
    ReadText("Ime: "),
    ReadText("Prezime: "),
    ReadDate("Datum rođenja (yyyy-MM-dd): "),
    ReadText("Telefon: "),
    ReadText("Email: "),
    ReadInt("Trenutna godina članstva: "),
    ReadStatus(),
    ReadDouble("Prosečna ocena: ")) { Adresa = ReadAddress() };

Kupovina ReadPurchase(int id = 0)
{
    string cardNumber = ReadText("Broj članske karte: ");
    int bookId = ReadInt("ID knjige: ");
    return new Kupovina(id, ReadDateTime("Datum kupovine (yyyy-MM-dd): "), ReadDouble("Ocena: "), ReadText("Komentar: "))
    {
        BrojClanskeKarte = cardNumber,
        PosetilacKupuje = new Posetilac { BrojClanskeKarte = cardNumber },
        KupljenaKnjiga = new Knjiga { Id = bookId }
    };
}

StatusPosetioca ReadStatus()
{
    while (true)
    {
        string value = ReadText("Status (R/V): ").ToUpperInvariant();
        if (Enum.TryParse(value, out StatusPosetioca status)) return status;
        Console.WriteLine("Unesite R ili V.");
    }
}

string ReadText(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine()?.Trim() ?? string.Empty;
}

int ReadInt(string prompt)
{
    while (true)
    {
        if (int.TryParse(ReadText(prompt), out int value)) return value;
        Console.WriteLine("Unesite ceo broj.");
    }
}

double ReadDouble(string prompt)
{
    while (true)
    {
        string input = ReadText(prompt).Replace(',', '.');
        if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)) return value;
        Console.WriteLine("Unesite decimalni broj.");
    }
}

DateOnly ReadDate(string prompt)
{
    while (true)
    {
        if (DateOnly.TryParseExact(ReadText(prompt), "yyyy-MM-dd", out DateOnly value)) return value;
        Console.WriteLine("Format datuma mora biti yyyy-MM-dd.");
    }
}

DateTime ReadDateTime(string prompt)
{
    while (true)
    {
        if (DateTime.TryParseExact(ReadText(prompt), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime value)) return value;
        Console.WriteLine("Format datuma mora biti yyyy-MM-dd.");
    }
}

void ShowCrudMenu(string name)
{
    Console.Clear();
    Console.WriteLine($"=== {name} ===");
    Console.WriteLine("1. Dodavanje");
    Console.WriteLine("2. Izmena");
    Console.WriteLine("3. Brisanje");
    Console.WriteLine("4. Prikaz svih");
    Console.WriteLine("0. Nazad");
}

void PrintAll<T>(IEnumerable<T> values)
{
    Console.Clear();
    foreach (T value in values) Console.WriteLine($"{value}\n--------------------");
    if (!values.Any()) Console.WriteLine("Nema podataka.");
    Pause();
}

void Pause(string message = "")
{
    if (!string.IsNullOrWhiteSpace(message)) Console.WriteLine(message);
    Console.WriteLine("Pritisnite Enter za nastavak...");
    Console.ReadLine();
}
