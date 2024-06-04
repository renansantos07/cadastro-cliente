using System.Linq.Expressions;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace CadastroCliente.Infrastructure.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly ApplicationDbContext _dataBase;
        protected readonly DbSet<T> _dataBaseSet;

        public Repository(ApplicationDbContext db)
        {
            _dataBase = db;
            _dataBaseSet = db.Set<T>();    
        }

        public async Task Adicionar(T entity)
        {
            _dataBaseSet.Add(entity);
            await SaveChanges();
        }

        public async Task<List<T>> ObterQuery(Expression<Func<T, bool>> query)
        {
            return await _dataBaseSet.AsNoTracking().Where(query).ToListAsync();
        }

        public async Task<List<T>> ObterTodos()
        {
            return await _dataBaseSet.ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _dataBase.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataBase?.Dispose();
        }

        public Task Atualizar(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Remover(T entity)
        {
            throw new NotImplementedException();
        }
    }

}