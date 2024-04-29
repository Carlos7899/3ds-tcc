using System.ComponentModel.DataAnnotations.Schema;

namespace BairroConnectAPI.Models
{
    public class OrgEventos
    {
       public int idOrganizador { get; set; }
       public int idPessoa { get; set; }
       public string profissao { get; set; } = string.Empty;
       public string empresa { get; set; } = string.Empty;
       public string telOrganizador { get; set; } = string.Empty;

      [NotMapped]
      public Logins? logins { get; set; }
    }
}