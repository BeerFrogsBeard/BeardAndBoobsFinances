using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models;

    public class BudgetModel
    {
        public DateTime Month { get; set; }
        public string Group { get; set; }
        public string Category { get; set; }
        public double Budgeted { get; set; }
        public double Activity { get; set; }
        public double Available { get; set; }
    }