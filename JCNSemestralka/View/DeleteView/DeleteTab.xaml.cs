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
        customTableNameTextBox.Visibility = System.Windows.Visibility.Hidden;
    }

    private void OnClickGenerate(object sender, System.Windows.RoutedEventArgs e)
    {
        string? tableName;
        if (customCheck.IsChecked!.Value)
            tableName = customTableNameTextBox.Text;
        else
            tableName = tablesComboBox.SelectedItem.ToString();

        QueryOutput.AppendText($"DELETE FROM {tableName}\n\t WHERE {whereTextBox.Text};\n");
    }

    private void OnClickClear(object sender, System.Windows.RoutedEventArgs e)
    {
        QueryOutput.Clear();
        whereTextBox.Clear();
        customTableNameTextBox.Clear();
    }

    private void OnClickSave(object sender, System.Windows.RoutedEventArgs e)
    {
        SaveFileDialog sfd = new();
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

    private void OnClickCustomTable(object sender, System.Windows.RoutedEventArgs e)
    {
        if (tablesComboBox.IsVisible)
        {
            tablesComboBox.Visibility = System.Windows.Visibility.Hidden;
            customTableNameTextBox.Visibility = System.Windows.Visibility.Visible;
        }
        else
        {
            tablesComboBox.Visibility = System.Windows.Visibility.Visible;
            customTableNameTextBox.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
