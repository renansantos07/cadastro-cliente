using System.Linq.Expressions;
using CadastroCliente.Domain.Model;
using CadastroCliente.Domain.Model.Cliente;

namespace CadastroCliente.Domain.Interfaces
{
    public interface IClienteService : IDisposable
    {
        Task Adicionar(ClienteEntity cliente);
        Task<ClienteEntity> Obter(Guid Id);
        Task<List<ClienteEntity>> ObterTodos();
        Task<List<ClienteEntity>> ObterQuery(ClienteEntity cliente);
        Task Atualizar(ClienteEntity cliente);
        Task Remover(Guid Id);
    }    
}