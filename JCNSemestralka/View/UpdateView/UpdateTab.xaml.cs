using Library;
using Microsoft.Win32;
using System.IO;
using System.Windows;
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
    private Databaza _databaza;

    public UpdateTab()
    {
        InitializeComponent();

        _databaza = new Databaza();

        string[] tabulky = { "os_udaje", "student", "zap_predmety", "predmet", "ucitel", "predmet_bod", "st_program", "st_odbory" };

        foreach (var tabulka in tabulky)
        {
            TablesComboBox.Items.Add(tabulka);
        }

        CustomTableTextBox.Visibility = Visibility.Hidden;

        TablesComboBox.SelectedIndex = 0;
    }

    private void OnSelectChanged(object sender, SelectionChangedEventArgs e)
    {
        StackPanel.Children.Clear();
        Add();
    }

    private void OnClicklAdd(object sender, System.Windows.RoutedEventArgs e) => Add();

    private void OnClickClear(object sender, System.Windows.RoutedEventArgs e)
    {
        QueryOutput.Clear();
        StackPanel.Children.Clear();
        Add();
    }

    private void OnClickGenerate(object sender, System.Windows.RoutedEventArgs e)
    {
        QueryOutput.AppendText($"UPDATE {TablesComboBox.SelectedItem} SET ");

        for (int i = 0; i < StackPanel.Children.Count; i++)
        {
            if (i % 3 == 0)
            {
                ComboBox? comboBox = StackPanel.Children[i] as ComboBox;
                QueryOutput.AppendText($"{comboBox!.SelectedItem.ToString()} ");
            }
            else if (i % 3 == 1)
            {
                TextBlock? textBlock = StackPanel.Children[i] as TextBlock;
                QueryOutput.AppendText($"{textBlock!.Text} ");
            }
            else if (i % 3 == 2)
            {
                TextBox? textBox = StackPanel.Children[i] as TextBox;
                QueryOutput.AppendText($"{textBox!.Text}");
            }
            if (i != StackPanel.Children.Count - 1 && i % 3 == 2)
                QueryOutput.AppendText(", ");
        }
        QueryOutput.AppendText($" WHERE {whereTextBox.Text};\n");
    }

    private void OnClickSave(object sender, System.Windows.RoutedEventArgs e)
    {
        var sfd = new SaveFileDialog();
        sfd.Filter = "CSV súbory (*.csv) | *.csv";
        sfd.ShowDialog();

        if (!sfd.FileName.Equals(""))
        {
            FileInfo file = new(sfd.FileName);
            Databaza.Save(file, sw =>
            {
                sw.Write(QueryOutput.Text);
            });
        }
    }

    private void Add()
    {
        var comboBox = new ComboBox();
        comboBox.Margin = new System.Windows.Thickness(5);
        comboBox.Name = "combo";
        foreach (var item in stlpce[TablesComboBox.SelectedIndex])
        {
            comboBox.Items.Add(item);
        }
        StackPanel.Children.Add(comboBox);

        var textBlok = new TextBlock();
        textBlok.Text = "=";
        textBlok.Margin = new System.Windows.Thickness(5);
        StackPanel.Children.Add(textBlok);

        var textBox = new TextBox();
        textBox.Margin = new System.Windows.Thickness(7, 5, 20, 5);
        textBox.Width = 50;
        StackPanel.Children.Add(textBox);
    }
}
