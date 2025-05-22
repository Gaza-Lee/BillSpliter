using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillSpliter.Models
{
    public class BillData
    {
        public decimal Bill {  get; set; }
        public int Tip { get; set; }
        public int NoPersons { get; set; } = 1;
    }
}
