using System.Linq.Expressions;
using CadastroCliente.Domain.Model;

namespace CadastroCliente.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task Adicionar(T entity);
        Task<List<T>> ObterTodos();
        Task<List<T>> ObterQuery(Expression<Func<T, bool>> query);
        Task Atualizar(T entity);
        Task Remover(T entity);
        Task<int> SaveChanges();

    }

}