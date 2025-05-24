using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BillSpliter.ViewModels
{
    public class MainViewViewModel : INotifyPropertyChanged
    {
        private decimal _bill;
        private int tip;
        private int noPersons = 1;
        private decimal totalTip;
        private decimal tipByperson;
        private decimal subtotal;
        private decimal totalByPerson;
        public ICommand IncreasePersonsCommand { get; }
        public ICommand DecreasePersonsCommand { get; }
        public ICommand SetTipCommand { get; }

        public MainViewViewModel()
        {
            SetTipCommand = new Command<int>((tipValue) =>
            {
                Console.WriteLine($"Button clicked, changing Tip to {tipValue}");
                Tip = tipValue;
                OnPropertyChanged(nameof(Tip));
                CalculateTotal();
            });
            IncreasePersonsCommand = new Command(() =>
            {
                NoPersons++;
                OnPropertyChanged(nameof(NoPersons));
                CalculateTotal();
            });
            DecreasePersonsCommand = new Command(() =>
            {
                if (NoPersons > 1)
                {
                    NoPersons--;
                    OnPropertyChanged(nameof(NoPersons));
                    CalculateTotal();
                }
            });
            Tip = 10;
            OnPropertyChanged(nameof(Tip));
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
            get => tip;
            set
            {
                if (tip != value)
                {
                    tip = value;
                    Console.WriteLine($"Tip updated to: {tip}");
                    OnPropertyChanged(nameof(Tip));
                    CalculateTotal();
                }
            }
        }

        public int NoPersons
        {
            get => noPersons;
            set
            {
                if (noPersons != value)
                {
                    noPersons = value;
                    OnPropertyChanged();
                    CalculateTotal();
                }
            }
        }

        public decimal TotalTip
        {
            get => totalTip;
            set
            {
                totalTip = value;
                OnPropertyChanged(nameof(TotalTip));
            }
        }

        public decimal TipByperson
        {
            get => tipByperson;
            private set
            {
                tipByperson = value;
                OnPropertyChanged(nameof(TipByperson));
            }
        }

        public decimal Subtotal
        {
            get => subtotal;
            private set
            {
                subtotal = value;
                OnPropertyChanged(nameof(Subtotal));
            }
        }

        public decimal TotalByPerson
        {
            get => totalByPerson;
            private set
            {
                totalByPerson = value;
                OnPropertyChanged(nameof(TotalByPerson));
            }
        }

        private void CalculateTotal()
        {
            TotalTip = (Bill * Tip) / 100;
            TipByperson = TotalTip / NoPersons;
            Subtotal = Bill/NoPersons;
            TotalByPerson = (Bill + TotalTip) / NoPersons;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
