using System;
using System.DirectoryServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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

    private void OnClickConnectButton(object sender, RoutedEventArgs e)
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
    private void OnClickDisconnectButton(object sender, RoutedEventArgs e)
    {
        if (_entry != null)
        {
            _entry.Close();
            _entry.Dispose();
            infoLoggedTextBlock.Text = "Logged out";
        }
    }

    private void OnClickSearchButton(object sender, RoutedEventArgs e)
    {
        string[] names = { "skupina", "priezvisko", "meno", "os. číslo", "čip. karta" };
        string[] propertyNames = { "physicalDeliveryOfficeName", "sn", "givenName", "uidNumber", "employeeNumber" };
        string ldapPath = pathTextBox.Text;

        var vlakno = new Thread(() =>
        {
            try
            {
                using DirectorySearcher searcher = new DirectorySearcher(_entry);
                string filter = "";
                conditionTextBox.Dispatcher.Invoke(() => filter = conditionTextBox.Text);
                searcher.Filter = filter;
                SearchResultCollection results = searcher.FindAll();
                foreach (SearchResult result in results)
                {
                    DirectoryEntry resultEntry = result.GetDirectoryEntry();
                    UpdateTextBox($"{result.Path}\n\t");
                    foreach (string propertyName in resultEntry.Properties.PropertyNames)
                        UpdateTextBox(propertyName + ": " + resultEntry.InvokeGet(propertyName) + "\n\t");

                    //int i = 0;
                    //foreach (var propertyName in propertyNames)
                    //{
                    //    output.AppendText(names[i] + ": " + resultEntry.Properties[propertyName][0] + "\n\t");
                    //    i++;
                    //}
                }
            }
            catch (Exception ex)
            {
                UpdateTextBox($"Error: {ex.Message}\n");
            }
        });
        vlakno.Start();
    }

    private void UpdateTextBox(string txt)
    {
        if (this.CheckAccess())
            output.AppendText(txt);
        else
            this.Dispatcher.Invoke(DispatcherPriority.Background, new Action<string>(UpdateTextBox), txt);
    }
}

