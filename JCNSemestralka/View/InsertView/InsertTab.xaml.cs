using Library;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace JCNSemestralka.View.InsertView;

/// <summary>
/// Interaction logic for InsertTab.xaml
/// </summary>
public partial class InsertTab : UserControl
{
    private readonly string[] tabulky = { "os_udaje", "student", "zap_predmety", "predmet", "ucitel", "predmet_bod", "st_program", "st_odbory" };
    private Databaza _databaza;

    public InsertTab()
    {
        InitializeComponent();
        foreach (var collumn in tabulky)
        {
            TablesComboBox.Items.Add(collumn);
            CustomComboBox.Items.Add(collumn);
        }
        _databaza = new Databaza();
        TablesComboBox.SelectedItem = TablesComboBox.Items.GetItemAt(0);
        CustomComboBox.SelectedItem = CustomComboBox.Items.GetItemAt(0);
        allRadio.IsChecked = true;
        readButton.IsEnabled = false;
        deleteButton.IsEnabled = false;
        Custom.Visibility = Visibility.Hidden;
        CustomTableNAme.Visibility = Visibility.Hidden;
    }

    private void OnClickFindButton(object sender, RoutedEventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "CSV súbory (*.csv) | *.csv";
        ofd.ShowDialog();
        CitacTextBox.Text = ofd.FileName;
    }

    private void OnTextChangedCitacTextBox(object sender, TextChangedEventArgs e)
    {
        if (CitacTextBox.Text.Length == 0)
        {

        }
        else
        {
            var subor = new FileInfo(CitacTextBox.Text);
            if (subor.Exists)
                readButton.IsEnabled = true;
            else
                readButton.IsEnabled = false;
        }
    }

    private void OnClickReadButton(object sender, RoutedEventArgs e)
    {
        _databaza.GetUniversal().Clear();

        var subor = new FileInfo(CitacTextBox.Text);

        _databaza.Load(subor, (casti) =>
        {
            var item = new Item();
            foreach (var cast in casti)
                item.AddStlpec(cast);

            _databaza.Add<Item>(_databaza.GetUniversal(), item);
        });

        var list = _databaza.GetUniversal();
        for (int i = 0; i < list.ElementAt(0).Kapacita(); i++)
        {
            ValuesTextBox.AppendText($"{list.ElementAt(0).GetStlpec(i)}, ");
            CustomColls.AppendText($"{list.ElementAt(0).GetStlpec(i)}, ");
        }
        deleteButton.IsEnabled = true;
    }

    private void OnClickGenerate(object sender, RoutedEventArgs e)
    {
        string? tabulka;
        string[] stlpce = new string[15];
        if (TablesComboBox.IsVisible)
            tabulka = TablesComboBox.SelectedItem.ToString();
        else if (CustomTableNAme.IsVisible)
            tabulka = CustomTableNAme.Text;
        else if (CustomComboBox.IsVisible)
        {
            tabulka = CustomComboBox.SelectedItem.ToString();
            stlpce = CustomColls.Text.Split(", ");
        }
        else if (CustomTextBox.IsVisible)
        {
            tabulka = CustomTextBox.Text;
            stlpce = CustomColls.Text.Split(", ");
        }
        else
        {
            tabulka = "";
            stlpce = new string[16];
            for (int i = 0; i < 16; i++)
                stlpce[i] = "";
        }

        var sb = new StringBuilder();
        string[] casti = ValuesTextBox.Text.Split(",");
        List<int> col = new List<int>();
        int cisloStlpcu = 0;
        foreach (var cast in casti)
        {
            if (cast.Contains('\''))
                col.Add(cisloStlpcu);
            cisloStlpcu++;
        }

        bool insert = true;
        if (generateCheck.IsChecked == true) //ValuesTextBox.Text.Equals("default")
        {
            int index = 0;
            var list = _databaza.GetUniversal();
            foreach (var osoba in list)
            {
                if (insert)
                {
                    if (CustomColls.IsVisible)
                        QueryOutput.AppendText($"INSERT INTO {tabulka} ({CustomColls.Text})\n\tVALUES (");
                    else
                        QueryOutput.AppendText($"INSERT INTO {tabulka}\n\tVALUES (");
                    insert = false;
                }
                int max = list.ElementAt(index).Kapacita();
                for (int i = 0; i < max; i++)
                {
                    if (CustomColls.IsVisible)
                    {
                        for (int j = 0; j < stlpce.Length; j++)
                        {
                            if (list.ElementAt(0).GetStlpec(i).Contains(stlpce[j]))
                            {
                                if (j == stlpce.Length - 1)
                                    sb.Append($"{osoba.GetStlpec(i)}"); //QueryOutput.AppendText($"{osoba.GetStlpec(i)}");
                                else
                                    sb.Append($"{osoba.GetStlpec(i)}, "); //QueryOutput.AppendText($"{osoba.GetStlpec(i)}, ");
                            }
                        }
                    }
                    else if (CustomColls.IsVisible)
                    {

                    }
                    else
                    {
                        //QueryOutput.AppendText($"{osoba.GetStlpec(i)}, ");
                        sb.Append($"{osoba.GetStlpec(i)}, ");
                    }
                }
                //QueryOutput.AppendText($"INSERT INTO {tabulka}\n\tVALUES {osoba.GetStlpec(0)}, {osoba._meno}, {osoba._priezvisko}, {osoba._ulica}, {osoba._psc}, {osoba._obec};\n\n");

                string[] strings = sb.ToString().Split(",");
                for (int b = 0; b < col.Count; b++)
                {
                    strings[col.ElementAt(b)] = strings[col.ElementAt(b)].Trim();
                }
                for (int b = 0; b < col.Count; b++)
                {
                    strings[col.ElementAt(b)] = $"'{strings[col.ElementAt(b)]}'";
                }
                sb.Clear();
                for (int b = 0; b < strings.Length; b++)
                {
                    if (b == strings.Length - 1)
                        sb.Append($"{strings[b]}");
                    else
                        sb.Append($"{strings[b]}, ");
                }
                if (index % int.Parse(multiple.Text) == 0)
                {
                    QueryOutput.AppendText($"{sb.ToString()});\n");
                    sb.Clear();
                    insert = true;
                }
                else
                {
                    QueryOutput.AppendText($"{sb.ToString()}), (");
                    sb.Clear();
                }
                index++;
                cisloStlpcu = 0;
            }
        }
        else
            QueryOutput.AppendText($"INSERT INTO {tabulka}\n\tVALUES {ValuesTextBox.Text};");
    }

