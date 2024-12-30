using WebApplication1.Models;

namespace WebApplication1.Store;

public class Store : IStore
{
    private readonly List<UrlDto> _storedItems = new List<UrlDto>();

    public Store()
    {
    }

    public async Task StoreItem(UrlDto item)
    {
       if (_storedItems.All(si => si.Url != item.Url))
            _storedItems.Add(item);
    }

    public async Task<UrlDto> GetItem(Guid id)
    {
        return await Task.FromResult(_storedItems.FirstOrDefault(k => k.Uuid == id)) ?? new UrlDto();
    }

    public async Task<List<UrlDto>> GetAllItems()
    {
        return await Task.FromResult(_storedItems);
    }

    public async Task<List<UrlDto>> GetItemsContainingKeyword(string key)
    {
        return await Task.FromResult(_storedItems.Where(urlDto => urlDto.Html.Contains(key, StringComparison.OrdinalIgnoreCase)).ToList());
    }

    public bool RemoveItem(Guid uuid)
    {
        var removable = _storedItems.FirstOrDefault(s => s.Uuid == uuid);
        if (removable != null)
            return _storedItems.Remove(removable);
        return false;
    }
}