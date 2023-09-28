using BattleShipAPI.Exceptions;

namespace BattleShipAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (OutOfGameBoardException  ex ) 
            {
                await HandleException(context, ex, 401);
            }
            catch (NotFoundException ex )
            {
                await HandleException(context, ex, 404);
            }
            catch(WrongConfigException ex) {

                await HandleException(context, ex, 401);
            }
            catch (Exception ex) 
            {
                await HandleException(context, ex, 500);
            }
        }
        private async Task HandleException(HttpContext context, Exception ex, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
