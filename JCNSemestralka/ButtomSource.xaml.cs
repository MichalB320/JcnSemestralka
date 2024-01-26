using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace JCNSemestralka;

/// <summary>
/// Interaction logic for ButtomSource.xaml
/// </summary>
public partial class ButtomSource : UserControl
{
    public ButtomSource()
    {
        InitializeComponent();
    }

    public void uloha()
    {

    }

    public void Clik()
    {
        
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        //MessageBox.Show("hello");
    }

    private void OnEnter(object sender, MouseEventArgs e)
    {
        tlacidlo.Background = new SolidColorBrush(Colors.LightBlue);
        obr.Visibility = Visibility.Visible;
    }

    private void OnLeave(object sender, MouseEventArgs e)
    {
        tlacidlo.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD2E2ED"));
        obr.Visibility = Visibility.Hidden;
    }

    public void SetNazov(string  nazov)
    {
        nazovX.Text = nazov;
    }
}
