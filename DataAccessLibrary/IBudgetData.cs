namespace DataAccessLibrary
{
    public interface IBudgetData
    {
        Task<List<BudgetModel>> GetBudget();
    }
}