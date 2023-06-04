using Library;
using Microsoft.Win32;
using System.ComponentModel.Design;
using System.IO;
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
    //private int start;
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


        TablesComboBox.SelectedIndex = 0;
        //foreach (var item in stlpce[TablesComboBox.SelectedIndex])
        //{
        //    CollComboBox.Items.Add(item);
        //}
        //start = 3;
        //StackPanel.Children[5].Visibility = System.Windows.Visibility.Hidden;
        //for (int i = 3; i < 33; i++) ------------------------------------------odkomentuj potom-----------
        //
        //{
        //    StackPanel.Children[i].Visibility = System.Windows.Visibility.Hidden;
        //}
    }

    private void OnSelectChanged(object sender, SelectionChangedEventArgs e)
    {
        //CollComboBox.Items.Clear();
        //foreach (var item in stlpce[TablesComboBox.SelectedIndex])
        //{
        //    CollComboBox.Items.Add(item);
        //    CollComboBox1.Items.Add(item);
        //    CollComboBox2.Items.Add(item);
        //    CollComboBox3.Items.Add(item);
        //    CollComboBox4.Items.Add(item);
        //    CollComboBox5.Items.Add(item);
        //    CollComboBox6.Items.Add(item);
        //    CollComboBox7.Items.Add(item);
        //    CollComboBox8.Items.Add(item);
        //    CollComboBox9.Items.Add(item);
        //    CollComboBox10.Items.Add(item);
        //}

        StackPanel.Children.Clear();
        Add();
    }

    private void OnClicklAdd(object sender, System.Windows.RoutedEventArgs e)
    {
        Add();
        //for (int i = start; i < start + 3; i++)
        //{
        //    StackPanel.Children[i].Visibility = System.Windows.Visibility.Visible;
        //}
        //start = start + 3;
    }

    private void OnClickClear(object sender, System.Windows.RoutedEventArgs e)
    {
        QueryOutput.Clear();
        StackPanel.Children.Clear();
        Add();
    }

    private void OnClickGenerate(object sender, System.Windows.RoutedEventArgs e)
    {
        //QueryOutput.AppendText($"UPDATE {TablesComboBox.SelectedItem} SET {CollComboBox.SelectedItem} = {ValuesTextBox.Text} WHERE {whereTextBox.Text};\n");
        //foreach (var child in StackPanel.Children)
        //{

        QueryOutput.AppendText($"UPDATE {TablesComboBox.SelectedItem} SET ");

        //}
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
                TextBox? textBox = StackPanel.Children[2] as TextBox;
                QueryOutput.AppendText($"{textBox!.Text} ");
            }
        }
        //ComboBox? comboBox = StackPanel.Children[0] as ComboBox;
        //QueryOutput.AppendText(comboBox!.SelectedItem.ToString());

        QueryOutput.AppendText($" WHERE {whereTextBox.Text};\n");
    }

    private void OnClickSave(object sender, System.Windows.RoutedEventArgs e)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = "CSV súbory (*.csv) | *.csv";
        sfd.ShowDialog();

        if (!sfd.FileName.Equals(""))
        {
            FileInfo file = new FileInfo(sfd.FileName);
            _databaza.Save(file, sw =>
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
