using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library;

public class Sources
{
    private List<List<string>> riadkyCSV;
    private List<CSVData> csvData;

    public Sources() 
    {
        riadkyCSV = new List<List<string>>();
        csvData = new List<CSVData>();
    }

    public void AddCsv(CSVData data)
    {
        csvData.Add(data);
    }

    public void AddRiadok(List<string> riadok)
    {
        riadkyCSV.Add(riadok);
    }

    public List<List<string>> GetRiadky()
    {
        return riadkyCSV;
    }

    public string Vypi(int i)
    {
        CSVData csv = csvData[i];

        StringBuilder sb = new();
        foreach (var riadok in csv.GetRiadky())
        {
            foreach (var stlpec in riadok)
            {
                sb.Append(stlpec + ";");
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }

    public string Vypis()
    {
        StringBuilder sb = new();
        foreach (var riadok in riadkyCSV)
        {
            foreach (var stlpec in riadok)
            {
                sb.Append(stlpec);
            }
        }
        return sb.ToString();
    }

    public void Load(FileInfo csvFile, Action<string[]> action)
    {
        using (var sr = new StreamReader(csvFile.FullName))
        {
            string? line;
            //line = sr.ReadLine();//Preskoc hlavicku
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line is not null)
                {
                    string[] casti = line.Split(';');

                    action(casti);
                    //var osoba = new Osoba(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
                    //_osUdaje.Add(osoba);
                }
            }
            sr.Close();
        }
    }
}
