# Sajam knjiga — vodič za usmenu odbranu

> Ovaj fajl se nalazi u glavnom folderu solution-a, izvan projekata `Core` i `ConsoleClient`. Namenjen je objašnjenju projekta na usmenoj odbrani.

## 1. Osnovna namena projekta

Aplikacija predstavlja konzolni sistem za upravljanje podacima sajma knjiga. Omogućava rad sa:

- adresama;
- autorima;
- izdavačima;
- knjigama;
- posetiocima;
- kupovinama;
- listom želja.

Podaci se čuvaju u CSV tekstualnim fajlovima u folderu `Core/Data`.

## 2. Struktura solution-a

Solution ima dva projekta:

### `Core`

Sadrži poslovnu logiku aplikacije:

- modele podataka u folderu `Models`;
- DAO klase u folderu `DAO`;
- čuvanje podataka u folderu `Storage`;
- početne podatke u folderu `Data`.

### `ConsoleClient`

Sadrži konzonski korisnički interfejs. Glavni fajl je `Program.cs`, u kome se prikazuju meniji, čitaju podaci od korisnika i pozivaju DAO metode.

`ConsoleClient` ima referencu na projekat `Core`, pa može da koristi njegove modele i DAO klase.

## 3. Pokretanje aplikacije

Prilikom pokretanja programa u `Program.cs` se kreiraju DAO objekti:

```csharp
var adresaDao = new AdresaDAO();
var autorDao = new AutorDAO();
var izdavacDao = new IzdavacDAO();
var knjigaDao = new KnjigaDAO();
var posetilacDao = new PosetilacDAO();
var kupovinaDao = new KupovinaDAO();
var zeljeDao = new ZeljeDAO();
```

Svaki DAO tada učitava svoje podatke iz odgovarajućeg CSV fajla. Nakon toga se prikazuje glavni meni.

Glavni meni ima sledeće opcije:

```text
1. Adrese
2. Autori
3. Izdavači
4. Knjige
5. Posetioci
6. Kupovine
7. Lista želja
0. Izlaz
```

Program radi u beskonačnoj petlji i prekida se kada korisnik izabere opciju `0`.

## 4. Modeli

Model predstavlja jednu vrstu podataka u sistemu.

### `Adresa`

Sadrži:

- `Id`;
- `Ulica`;
- `Broj`;
- `Grad`;
- `Drzava`.

### `Autor`

Sadrži:

- identifikacioni broj;
- ime i prezime;
- datum rođenja;
- adresu;
- telefon;
- email;
- broj lične karte;
- godine iskustva.

Datum se u CSV fajlu čuva u formatu `yyyy-MM-dd`, na primer:

```text
1893-11-26
```

### `Knjiga`

Sadrži:

- `Id`;
- ISBN;
- naziv;
- žanr;
- godinu izdanja;
- cenu;
- broj strana;
- ID izdavača;
- listu autora.

### `Izdavac`

Sadrži ID i naziv izdavača. Može imati i šefa, autore i knjige.

### `Posetilac`

Sadrži:

- broj članske karte;
- ime i prezime;
- datum rođenja;
- kontakt telefon;
- email;
- godinu učlanjenja;
- status;
- prosečnu ocenu recenzija.

### `Kupovina`

Sadrži:

- ID kupovine;
- posetioca;
- kupljenu knjigu;
- datum kupovine;
- ocenu;
- komentar.

### `Zelje`

Predstavlja jednu stavku liste želja. Čuva broj članske karte i ID knjige.

### `AutorKnjiga`

Predstavlja vezu više-prema-više između autora i knjiga. Čuva:

- `AutorId`;
- `KnjigaId`.

## 5. DAO sloj

DAO znači Data Access Object. Njegova uloga je da sakrije način čuvanja podataka od ostatka aplikacije.

Na primer, `AutorDAO` sadrži metode za:

- dodavanje autora;
- izmenu autora;
- brisanje autora;
- pronalaženje autora po ID-u;
- prikaz svih autora;
- pretragu autora.

Kada se doda, izmeni ili obriše podatak, DAO poziva `Storage.Save()` i upisuje novu listu u CSV fajl.

Najvažnije DAO klase su:

- `AdresaDAO` — rad sa adresama;
- `AutorDAO` — rad sa autorima;
- `KnjigaDAO` — rad sa knjigama;
- `IzdavacDAO` — rad sa izdavačima;
- `PosetilacDAO` — rad sa posetiocima;
- `KupovinaDAO` — rad sa kupovinama;
- `ZeljeDAO` — rad sa listom želja;
- `AutorKnjigaDAO` — rad sa vezama autora i knjiga.

## 6. Skladištenje podataka

Klasa `Storage<T>` je generička klasa koja omogućava čuvanje različitih modela bez ponavljanja istog koda.

Ona ima dve glavne metode:

```csharp
public List<T> Load()
public void Save(List<T> objects)
```

`Load()` čita redove iz CSV fajla i pretvara ih u objekte.

`Save()` pretvara objekte u CSV tekst i upisuje ih u fajl.

Putanja do podataka se formira u odnosu na lokaciju pokrenute aplikacije, a podaci se nalaze u:

```text
Core/Data
```

## 7. Serializacija

Interfejs `ISerializable` zahteva od svakog modela dve metode:

```csharp
string[] ToCSV();
void FromCSV(string[] values);
```

`ToCSV()` pretvara objekat u niz tekstualnih vrednosti.

