using Buscador.Dtos;
using Buscador.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var response = new SituacaoDto();
            try
            {
                response =  await _buscadorAplication.CriaSituacoesAsync(add);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        //// GET: HomeController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}
        //
        //// POST: HomeController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
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
        //
        //// GET: HomeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}
        //
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
