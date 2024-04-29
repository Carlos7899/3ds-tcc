using System.ComponentModel.DataAnnotations.Schema;

namespace BairroConnectAPI.Models
{
    public class EventoMunicipe
    {
       public int idEventoMunicipe { get; set; }
       public int idMunicipe { get; set; }
       public int idEvento { get; set; }
       public  DateTime horaInicio { get; set; }
       public  DateTime horaFim { get; set; }
    }
}