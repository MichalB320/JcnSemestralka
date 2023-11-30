using System;
using System.DirectoryServices;
using System.Windows;
using System.Windows.Controls;

namespace JCNSemestralka.View.ConnectView;

/// <summary>
/// Interaction logic for ConnectTab.xaml
/// </summary>
public partial class ConnectTab : UserControl
{
    public DirectoryEntry _entry { get; set; }

    public ConnectTab()
    {
        InitializeComponent();
        _entry = null!;
    }

    private void onClickConnectButton(object sender, RoutedEventArgs e)
    {
        string ldapPath = pathTextBox.Text;
        string username = userTextBox.Text;
        var password = passwordBox.Password;

        try
        {
            _entry = new DirectoryEntry(ldapPath, username, password);
            object nativeObject = _entry.NativeObject;
            infoLoggedTextBlock.Text = "Logged in";
        }
        catch (Exception ex)
        {
            infoLoggedTextBlock.Text = $"Error: {ex.Message}";
        }
    }
    private void onClickDisconnectButton(object sender, RoutedEventArgs e)
    {
        if (_entry != null)
        {
            _entry.Close();
            _entry.Dispose();
            infoLoggedTextBlock.Text = "Logged out";
        }
    }

    private void onClickSearchButton(object sender, RoutedEventArgs e)
    {
        string[] names = { "skupina", "priezvisko", "meno", "os. číslo", "čip. karta" };
        string[] propertyNames = { "physicalDeliveryOfficeName", "sn", "givenName", "uidNumber", "employeeNumber" };
        string ldapPath = pathTextBox.Text;

        try
        {
            using (DirectorySearcher searcher = new DirectorySearcher(_entry))
            {
                searcher.Filter = conditionTextBox.Text;

                SearchResultCollection results = searcher.FindAll();

                foreach (SearchResult result in results)
                {
                    DirectoryEntry resultEntry = result.GetDirectoryEntry();

                    //output.AppendText("User: " + resultEntry.Properties["samAccountName"].Value);
                    //output.AppendText("heslo: " + resultEntry.InvokeGet());

                    output.AppendText($"{result.Path}\n\t");
                    //int i = 0;
                    foreach (string propertyName in resultEntry.Properties.PropertyNames)
                    {
                        output.AppendText(propertyName + ": " + resultEntry.InvokeGet(propertyName) + "\n\t");
                    }

                    //foreach (var propertyName in propertyNames)
                    //{
                    //    output.AppendText(names[i] + ": " + resultEntry.Properties[propertyName][0] + "\n\t");
                    //    i++;
                    //}


                    //foreach (string objectClass in result.Properties["objectClass"])
                    //{
                    //    output.AppendText($"ObjectClass: {objectClass}");
                    //}

                }
            }
        }
        catch (Exception ex)
        {
            output.AppendText($"chyba: {ex.Message}\n");
        }
    }
}

