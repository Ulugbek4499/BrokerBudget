using MediatR;
using Serilog;

namespace BrokerBudget.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {

                string requestName = typeof(TRequest).Name;
                Log.Error(ex, $"Game Store Application: Unhandled Exception for request {requestName} {request}\n");
                throw;
            }
        }
    }
}
