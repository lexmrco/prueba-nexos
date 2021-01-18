using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Nexos.PruebaTecnica.NLog
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                await _next(httpContext);
                
                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                ReadingLog(httpContext, ts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }

        private void ReadingLog(HttpContext httpContext, TimeSpan ts)
        {
            string method = httpContext.Request.Method;

            string endPoint = httpContext.Request.Path;
            int resStatusCode = httpContext.Response.StatusCode;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            if (httpContext.Request.QueryString != null)
                endPoint = string.Concat(endPoint, HttpUtility.UrlDecode(httpContext.Request.QueryString.Value.Trim()));

            string logMessage = string.Format("Tracing log:{4}Endpoint: {0}{4}Method: {1}{4}Response status code: {2}{4}Elapsed time(hh:mm:ss.ms): {3}{4}", 
                endPoint,
                method,
                resStatusCode, 
                elapsedTime,
                Environment.NewLine);

            _logger.LogInfo(logMessage);

        }
    }
}
