using JunDevelopers.Dominio.Comum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JunDevelopers.Dominio.Interfaces
{
    public interface IRepositorio<TEntity> where TEntity : Entidade
    {
        Task<TEntity> SelecionarPorId(int id);

        Task<List<TEntity>> Selecionar();

        Task Alterar(TEntity entity);

        Task Inserir(TEntity entity);

        Task Deletar(TEntity entity);
    }
}
