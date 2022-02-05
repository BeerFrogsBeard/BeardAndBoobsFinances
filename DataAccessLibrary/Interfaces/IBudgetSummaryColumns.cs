
namespace DataAccessLibrary
{
    public interface IBudgetSummaryColumns
    {
        List<BudgetColumnsModel> GetColumnsAdvanced(IEnumerable<ColumnXREF_Import> formatting);
    }
}