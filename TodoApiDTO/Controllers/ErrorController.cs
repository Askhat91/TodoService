using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace TodoApiDTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger _logger;

        public ErrorController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("APIErrors_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
        }

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, HttpPatch]
        public IActionResult Handle()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            RecordToFile(exceptionHandlerPathFeature);

            return BadRequest(new { error = "A server error occurred. Please try again later. " });
        }

        private void RecordToFile(IExceptionHandlerPathFeature exceptionHandlerPathFeature)
        {
            _logger.LogError(
                $"DateTime: {DateTime.Now} " + Environment.NewLine +
                $"Path: {exceptionHandlerPathFeature.Path} " + Environment.NewLine +
                $"Error: {exceptionHandlerPathFeature.Error} " + Environment.NewLine +
                new string('-', 70)
              );
        }
    }
}
