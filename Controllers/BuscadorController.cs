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
        public async Task<IActionResult> CriarSituacao(CriarSituacaoDto add)
        {
            try
            {
                var response = await _buscadorAplication.CriaSituacoesAsync(add);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        /// <summary>
        /// Deleta situaxcoes pelo Id.
        /// </summary>
        /// DELETE: BuscadorController/Delete/5
        [HttpDelete("situacoes/{id}")]
        public async Task<HttpStatusCode> Delete(int id)
        {
            var result = HttpStatusCode.NoContent;
            try
            {
                result = await _buscadorAplication.DeleteSituacoesAsync(id);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

            return result;
        }
        
        //// POST: HomeController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
