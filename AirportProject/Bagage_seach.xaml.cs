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
using Airport.Models;
using Airport.ViewModel;

namespace AirportProject
{
    /// <summary>
    /// Logique d'interaction pour Bagage_seach.xaml
    /// </summary>
    public partial class Bagage_seach : Window
    {
        private ModelSql model = new ModelSql();
        private VMBagage bagage_list = new VMBagage();
        public Bagage_seach()
        {
            InitializeComponent();
        }

        private void add_filter_Click(object sender, RoutedEventArgs e)
        {
            DateTime depart_date;
            string codeIata = code_iata.Text.ToString();     
            typeTaskBag type_task_ = (typeTaskBag)type_task_list.SelectedItem;
            vol_search vol_id = (vol_search)vol_depart.SelectedItem;
            SortieTri tri = (SortieTri)sortie_tri_ejection.SelectedItem;
            string ligne = ligne_bag.Text.ToString();
            string compagnie = compagnie_name.Text.ToString();
            if (date_departure.SelectedDate != null)
            {
                show_error.Visibility = Visibility.Hidden;
                depart_date = (DateTime)date_departure.SelectedDate;
                bagage_list.ListBagage = model.SelectBagagesParam(type_task_, vol_id, tri, depart_date, ligne, compagnie, codeIata);
                this.bagage_data_.ItemsSource = bagage_list.ListBagage; 
                this.bagage_data_.DataContext = bagage_list.ListBagage;
                this.bagage_data_.Columns[0].Visibility = Visibility.Hidden;
            }
            else
            {
                show_error.Content = "veuillez choisir la date";
                this.Show();
            }
            
        }

        private void show_bagage_details(object sender, MouseButtonEventArgs e)
        {
               Bagage item = (Bagage)this.bagage_data_.SelectedItem;
               Bagage_details bagage_detail = new Bagage_details(item.Id_bagage);
               bagage_detail.Show();


        }

       

       
    }
}
