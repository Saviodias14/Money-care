using Controle_Financeiro___Back.Data.Dtos.Services;

namespace Controle_Financeiro___Back.Data.Dtos;

public class ReadExpenseResponse
{
    public int Id { get; internal set; }
    public ReadUserDto User { get; set; }
    public double Amount { get; set; }
    public ReadTypesDto Type { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}
