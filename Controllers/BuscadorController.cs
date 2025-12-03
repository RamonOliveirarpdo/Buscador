using Buscador.Dtos;
using Buscador.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Buscador.Controllers
{
    public class BuscadorController : Controller
    {
        private readonly IBuscadorAplication _buscadorAplication;

        public BuscadorController(IBuscadorAplication buscadorAplication)
        {
            _buscadorAplication = buscadorAplication;
        }

        // GET: BuscadorController
        [HttpGet("pesquisa Situacoes")]
        public async Task<List<SituacaoDto>> BuscadorControllers(string pesquisa)
        {
            var response = await _buscadorAplication.BuscarSituacoesAsync(pesquisa);
            return response;
        }

        // POST: HomeController/Create
        [HttpPost("Adiciona Situacoes")]
        public async Task<List<SituacaoDto>> CriaSituacao(CriarSituacaoDto add)
        {
            var response = new List<SituacaoDto>();
            try
            {
                response = await _buscadorAplication.CriaSituacoesAsync(add);

               return response;
            }
            catch (DbUpdateException ex)
            {
                return response;
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
