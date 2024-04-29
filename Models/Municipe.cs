using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BairroConnectAPI.Models
{
    public class Municipe 
    {
       public int idMunicipe { get; set; }
       public int idPessoa { get; set; } 
        public string estado { get; set; } = string.Empty;
       public string cidade { get; set; } = string.Empty;

       [NotMapped]
       public Logins? logins { get; set; }
    }
}