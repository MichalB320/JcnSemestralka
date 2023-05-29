namespace Library;

public class Student : Item
{
    public string _os_cislo { get; }
    public string _st_odbor { get; }
    public string _st_zameranie { get; }
    public string _rod_cislo { get; }
    public string _rocnik { get; }
    public string _st_skupina { get; }
    public string _stav { get; }
    public DateTime _ukoncenie { get; }
    public DateTime _dat_zapisu { get; }

    public Student(string os_cislo, string st_odbor, string st_zameranie, string rod_cislo, string rocnik, string st_skupina, string stav, DateTime ukoncenie, DateTime dat_zapisu)
    {
        _os_cislo = os_cislo;
        _st_odbor = st_odbor;
        _st_zameranie = st_zameranie;
        _rod_cislo = rod_cislo;
        _rocnik = rocnik;
        _st_skupina = st_skupina;
        _stav = stav;
        _ukoncenie = ukoncenie;
        _dat_zapisu = dat_zapisu;
    }

    public Student(string os_cislo, string st_odbor, string st_zameranie, string rod_cislo, string rocnik, string st_skupina)
    {
        _os_cislo = os_cislo;
        _st_odbor = st_odbor;
        _st_zameranie = st_zameranie;
        _rod_cislo = rod_cislo;
        _rocnik = rocnik;
        _st_skupina = st_skupina;
        _stav = "";
        _ukoncenie = DateTime.MinValue;
        _dat_zapisu = DateTime.MinValue;
    }
}
