
namespace DataAccessLibrary
{
    public interface IBudgetSummaryColumns
    {
        List<BudgetColumnsModelAdvanced> GetColumnsAdvanced(IEnumerable<BudgetSummaryModel> budget);
    }
}