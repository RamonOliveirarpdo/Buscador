using Buscador.Dtos;
using Buscador.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Buscador.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserAplication _userAplication;

        public UserController(IUserAplication userAplication)
        {
            _userAplication = userAplication;
        }

        /// <summary>
        /// Realiza a pesquisa de usuario por id.
        /// </summary>
        // GET: UserController
        [HttpGet("pesquisa-usuarios")]
        public async Task<UserDto> GetId(int userId)
        {
            var user = await _userAplication.GetUserNameAsync(userId);

            return user;
        }

        //// GET: UserController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        
        //// GET: UserController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //
        //// POST: UserController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
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
        //// GET: UserController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}
        //
        //// POST: UserController/Edit/5
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
        //// GET: UserController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}
        //
        //// POST: UserController/Delete/5
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
