using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BillSpliter.ViewModels
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        private decimal _bill;
        private int _tip;
        private int _noPersons = 1;
        private decimal _totalTip;
        private decimal _tipByperson;
        private decimal _subtotal;
        private decimal _totalByPerson;
        private double _tipSliderValue;
        public ICommand IncreasePersonsCommand { get; }
        public ICommand DecreasePersonsCommand { get; }
        public ICommand SetTipCommand { get; }

        public MainViewViewModel()
        {
            SetTipCommand = new Command<int>(SetTip);
            IncreasePersonsCommand = new Command(IncreasePersons);
            DecreasePersonsCommand = new Command(DecreasePersons);
            OnPropertyChanged(nameof(Tip));
        }

        private void SetTip(int tipValue)
        {
            Debug.WriteLine($"Tip update to {tipValue}");
            Tip = tipValue;
            OnPropertyChanged(nameof(Tip));
        }

        private void IncreasePersons()
        {
            NoPersons++;
        }
        private void DecreasePersons()
        {
           if (NoPersons > 1)
            {
                NoPersons--;
            }
        }

        public double TipSliderValue
        {
            get => _tipSliderValue;
            set
            {
                if (_tipSliderValue != value)
                {
                    Debug.WriteLine($"Tip updated to {value}");
                    _tipSliderValue = value;
                    Tip = (int)value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Bill
        {
            get => _bill;
            set
            {
                _bill = value;
                OnPropertyChanged();
                CalculateTotal();
            }
        }

        public int Tip
        {
            get => _tip;
            set
            {
                if (_tip != value)
                {
                    _tip = value;
                    OnPropertyChanged(nameof(Tip));
                    CalculateTotal();
                }
            }
        }

        public int NoPersons
        {
            get => _noPersons;
            set
            {
                if (_noPersons != value && _noPersons > 0)
                {
                    _noPersons = value;
                    OnPropertyChanged();
                    CalculateTotal();
                }
            }
        }

        public decimal TotalTip
        {
            get => _totalTip;
            set
            {
                _totalTip = value;
                OnPropertyChanged(nameof(TotalTip));
            }
        }

        public decimal TipByperson
        {
            get => _tipByperson;
            private set
            {
                _tipByperson = value;
                OnPropertyChanged(nameof(TipByperson));
            }
        }

        public decimal Subtotal
        {
            get => _subtotal;
            private set
            {
                _subtotal = value;
                OnPropertyChanged(nameof(Subtotal));
            }
        }

        public decimal TotalByPerson
        {
            get => _totalByPerson;
            private set
            {
                _totalByPerson = value;
                OnPropertyChanged(nameof(TotalByPerson));
            }
        }

        private void CalculateTotal()
        {
            TotalTip = (Bill * Tip) / 100;
            TipByperson = TotalTip / NoPersons;
            Subtotal = Bill / NoPersons;
            TotalByPerson = (Bill + TotalTip) / NoPersons;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
