using JunDevelopers.Application.Input;
using JunDevelopers.Application.Interfaces;
using JunDevelopers.Dominio.Modelos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JunDevelopers.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Palestrante")]
    public class PalestranteController : Controller
    {
        private IPalestranteAppService _appService { get; }
        public PalestranteController(IPalestranteAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("{id}", Name = "SelecionarPorId")]
        [ProducesResponseType(typeof(Palestrante), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _appService.SelecionarPorId(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Palestrante>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _appService.Selecionar());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Palestrante), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Post([FromBody] PalestranteInput obj)
        {
            var retorno = await _appService.Adicionar(obj);
            return CreatedAtRoute("SelecionarPorId", new { id = retorno.Id }, retorno);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Put(int id, [FromBody]PalestranteInput obj)
        {
            var retorno = await _appService.Atualizar(id, obj);
            return Accepted(retorno);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Palestrante> obj)
        {
            var retorno = await _appService.Atualizar(id, obj);
            return Accepted(retorno);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            await _appService.Excluir(id);
            return Ok();
        }
    }
}