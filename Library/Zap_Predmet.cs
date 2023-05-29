namespace Library;

public class Zap_Predmet : Item
{
    public string _os_cislo { get; }
    public string _cis_predm { get; }
    public string _skrok { get; }
    public string _prednasajuci { get; }
    public string _ects { get; }
    public string _zapocet { get; }
    public string _vysledok { get; }
    public string _datum_sk { get; }

    public Zap_Predmet(string os_cislo, string cis_predm, string skrok, string prednasajuci, string ects, string zapocet, string vysledok, string datum_sk)
    {
        _os_cislo = os_cislo;
        _cis_predm = cis_predm;
        _skrok = skrok;
        _prednasajuci = prednasajuci;
        _ects = ects;
        _zapocet = zapocet;
        _vysledok = vysledok;
        _datum_sk = datum_sk;
    }

    public Zap_Predmet(string os_cislo, string cis_predm, string skrok, string prednasajuci, string ects)
    {
        _os_cislo = os_cislo;
        _cis_predm = cis_predm;
        _skrok = skrok;
        _prednasajuci = prednasajuci;
        _ects = ects;
        _zapocet = "";
        _vysledok = "";
        _datum_sk = "";
    }
}
