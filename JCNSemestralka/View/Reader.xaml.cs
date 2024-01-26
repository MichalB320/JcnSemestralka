using Library;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace JCNSemestralka.View
{
    /// <summary>
    /// Interaction logic for Reader.xaml
    /// </summary>
    public partial class Reader : UserControl
    {
        private Databaza _databaza;

        public Reader()
        {
            InitializeComponent();
            _databaza = new Databaza();
        }

        private void OnClickFindButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = ".CSV súbory (*.csv) | *.csv";
            ofd.ShowDialog();
            CitacTextBox.Text = ofd.FileName;
        }

        private void OnClickReadButton(object sender, RoutedEventArgs e)
        {
            _databaza.GetUniversal().Clear();

            var subor = new FileInfo(CitacTextBox.Text);

            _databaza.Load(subor, (casti) =>
            {
                var item = new Item();
                foreach (var cast in casti)
                    item.AddStlpec(cast);

                _databaza.Add<Item>(_databaza.GetUniversal(), item);
            });

            //var list = _databaza.GetUniversal();
            //for (int i = 0; i < list.ElementAt(0).Kapacita(); i++)
            //{
            //    ValuesTextBox.AppendText($"{list.ElementAt(0).GetStlpec(i)}, ");
            //    CustomColls.AppendText($"{list.ElementAt(0).GetStlpec(i)}, ");
            //}
            //deleteButton.IsEnabled = true;
        }

        private void OnClickDeleteButton(object sender, RoutedEventArgs e)
        {
            //_databaza.GetUniversal().Clear();
            CitacTextBox.Clear();
            readButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }
    }
}
