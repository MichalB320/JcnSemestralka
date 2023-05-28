using System.Windows.Controls;

namespace JCNSemestralka.View.UpdateView;

/// <summary>
/// Interaction logic for UpdateTab.xaml
/// </summary>
public partial class UpdateTab : UserControl
{
    private readonly string[][] stlpce = { new string[] { "rod_cislo", "meno", "priezvisko", "ulica", "psc", "obec" },
                              new string[] { "os_cislo", "st_odbor", "st_zameranie", "rod_cislo", "rocnik", "st_skupina", "stav", "ukoncenie", "dat_zapisu" },
                              new string[] { "os_cislo", "cis_predm", "skrok", "prednasajuci", "ects", "zapocet", "vysledok", "datum_sk"},
                              new string[] { "cis_predm", "nazov"},
                              new string[] { "os_cislo", "meno", "priezvisko", "katedra"},
                              new string[] { "cis_predm", "skrok", "garant", "ects", "semester", "forma_kont"},
                              new string[] { "st_odbor", "st_zameranie", "cis_predm", "skrok", "typ_povin", "rocnik", "odpor_rocnik"},
                              new string[] { "st_odbor", "st_zameranie", "popis_odboru", "popis_zamerania"} };
    public UpdateTab()
    {
        InitializeComponent();

        string[] tabulky = { "os_udaje", "student", "zap_predmety", "predmet", "ucitel", "predmet_bod", "st_program", "st_odbory" };

        foreach (var tabulka in tabulky)
        {
            TablesComboBox.Items.Add(tabulka);
        }

        TablesComboBox.SelectedIndex = 0;
        //foreach (var item in stlpce[TablesComboBox.SelectedIndex])
        //{
        //    CollComboBox.Items.Add(item);
        //}
    }

    private void OnSelectChanged(object sender, SelectionChangedEventArgs e)
    {
        CollComboBox.Items.Clear();
        foreach (var item in stlpce[TablesComboBox.SelectedIndex])
        {
            CollComboBox.Items.Add(item);
        }
    }
}
