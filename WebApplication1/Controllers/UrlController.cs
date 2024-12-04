using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Store;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController : ControllerBase
{
    private readonly IStore _store;

    public UrlController(IStore store)
    {
        _store = store;
    }

    [HttpGet("GetById")]
    public UrlDto GetById(Guid id)
    {
        return _store.GetItem(id);
    }
    
    [HttpGet("GetDownloadedUrlsHistory")]
    public List<UrlDto> GetDownloadedUrlsHistory()
    {
        return _store.GetAllItems();
    }

    [HttpGet("GetByKeyword")]
    public List<UrlDto> GetByKeyword(string keyword)
    {
        return _store.GetItemsContainingKeyword(keyword);
    }

    [HttpPost("DownloadFromUrl")]
    public async Task DownloadFromUrl([FromForm]string url)
    {
        using HttpClient client = new();
        
        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
        {
            url = "http://" + url;
        }
        
        Uri uri = new Uri(url);
        
        var response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        _store.StoreItem(new UrlDto()
        {
            Html = responseBody,
            Url = url,
            Uuid = Guid.NewGuid()
        });
    }
    
    [HttpDelete("RemoveItem")]
    public IActionResult RemoveItem(Guid id)
    {
        if (_store.RemoveItem(id))
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}