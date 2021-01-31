using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperChat.BL.DTOs;
using SuperChat.Identity;
using SuperChat.Services.ChatRoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IMapper _mapper;
        public ChatsController(IChatRoomService chatRoomService,
                            IMapper mapper)
        {
            _chatRoomService = chatRoomService;
            _mapper = mapper;
        }
        // GET: Chats
        public async Task<ActionResult> Index()
        {
            var chatRooms = await _chatRoomService.GetChatRooms();
            //
            return View("Index", chatRooms);
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
