namespace Library;

public class Predmet : Item
{
    public string _cis_predm { get; }
    public string _nazov { get; }

    public Predmet(string cis_predm, string nazov)
    {
        _cis_predm = cis_predm;
        _nazov = nazov;
    }

    public Predmet(string cis_predm)
    {
        _cis_predm = cis_predm;
        _nazov = "";
    }
}
