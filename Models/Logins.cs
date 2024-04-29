using BairroConnectAPI.Models.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BairroConnectAPI.Models
{
    public class Logins
    {
        public int idPessoa { get; set; }       
        public string nome { get; set; } = string.Empty;
        public string sobrenome { get; set; } = string.Empty;
        [EmailAddress]
        public string email { get; set; } = string.Empty;
        public DateTime dataNasc { get; set; }
        public string senha { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[]? Foto { get; set; }
        public TipoConta tipoConta { get; set; }
       
        [NotMapped]
        public string Token { get; set; } = string.Empty;
    }
}