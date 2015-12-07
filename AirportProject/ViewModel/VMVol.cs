using Airport.Entities;
using Airport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Airport.ViewModel
{
    public class VMVol: INotifyPropertyChanged
    {
        public static RoutedCommand RechercherVolCommand;
        //IModel Model;

        private Vol currentVol;
        List<Vol> resSearch;
        private Dictionary<string, string> vol_fields;

        public Dictionary<string, string> Vol_Fields
        {
            get { return this.vol_fields; }
            set
            {
                this.vol_fields = value;
                OnNotifyPropertyChange("vol_fields");
            }
        }
      
        public Vol CurrentVol
        {
            get { return this.currentVol; }
            set
            {
                this.currentVol = value;
                OnNotifyPropertyChange("CurrentVol");
            }                
        }

        public List<Vol> ResSearch
        {
            get { return this.resSearch; }
            set
            {
                this.resSearch = value;
                OnNotifyPropertyChange("ResSearch");
            }
        }

        public VMVol()
        {     
            ModelSql model_sql = new ModelSql();
            vol_fields = model_sql.SelectVolsFields();
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


    public class Event : INotifyPropertyChanged
    {
        public String ID { get; set; }
        public String Name { get; set; }

        public Event(string id, string name)
        {
            ID = id;
            Name = name;
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
