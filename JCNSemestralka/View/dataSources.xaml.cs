using Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JCNSemestralka.View;

/// <summary>
/// Interaction logic for dataSources.xaml
/// </summary>
public partial class dataSources : UserControl
{
    private Sources zdroje;
    private int cislovac = 0;

    public dataSources()
    {
        InitializeComponent();
        zdroje = new Sources();
        cislovac = 0;
    }

    private void Onclick(object sender, RoutedEventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "CSV súbory (*.csv) | *.csv";
        ofd.ShowDialog();
        //var name = ofd.FileName;

        if (!ofd.FileName.Equals(""))
        {
            var csvFile = new FileInfo(ofd.FileName);
            CSVData csv = new();

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


                        List<string> riadok = new();
                        foreach (var cast in casti)
                        {
                            riadok.Add(cast);
                        }
                        csv.AddRiadok(riadok);

                        zdroje.AddRiadok(riadok);

                        //action(casti);
                        //var osoba = new Osoba(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
                        //_osUdaje.Add(osoba);
                    }
                }
                zdroje.AddCsv(csv);
                sr.Close();

            }
            ButtomSource sc = new();
            sc.SetNazov(ofd.SafeFileName);
            sc.tlacidlo.Click += (sender, e) => ButtonClick(stack.Children.IndexOf(sc));

            //Button btn = new Button();
            //btn.Name = "ahojky";
            //btn.Content = ofd.SafeFileName;
            ////stack.Children.Add(btn);

            stack.Children.Add(sc);

            //btn.Click += (sender, e) => ButtonClick(stack.Children.IndexOf(btn));


            //cislovac++;
        }
    }

    private void ButtonClick(int i)
    {
        // Logika pre kliknutie na tlačidlo
        //MessageBox.Show("Ahoj, svet!");
        
        //string vypis = zdroje.Vypis();
        //ukazka.Text = vypis;

        string vypis2 = zdroje.Vypi(i - 1);
        ukazka.Text = vypis2;
    }

    private void OnLDAPClik(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("heloo");
    }
}
