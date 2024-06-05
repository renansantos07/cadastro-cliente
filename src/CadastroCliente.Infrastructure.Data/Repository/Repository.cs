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
        public async Task Atualizar(T entity)
        {
            try
            {
                _dataBase.Update(entity);
                await SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is T)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            proposedValues[property] = proposedValue;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }
        }

        public virtual async Task Remover(Guid Id)
        {
            _dataBaseSet.Remove(new T { Id = Id });
            await SaveChanges();
        }
    
        public async Task<int> SaveChanges()
        {
            return await _dataBase.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataBase?.Dispose();
        }

        public async Task<T> Obter(Guid Id)
        {
            return await _dataBaseSet.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }

}