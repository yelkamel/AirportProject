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

namespace AirportProject
{
    /// <summary>
    /// Logique d'interaction pour acceuil.xaml
    /// </summary>
    public partial class acceuil : Window
    {
        public acceuil()
        {
            InitializeComponent();
        }

        private void departure_vol_clic(object sender, RoutedEventArgs e)
        {
            DepartureVol window_avion = new DepartureVol();
            window_avion.Show();
        }

        private void list_all_bagage_clic(object sender, RoutedEventArgs e)
        {
            ListBagage list_all_bagage = new ListBagage();
            list_all_bagage.Show();
        }

        private void search_bagage_clic(object sender, RoutedEventArgs e)   
        {
            Bagage_seach window_bagage = new Bagage_seach();
            window_bagage.Show();
        }
        private void arrived_vol_clic(object sender, RoutedEventArgs e)
        {
            DepartureVol window_avion = new DepartureVol();
            window_avion.Show();
        }

        private void seach_vol_clic(object sender, RoutedEventArgs e)
        {
            DepartureVol window_avion = new DepartureVol();
            window_avion.Show();
        }

        private void exit_application(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
