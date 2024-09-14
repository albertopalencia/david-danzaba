// ***********************************************************************
// Assembly         : RealState.Api
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-11-2024
// ***********************************************************************
// <copyright file="ExceptionHandler.cs" company="RealState.Api">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Azure.Core;
using RealState.Domain.Exceptions;
using System.Net;

namespace RealState.Api.Middleware;

/// <summary>
/// Class AppExceptionHandlerMiddleware.
/// </summary>
public class AppExceptionHandlerMiddleware
{
    /// <summary>
    /// The next
    /// </summary>
    private readonly RequestDelegate _next;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<AppExceptionHandlerMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppExceptionHandlerMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next.</param>
    /// <param name="logger">The logger.</param>
    public AppExceptionHandlerMiddleware(RequestDelegate next, ILogger<AppExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invoke as an asynchronous operation.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error:",ex.Message);

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                ErrorMessage = ex.Message
            });

            context.Response.ContentType = ContentType.ApplicationJson.ToString();

            context.Response.StatusCode = ex switch
            {
                CoreBusinessException => (int)HttpStatusCode.BadRequest, 
                ArgumentException => (int)HttpStatusCode.NoContent,
                _ => (int)HttpStatusCode.InternalServerError
            };

            await context.Response.WriteAsync(result);
        }
    }
}
