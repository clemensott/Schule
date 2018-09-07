using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LabyrinthSim
{
    class Waiter : INotifyPropertyChanged
    {
        private TimeSpan time;
        private bool pause, isPaused;
        private uint count;

        public TimeSpan Time
        {
            get { return time; }
            set
            {
                if (value == time) return;

                time = value;
                OnPropertyChanged("Time");
            }
        }

        public bool Pause
        {
            get { return pause; }
            set
            {
                if (value == pause) return;

                pause = value;
                OnPropertyChanged("Pause");
            }
        }

        public bool IsPaused
        {
            get { return isPaused; }
            set
            {
                if (value == isPaused) return;

                isPaused = value;
                OnPropertyChanged("IsPaused");
            }
        }

        public uint Count
        {
            get { return count; }
            set
            {
                if (value == count) return;

                count = value;
                //      OnPropertyChanged("Count");
            }
        }

        public ObservableCollection<UIntText> Breakpoints { get; private set; }

        public Waiter()
        {
            Time = TimeSpan.Zero;
            Pause = false;
            IsPaused = false;
            Count = 0;
            Breakpoints = new ObservableCollection<UIntText>();
        }

        public void Wait()
        {
            lock (this)
            {
                Count++;

                if (Pause || Breakpoints.Any(b => b.Value == Count))
                {
                    IsPaused = true;

                    Monitor.Wait(this);
                    return;
                }
                else if (Time.Ticks <= 0) return;
            }

            Task.Delay(Time).Wait(Time);
        }

        public void PulseAll()
        {
            lock (this)
            {
                Monitor.PulseAll(this);

                IsPaused = false;
            }
        }

        public void UpdateCount()
        {
            OnPropertyChanged("Count");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null) return;

            var args = new PropertyChangedEventArgs(name);

            if (Thread.CurrentThread == Application.Current.Dispatcher.Thread) PropertyChanged(this, args);
            else Application.Current.Dispatcher.BeginInvoke(PropertyChanged, this, args);
        }
    }
}
