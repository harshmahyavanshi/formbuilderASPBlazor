using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SurveyJsWithBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyResultController : ControllerBase
    {

            private readonly ApplicationDbContext _context;
            private readonly ILogger<SurveyController> _logger;

            public SurveyResultController(ApplicationDbContext context, ILogger<SurveyController> logger)
            {
                _context = context;
                _logger = logger;
            }

            [HttpPost("{id}")]
            public async Task<IActionResult> SaveSurveyResult([FromBody] string surveyContent,int id)
            {
                if (string.IsNullOrEmpty(surveyContent))
                {
                    _logger.LogWarning("Received empty survey content");
                    return BadRequest("Survey content is empty");
                }

                try
                {
                var surveyResult = new SurveyResult
                {
                    formID = id,
                    Result = surveyContent,
                    CreatedAt = DateTime.UtcNow
                };

                    _context.SurveyResult.Add(surveyResult);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving survey content");
                    return StatusCode(500, "Internal server error");
                }
            }
        }
 
}
