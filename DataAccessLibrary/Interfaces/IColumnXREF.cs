namespace DataAccessLibrary
{
    public interface IColumnXREF
    {
        Task<List<ColumnXREF_Import>> GetColumnXREF();
    }
}