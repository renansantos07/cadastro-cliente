using System.Linq.Expressions;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Model.Cliente;
using CadastroCliente.Domain.Notifications;
using CadastroCliente.Domain.Validate;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CadastroCliente.Domain.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IRepository<ClienteEntity> _repository;
        private readonly INotifier _notifier;
        public ClienteService(
            INotifier notifier,
            IRepository<ClienteEntity> repository
            )
        {
            _notifier = notifier;
            _repository = repository;
        }

        public async Task Adicionar(ClienteEntity cliente)
        {
            ClienteValidator clienteValidator = new ClienteValidator();

            ValidationResult results = clienteValidator.Validate(cliente);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    _notifier.Handle(new Notification("Propriedade " + failure.PropertyName + " Falha na validação: " + failure.ErrorMessage));
                }
            }

            await _repository.Adicionar(cliente);
        }

        public async Task<List<ClienteEntity>> ObterQuery(ClienteEntity cliente)
        {
            Expression<Func<ClienteEntity, bool>> query = x => x.NomeFantasia == cliente.NomeFantasia
                                                    && x.RazaoSocial == cliente.RazaoSocial
                                                    && x.Documento == cliente.Documento;

            return await _repository.ObterQuery(query);
        }

        public async Task<List<ClienteEntity>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public Task Atualizar(ClienteEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Remover(ClienteEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}