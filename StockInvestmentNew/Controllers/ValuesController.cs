using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockInvestmentNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Authorize]  // This ensures only authenticated users can access this endpoint
        public IActionResult GetValues()
        {
            var values = new List<string> { "Value1", "Value2", "Value3" };
            return Ok(values);
        }
    }
}
