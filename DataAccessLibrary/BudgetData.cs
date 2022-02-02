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

    public class BudgetDataColumns : IBudgetDataColumns
    {
        private readonly ISqlDataAccess _db;

        public BudgetDataColumns(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<BudgetColumnsModel>> GetColumns()
        {
            string sql = @"
                SELECT 
                 COLUMN_NAME AS PROPERTY
                FROM 
                 INFORMATION_SCHEMA.COLUMNS 
                WHERE 
                 TABLE_NAME = N'YNAB_Budget_View'
                ORDER BY
                 ORDINAL_POSITION";

            return _db.LoadData<BudgetColumnsModel, dynamic>(sql, new { });
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
        private List<BudgetColumnsModelAdvanced> _columns = new List<BudgetColumnsModelAdvanced>();

        public List<BudgetColumnsModelAdvanced> GetColumnsAdvanced(IEnumerable<BudgetSummaryModel> budget)
        {

            foreach (var prop in typeof(BudgetSummaryModel).GetProperties())
            {
                _columns.Add(new BudgetColumnsModelAdvanced { Name = prop.Name, DataType = prop.PropertyType.ToString() });

            }

            return _columns;
        }
    }
}
