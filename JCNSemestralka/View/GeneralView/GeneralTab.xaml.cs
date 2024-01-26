using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JCNSemestralka.View.GeneralView;

/// <summary>
/// Interaction logic for GeneralTab.xaml
/// </summary>
public partial class GeneralTab : UserControl
{
    private DirectoryEntry _entry;
    private DirectoryEntry _resultEntry;
    private SearchResultCollection _results;

    public GeneralTab()
    {
        InitializeComponent();
        _entry = null!;
        _resultEntry = null!;
        _results = null!;
        boxRadioButton.IsChecked = true;
    }

    private async void OnClickGenerate(object sender, RoutedEventArgs e)
    {
        string inBox = expressionTextBox.Text;
        int count = inBox.Count(c => c == '$');

        if (count % 2 == 0)
        {
            await Task.Run(async () =>
            {
                List<string> vars = new();
                vars = await FindVariables(inBox, count);

                foreach (SearchResult result in _results)
                {
                    DirectoryEntry resultEntry = result.GetDirectoryEntry();

                    int startIndex1 = -1;
                    for (int i = 0; i < count / 2; i++)
                    {
                        int startIndex = startIndex1;
                        int endIndex = inBox.IndexOf('$', startIndex + 1);
                        startIndex1 = inBox.IndexOf('$', endIndex + 1);
                        string subStr = inBox.Substring(startIndex + 1, endIndex - 1 - startIndex);
                        output.Dispatcher.Invoke(() => output.AppendText($"{subStr}{resultEntry.InvokeGet(vars[i])}"));
                    }
                    output.Dispatcher.Invoke(() =>
                    {
                        output.AppendText("\n");
                        output.AppendText("\n");
                    });
                }
            });
        }
    }

    private async void OnClickConnect(object sender, RoutedEventArgs e)
    {
        string ldapPath = pathTextBox.Text;
        string user = userTextBox.Text;
        string password = passwordBox.Password;

        await Task.Run(() =>
        {
            try
            {
                _entry = new DirectoryEntry(ldapPath, user, password);
                object nativeObject = _entry.NativeObject;
                infoLoggedInTextBlock.Dispatcher.Invoke(() => infoLoggedInTextBlock.Text = "Logged in");
            }
            catch (Exception exception)
            {
                infoLoggedInTextBlock.Dispatcher.Invoke(() => infoLoggedInTextBlock.Text = $"Error: {exception.Message}");
            }
        });
    }

    private void OnClickDisconnect(object sender, RoutedEventArgs e)
    {
        if (_entry != null)
        {
            _entry.Close();
            _entry.Dispose();
            infoLoggedInTextBlock.Text = "Logged out";
        }

        Reader reader = new();

    }

    private async void OnClickSearchButton(object sender, RoutedEventArgs e)
    {
        try
        {
            using DirectorySearcher searcher = new(_entry);
            searcher.Filter = ConditionTextBox.Text;
            await Task.Run(() =>
            {
                _results = searcher.FindAll();

                foreach (SearchResult result in _results)
                    _resultEntry = result.GetDirectoryEntry();
            });
        }
        catch (Exception exception)
        {
            output.Text = $"Error: {exception.Message}";
        }
    }

    private void OnClickFindButton(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new();
        ofd.Filter = "CSV súbory (*.csv) | *.csv";
        ofd.ShowDialog();
        //CitacTextBox.Text = ofd.FileName;
    }

    private static async Task<List<string>> FindVariables(string input, int count)
    {
        return await Task.Run(() =>
        {
            List<string> vars = new();
            int startI = input.IndexOf('$');

            for (int i = 0; i < count / 2; i++)
            {
                int startIndex = startI;
                int endIndex = input.IndexOf('$', startIndex + 1);
                startI = input.IndexOf('$', endIndex + 1);
                string subStr = input.Substring(startIndex + 1, endIndex - 1 - startIndex);
                vars.Add(subStr);
            }

            return vars;
        });
    }
}
