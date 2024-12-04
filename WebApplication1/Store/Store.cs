using WebApplication1.Models;

namespace WebApplication1.Store;

public class Store : IStore
{
    private readonly List<UrlDto> _storedItems = new List<UrlDto>();

    public Store()
    {
    }

    public void StoreItem(UrlDto item)
    {
        if (_storedItems.All(si => si.Url != item.Url))
            _storedItems.Add(item);
    }

    public UrlDto GetItem(Guid id)
    {
        var item = _storedItems.FirstOrDefault(k => k.Uuid == id);
        return item ?? new UrlDto();
    }

    public List<UrlDto> GetAllItems()
    {
        return _storedItems;
    }

    public List<UrlDto> GetItemsContainingKeyword(string key)
    {
        return _storedItems.Where(urlDto => urlDto.Html.Contains(key, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public bool RemoveItem(Guid uuid)
    {
        var removable = _storedItems.FirstOrDefault(s => s.Uuid == uuid);
        if (removable != null)
            return _storedItems.Remove(removable);
        return false;
    }
}