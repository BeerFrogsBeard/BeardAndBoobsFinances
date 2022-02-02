namespace DataAccessLibrary
{
    public interface IBudgetDataSummary
    {
        Task<List<BudgetSummaryModel>> GetBudgetSummary();
    }
}