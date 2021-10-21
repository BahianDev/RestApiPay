namespace deCasa.Dtos
{
  public class CreateTransactionDto
  {
    public string PayerId { get; set; }
    public string PayeeId { get; set; }
    public decimal Value { get; set; }


  }
}