using JunDevelopers.Application.Interfaces;
using JunDevelopers.Application.Input;
using JunDevelopers.Dominio.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using JunDevelopers.Dominio.Interfaces;
using JunDevelopers.Dominio.Excecoes;
using Microsoft.AspNetCore.JsonPatch;

namespace JunDevelopers.Application
{
    public class PalestranteAppService : IPalestranteAppService
    {
        private IPalestranteRepositorio _palestranteRepositorio { get; }
        public PalestranteAppService(IPalestranteRepositorio palestranteRepositorio)
        {
            _palestranteRepositorio = palestranteRepositorio;
        }

        public async Task<Palestrante> Adicionar(PalestranteInput obj)
        {
            var palestrante = new Palestrante(obj.Nome, obj.Empresa, obj.TituloPalestra);
            await _palestranteRepositorio.Inserir(palestrante);

            return palestrante;
        }

        public async Task<Palestrante> Atualizar(int id, PalestranteInput obj)
        {
            var palestrante = await ObterPalestrante(id);
            palestrante.AtualizarInformacoes(obj.Nome, obj.Empresa, obj.TituloPalestra);

            await _palestranteRepositorio.Alterar(palestrante);

            return palestrante;
        }

        public async Task<Palestrante> Atualizar(int id, JsonPatchDocument<Palestrante> obj)
        {
            var palestrante = await ObterPalestrante(id);
            obj.ApplyTo(palestrante);

            await _palestranteRepositorio.Alterar(palestrante);

            return palestrante;
        }

        public async Task Excluir(int id)
        {
            var palestrante = await ObterPalestrante(id);
            await _palestranteRepositorio.Deletar(palestrante);
        }

        public async Task<List<Palestrante>> Selecionar()
        {
            return await _palestranteRepositorio.Selecionar();
        }

        public async Task<Palestrante> SelecionarPorId(int id)
        {
            return await ObterPalestrante(id);
        }

        async Task<Palestrante> ObterPalestrante(int id)
        {
            var palestrante = await _palestranteRepositorio.SelecionarPorId(id);
            if (palestrante == null)
                throw new NaoEncontradoException("Palestrante não encontrado");

            return palestrante;
        }
    }
}
