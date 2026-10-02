
// FILE: Middleware/CorrelationIdMiddleware.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;
namespace RealState.UI.Middleware
{

    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdMiddleware> _logger;
        public const string HeaderKey = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers.ContainsKey(HeaderKey)
                ? context.Request.Headers[HeaderKey].ToString()
                : Guid.NewGuid().ToString();

            // store for other components (e.g. controllers/services) to reuse
            context.Items[HeaderKey] = correlationId;

            // ensure response contains the correlation id
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey(HeaderKey))
                    context.Response.Headers.Add(HeaderKey, correlationId);
                return Task.CompletedTask;
            });

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                _logger.LogDebug("CorrelationId assigned | {CorrelationId} | Path: {Path}", correlationId, context.Request.Path); // LOG ADDED
                await _next(context);
            }
        }
    }
}    


