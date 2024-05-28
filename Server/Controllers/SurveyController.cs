using CrystalDecisions.CrystalReports.ViewerObjectModel;
using Database.Context;
using Database.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace SurveyJsWithBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SurveyController> _logger;

        public SurveyController(ApplicationDbContext context, ILogger<SurveyController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSurvey([FromBody] string surveyContent)
        {
            if (string.IsNullOrEmpty(surveyContent))
            {
                _logger.LogWarning("Received empty survey content");
                return BadRequest("Survey content is empty");
            }

            try
            {
                var survey = new Survey
                {
                    Content = surveyContent,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Survey.Add(survey);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving survey content");
                return StatusCode(500, "Internal server error");
            }
        }

        // Method to get all surveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetAllSurveys()
        {
            return await _context.Survey.ToListAsync();
        }

        // Method to get a specific survey by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurveyById(int id)
        {
            try
            {
                var survey = await _context.Survey.FindAsync(id);
                var existingSurveyResult = await _context.SurveyResult
                    .Where(sr => sr.formID == id)
                    .FirstOrDefaultAsync();
                if (survey == null)
                {
                    return NotFound();
                }
                if (existingSurveyResult != null)
                {
                    // If the result exists, retrieve it into a variable
                    // Deserialize json1 and json2 into JsonNode objects
                    var json1Object = JsonNode.Parse(survey.Content);
                    var json2Object = JsonDocument.Parse(existingSurveyResult.Result).RootElement;

                    // Traverse through the elements in the first JSON and add "defaultValue" property


                    // Serialize the modified json1Object back to JSON string
                    string updatedJson1 = json1Object.ToJsonString();
                }


                return survey;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error");
                throw;
            }
        }

    }

   



}
