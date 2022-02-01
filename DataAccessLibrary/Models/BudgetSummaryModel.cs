using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models;
    public class BudgetSummaryModel
    {
        public DateTime Date { get; set; }
        public double Outflow { get; set; }
        public double Inflow { get; set; }
        public double Total { get; set; }
    }
