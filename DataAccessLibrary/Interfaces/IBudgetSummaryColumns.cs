
namespace DataAccessLibrary
{
    public interface IBudgetSummaryColumns
    {
        List<BudgetColumnsModel> GetColumnsAdvanced(IEnumerable<BudgetSummaryModel> budget, IEnumerable<ColumnXREF_Import> formatting);
    }
}