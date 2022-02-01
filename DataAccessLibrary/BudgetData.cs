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
            //string sql = "SELECT name as Property FROM sys.columns WHERE object_id = OBJECT_ID('YNAB_Budget_View') ORDER BY column_id";

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
        //This should be refactored to not spoof the data via a SQL Server temp table
        private readonly ISqlDataAccess _db;


        public BudgetSummaryColumns(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<BudgetColumnsModelAdvanced>> GetColumnsAdvanced(IEnumerable<BudgetSummaryModel> budget)
        {
            string sql = @"
                CREATE TABLE #DATA (
	                [NAME] VARCHAR(50)
	                , [DATA_TYPE] VARCHAR(50)
                )
            ";

            foreach (var prop in typeof(BudgetSummaryModel).GetProperties())
            {
                sql += "INSERT INTO #DATA VALUES('" + prop.Name + "','" + prop.PropertyType + "')";
            }

            sql += @"
                SELECT * FROM #DATA 
                DROP TABLE #DATA
            ";

            return _db.LoadData<BudgetColumnsModelAdvanced, dynamic>(sql, new { });
        }
    }
}
