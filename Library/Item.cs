namespace Library;

public class Item
{
    private List<string> stlpce;

    public Item()
    {
        stlpce = new();
    }

    public void AddStlpec(string stlpec)
    {
        stlpce.Add(stlpec);
    }

    public string GetStlpec(int i)
    {
        return stlpce.ElementAt(i);
    }

    public int Kapacita()
    {
        return stlpce.Count;
    }
}
