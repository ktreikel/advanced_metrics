using WebApplication1.Models;

namespace WebApplication1.Store;

public interface IStore
{
    Task StoreItem(UrlDto item);
    Task<UrlDto> GetItem(Guid id);
    Task<List<UrlDto>> GetAllItems();
    Task<List<UrlDto>> GetItemsContainingKeyword(string key);
    bool RemoveItem(Guid uuid);
}