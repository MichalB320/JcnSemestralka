namespace Library;

public class Predmet_bod : Item
{
    public string _cis_predm { get; }
    public string _skrok { get; }
    public string _garant { get; }
    public string _ects { get; }
    public string _semester { get; }
    public string _forma_kont { get; }

    public Predmet_bod(string cis_predm, string skrok, string garant, string ects, string semester, string forma_kont)
    {
        _cis_predm = cis_predm;
        _skrok = skrok;
        _garant = garant;
        _ects = ects;
        _semester = semester;
        _forma_kont = forma_kont;
    }
}
