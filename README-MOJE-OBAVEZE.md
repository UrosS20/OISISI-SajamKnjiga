# Moje obaveze — vodič za usmenu odbranu

Ovaj fajl objašnjava funkcionalnosti iz raspodele posla koje pripadaju prvom studentu. Fajl se nalazi u glavnom folderu solution-a, izvan projekata `Core` i `ConsoleClient`.

## 1. Osnovni tok aplikacije

Aplikacija je konzolna aplikacija. Glavni korisnički interfejs nalazi se u:

```text
ConsoleClient/Program.cs
```

Poslovna logika i rad sa podacima nalaze se u projektu `Core`.

Tok rada je:

```text
Korisnik → Program.cs → DAO → Storage<T> → Serializer<T> → CSV fajl
```

Na primer, kod dodavanja posetioca:

1. korisnik bira opciju u meniju;
2. `ReadVisitor()` čita podatke;
3. kreira se objekat `Posetilac`;
4. `PosetilacDAO.AddPosetilac()` dodeljuje ID;
5. objekat se dodaje u listu;
6. `Storage.Save()` upisuje podatke u `Core/Data/posetilac.csv`.

---

# 2. Oznake iz raspodele posla

## `#posetilac`

Glavni model je:

```text
Core/Models/Posetilac.cs
```

Model sadrži:

- ID posetioca;
- broj članske karte;
- ime i prezime;
- datum rođenja;
- adresu;
- kontakt telefon;
- email;
- trenutnu godinu članstva;
- status posetioca;
- prosečnu ocenu recenzije;
- listu kupovina;
- listu želja.

Podaci se čuvaju u:

```text
Core/Data/posetilac.csv
```

Za čuvanje objekta koriste se metode `ToCSV()` i `FromCSV()`.

## `#kupovina`

Model kupovine nalazi se u:

```text
Core/Models/Kupovina.cs
```

DAO klasa je:

```text
Core/DAO/KupovinaDAO.cs
```

Kupovina sadrži:

- ID kupovine;
- broj članske karte kupca;
- ID kupljene knjige;
- datum kupovine;
- ocenu;
- komentar.

Podaci se čuvaju u:

```text
Core/Data/kupovina.csv
```

`KupovinaDAO` podržava:

- dodavanje kupovine;
- izmenu kupovine;
- brisanje kupovine;
- pronalaženje po ID-u;
- prikaz svih kupovina.

Pri učitavanju se čuvaju ID vrednosti posetioca i knjige. To omogućava povezivanje sa drugim podacima.

## `#izdavac`

Model izdavača nalazi se u:

```text
Core/Models/Izdavac.cs
```

DAO klasa je:

```text
Core/DAO/IzdavacDAO.cs
```

Podaci se čuvaju u:

```text
Core/Data/izdavaci.csv
```

Izdavač ima:

- ID;
- naziv;
- šefa, odnosno autora;
- listu autora;
- listu knjiga.

`IzdavacDAO` ima metode:

```csharp
GetAll()
GetById(int id)
Add(Izdavac izdavac)
Update(Izdavac izdavac)
Delete(int id)
```

Kod dodavanja izdavača ID se generiše kao najveći postojeći ID plus jedan.

## `#menu_bar`

Glavni meni je u `Program.cs` i prikazuje se na početku programa:

```text
=== SAJAM KNJIGA ===
1. Adrese
2. Autori
3. Izdavači
4. Knjige
5. Posetioci
6. Kupovine
7. Lista želja
0. Izlaz
```

Glavni meni radi u `while (true)` petlji. Izbor korisnika se obrađuje kroz `switch` naredbu.

Primer:

```csharp
switch (ReadText("Izbor: "))
{
	case "1": AddressMenu(); break;
	case "2": AuthorMenu(); break;
	case "3": PublisherMenu(); break;
	case "4": BookMenu(); break;
	case "5": VisitorMenu(); break;
	case "6": PurchaseMenu(); break;
	case "7": WishlistMenu(); break;
	case "0": return;
}
```

