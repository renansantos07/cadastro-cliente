using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroCliente.Domain.Model.Cliente
{
    [Table("Cliente")]
    public class ClienteEntity : Entity
    {
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento { get; set; }
    }
}