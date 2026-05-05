using TaskManager.Domain.Exceptions;

namespace TaskManager.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception)
            {
                // Erro inesperado (BUG, falha de banco, null reference, etc)

                // Aqui daria pra evoluir para:
                // - Criar um escopo (IServiceScopeFactory)
                // - Resolver um serviço (ex: ErrorLogService)
                // - Salvar no banco:
                //      - Message
                //      - StackTrace
                //      - Data/Hora
                //      - Usuário (opcional)
                //
                // O retorno ficaria algo do tipo:
                // var errorId = await SaveUnexpectedErrorAsync(ex);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { error = "Internal server error" });
            }
        }
    }
}