Na odbrani se može reći da `menu_bar` predstavlja navigaciju između funkcionalnih celina aplikacije.

## `#status_bar`

U ovoj konzolnoj verziji ne postoji posebna grafička statusna traka kao kod desktop aplikacija. Njenu ulogu imaju:

- poruke nakon operacije;
- `Pause()` metoda;
- poruke o greškama i uspehu;
- tekst `Pritisnite Enter za nastavak...`.

Primer:

```csharp
Pause("Posetilac je dodat.");
```

Metoda `Pause()` prikazuje poruku, čeka da korisnik pritisne Enter i sprečava da se ekran odmah očisti.

Ako profesor pita zašto nema posebnog `StatusBar` objekta, odgovor je:

> Pošto je projekat konzolna aplikacija, statusne informacije su implementirane tekstualnim porukama kroz `Pause()` i validacione poruke.

## `#prikaz_posetilaca`

Prikaz posetilaca nalazi se u `VisitorMenu()`.

Opcija:

```text
4. Prikaži sve
```

poziva:

```csharp
PrintAll(posetilacDao.GetAllPosetioci());
```

`GetAllPosetioci()` vraća listu svih posetilaca iz memorije, a `PrintAll()` ih prikazuje na konzoli.

Postoji i opcija:

```text
5. Prikaži profil po ID-u
```

Tada se prvo poziva:

```csharp
posetilacDao.GetPosetilacByID(visitorId)
```

Ako posetilac postoji, prikazuje se njegov `ToString()` prikaz. Ako ne postoji, ispisuje se poruka `Posetilac ne postoji.`

## `#dodavanje_posetioca`

Opcija se nalazi u `VisitorMenu()`:

```text
1. Dodavanje
```

Poziv je:

```csharp
posetilacDao.AddPosetilac(ReadVisitor());
```

`ReadVisitor()` čita:

- broj članske karte;
- ime;
- prezime;
- datum rođenja;
- telefon;
- email;
- godinu članstva;
- status;
- prosečnu ocenu;
- adresu.

DAO zatim:

1. generiše ID;
2. dodaje posetioca u listu;
3. poziva `Storage.Save()`;
4. čuva podatke u `posetilac.csv`.

## `#izmena_posetioca`

Opcija je:

```text
2. Izmena
```

Program prvo traži ID i proverava da li posetilac postoji:

```csharp
if (posetilacDao.GetPosetilacByID(id) == null)
```

Zatim se čitaju novi podaci pomoću `ReadVisitor(id)` i poziva se:

```csharp
posetilacDao.UpdatePosetilac(ReadVisitor(id))
```

DAO pronalazi starog posetioca i menja njegova svojstva. Nakon toga poziva `Save()`.

## `#brisanje_posetioca`

Opcija je:

```text
3. Brisanje
```

Poziva:

```csharp
posetilacDao.RemovePosetilac(ReadInt("ID posetioca: "))
```

DAO pronalazi posetioca po ID-u, uklanja ga iz liste i čuva izmenjene podatke u CSV fajl.

Ako posetilac ne postoji, metoda vraća `null`, a korisnik dobija poruku:

```text
Posetilac ne postoji.
```

## `#dodavanje_knjige`

Opcija se nalazi u `BookMenu()`:

```text
1. Dodavanje
```

Poziva:

```csharp
knjigaDao.AddKnjiga(ReadBook());
```

`ReadBook()` čita:

- ISBN;
- naziv;
- žanr;
- godinu izdanja;
- cenu;
- broj strana;
- ID izdavača.

`KnjigaDAO.AddKnjiga()` generiše ID, dodaje knjigu u listu i čuva je u:

```text
Core/Data/knjiga.csv
```

Decimalne vrednosti se čuvaju pomoću `CultureInfo.InvariantCulture`, tako da CSV koristi decimalnu tačku.

