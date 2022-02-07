using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class BudgetData : IBudgetData
    {
        private readonly ISqlDataAccess _db;

        public BudgetData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<BudgetModel>> GetBudget()
        {
            string sql = "select * from dbo.YNAB_Budget_View";

            return _db.LoadData<BudgetModel, dynamic>(sql, new { });
        }

    }

    public class BudgetDataSummary : IBudgetDataSummary
    {
        private readonly ISqlDataAccess _db;

        public BudgetDataSummary(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<BudgetSummaryModel>> GetBudgetSummary()
        {
            string sql = @"
                SELECT 
	                [DATE]
	                , SUM(OUTFLOW) OUTFLOW
	                , SUM(INFLOW) INFLOW
	                , TOTAL = SUM(INFLOW) - SUM(OUTFLOW)
                FROM 
	                [DBO].[YNAB_TRANSACTIONS]
                GROUP BY
	                [DATE]";

            return _db.LoadData<BudgetSummaryModel, dynamic>(sql, new { });
        }
    }

    public class BudgetSummaryColumns : IBudgetSummaryColumns
    {
        private List<BudgetColumnsModel> _columns = new List<BudgetColumnsModel>();

        public List<BudgetColumnsModel> GetColumnsAdvanced(IEnumerable<ColumnXREF_Import> formatting) 
        {
            //default values
            string PropertyName = "";
            string DisplayName = null;
            string DataType = "";
            string Formatting = null;
            
            bool Visible = true;
            string CssClass = "";
            bool Filterable = true;

            foreach (var prop in typeof(BudgetSummaryModel).GetProperties())
            {
                PropertyName = prop.Name;                
                DataType = prop.PropertyType.ToString();

                var count = formatting.Count();

                if (count > 0)
                {
                    IEnumerable<ColumnXREF_Import> _formatting = formatting.Where(t => t.PropertyName == PropertyName.ToUpper());
                    foreach (var item in _formatting)
                    {
                        DisplayName = item.DisplayName;
                        Formatting = item.Formatting;
                        Visible = item.Visible;
                        CssClass = item.CssClass;
                        Filterable = item.Filterable;
                    }
                }
                else 
                {
                    DisplayName = prop.Name;

                    switch (DataType)
                    {
                        case "System.DateTime":
                            Formatting = "{0:d}";
                            break;
                        case "System.Double":
                            Formatting = "{0:c2}";
                            break;
                        default:
                            Formatting = "";
                            break;
                    }
                }

                _columns.Add(new BudgetColumnsModel
                {
                    Name = PropertyName
                    , DisplayName = DisplayName
                    , Formatting = Formatting
                    , Visible = Visible
                    , CssClass = CssClass
                    , Filterable = Filterable
                });

            }

            return _columns;
        }

    }

    public class ColumnXREF : IColumnXREF
    {
        private readonly ISqlDataAccess _db;

        public ColumnXREF(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<ColumnXREF_Import>> GetColumnXREF()
        {
            string sql = @"
                    SELECT
                        XREF_TYPE
                        , PropertyName
                        , DisplayName
                        , Formatting
                        , Visible
                        , CssClass
                        , Filterable
                    FROM 
                        DBO.BB_XREF
                    WHERE
                        XREF_TYPE = 'BUDGET_SUMMARY'";

            return _db.LoadData<ColumnXREF_Import, dynamic>(sql, new { });
        }
    }
}