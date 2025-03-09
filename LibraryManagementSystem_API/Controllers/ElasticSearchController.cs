using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Controllers
{
    [Route("")]
    public class ElasticSearchController : ControllerBase
    {
        private readonly IELasticSearchServices _services;
        public ElasticSearchController(IELasticSearchServices elasticSearchServices) {
            _services = elasticSearchServices;
        }

        /*        app.MapGet("/", () => "Hello World!");

                app.MapGet("/search", async(ElasticSearchServices elasticSearchServices, string query) =>
                    {
                        var results = await elasticSearchServices.SearchAsync(query);
                        return Results.Ok(results.Documents);
                    }
                );

                app.MapPost("/index", async(ElasticSearchServices elasticSearchServices) =>
                    {
                        await elasticSearchServices.IndexDataAsync();
                        return Results.Ok();
                    }*/


        [HttpGet("search")]
        public async Task<IActionResult> search([FromHeader]string query)
        {
            var results = await _services.SearchAsync(query);

            return Ok(results.Documents);
        }

        [HttpPost("index")]
        public async Task<IActionResult> index()
        {
            await _services.IndexDataAsync();
            return Ok();
        }




    }
}