## `#izmena_knjige`

Opcija je:

```text
2. Izmena
```

Program traži ID knjige i proverava da li knjiga postoji. Zatim čita nove podatke i poziva:

```csharp
knjigaDao.UpdateKnjiga(ReadBook(id))
```

DAO menja:

- ISBN;
- naziv;
- žanr;
- godinu izdanja;
- cenu;
- broj strana;
- ID izdavača.

Na kraju se podaci upisuju u `knjiga.csv`.

---

# 3. Dodatne obaveze

## `#prikaz_kupljenih_knjiga`

Podaci o kupovinama prikazuju se kroz meni `Kupovine`.

Opcija:

```text
4. Prikaz svih
```

poziva:

```csharp
PrintAll(kupovinaDao.GettAllKupovine());
```

Metoda vraća sve kupovine učitane iz `kupovina.csv`.

Naziv metode `GettAllKupovine()` ima slovnu grešku, ali funkcionalno radi. Ispravniji naziv bi bio `GetAllKupovine()`.

## `#dodavanje_knjige_na_listu_zelja`

Ova funkcionalnost se nalazi u `WishlistMenu()` pod opcijom:

```text
1. Dodaj knjigu na listu želja
```

Korisnik unosi:

- broj članske karte;
- ID knjige.

Program proverava:

1. da li posetilac postoji;
2. da li knjiga postoji;
3. da li je knjiga već na listi želja.

Ako je sve ispravno, poziva se:

```csharp
zeljeDao.AddZelja(cardNumber, bookId);
```

Podaci se čuvaju u:

```text
Core/Data/zelje.csv
```

## `#ponistavanje_kupovine`

U trenutnom kodu ne postoji posebna opcija sa nazivom `Poništavanje kupovine`.

Postojeće funkcionalnosti su:

- izmena kupovine;
- brisanje kupovine kroz meni `Kupovine`;
- uklanjanje knjige sa liste želja;
- uklanjanje kupljene knjige sa liste želja nakon kupovine.

Brisanje kupovine se nalazi u `PurchaseMenu()` pod opcijom:

```text
3. Brisanje
```

Poziva:

```csharp
kupovinaDao.RemoveKupovina(id)
```

Ako se pod `poništavanjem kupovine` očekuje vraćanje knjige na listu želja ili vraćanje zalihe, ta funkcionalnost još nije posebno implementirana.

## `#pretraga_posetilaca`

U trenutnom `VisitorMenu()` postoji prikaz svih posetilaca i prikaz profila po ID-u, ali ne postoji posebna opcija za tekstualnu pretragu posetilaca.

Postoji:

```csharp
posetilacDao.GetPosetilacByID(id)
```

To je pretraga po ID-u, ali nije pretraga po imenu, prezimenu ili emailu.

Ako se na odbrani pita, precizan odgovor je:

> Pretraga posetioca po ID-u je implementirana, dok posebna tekstualna pretraga po više polja nije dodata u trenutnoj verziji.

## `#sortiranje_posetilaca`

U trenutnom `PosetilacDAO` ne postoji metoda za sortiranje posetilaca. Zato ova oznaka nije u potpunosti implementirana.

U drugim DAO klasama postoje primeri sortiranja i paginacije, ali `PosetilacDAO` trenutno ima samo:

- dodavanje;
- izmenu;
- brisanje;
- prikaz svih;
- pronalaženje po ID-u;
- ponovno učitavanje.

Potrebna metoda bi mogla da izgleda ovako:

```csharp
public List<Posetilac> GetAllPosetioci(string sortCriteria)
{
	return sortCriteria switch
	{
		"Ime" => _posetioci.OrderBy(p => p.Ime).ToList(),
		"Prezime" => _posetioci.OrderBy(p => p.Prezime).ToList(),
		"Godina" => _posetioci.OrderBy(p => p.TrenutnaGodinaClanstva).ToList(),
		_ => _posetioci
	};
}
```

