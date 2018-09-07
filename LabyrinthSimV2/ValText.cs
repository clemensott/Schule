using System;
using System.ComponentModel;

namespace LabyrinthSim
{
    abstract class ValText<T> : INotifyPropertyChanged where T : struct, IEquatable<T>
    {
        private string text;
        private T value;

        public string Text
        {
            get { return text; }
            set
            {
                if (value == text) return;

                text = value;
                this.value = Convert(value);

                OnPropertyChanged("Text");
                OnPropertyChanged("Value");
            }
        }

        public T Value
        {
            get { return value; }
            set
            {
                if (value.Equals(this.value)) return;

                this.value = value;
                text = Convert(value);

                OnPropertyChanged("Value");
                OnPropertyChanged("Text");
            }
        }

        protected abstract string Convert(T value);

        protected abstract T Convert(string text);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
