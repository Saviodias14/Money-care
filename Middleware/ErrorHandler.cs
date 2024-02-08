using Microsoft.AspNetCore.Mvc;

namespace Controle_Financeiro___Back.Middleware;
public class ErrorHandler : ControllerBase
{
    public IActionResult SendErrors(Exception ex)
    {
        var exception = ex.GetType().FullName.Split('.')[1];
        if (exception.Equals("ArgumentNullException") || exception.Equals("ArgumentException") || ex.Message.Equals("Unauthorized"))
        {
            return Unauthorized("Unauthorized");
        }
        else if (ex.Message.Equals("NotFound"))
        {
            return NotFound();
        }
        return new ObjectResult("InternalServerError")
        {
            StatusCode = 500
        };
    }
}
