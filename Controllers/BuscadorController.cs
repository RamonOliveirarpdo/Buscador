using Buscador.Dtos;
using Buscador.Dtos.Requests;
using Buscador.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Buscador.Controllers
{
    [ApiController]
    [Route("buscador")]
    public class BuscadorController : ControllerBase
    {
        private readonly IBuscadorAplication _buscadorAplication;

        public BuscadorController(IBuscadorAplication buscadorAplication)
        {
            _buscadorAplication = buscadorAplication;
        }

        /// <summary>
        /// Realiza a pesquisa de situação com base no id.
        /// </summary>
        // GET: BuscadorController
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SituacaoDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _buscadorAplication.BuscarSituacoesAsync(id);

            return Ok(response);
        }

        /// <summary>
        /// Realiza a pesquisa de uma lista de situações com base em um critério de busca.
        /// </summary>
        // GET: BuscadorController
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<SituacaoDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ListAll([FromQuery] string pesquisa)
        {
            var response = await _buscadorAplication.ListAllAsync(pesquisa);

            return Ok(response);
        }

        /// <summary>
        /// Adiciona situações.
        /// </summary>
        // POST: BuscadorController/Create
        [HttpPost("")]
        [ProducesResponseType(typeof(SituacaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CriarSituacao([FromBody] CriarSituacaoRequest request)
        {
            var response = await _buscadorAplication.CriaSituacoesAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Deleta situacoes pelo Id.
        /// </summary>
        /// DELETE: BuscadorController/Delete/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            await _buscadorAplication.DeleteSituacoesAsync(id);

            return NoContent();
        }
    }
}
