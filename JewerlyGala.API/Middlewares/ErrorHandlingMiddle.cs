using JewerlyGala.Domain.Exceptions;

namespace JewerlyGala.API.Middlewares
{
    public class ErrorHandlingMiddle: IMiddleware
    {
        private ILogger<ErrorHandlingMiddle> logger;
        public ErrorHandlingMiddle(ILogger<ErrorHandlingMiddle> logger) 
        {
            this.logger = logger;
        }    

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (InvalidParamException ex)
            {
                logger.LogWarning(ex, ex.Message);


                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }catch (NotFoundException ex)
            {
                logger.LogError(ex, ex.Message);


                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);


                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Someting went wrong");
            }
        }
    }
}
