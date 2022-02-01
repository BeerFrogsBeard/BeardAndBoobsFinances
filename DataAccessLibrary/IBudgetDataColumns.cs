
namespace DataAccessLibrary
{
    public interface IBudgetDataColumns
    {
        Task<List<BudgetColumnsModel>> GetColumns();
    }
}