## `#knjige_autora`

Model `Autor` ima listu:

```csharp
public List<Knjiga> Knjige { get; set; }
```

Model `Knjiga` ima listu autora:

```csharp
public List<Autor> Autori { get; set; }
```

Za vezu postoji model:

```text
Core/Models/AutorKnjiga.cs
```

i DAO:

```text
Core/DAO/AutorKnjigaDAO.cs
```

Veza se čuva preko dva ID-a:

```csharp
AutorId
KnjigaId
```

Međutim, u trenutnoj verziji nema posebnog menija koji učitava i prikazuje sve autore određene knjige. Modeli i DAO za vezu postoje, ali povezivanje nije završeno kroz korisnički interfejs.

## `#dodavanje_knjige_autoru`

`AutorKnjigaDAO` ima metodu:

```csharp
AddVeza(int autorId, int knjigaId)
```

Ona kreira objekat `AutorKnjiga`, dodaje ga u listu veza i čuva podatke.

Očekivani tok je:

1. proveriti da autor postoji;
2. proveriti da knjiga postoji;
3. kreirati vezu pomoću `AddVeza()`;
4. sačuvati par ID vrednosti.

U trenutnoj verziji ne postoji opcija u `Program.cs` koja ovu metodu poziva. Zato je DAO spreman za funkcionalnost, ali meni za dodavanje veze još nije dodat.

## `#uklanjanje_knjige_sa_autora`

`AutorKnjigaDAO` ima metodu:

```csharp
RemoveVeza(int autorId, int knjigaId)
```

Metoda pronalazi vezu po oba ID-a, uklanja je iz liste i čuva izmenjene podatke.

Kao i kod dodavanja veze, trenutno ne postoji opcija u glavnom meniju koja ovu metodu poziva. Za kompletnu funkcionalnost potrebno je dodati meni za upravljanje vezama autora i knjiga.

## `#postavljanje_sefa_izdavaca`

Model `Izdavac` ima svojstvo:

```csharp
public Autor? Sef { get; set; }
```

`Izdavac.ToCSV()` čuva ID šefa. Ako šef nije postavljen, čuva se vrednost `0`.

`Izdavac.FromCSV()` učitava ID šefa i kreira autora samo sa tim ID-em.

U trenutnom `PublisherMenu()` postoje:

- dodavanje izdavača;
- izmena izdavača;
- brisanje izdavača;
- prikaz svih izdavača.

Ne postoji posebna opcija za izbor autora i postavljanje tog autora kao šefa izdavača. Model podržava podatak, ali korisnički meni za postavljanje šefa još nije dodat.

## `5`

Broj `5` se u raspodeli pojavljuje samostalno. U ovom projektu opcija `5` ima različita značenja u zavisnosti od menija:

- u meniju autora: pretraga autora;
- u meniju knjiga: pretraga knjiga;
- u meniju posetilaca: prikaz profila po ID-u;
- u listi želja: prikaz svih stavki liste želja.

Zato treba proveriti originalni dokument raspodele da bi se utvrdilo na koju tačno funkcionalnost se odnosi samostalna oznaka `5`.

## `#autor_posetioci`

U trenutnom kodu ne postoji posebna metoda ili meni koji direktno povezuje autora sa posetiocima.

Autor ima podatke o svojim knjigama, a posetilac ima podatke o kupovinama i listi želja. Veza između autora i posetioca može se izvesti preko:

```text
Posetilac → Kupovina → Knjiga → Autor
```

Da bi ovo radilo potpuno, potrebno je prvo učitati relaciju autor–knjiga i povezati objekte, a zatim pronaći knjige koje je posetilac kupio ili dodao na listu želja.

U trenutnoj verziji ova relacija postoji na nivou modela, ali nije automatski izgrađena pri učitavanju CSV fajlova.

## `#posetilac_uslov_knjiga`

Ova oznaka nije eksplicitno implementirana kao posebna metoda.

