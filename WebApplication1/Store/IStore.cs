using WebApplication1.Models;

namespace WebApplication1.Store;

public interface IStore
{
    void StoreItem(UrlDto item);
    UrlDto GetItem(Guid id);
    List<UrlDto> GetAllItems();
    List<UrlDto> GetItemsContainingKeyword(string key);
    bool RemoveItem(Guid uuid);
}