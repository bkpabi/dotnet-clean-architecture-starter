using CleanArch.ApplicationCore.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : HandlerResponse
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        //Request
        _logger.LogInformation(
            "Handling {Name}. {@Date}",
            requestName,
            DateTime.UtcNow);

        var response = await next();
        if (response.Success)
        {
            //Response
            _logger.LogInformation(
                "CleanArchitecture Request: {Name} {@request}. {@Date}",
                requestName,
                request,
                DateTime.UtcNow);
        }
        else
        {
            using (LogContext.PushProperty("Error", response.Errors, true))
            {
                _logger.LogError(
                    "Completed request {RequestName} with error",
                    requestName);
            }
        }

        return response;
    }
}
