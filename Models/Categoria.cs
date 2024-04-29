using BairroConnectAPI.Models;

namespace BairroConnectAPI.Models
{
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string nomeCategoria { get; set; } = string.Empty;
        public string descricao { get; set; } = string.Empty;
    }
}