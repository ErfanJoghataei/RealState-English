// FILE: Middleware/ExceptionHandlingMiddleware.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RealState.UI.Middleware;
using Serilog.Context;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RealState.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var correlationId = context.Items[CorrelationIdMiddleware.HeaderKey]?.ToString() ?? context.TraceIdentifier;
                using (LogContext.PushProperty("CorrelationId", correlationId))
                {
                    _logger.LogError(ex, "Unhandled exception | CorrelationId: {CorrelationId} | Path: {Path}", correlationId, context.Request.Path); // LOG ADDED
                }

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = _env.IsDevelopment()
                    ? System.Text.Json.JsonSerializer.Serialize(new { error = ex.Message, correlationId })
                    : System.Text.Json.JsonSerializer.Serialize(new { error = "Internal server error", correlationId });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
