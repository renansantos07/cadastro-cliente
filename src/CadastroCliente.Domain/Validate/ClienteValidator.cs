using CadastroCliente.Domain.Model.Cliente;
using CadastroCliente.Domain.Utils;
using FluentValidation;

namespace CadastroCliente.Domain.Validate
{
    public class ClienteValidator :  AbstractValidator<ClienteEntity>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.RazaoSocial).NotEmpty().WithMessage("O campo razão social precisa ser preenchido");
            RuleFor(c => c.NomeFantasia).NotEmpty().WithMessage("O campo nome fantasia precisa ser preenchido");
            RuleFor(c => c.Documento).NotEmpty().WithMessage("O campo documento precisa ser preenchido")
                .MinimumLength(11).WithMessage("Você deve informar um CPF ou CNPJ.").MaximumLength(14).WithMessage("Você deve informar um CPF ou CNPJ.")
                .Must(CpfCnpjUtils.IsValid).WithMessage("CPF ou CNPJ inválido.");

        }
    }
}