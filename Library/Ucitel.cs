namespace Library;

public class Ucitel
{
    public string _os_cislo { get; }
    public string _meno { get; }
    public string _priezvisko { get; }
    public string _katedra { get; }

    public Ucitel(string os_cislo, string meno, string priezvisko, string katedra)
    {
        _os_cislo = os_cislo;
        _meno = meno;
        _priezvisko = priezvisko;
        _katedra = katedra;
    }

    public Ucitel(string os_cislo, string meno, string priezvisko)
    {
        _os_cislo = os_cislo;
        _meno = meno;
        _priezvisko = priezvisko;
        _katedra = "";
    }
}
