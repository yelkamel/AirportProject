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
using System.Windows.Shapes;
using Airport.ViewModel;

namespace AirportProject
{
    /// <summary>
    /// Logique d'interaction pour departure_vol.xaml
    /// </summary>
    public partial class DepartureVol : Window
    {
        public DepartureVol()
        {
            InitializeComponent();
        }

        private void ajouter (object sender, RoutedEventArgs e)
        {
            String path = critere_vol_list.DisplayMemberPath.ToString();
            String value_selected = critere_vol_list.SelectedValue.ToString();
         
            Event evenement = new Event(path, value_selected);
            fields_table.Items.Add(evenement);
        }

        private void retirer(object sender, RoutedEventArgs e)
        {
            fields_table.Items.Remove(fields_table.SelectedItem);
            
        }
    }
}
