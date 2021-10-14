using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{               //first part of our path. (localhost:xxxxx/Note)
    public class NoteController : Controller
    {
        [Authorize]
        // GET: Note
               //return type //localhost:xxxxx/Note/Index.
        public ActionResult Index()
        {
            var model = new NoteListItem[0];
            //return a view for that path
            return View(model);
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if(ModelState.IsValid)
            {

            }

            return View(model);
        }
    }
}