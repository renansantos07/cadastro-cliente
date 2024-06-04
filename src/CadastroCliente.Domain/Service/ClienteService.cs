using System.Linq.Expressions;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Model.Cliente;

namespace CadastroCliente.Domain.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IRepository<ClienteEntity> _repository;

        public ClienteService(IRepository<ClienteEntity> repository)
        {
            _repository = repository;
        }

        public async Task Adicionar(ClienteEntity cliente)
        {
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