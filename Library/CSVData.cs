using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library;

public class CSVData
{
    private List<List<string>> riadkyCSV;

    public CSVData()
    {
        riadkyCSV = new List<List<string>>();
    }

    public void AddRiadok(List<string> riadok)
    {
        riadkyCSV.Add(riadok);
    }

    public List<List<string>> GetRiadky()
    {
        return riadkyCSV;
    }
}

