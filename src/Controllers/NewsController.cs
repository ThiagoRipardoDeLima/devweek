using Microsoft.AspNetCore.Mvc;

using src.Entities.ViewModel;
using src.Services;

namespace src.Controllers;

[ApiController]
[Route("[Controller]")]
public class NewsController : Controller
{
    private readonly ILogger<NewsController> _logger;
    private readonly NewsServices _newsServices;

    public NewsController(ILogger<NewsController> logger, NewsServices newsService)
    {
        _logger = logger;
        _newsServices = newsService;
    }

    [HttpGet]
    public ActionResult<List<NewsViewModel>> Get() => _newsServices.Get();

    [HttpGet("{id:length(24)}", Name = "GetNews")]
    public ActionResult<NewsViewModel> Get(string id)
    {
        var news = _newsServices.Get(id);

        if(news is null)
            return NotFound();

        return news;
    }


    [HttpPost]
    public ActionResult<NewsViewModel> Create(NewsViewModel news)
    {
        var result = _newsServices.Create(news);

        return CreatedAtRoute("GetNews", new { id = result.Id.ToString() }, result);
    }

}