`FromCSV()` čita niz tekstualnih vrednosti i popunjava objekat.

Klasa `Serializer<T>` koristi ovaj interfejs i generički radi sa svim modelima.

Glavni delimiter između kolona je znak:

```text
|
```

Adresa unutar autora ili posetioca koristi znak `;` između svojih delova.

Primer reda iz `autor.csv`:

```text
1|Miloš|Crnjanski|1893-11-26|1;Bulevar Evrope;42;Novi Sad;Srbija|064111111|milos.crnjanski@example.com|LK001|25
```

## 8. CRUD operacije

CRUD je skraćenica za:

- Create — dodavanje;
- Read — čitanje i prikaz;
- Update — izmena;
- Delete — brisanje.

Na primer, meni autora ima:

```text
1. Dodavanje
2. Izmena
3. Brisanje
4. Prikaz svih
5. Pretraga
0. Nazad
```

Kod dodavanja autor se prvo čita iz konzole, zatim mu DAO dodeljuje novi ID i čuva ga u `autor.csv`.

## 9. Pretraga

Pretraga autora se vrši po:

- imenu;
- prezimenu;
- telefonu;
- email adresi;
- broju lične karte.

Pretraga knjiga se vrši po:

- ISBN-u;
- nazivu;
- žanru.

Pretraga je delimična i ne razlikuje velika i mala slova. Na primer, upit `roman` pronalazi žanr `Roman`.

## 10. Lista želja i kupovina

Kod dodavanja knjige na listu želja proverava se:

1. da li posetilac postoji;
2. da li knjiga postoji;
3. da li se knjiga već nalazi na listi želja.

Kod kupovine sa liste želja proverava se da li stavka postoji. Ako postoji:

1. kreira se kupovina;
2. kupovina se čuva u `kupovina.csv`;
3. knjiga se uklanja iz `zelje.csv`.

## 11. Kultura i brojevi

CSV fajlovi koriste decimalnu tačku, na primer:

```text
890.00
4.50
```

Zbog toga se za decimalne vrednosti koristi `CultureInfo.InvariantCulture`. Na taj način program radi isto bez obzira na regionalna podešavanja računara.

Datumi se takođe čuvaju u standardnom formatu:

```text
yyyy-MM-dd
```

## 12. Kako objasniti tok dodavanja autora

Ako se na odbrani pita šta se dešava kada se doda autor, odgovor može biti:

1. Korisnik u glavnom meniju bira `Autori`.
2. Bira opciju `Dodavanje`.
3. `ReadAuthor()` čita podatke sa konzole.
4. Kreira se objekat klase `Autor`.
5. Poziva se `autorDao.AddAutor(autor)`.
6. DAO generiše novi ID.
7. Autor se dodaje u internu listu.
8. `Storage.Save()` poziva serializer.
9. `Autor.ToCSV()` pretvara autora u tekstualne kolone.
10. Novi sadržaj se upisuje u `Core/Data/autor.csv`.
11. Pri sledećem pokretanju `AutorDAO` poziva `Storage.Load()` i ponovo učitava autora iz fajla.

## 13. Kako objasniti grešku sa datumom

Greška je nastala zato što je program ranije očekivao format:

```text
yyyy-MM-dd
```

a u CSV fajlu je bio datum oblika:

```text
11/26/1893
```

Problem je rešen tako što se pri upisu koristi eksplicitni format `yyyy-MM-dd`, a pri učitavanju se koristi `DateOnly.ParseExact` i `CultureInfo.InvariantCulture`.

## 14. Važne stvari koje treba napomenuti na odbrani

- DAO odvaja poslovnu logiku od rada sa fajlovima.
- `Storage<T>` i `Serializer<T>` sprečavaju ponavljanje koda.
- Modeli implementiraju `ISerializable` zbog jedinstvenog CSV mehanizma.
- CSV fajlovi predstavljaju jednostavan oblik trajnog čuvanja podataka.
- ID vrednosti se koriste za povezivanje objekata.
- Liste u modelima predstavljaju moguće relacije sa drugim objektima.
- `AutorKnjiga` predstavlja veznu klasu za odnos više autora i više knjiga.
- Konzolni meni je odvojen od Core sloja.

## 15. Trenutna ograničenja projekta

Neke relacije postoje u modelima, ali nisu u potpunosti povezane pri učitavanju:

- autori knjige se ne popunjavaju automatski iz `AutorKnjigaDAO`;
- izdavač knjige se čuva preko `IzdavacId`, ali se objekat izdavača ne učitava automatski;
- kod učitanih kupovina posetilac i knjiga imaju osnovne ID podatke;
- `AutorKnjigaDAO` postoji, ali treba dodati meni za upravljanje vezama autora i knjiga.

Ovo su moguća proširenja projekta, a osnovni CRUD i rad sa CSV fajlovima su implementirani.

## 16. Kratak odgovor za kraj odbrane

Ovaj projekat je konzolna aplikacija za upravljanje sajmom knjiga. Organizovan je kroz Core sloj sa modelima, DAO klasama i 
generičkim sistemom za CSV skladištenje, dok ConsoleClient predstavlja korisnički interfejs. Podaci se učitavaju pri pokretanju,
menjaju kroz menije i čuvaju u CSV fajlovima. Za serijalizaciju se koristi zajednički interfejs `ISerializable`, a za smanjenje 
dupliranja koda koriste se generičke klase `Storage<T>` i `Serializer<T>`.
