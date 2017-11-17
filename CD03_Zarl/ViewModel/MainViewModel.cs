using DataSimulation;
using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CD03_Zarl.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        public List<ItemVm> modelList= new List<ItemVm>();

        public ObservableCollection<ItemVm> ActorList { get; set; }

        public ObservableCollection<ItemVm> SensorList { get; set; }

        public ObservableCollection<string> ActorModeSelectionList { get; private set; }

        public ObservableCollection<string> SensorModeSelectionList { get; private set; }

        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();

        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }

        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }



        public MainViewModel()
        {
            ActorList = new ObservableCollection<ItemVm>();
            SensorList = new ObservableCollection<ItemVm>();
            SensorModeSelectionList = new ObservableCollection<string>();
            ActorModeSelectionList = new ObservableCollection<string>();

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                //Aus unerklärlichen Gründen werden "Auto" und "Manual" ebenfalls in der SensorModeSelectionList gespeichert.
                SensorModeSelectionList.Add(item);
            }
            foreach(var item in Enum.GetNames(typeof(ModeType)))
            {
                ActorModeSelectionList.Add(item);
            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 30);
            timer.Tick += updateTimer;
            timer.Start();

            loadData();
            
        }

        private void updateTimer(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();

        }

        public void loadData()
        {
            sim = new Simulator(modelList);

            foreach(var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                {
                    SensorList.Add(item);
                }else if (item.ItemType.Equals(typeof(IActuator)))
                {
                    ActorList.Add(item);
                }
            }
        }
    }
}