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
using Airport.Models;
using Airport.ViewModel;
using Airport.Entities;

namespace AirportProject
{
    /// <summary>
    /// Logique d'interaction pour Bagage_details.xaml
    /// </summary>
    public partial class Bagage_details : Window
    {
        private int id_bagage;
        private ModelSql model;
        private VMBagage model_bagage;
        private Bagage bagage;
        private List<Trace> TraceBag;
        private List<Vol> vol_bagage;

        public Bagage_details(int id_bagage)
        {
            this.id_bagage = id_bagage;
            model = new ModelSql();
            model_bagage = new VMBagage();
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bagage = model.GetBagageId(id_bagage);
            this.code_iata.Text = bagage.CodeIata;
            this.compagnie.Text = bagage.Compagnie;
            this.creation.Text = bagage.DateCreation.ToString();
            
            //fill tracibility
            TraceBag = model.GetTracabilite(id_bagage);
            this.tracabilite.DataContext = TraceBag;
            this.tracabilite.ItemsSource = TraceBag;
            
            //fill vol bagage 
            this.vol_bagage = model.GetVolBagage(id_bagage);
            this.vol_associe.DataContext = vol_bagage;
            this.vol_associe.ItemsSource = vol_bagage;
        }
    }
}
