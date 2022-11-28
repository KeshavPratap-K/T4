

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FastBite.Data;
using FastBite.Models;
using Microsoft.AspNetCore.Authorization;
using FastBite.Utility;
using System.Collections.Generic;

namespace FastBite.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCatagoryList = _db.Category;
            return View(objCatagoryList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            _db.Category.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catFromDb = _db.Category.Find(id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            return View(catFromDb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            _db.Category.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catFromDb = _db.Category.Find(id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            return View(catFromDb);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var catFromDb = _db.Category.Find(id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            _db.Category.Remove(catFromDb);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}