using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace SurveyJsWithBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyReportController : Controller
    {
        [HttpGet("survey/{id}")]
        public IActionResult GetSurveyReport(int id)
        {
            // Load the Crystal Report
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine("Reports", "SurveyReport.rpt"));

            // Set the data source (for example, from a database)
            // Assume you have a method GetSurveyData to fetch data based on id
            var surveyData = GetSurveyData(id);
            reportDocument.SetDataSource(surveyData);

            // Export to PDF format
            Stream stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", $"SurveyReport_{id}.pdf");
        }

        private static DataTable GetSurveyData(int id)
        {
            return new DataTable();
            // Your logic to fetch data from the database and return as DataTable
        }
    }
}
