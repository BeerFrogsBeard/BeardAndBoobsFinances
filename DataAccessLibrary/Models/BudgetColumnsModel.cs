﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models;

    public class BudgetColumnsModel
    {
        public string Property { get; set; }
    }

    public class BudgetColumnsModelAdvanced
    {
        public string Name { get; set; }
        public string DataType { get; set; }
    }