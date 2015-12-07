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
using Airport.Entities;


namespace AirportProject
{
    /// <summary>
    /// Logique d'interaction pour ListBagage.xaml
    /// </summary>
    public partial class ListBagage : Window
    {
        public ListBagage()
        {
            InitializeComponent();
        }

        private void show_bagage_details(object sender, MouseButtonEventArgs e)
        {
            Bagage item = (Bagage)this.all_bagage.SelectedItem;
            Bagage_details bagage_detail = new Bagage_details(item.Id_bagage);
            bagage_detail.Show();

        }
    }
}