    private void OnClickClear(object sender, RoutedEventArgs e)
    {
        QueryOutput.Clear();
        CustomColls.Clear();
        CustomTextBox.Clear();
        ValuesTextBox.Clear();
    }

    private void OnClickSave(object sender, RoutedEventArgs e)
    {
        var sfd = new SaveFileDialog();
        sfd.Filter = "CSV súbory (*.csv) | *.csv";
        sfd.ShowDialog();
        if (!sfd.FileName.Equals(""))
        {
            var file = new FileInfo(sfd.FileName);
            Databaza.Save(file, (sw) =>
            {
                sw.Write(QueryOutput.Text);
            });
        }
    }

    private void OnClickCustomRadio(object sender, RoutedEventArgs e)
    {
        if (customCheck.IsChecked!.Value)
        {
            TablesComboBox.Visibility = Visibility.Hidden;
            CustomTableNAme.Visibility = Visibility.Hidden;
            Custom.Visibility = Visibility.Visible;
            CustomComboBox.Visibility = Visibility.Hidden;
            CustomTextBox.Visibility = Visibility.Visible;
        }
        else
        {
            TablesComboBox.Visibility = Visibility.Hidden;
            CustomTableNAme.Visibility = Visibility.Hidden;
            Custom.Visibility = Visibility.Visible;
            CustomComboBox.Visibility = Visibility.Visible;
            CustomTextBox.Visibility = Visibility.Hidden;
        }
    }

    private void OnClickAllRadio(object sender, RoutedEventArgs e)
    {
        if (customCheck.IsChecked!.Value)
        {
            Custom.Visibility = Visibility.Hidden;
            TablesComboBox.Visibility = Visibility.Hidden;
            CustomTableNAme.Visibility = Visibility.Visible;
        }
        else
        {
            Custom.Visibility = Visibility.Hidden;
            TablesComboBox.Visibility = Visibility.Visible;
            CustomTableNAme.Visibility = Visibility.Hidden;
        }
    }

    private void OnClickCustomCheck(object sender, RoutedEventArgs e)
    {
        if (customCheck.IsChecked!.Value && allRadio.IsChecked!.Value)
        {
            TablesComboBox.Visibility = Visibility.Hidden;
            CustomTableNAme.Visibility = Visibility.Visible;
        }
        else if (customCheck.IsChecked!.Value && customRadio.IsChecked!.Value)
        {
            CustomComboBox.Visibility = Visibility.Hidden;
            CustomTextBox.Visibility = Visibility.Visible;
        }
        else if (!customCheck.IsChecked!.Value && customRadio.IsChecked!.Value)
        {
            CustomComboBox.Visibility = Visibility.Visible;
            CustomTextBox.Visibility = Visibility.Hidden;
        }
        else if (!customCheck.IsChecked!.Value && allRadio.IsChecked!.Value)
        {
            TablesComboBox.Visibility = Visibility.Visible;
            CustomTableNAme.Visibility = Visibility.Hidden;
        }
    }

    private void OnTextChangedCustomColls(object sender, TextChangedEventArgs e) => ValuesTextBox.Text = CustomColls.Text;

    private void OnClickDeleteButton(object sender, RoutedEventArgs e)
    {
        _databaza.GetUniversal().Clear();
        CitacTextBox.Clear();
        readButton.IsEnabled = false;
        deleteButton.IsEnabled = false;
    }
}
