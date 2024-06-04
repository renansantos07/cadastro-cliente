using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroCliente.Domain.Model.Cliente
{
    [Table("Cliente")]
    public class ClienteEntity : Entity
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }

        [StringLength(14)]
        public string Documento { get; set; }
    }
}