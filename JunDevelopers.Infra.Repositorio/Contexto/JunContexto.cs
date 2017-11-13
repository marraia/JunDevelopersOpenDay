using JunDevelopers.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace JunDevelopers.Infra.Repositorio.Contexto
{
    public class JunContexto : DbContext
    {
        public DbSet<Palestrante> Palestrante { get; set; }
        public JunContexto(DbContextOptions<JunContexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento de tabelas
            modelBuilder
                .Entity<Palestrante>(entity =>
                {
                    entity
                    .ToTable("Palestrantes");
                });
        }
    }
}
