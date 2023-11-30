using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    }

    private void OnClickGenerate(object sender, RoutedEventArgs e)
    {
        string inBox = expressionTextBox.Text;

        int count = 0;

        foreach (char c in inBox)
        {
            if (c.Equals('$'))
                count++;
        }

        string[] pole = { "bezo1", "559550", "5YR32", "bezo1", "559550", "5YR32" };
        List<string> dolariky = new List<string>();

        if (count % 2 == 0)
        {
            int startI = inBox.IndexOf('$');
            for (int i = 0; i < count / 2; i++)
            {
                int startIndex = startI;
                int endIndex = inBox.IndexOf('$', startIndex + 1);
                startI = inBox.IndexOf('$', endIndex + 1);
                string subStr = inBox.Substring(startIndex + 1, endIndex - 1 - startIndex);
                //output.AppendText($"{subStr}, ");
                //output.AppendText("\n");
                dolariky.Add(subStr);
            }


            foreach (SearchResult result in _results)
            {
                //output.AppendText($"{result.Path}\n");
                DirectoryEntry resultEntry = result.GetDirectoryEntry();

                //foreach (string property in resultEntry.Properties.PropertyNames)
                //{
                //    output.AppendText(property + ": " + resultEntry.InvokeGet(property) + "\n");
                //}


                int startIndex1 = -1;
                for (int i = 0; i < count / 2; i++)
                {
                    int startIndex = startIndex1;
                    int endIndex = inBox.IndexOf('$', startIndex + 1);
                    startIndex1 = inBox.IndexOf('$', endIndex + 1);
                    string subStr = inBox.Substring(startIndex + 1, endIndex - 1 - startIndex);
                    //output.AppendText($"{subStr}{pole[i]}");
                    output.AppendText($"{subStr}{resultEntry.InvokeGet(dolariky[i])}");
                }
                output.AppendText("\n");
                output.AppendText("\n");
                output.AppendText("\n");
            }
        }
    }

    private void OnClickConnect(object sender, RoutedEventArgs e)
    {
        string ldapPath = pathTextBox.Text;
        string user = userTextBox.Text;
        string password = passwordBox.Password;
        try
        {
            _entry = new DirectoryEntry(ldapPath, user, password);
            object nativeObject = _entry.NativeObject;
            infoLoggedInTextBlock.Text = "Logged in";
        }
        catch (Exception exception)
        {
            infoLoggedInTextBlock.Text = $"Error: {exception.Message}";
        }
    }

    private void OnClickDisconnect(object sender, RoutedEventArgs e)
    {
        if (_entry != null)
        {
            _entry.Close();
            _entry.Dispose();
            infoLoggedInTextBlock.Text = "Logged out";
        }
    }

    private void OnClickSearchButton(object sender, RoutedEventArgs e)
    {
        try
        {
            using (DirectorySearcher searcher = new DirectorySearcher(_entry))
            {
                searcher.Filter = ConditionTextBox.Text;
                _results = searcher.FindAll();

                foreach (SearchResult result in _results)
                {
                    output.AppendText($"{result.Path}\n");
                    _resultEntry = result.GetDirectoryEntry();


                    //foreach (string property in _resultEntry.Properties.PropertyNames)
                    //{
                    //    output.AppendText(property + ": " + _resultEntry.InvokeGet(property) + "\n");
                    //}
                }
            }
        }
        catch (Exception exception)
        {
            output.Text = $"Error: {exception.Message}";
        }
    }
}
