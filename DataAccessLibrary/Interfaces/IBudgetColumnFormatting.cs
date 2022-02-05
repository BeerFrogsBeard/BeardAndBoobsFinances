
namespace DataAccessLibrary
{
    public interface IBudgetColumnFormatting
    {
        Task<List<ColumnXREF_Import>> GetBudgetColumns();
    }
}