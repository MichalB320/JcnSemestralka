namespace Library;

public class Osoba
{
    public string _rod_cislo { get; set; }
    public string _meno { get; set; }
    public string _priezvisko { get; set; }
    public string _ulica { get; set; }
    public string _psc { get; set; }
    public string _obec { get; set; }
    private string[] stlpce = { "rod_cislo", "meno", "priszvisko", "ulica", "psc", "obec" };


    public Osoba(string rod_cislo, string meno, string priezvisko, string ulica, string psc, string obec)
    {
        _rod_cislo = rod_cislo;
        _meno = meno;
        _priezvisko = priezvisko;
        _ulica = ulica;
        _psc = psc;
        _obec = obec;
    }

    public Osoba(string rod_cislo, string meno, string priezvisko)
    {
        _rod_cislo = rod_cislo;
        _meno = meno;
        _priezvisko = priezvisko;
        _ulica = "";
        _psc = "";
        _obec = "";
    }
}
