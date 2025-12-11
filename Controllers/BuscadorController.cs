using Buscador.Dtos;
using Buscador.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Buscador.Controllers
{
    public class BuscadorController : ControllerBase
    {
        private readonly IBuscadorAplication _buscadorAplication;

        public BuscadorController(IBuscadorAplication buscadorAplication)
        {
            _buscadorAplication = buscadorAplication;
        }

        /// <summary>
        /// Realiza a pesquisa de situações com base em um critério de busca.
        /// </summary>
        // GET: BuscadorController
        [HttpGet("pesquisa-situacoes")]
        public async Task<List<SituacaoDto>> BuscarSituacoes(string pesquisa)
        {
            var response = await _buscadorAplication.BuscarSituacoesAsync(pesquisa);
            
            return response;
        }

        /// <summary>
        /// Adiciona situações.
        /// </summary>
        // POST: BuscadorController/Create
        [HttpPost("adiciona-situacoes")]
        [ProducesResponseType(typeof(SituacaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CriarSituacao([FromBody] CriarSituacaoDto add)
        {
            try
            {
                var response = await _buscadorAplication.CriaSituacoesAsync(add);

                return CreatedAtAction(nameof(CriarSituacao), response);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deleta situaxcoes pelo Id.
        /// </summary>
        /// DELETE: BuscadorController/Delete/5
        [HttpDelete("situacoes/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _buscadorAplication.DeleteSituacoesAsync(id);

                if(!await _buscadorAplication.DeleteSituacoesAsync(id))
                {

                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { mensagem = "Erro interno ao processar a requisição.", detalhe = ex.Message });
            }
        }
    }
}
