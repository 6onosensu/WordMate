namespace WordMate.Core.Interfaces
{
    public interface IRefreshManager
    {
        Task RefreshPageComponents();
        Task RefreshAfterUpdating(int categoryId);
    }
}
