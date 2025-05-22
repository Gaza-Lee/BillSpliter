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
        private decimal totalbyPerson;
        private ICommand IncreasePersonsCommand { get; }
        private ICommand DecreasePersonsCommand { get; }
        public ICommand SetTipCommand { get; }

        public MainViewViewModel()
        {
            SetTipCommand = new Command<int>((tipValue) => Tip = tipValue);
            IncreasePersonsCommand = new Command(() => NoPersons++);
            DecreasePersonsCommand = new Command(() =>
            {
                if (NoPersons > 1) NoPersons--;
            });
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
                tip = value;
                OnPropertyChanged();
                CalculateTotal();
            }
        }

        public int NoPersons
        {
            get => noPersons;
            set
            {
                if (value >= 1)
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
                OnPropertyChanged();
            }
        }

        public decimal TipByperson
        {
            get => tipByperson;
            private set
            {
                tipByperson = value;
                OnPropertyChanged();
            }
        }

        public decimal Subtotal
        {
            get => subtotal;
            private set
            {
                subtotal = value;
                OnPropertyChanged();
            }
        }

        private decimal TotalByPerson
        {
            get => totalbyPerson;
            set
            {
                totalbyPerson = value;
                OnPropertyChanged();
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
