namespace LoanAPI.Models;

public enum LoanStatus
{
    Published,
    Unpublished
}

public class LoanApplication
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int TermValue { get; set; }
    public decimal InterestValue { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.Published;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}
