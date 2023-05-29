namespace Library;

public class Ukazka : Item
{
    public string _skupina { get; }
    public string _priezvisko { get; }
    public string _meno { get; }
    public string _os_cislo { get; }
    public string _cip_karta { get; }
    public string _bodyZaSemester { get; }
    public string _datumZaverHodnotenia { get; }
    public string _znamkaZaverHodnotenia { get; }
    public string _datum1T { get; }
    public string _znamka1T { get; }
    public string _datum2T { get; }
    public string _znamka2T { get; }
    public string _datum3T { get; }
    public string _znamka3T { get; }
    public string _body { get; }
    public string _opakuje { get; }

    public Ukazka(string skupina, string priezvisko, string meno, string os_cislo, string cip_karta, string bodyZaSemester, string datumZaverHodnotenia, string znamkaZaverHodnotenia, string datum1T, string znamka1T, string datum2T, string znamka2T, string datum3T, string znamka3T, string body, string opakuje)
    {
        _skupina = skupina;
        _priezvisko = priezvisko;
        _meno = meno;
        _os_cislo = os_cislo;
        _cip_karta = cip_karta;
        _bodyZaSemester = bodyZaSemester;
        _datumZaverHodnotenia = datumZaverHodnotenia;
        _znamkaZaverHodnotenia = znamkaZaverHodnotenia;
        _datum1T = datum1T;
        _znamka1T = znamka1T;
        _datum2T = datum2T;
        _znamka2T = znamka2T;
        _datum3T = datum3T;
        _znamka3T = znamka3T;
        _body = body;
        _opakuje = opakuje;
    }
}
