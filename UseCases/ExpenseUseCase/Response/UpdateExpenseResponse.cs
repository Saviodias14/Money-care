namespace Controle_Financeiro___Back.UseCases.ExpenseUseCase.Response;
public class UpdateExpenseResponse
{
    public int Id { get; internal set; }
    public double Amount { get; set; }
    public string UserId { get; set; }
    public string Description { get; set; }
    public int TypeId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public virtual Models.Type Type { get; set; }
}
