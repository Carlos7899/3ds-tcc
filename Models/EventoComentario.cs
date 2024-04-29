namespace BairroConnectAPI.Models
{
    public class EventoComentario
    {
       public int idEvento { get; set; }
       public int idMunicipe { get; set; }
       public string comentario { get; set; } = string.Empty;
       public float avaliacao { get; set; }
    }
}