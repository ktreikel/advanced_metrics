using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Store;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlController : ControllerBase
{
    private readonly IStore _store;
    private readonly IStoreWebsiteService _storeWebsiteService;
    public UrlController(IStore store, IStoreWebsiteService storeWebsiteService)
    {
        _store = store;
        _storeWebsiteService = storeWebsiteService;
    }

    [HttpGet("GetById")]
    public async Task<UrlDto> GetById(Guid id)
    {
        return await _store.GetItem(id);
    }
    
    [HttpGet("GetDownloadedUrlsHistory")]
    public async Task<List<UrlDto>> GetDownloadedUrlsHistory()
    {
        return await _store.GetAllItems();
    }

    [HttpGet("GetByKeyword")]
    public async Task<List<UrlDto>> GetByKeyword(string keyword)
    {
        return await _store.GetItemsContainingKeyword(keyword);
    }

    [HttpPost("DownloadFromUrl")]
    public async Task DownloadFromUrl([FromForm]string url)
    {
        await _storeWebsiteService.StoreWebsite(url);
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