using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Application.DTO
{
    public class ClienteDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Documento { get; set; }
    }
}