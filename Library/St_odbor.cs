namespace Library;

public class St_odbor : Item
{
    public string _st_odbor { get; }
    public string _st_zameranie { get; }
    public string _popis_odboru { get; }
    public string _popis_zamerania { get; }

    public St_odbor(string st_odbor, string st_zameranie, string popis_odboru, string popis_zamerania)
    {
        _st_odbor = st_odbor;
        _st_zameranie = st_zameranie;
        _popis_odboru = popis_odboru;
        _popis_zamerania = popis_zamerania;
    }

    public St_odbor(string st_odbor, string st_zameranie)
    {
        _st_odbor = st_odbor;
        _st_zameranie = st_zameranie;
        _popis_odboru = "";
        _popis_zamerania = "";
    }
}
