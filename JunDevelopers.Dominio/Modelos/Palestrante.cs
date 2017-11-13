using JunDevelopers.Dominio.Comum;
using JunDevelopers.Dominio.Excecoes;

namespace JunDevelopers.Dominio.Modelos
{
    public class Palestrante : Entidade
    {
        public string Nome { get; private set; }
        public string Empresa { get; private set; }
        public string TituloPalestra { get; private set; }

        private Palestrante()
        {
        }

        public Palestrante(string nome, string empresa, string tituloPalestra)
        {
            AtualizarInformacoes(nome, empresa, tituloPalestra);
        }

        public void AtualizarInformacoes(string nome, string empresa, string tituloPalestra)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ConflitoException("Nome é obrigatório");

            if (string.IsNullOrEmpty(empresa))
                throw new ConflitoException("Empresa é obrigatório");

            if (string.IsNullOrEmpty(tituloPalestra))
                throw new ConflitoException("Título da palestra é obrigatório");

            Nome = nome;
            Empresa = empresa;
            TituloPalestra = tituloPalestra;
        }
    }
}
