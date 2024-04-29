using System.ComponentModel.DataAnnotations.Schema;

namespace BairroConnectAPI.Models
{
    public class Evento
    {
        public int idEvento { get; set; }
        public int idOrganizador { get; set; }
        public int idCategoria { get; set; }
        public string titulo { get; set; } = string.Empty;
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
        public int limiteParticipantes { get; set; }
        public string descricao { get; set; } = string.Empty;
        public int valorIngresso { get; set; }
        public DateTime horaInicio { get; set; }
        public DateTime horaFim { get; set; }

    }
}