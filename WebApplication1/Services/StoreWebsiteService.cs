using WebApplication1.Models;
using WebApplication1.Store;

namespace WebApplication1.Services;

public class StoreWebsiteService : IStoreWebsiteService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IStore _store;

    public StoreWebsiteService(IStore store, IHttpClientFactory clientFactory)
    {
        _store = store;
        _clientFactory = clientFactory;
    }

    public async Task StoreWebsite(string url)
    {
        
        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
        {
            url = "http://" + url;
        }
        
        var client = _clientFactory.CreateClient(url);

        Uri uri = new Uri(url);
        
        var response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        
       await _store.StoreItem(new UrlDto()
        {
            Html = responseBody,
            Url = url,
            Uuid = Guid.NewGuid()
        });
    }
}