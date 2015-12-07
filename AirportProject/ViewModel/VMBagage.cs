using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Entities;
using Airport.Models;

namespace Airport.ViewModel
{
    public class VMBagage : INotifyPropertyChanged
    {
        /*get trace bagage*/
        private List<Trace> trace_bag;
        public List<Trace> GetTraceBag
        {
            get { return this.trace_bag; }
            set
            {
                this.trace_bag = value;
                OnNotifyPropertyChange("trace_bag");
            }
        }



        /*get bagage details by id bagage*/
        private Bagage bagage;
        public Bagage GetBagage
        {
            get { return this.bagage; }
            set
            {
                this.bagage = value;
                OnNotifyPropertyChange("bagage");
            }
        }

        /*remplissage de la liste deroulante des avions*/
        private List<vol_search> listvol;
        public List<vol_search> ListVol
        {
            get { return this.listvol; }
            set
            {
                this.listvol = value;
                OnNotifyPropertyChange("listvol");
            }
        }

        //remplissage de la liste deroulante des sorties de tri
        private List<SortieTri> list_sortie_tri;
        public List<SortieTri> ListSortieTri
        {
            get { return this.list_sortie_tri; }
            set
            {
                this.list_sortie_tri = value;
                OnNotifyPropertyChange("listvol");
            }
        }

        //remplissage de la liste deroulante des  taches
        private List<typeTaskBag> list_task;
        public List<typeTaskBag> ListTask
        {
            get { return this.list_task; }
            set
            {
                this.list_task = value;
                OnNotifyPropertyChange("listvol");
            }
        }

        //remplissage de la datagridview 
        private List<Bagage> list_bagage;
        public List<Bagage> ListBagage
        {
            get { return this.list_bagage; }
            set
            {
                this.list_bagage = value;
                OnNotifyPropertyChange("list_bagage");
            }
        }

        //remplissage de la datagridview 
        private List<Bagage> list_all_bagage;
        public List<Bagage> ListAllBagage
        {
            get { return this.list_all_bagage; }
            set
            {
                this.list_all_bagage = value;
                OnNotifyPropertyChange("list_bagage");
            }
        }


        public VMBagage()
        {
            ModelsCombo model_sql = new ModelsCombo();
            ModelSql model_main = new ModelSql();
            listvol = model_sql.SelectVols();
            list_sortie_tri = model_sql.SelectSortieTri();
            list_task = model_sql.SelectTypeTask();
            list_all_bagage = model_main.GetBagages();
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void OnNotifyPropertyChange(PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, args);
        }

        protected void OnNotifyPropertyChange(string propertyName)
        {
            this.OnNotifyPropertyChange(new PropertyChangedEventArgs(propertyName));
        }
    }
}
