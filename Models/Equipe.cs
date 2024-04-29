using BairroConnectAPI.Models.Enuns;
using System.ComponentModel.DataAnnotations.Schema;

namespace BairroConnectAPI.Models
{
    public class Equipe
    {
       public int idEvento { get; set; }
       public string respoEquipe { get; set; } = string.Empty;
       public int tamanhoEquipe { get; set; }
       public SelecaoEquipe SelecaoEquipe { get; set; }
    }
}