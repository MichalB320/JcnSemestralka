namespace Library;

public class st_program
{
    public string _st_odbor { get; }
    public string _st_zameranie { get; }
    public string _cis_predm { get; }
    public string _skrok { get; }
    public string _typ_povin { get; }
    public string _rocnik { get; }
    public string _odpor_rocnik { get; }

    public st_program(string st_odbor, string st_zameranie, string cis_predm, string skrok, string typ_povin, string rocnik, string odpor_rocnik)
    {
        _st_odbor = st_odbor;
        _st_zameranie = st_zameranie;
        _cis_predm = cis_predm;
        _skrok = skrok;
        _typ_povin = typ_povin;
        _rocnik = rocnik;
        _odpor_rocnik = odpor_rocnik;
    }

    public st_program(string st_odbor, string st_zameranie, string cis_predm, string skrok, string typ_povin, string rocnik)
    {
        _st_odbor = st_odbor;
        _st_zameranie = st_zameranie;
        _cis_predm = cis_predm;
        _skrok = skrok;
        _typ_povin = typ_povin;
        _rocnik = rocnik;
        _odpor_rocnik = "";
    }
}