Najbliža postojeća funkcionalnost je dodavanje knjige na listu želja. Program proverava da posetilac postoji pre nego što mu dozvoli dodavanje knjige:

```csharp
if (posetilacDao.GetAllPosetioci().All(p => p.BrojClanskeKarte != cardNumber))
{
	Pause("Posetilac ne postoji.");
}
```

Takođe se proverava da knjiga postoji. Ako se pod `uslov knjiga` misli na dodatno pravilo, na primer maksimalan broj knjiga ili uslov članstva, takvo pravilo trenutno nije izdvojeno u posebnu metodu.

---

# 4. Najvažnije metode za odbranu

## `ReadVisitor()`

Čita podatke posetioca sa konzole i kreira objekat `Posetilac`.

## `AddPosetilac()`

Dodeljuje novi ID, dodaje posetioca u listu i čuva podatke.

## `UpdatePosetilac()`

Pronalazi postojećeg posetioca po ID-u, menja njegove podatke i snima listu.

## `RemovePosetilac()`

Pronalazi posetioca po ID-u, uklanja ga i snima listu.

## `AddKupovina()`

Dodeljuje ID kupovini, dodaje je u listu i čuva podatke.

## `AddZelja()`

Dodaje knjigu na listu želja i sprečava dupliranje iste knjige za istog posetioca.

## `AddVeza()` i `RemoveVeza()`

Dodaju i uklanjaju vezu između autora i knjige pomoću dva ID-a.

---

# 5. Pitanja i kratki odgovori

### Zašto postoji DAO?

DAO odvaja rad sa podacima od korisničkog interfejsa. `Program.cs` ne mora da zna kako se podaci upisuju u CSV; on poziva metode DAO klase.

### Šta radi `Storage<T>`?

To je generička klasa koja učitava i čuva listu bilo kog modela koji implementira `ISerializable`.

### Zašto se koristi `ISerializable`?

Da bi svaki model imao standardne metode `ToCSV()` i `FromCSV()` za pretvaranje objekta u tekst i obrnuto.

### Gde se čuvaju podaci?

U CSV fajlovima u folderu `Core/Data`.

### Kako se dodeljuje ID?

DAO pronalazi najveći postojeći ID i dodaje jedan. Ako nema podataka, koristi početnu vrednost.

### Kako se proverava da posetilac postoji?

Poziva se `GetPosetilacByID()` ili se pretražuje lista posetilaca po broju članske karte.

### Šta se dešava ako posetilac ne postoji?

Operacija se prekida i korisniku se prikazuje poruka da posetilac ne postoji.

### Šta je trenutno nedovršeno?

Najvažnije nedovršene funkcionalnosti su:

- tekstualna pretraga posetilaca;
- sortiranje posetilaca;
- poseban meni za autor–knjiga veze;
- posebno postavljanje šefa izdavača;
- automatsko povezivanje svih objekata nakon učitavanja CSV fajlova;
- posebno poništavanje kupovine.

---

# 6. Kratak govor za odbranu

> Moj deo projekta obuhvata rad sa posetiocima, kupovinama, izdavačima, glavnim menijem i većinom operacija nad knjigama. Za svaki entitet postoji model i DAO klasa. Model opisuje podatke, a DAO obezbeđuje dodavanje, izmenu, brisanje i učitavanje. Podaci se čuvaju u CSV fajlovima preko generičkih klasa `Storage<T>` i `Serializer<T>`. U `Program.cs` se nalaze meniji koji pozivaju odgovarajuće DAO metode. Kod posetilaca su implementirani prikaz, dodavanje, izmena i brisanje. Kod knjiga su implementirani dodavanje i izmena. Kupovine i lista želja koriste ID vrednosti knjiga i posetilaca za povezivanje. Za relaciju autora i knjige postoje model `AutorKnjiga` i DAO, ali je za potpunu funkcionalnost potrebno dodati meni koji će te veze koristiti.\n