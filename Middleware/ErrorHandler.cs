using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro___Back.Middleware;
public class ErrorHandler
{
    public static IActionResult Unauthorized(string message = "Acesso não autorizado.")
    {
        return new ObjectResult(message)
        {
            StatusCode = 401
        };
    }

    public static IActionResult NotFound(string message = "Recurso não encontrado.")
    {
        return new ObjectResult(message)
        {
            StatusCode = 404
        };
    }

    public static IActionResult BadRequest(string message = "Solicitação inválida.")
    {
        return new ObjectResult(message)
        {
            StatusCode = 400
        };
    }

    public static IActionResult InternalServerError(string message = "Ocorreu um erro interno do servidor.")
    {
        return new ObjectResult(message)
        {
            StatusCode = 500
        };
    }
}
