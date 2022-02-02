﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models;

    public class BudgetColumnsModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public string Formatting { get; set; }
        public bool Visible { get; set; }
        public string CssClass { get; set; }
        public bool Filterable { get; set; }
    }