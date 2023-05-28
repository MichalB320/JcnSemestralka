namespace Library;

public class Databaza
{
    private List<Osoba> _osUdaje;
    private List<Student> _studenti;
    private List<Zap_Predmet> _zapPredmety;
    private List<Predmet> _predmety;
    private List<Ucitel> _ucitelia;
    private List<Predmet_bod> _predmetBody;
    private List<st_program> _stProgramy;
    private List<St_odbor> _stOdbory;
    private string[] _collOsUdaje = { "rod_cislo", "meno", "priszvisko", "ulica", "psc", "obec" };
    private string[] _collStudenti = { "os_cislo", "st_odbor", "st_zameranie", "rod_cislo", "rocnik", "st_skupina", "stav", "ukoncenie", "dat_zapisu" };


    //public Osoba this[int index]
    //{
    //    get { return _osUdaje[index]; }
    //    set { _osUdaje[index] = value; }
    //}

    public Databaza()
    {
        _osUdaje = new();
        _studenti = new List<Student>();
        _zapPredmety = new();
        _predmety = new();
        _ucitelia = new();
        _predmetBody = new();
        _stProgramy = new();
        _stOdbory = new();

    }

    public void LoadOsUdaje(FileInfo csvFile, Action<string[]> action)
    {
        using (var sr = new StreamReader(csvFile.FullName))
        {
            string? line;
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

    //public void Load(FileInfo csvFile)
    //{
    //    LoadOsUdaje(csvFile,  (casti) => {
    //        var osoba = new Osoba(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
    //        _osUdaje.Add(osoba);
    //    });
    //}

    public void Add<T>(List<T> list, T item) => list.Add(item);

    public List<Osoba> GetOsoby() => _osUdaje;
    public List<Student> GetStudenti() => _studenti;
    public List<Zap_Predmet> GetZapPredmety() => _zapPredmety;
    public List<Predmet> GetPredmety() => _predmety;
    public List<Ucitel> GetUcitelia() => _ucitelia;
    public List<Predmet_bod> GetPredmet_Body() => _predmetBody;
    public List<st_program> GetStProgramy() => _stProgramy;
    public List<St_odbor> GetStOdbory() => _stOdbory;
}
