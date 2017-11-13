using JunDevelopers.Dominio.Comum;
using JunDevelopers.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JunDevelopers.Infra.Repositorio
{
    public abstract class Repositorio<TEntity, TContext> : IRepositorio<TEntity>, IDisposable
            where TEntity : Entidade
            where TContext : DbContext
    {
        protected TContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repositorio(TContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public Task<List<TEntity>> Selecionar()
        {
            return DbSet.ToListAsync();
        }

        public Task Inserir(TEntity entity)
        {
            DbSet.Add(entity);
            return Db.SaveChangesAsync();
        }

        public Task<TEntity> SelecionarPorId(int id)
        {
            return DbSet.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task Alterar(TEntity entity)
        {
            DbSet.Update(entity);
            return Db.SaveChangesAsync();
        }

        public Task Deletar(TEntity entity)
        {
            DbSet.Remove(entity);
            return Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
