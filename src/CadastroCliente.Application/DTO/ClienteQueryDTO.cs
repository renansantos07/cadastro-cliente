using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Application.DTO
{
    public class ClienteQueryDTO
    {
        [Key]
        public Guid? Id { get; set; }

        public string? RazaoSocial { get; set; }

        public string? NomeFantasia { get; set; }

        public string? Documento { get; set; }
    }
}