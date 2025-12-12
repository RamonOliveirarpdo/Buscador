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
        public async Task<UserResponse> GetId(int userId)
        {
            var user = await _userAplication.GetUserNameAsync(userId);

            return user;
        }

        /// <summary>
        /// Realiza a criação de usuario.
        /// </summary>
        // POST: UserController/Details/5
        [HttpPost("cria-usuarios")]
        public async Task<UserResponse> CreateUser(UserRequest user)
        {
            var data = await _userAplication.CreateUserAsync(user);

            return data;
        }
        
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
