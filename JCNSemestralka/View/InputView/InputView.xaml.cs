using Library;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace JCNSemestralka.View.InputView;

/// <summary>
/// Interaction logic for InputView.xaml
/// </summary>
public partial class InputView : UserControl
{
    private List<Osoba> _osoby { get; }

    public InputView()
    {
        InitializeComponent();
        _osoby = new();
    }

    private void OnClickOnCitajButton(object sender, RoutedEventArgs e)
    {
        using (var sr = new StreamReader("C:\\Users\\micha\\Desktop\\insertOs_udaje.csv"))
        {
            string? firstLine = sr.ReadLine();
            string? line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line is not null)
                {
                    string[] casti = line.Split(';');
                    string rod_cislo = casti[0];
                    string meno = casti[1];

                    Osoba osoba = new Osoba(casti[0], casti[1], casti[2]);
                    _osoby.Add(osoba);
                }
            }
            sr.Close();
        }
    }

    public List<Osoba> GetList()
    { return _osoby; }
}
