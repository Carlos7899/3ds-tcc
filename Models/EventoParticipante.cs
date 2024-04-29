using System.ComponentModel.DataAnnotations.Schema;

namespace BairroConnectAPI.Models
{
    public class EventoParticipante
    {
       public int idEvento { get; set; }
       public DateTime horaParticipacao { get; set; }
       public int limiteParticipantesHora { get; set; }
    }
}