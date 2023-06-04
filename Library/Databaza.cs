namespace Library;

public class Databaza
{
    private List<Item> _Items;
    private string[] _collOsUdaje = { "rod_cislo", "meno", "priszvisko", "ulica", "psc", "obec" };
    private string[] _collStudenti = { "os_cislo", "st_odbor", "st_zameranie", "rod_cislo", "rocnik", "st_skupina", "stav", "ukoncenie", "dat_zapisu" };

    private readonly string[][] stlpce = { new string[] { "rod_cislo", "meno", "priezvisko", "ulica", "psc", "obec" },
                              new string[] { "os_cislo", "st_odbor", "st_zameranie", "rod_cislo", "rocnik", "st_skupina", "stav", "ukoncenie", "dat_zapisu" },
                              new string[] { "os_cislo", "cis_predm", "skrok", "prednasajuci", "ects", "zapocet", "vysledok", "datum_sk"},
                              new string[] { "cis_predm", "nazov"},
                              new string[] { "os_cislo", "meno", "priezvisko", "katedra"},
                              new string[] { "cis_predm", "skrok", "garant", "ects", "semester", "forma_kont"},
                              new string[] { "st_odbor", "st_zameranie", "cis_predm", "skrok", "typ_povin", "rocnik", "odpor_rocnik"},
                              new string[] { "st_odbor", "st_zameranie", "popis_odboru", "popis_zamerania"} };

    public Databaza()
    {
        _Items = new();
    }

    public void Load(FileInfo csvFile, Action<string[]> action)
    {
        using (var sr = new StreamReader(csvFile.FullName))
        {
            string? line;
            //line = sr.ReadLine();//Preskoc hlavicku
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line is not null)
                {
                    string[] casti = line.Split(';');

                    action(casti);
                    //var osoba = new Osoba(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
                    //_osUdaje.Add(osoba);
                }
            }
            sr.Close();
        }
    }

    public void Save(FileInfo csvFile, Action<StreamWriter> action)
    {
        using (var sw = new StreamWriter(csvFile.FullName))
        {
            action(sw);
            sw.Close();
        }
    }

    public string[][] GetStlpce() => stlpce;
    public void Add<T>(List<T> list, T item) => list.Add(item);
    public List<Item> GetUniversal() => _Items;
}
