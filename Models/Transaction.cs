namespace deCasa.Models
{
  public class Transaction
  {
    public string Id { get; set; }
    public string PayerId { get; set; }
    public User Payer { get; set; }
    public string PayeeId { get; set; }
    public User Payee { get; set; }
    public decimal Value { get; set; }



  }
}