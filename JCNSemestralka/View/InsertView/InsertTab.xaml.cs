using Library;
using Microsoft.Win32;
using System.IO;
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
        }
        _databaza = new Databaza();
        TablesComboBox.SelectedItem = TablesComboBox.Items.GetItemAt(0);
    }

    private void OnClickFindButton(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "CSV súbory (*.csv) | *.csv";
        ofd.ShowDialog();
        CitacTextBox.Text = ofd.FileName;
    }

    private void OnClickReadButton(object sender, RoutedEventArgs e)
    {
        FileInfo subor = new FileInfo(CitacTextBox.Text);

        _databaza.LoadOsUdaje(subor, (casti) =>
        {
            if (TablesComboBox.SelectedItem.ToString() == "os_udaje")
            {
                var osoba = new Osoba(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
                _databaza.Add<Osoba>(_databaza.GetOsoby(), osoba);
            }
            else if (TablesComboBox.SelectedItem.ToString() == "student")
            {
                var student = new Student(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
                _databaza.Add<Student>(_databaza.GetStudenti(), student);
            }
            else if (TablesComboBox.SelectedItem.ToString() == "zap_predmety")
            {
                var zap_predmet = new Zap_Predmet(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5], casti[6], casti[7]);
                _databaza.Add<Zap_Predmet>(_databaza.GetZapPredmety(), zap_predmet);
            }
            else if (TablesComboBox.SelectedItem.ToString() == "predmet")
            {
                var predmet = new Predmet(casti[0], casti[1]);
                _databaza.Add<Predmet>(_databaza.GetPredmety(), predmet);
            }
            else if (TablesComboBox.SelectedItem.ToString() == "ucitel")
            {
                var ucitel = new Ucitel(casti[0], casti[1], casti[2], casti[3]);
                _databaza.Add<Ucitel>(_databaza.GetUcitelia(), ucitel);
            }
            else if (TablesComboBox.SelectedItem.ToString() == "predmet_bod")
            {
                var predmetBod = new Predmet_bod(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
                _databaza.Add<Predmet_bod>(_databaza.GetPredmet_Body(), predmetBod);
            }
            else if (TablesComboBox.SelectedItem.ToString() == "st_program")
            {
                var stProgram = new st_program(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5], casti[6]);
                _databaza.Add<st_program>(_databaza.GetStProgramy(), stProgram);
            }
            else if (TablesComboBox.SelectedItem.ToString() == "st_odbory")
            {
                var stOdbor = new St_odbor(casti[0], casti[1], casti[2], casti[3]);
                _databaza.Add<St_odbor>(_databaza.GetStOdbory(), stOdbor);
            }
        });

        ValuesTextBox.Text = "default";
    }

    private void OnClickGenerate(object sender, RoutedEventArgs e)
    {
        string? tabulka = TablesComboBox.SelectedItem.ToString();

        if (ValuesTextBox.Text.Equals("default"))
        {
            foreach (var osoba in _databaza.GetOsoby())
            {
                QueryOutput.AppendText($"INSERT INTO {tabulka}\n\tVALUES {osoba._rod_cislo}, {osoba._meno}, {osoba._priezvisko}, {osoba._ulica}, {osoba._psc}, {osoba._obec};\n\n");
            }
            Napis(tabulka!);
        }
        else
        {
            QueryOutput.AppendText($"INSERT INTO {tabulka}\n\tVALUES {ValuesTextBox.Text};");
        }
    }

    private void Napis(string tab)
    {
        if (tab.Equals("os_udaje"))
        {
            foreach (var osoba in _databaza.GetOsoby())
                QueryOutput.AppendText($"INSERT INTO {tab}\n\tVALUES {osoba._rod_cislo}, {osoba._meno}, {osoba._priezvisko}, {osoba._ulica}, {osoba._psc}, {osoba._obec};\n\n");
        }
        else if (tab.Equals("student"))
        {
            foreach (var student in _databaza.GetStudenti())
                QueryOutput.AppendText($"INSERT INTO {tab}\n\tVALUES {student._os_cislo}, {student._st_odbor}, {student._st_zameranie}, {student._st_zameranie}, {student._rod_cislo}, {student._rocnik}, {student._st_skupina}, {student._stav}, {student._ukoncenie}, {student._dat_zapisu};\n\n");
        }
    }

    private void OnClickClear(object sender, RoutedEventArgs e)
    {
        QueryOutput.Clear();
    }
}
