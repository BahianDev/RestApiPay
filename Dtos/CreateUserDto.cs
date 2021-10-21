namespace deCasa.Dtos
{
  public class CreateUserDto
  {
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public decimal Wallet { get; set; }
    public string Cpf { get; set; }
    public string Cnpj { get; set; }


  }
}