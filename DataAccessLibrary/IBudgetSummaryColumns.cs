
namespace DataAccessLibrary
{
    public interface IBudgetSummaryColumns
    {
        Task<List<BudgetColumnsModelAdvanced>> GetColumnsAdvanced(IEnumerable<BudgetSummaryModel> budget);
    }
}