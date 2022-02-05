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

        public List<BudgetColumnsModel> GetColumnsAdvanced(IEnumerable<BudgetSummaryModel> budget, IEnumerable<ColumnXREF_Import> formatting)
        {
            string PropertyName;
            string DataType;        //used to derive Formatting
            string Formatting;      //derived from datatype

            string DisplayName;     //needs to be stored  
            bool Visible;           //needs to be stored
            string CssClass;        //needs to be stored
            bool Filterable;        //needs to be stored

            //Need to pull BB_XREF Data XREF_TYPE = BUDGET_SUMMARY
            //Task<List<ColumnXREF_Import>> formatting = new ColumnXREF();

            foreach (var prop in typeof(BudgetSummaryModel).GetProperties())
            {
                PropertyName = prop.Name;
                DisplayName = prop.Name; //using this for now until I build process for storing data
                DataType = prop.PropertyType.ToString();
                Formatting = string.Empty;
                Visible = true;
                CssClass = "";
                Filterable = false;

                //Formatting defaults to be used if data not found in BB_XREF
                switch (DataType)
                {
                    case "System.DateTime":
                        Formatting = "{0:d}";
                        break;
                    case "System.Double" :
                        Formatting = "{0:c2}";
                        break;
                    default:
                        Formatting = "";
                        break;
                }
                
                _columns.Add(new BudgetColumnsModel 
                    { 
                        Name = PropertyName
                        , DisplayName = DisplayName
                        , DataType = DataType
                        , Formatting = Formatting
                        , Visible = Visible
                        , CssClass = CssClass
                        , Filterable = Filterable 
                    }
                );
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