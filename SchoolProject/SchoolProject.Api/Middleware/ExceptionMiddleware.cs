using SchoolProject.Domain.SchoolProjectExceptions.Exceptions;
using SchoolProject.Domain.SchoolProjectExceptions.Model;
using System.Net;

namespace SchoolProject.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (SchoolProjectException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Error = "internal_server_error",
                Message = "Internal Server Error."
            }.ToString());
        }

        private async Task HandleExceptionAsync(HttpContext context, SchoolProjectException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception.GetType() != typeof(Exception))
            {
                await context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = 400,
                    Error = exception.ErrorCode,
                    Message = exception.Message
                }.ToString());
            }

        }
    }
}
