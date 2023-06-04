using Library;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;

namespace JCNSemestralka.View.DeleteView;

/// <summary>
/// Interaction logic for DeleteTab.xaml
/// </summary>
public partial class DeleteTab : UserControl
{
    private Databaza _databaza;

    public DeleteTab()
    {
        InitializeComponent();

        _databaza = new Databaza();

        string[] tabulky = { "os_udaje", "student", "zap_predmety", "predmet", "ucitel", "predmet_bod", "st_program", "st_odbory" };

        foreach (var tabulka in tabulky)
        {
            tablesComboBox.Items.Add(tabulka);
        }
        tablesComboBox.SelectedIndex = 0;
    }

    private void OnClickGenerate(object sender, System.Windows.RoutedEventArgs e)
    {
        QueryOutput.AppendText($"DELETE FROM {tablesComboBox.SelectedItem}\n\t WHERE {whereTextBox.Text};\n");
    }

    private void OnClickClear(object sender, System.Windows.RoutedEventArgs e)
    {
        QueryOutput.Clear();
        whereTextBox.Clear();
    }

    private void OnClickSave(object sender, System.Windows.RoutedEventArgs e)
    {
        SaveFileDialog sfd = new();
        sfd.Filter = "CSV súbory (*.csv) | *.csv";
        sfd.ShowDialog();
        
        if (!sfd.FileName.Equals(""))
        {
            FileInfo file = new FileInfo(sfd.FileName); 
            _databaza.Save(file, (sw) =>
            {
                sw.Write(QueryOutput.Text);
            });
        }
    }
}
