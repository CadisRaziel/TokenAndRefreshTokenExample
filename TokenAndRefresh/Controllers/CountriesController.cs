using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TokenAndRefresh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [Authorize]
        [HttpGet]        
        public async Task<IActionResult> ListCountries()
        {
            var listCoutries = await Task.FromResult(new List<string> { "France", "Argentina", "Croatia", "Morocco" });
            return Ok(listCoutries);
        }
    }
}
