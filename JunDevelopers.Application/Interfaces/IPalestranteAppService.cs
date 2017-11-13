using JunDevelopers.Application.Input;
using JunDevelopers.Dominio.Modelos;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JunDevelopers.Application.Interfaces
{
    public interface IPalestranteAppService
    {
        Task<Palestrante> SelecionarPorId(int id);
        Task<List<Palestrante>> Selecionar();
        Task<Palestrante> Adicionar(PalestranteInput obj);
        Task Excluir(int id);
        Task<Palestrante> Atualizar(int id, PalestranteInput obj);
        Task<Palestrante> Atualizar(int id, JsonPatchDocument<Palestrante> obj);
    }
}
