namespace BairroConnectAPI.Models
{
    public class EventoEndereco
    {
       public int idEvento { get; set; } // chave estrangeira
       public string endereco { get; set; } = string.Empty;
       public string nroEndereco { get; set; } = string.Empty;
       public string? Complemento { get; set; } = string.Empty;
       public string bairroEndereco { get; set; } = string.Empty;
       public string cidadeEndereco { get; set; } = string.Empty;
       public string UFEndereco { get; set; } = string.Empty;
       public string CEPEndereco { get; set; } = string.Empty;
    }
}