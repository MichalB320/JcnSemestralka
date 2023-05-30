using Library;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JCNSemestralka.View.InsertView;

/// <summary>
/// Interaction logic for InsertTab.xaml
/// </summary>
public partial class InsertTab : UserControl
{
    private readonly string[] tabulky = { "ukazka", "os_udaje", "student", "zap_predmety", "predmet", "ucitel", "predmet_bod", "st_program", "st_odbory" };
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
        Custom.Visibility = Visibility.Hidden;
        CustomTableNAme.Visibility = Visibility.Hidden;
    }

    private void OnClickFindButton(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "CSV súbory (*.csv) | *.csv";
        ofd.ShowDialog();
        CitacTextBox.Text = ofd.FileName;
        //FileInfo subor = new FileInfo(CitacTextBox.Text);
    }

    private void OnTextChangedCitacTextBox(object sender, TextChangedEventArgs e)
    {
        if (CitacTextBox.Text.Length == 0)
        {

        }
        else
        {
            FileInfo subor = new FileInfo(CitacTextBox.Text);
            if
                (subor.Exists)
            {
                QueryOutput.AppendText("EXISTUJE");
                readButton.IsEnabled = true;
            }
            else
            {
                QueryOutput.AppendText("NON");
                readButton.IsEnabled = false;
            }
        }
    }

    private void OnClickReadButton(object sender, RoutedEventArgs e)
    {
        _databaza.GetUniversal().Clear();

        FileInfo subor = new FileInfo(CitacTextBox.Text);

        _databaza.LoadOsUdaje(subor, (casti) =>
        {

            //var ukazka = new Ukazka(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5], casti[6], casti[7], casti[8], casti[9], casti[10], casti[11], casti[12], casti[13], casti[14], casti[15]);
            var item = new Item();
            foreach (var cast in casti)
                item.AddStlpec(cast);

            //_databaza.Add<Ukazka>(_databaza.GetUkazky(), ukazka);
            _databaza.Add<Item>(_databaza.GetUniversal(), item);

            //if (TablesComboBox.SelectedItem.ToString() == "os_udaje")
            //{
            //    var osoba = new Osoba(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
            //    _databaza.Add<Osoba>(_databaza.GetOsoby(), osoba);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "ukazka")
            //{
            //    var ukazka = new Ukazka(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5], casti[6], casti[7], casti[8], casti[9], casti[10], casti[11], casti[12], casti[13], casti[14], casti[15]);
            //    //_databaza.Add<Ukazka>(_databaza.GetUkazky(), ukazka);
            //    _databaza.Add<Item>(_databaza.GetUniversal(), ukazka);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "student")
            //{
            //    var student = new Student(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
            //    _databaza.Add<Student>(_databaza.GetStudenti(), student);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "zap_predmety")
            //{
            //    var zap_predmet = new Zap_Predmet(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5], casti[6], casti[7]);
            //    _databaza.Add<Zap_Predmet>(_databaza.GetZapPredmety(), zap_predmet);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "predmet")
            //{
            //    var predmet = new Predmet(casti[0], casti[1]);
            //    _databaza.Add<Predmet>(_databaza.GetPredmety(), predmet);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "ucitel")
            //{
            //    var ucitel = new Ucitel(casti[0], casti[1], casti[2], casti[3]);
            //    _databaza.Add<Ucitel>(_databaza.GetUcitelia(), ucitel);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "predmet_bod")
            //{
            //    var predmetBod = new Predmet_bod(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5]);
            //    _databaza.Add<Predmet_bod>(_databaza.GetPredmet_Body(), predmetBod);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "st_program")
            //{
            //    var stProgram = new st_program(casti[0], casti[1], casti[2], casti[3], casti[4], casti[5], casti[6]);
            //    _databaza.Add<st_program>(_databaza.GetStProgramy(), stProgram);
            //}
            //else if (TablesComboBox.SelectedItem.ToString() == "st_odbory")
            //{
            //    var stOdbor = new St_odbor(casti[0], casti[1], casti[2], casti[3]);
            //    _databaza.Add<St_odbor>(_databaza.GetStOdbory(), stOdbor);
            //}
        });

        ValuesTextBox.Text = "default";
    }

    private void OnClickGenerate(object sender, RoutedEventArgs e)
    {
        string? tabulka;
        string[] stlpce = new string[15];
        if (TablesComboBox.IsVisible)
        {
            tabulka = TablesComboBox.SelectedItem.ToString();
        }
        else if (CustomTableNAme.IsVisible)
        {
            tabulka = CustomTableNAme.Text;
        }
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

        

        if (ValuesTextBox.Text.Equals("default"))
        {
            int index = 0;
            var list = _databaza.GetUniversal();
            foreach (var osoba in list)
            {
                if (CustomColls.IsVisible)
                    QueryOutput.AppendText($"INSERT INTO {tabulka} ({CustomColls.Text})\n\tVALUES (");
                else
                    QueryOutput.AppendText($"INSERT INTO {tabulka}\n\tVALUES (");
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
                                    QueryOutput.AppendText($"{osoba.GetStlpec(i)}");
                                else
                                    QueryOutput.AppendText($"{osoba.GetStlpec(i)}, ");
                            }
                        }
                    }
                    else if (CustomColls.IsVisible)
                    {

                    }
                    else
                    {
                        QueryOutput.AppendText($"{osoba.GetStlpec(i)}, ");
                    }
                    //if (list.ElementAt(0).GetStlpec(i).Contains(stlpce[5]))
                    //{
                    //    QueryOutput.AppendText($"{osoba.GetStlpec(i)}, ");
                    //}
                    //QueryOutput.AppendText($"{osoba.GetStlpec(i)}, ");
                }
                //QueryOutput.AppendText($"INSERT INTO {tabulka}\n\tVALUES {osoba.GetStlpec(0)}, {osoba._meno}, {osoba._priezvisko}, {osoba._ulica}, {osoba._psc}, {osoba._obec};\n\n");
                QueryOutput.AppendText(");\n");
                index++;
            }
            //Napis(tabulka!);
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
        else if (tab.Equals("ukazka"))
        {
            foreach (var ukazka in _databaza.GetUkazky())
                QueryOutput.AppendText($"INSERT INTO {tab}\n\tVALUES {ukazka._skupina}, {ukazka._priezvisko}, {ukazka._meno}, {ukazka._os_cislo}, {ukazka._cip_karta}, {ukazka._bodyZaSemester}, {ukazka._datumZaverHodnotenia}, {ukazka._znamkaZaverHodnotenia}, {ukazka._datum1T}, {ukazka._znamka1T}, {ukazka._datum2T}, {ukazka._znamka2T}, {ukazka._datum3T}, {ukazka._znamka3T}, {ukazka._body}, {ukazka._opakuje};\n\n");
        }
    }

    private void OnClickClear(object sender, RoutedEventArgs e) => QueryOutput.Clear();

    private void OnClickSave(object sender, RoutedEventArgs e)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = "CSV súbory (*.csv) | *.csv";
        sfd.ShowDialog();
        FileInfo subor = new FileInfo(sfd.FileName);

        string? tabulka;
        string[] stlpce = new string[15];
        if (TablesComboBox.IsVisible)
        {
            tabulka = TablesComboBox.SelectedItem.ToString();
        }
        else if (CustomTableNAme.IsVisible)
        {
            tabulka = CustomTableNAme.Text;
        }
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

        using (var sw = new StreamWriter(subor.FullName))
        {
            int index = 0;
            var list = _databaza.GetUniversal();
            foreach (var osoba in list)
            {
                if (CustomColls.IsVisible)
                    sw.Write($"INSERT INTO {tabulka} ({CustomColls.Text})\n\tVALUES (");
                else
                    sw.Write($"INSERT INTO {tabulka}\n\tVALUES (");
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
                                    sw.Write($"{osoba.GetStlpec(i)}");
                                else
                                    sw.Write($"{osoba.GetStlpec(i)}, ");
                            }
                        }
                    }
                    else if (CustomColls.IsVisible)
                    {

                    }
                    else
                    {
                        sw.Write($"{osoba.GetStlpec(i)}, ");
                    }
                }
                
                sw.Write(");\n");
                index++;
            }

            //foreach (var ukazka in _databaza.GetUkazky())
            //{
            //    sw.WriteLine($"INSERT INTO ukazka VALUES {ukazka._skupina}, {ukazka._priezvisko}, {ukazka._meno}, {ukazka._os_cislo}, {ukazka._cip_karta}, {ukazka._bodyZaSemester}, {ukazka._datumZaverHodnotenia}, {ukazka._znamkaZaverHodnotenia}, {ukazka._datum1T}, {ukazka._znamka1T}, {ukazka._datum2T}, {ukazka._znamka2T}, {ukazka._datum3T}, {ukazka._znamka3T}, {ukazka._body}, {ukazka._opakuje};\n");
            //}

            sw.WriteLine("fs");
            sw.Close();
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

   
}
