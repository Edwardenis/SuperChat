using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperChat.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.Controllers
{
    public class ChatsController : Controller
    {
        // GET: Chats
        public ActionResult Index()
        {
            var token = User.Identity.GetToken();

            return View();
        }

        // GET: Chats/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Chats/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chats/Edit/5
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

        // GET: Chats/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chats/Delete/5
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
