using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger; // logger for IDE
        }

        [AllowAnonymous] // non-viable route
        [Route("Error")] // doesn't require authorization
        public IActionResult Error() // viable route, but ID doesn't exists
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            // LogError() method logs the exception under Error category in the log
            logger.LogError($"The path {exceptionHandlerPathFeature?.Path} " +
                $"threw an exception {exceptionHandlerPathFeature?.Error}");

            return View("Error");
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult =
                HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource could not be found";
                    // LogWarning() method logs the message under
                    // Warning category in the log
                    logger.LogWarning($"404 error occured. Path = " +
                        $"{statusCodeResult?.OriginalPath} and QueryString = " +
                        $"{statusCodeResult?.OriginalQueryString}");
                    break;
            }

            return View("NotFound");
        }
    }
}