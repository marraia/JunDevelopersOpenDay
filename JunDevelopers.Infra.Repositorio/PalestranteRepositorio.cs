using JunDevelopers.Dominio.Interfaces;
using JunDevelopers.Dominio.Modelos;
using JunDevelopers.Infra.Repositorio.Contexto;

namespace JunDevelopers.Infra.Repositorio
{
    public class PalestranteRepositorio : Repositorio<Palestrante, JunContexto>, IPalestranteRepositorio
    {
        public PalestranteRepositorio(JunContexto context) : base(context)
        {
        }
    }
}
