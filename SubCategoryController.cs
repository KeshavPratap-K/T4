using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using FastBite.Data;
using FastBite.Models;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FastBite.Models.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using FastBite.Utility;
using FastBite.Models.Viewmodels;


//using System.Web.Mvc;

namespace FastBite.Areas.Admin.Controllers
{
    //  [Authorize(Roles=StaticDefinitions.Admin)]
    [Area("Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        private string errorMessage { get; set; }

        public SubCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<SubCategory> objSubCatagoryList = _db.SubCategory;
            //var list = _db.SubCategory.AsQueryable().ToList();
            //var objCatagoryList = _db.SCategory.Find(1);
            foreach (var obj in objSubCatagoryList)
            {
                obj.category = _db.Category.Find(obj.CategoryId);
            }
            return View(objSubCatagoryList);
        }

        public IActionResult Create()
        {

            var categoryAndSubCategoryModel = new CategoryAndSubCategoryModel();
            categoryAndSubCategoryModel.categoryList = _db.Category;
            //categoryAndSubCategoryModel.subCategoryList = _db.SubCategory;
            List<SubCategory> SubCatlist = _db.SubCategory.AsQueryable().ToList();
            categoryAndSubCategoryModel.subCategoryList = SubCatlist;
            return View(categoryAndSubCategoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryAndSubCategoryModel obj)
        {
            var subcat = new SubCategory();
            subcat.Name = obj.subCategory.Name;
            subcat.CategoryId = obj.SelectedCategoryId;

            Category objCatagoryList = _db.Category.Find(obj.SelectedCategoryId);
            subcat.category = objCatagoryList;

            _db.SubCategory.Add(subcat);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var SubcatFromDb = _db.SubCategory.Find(id);
            
            if (SubcatFromDb == null)
            {
                return NotFound();
            }
            CategoryAndSubCategoryModel categoryAndSubCategory = new CategoryAndSubCategoryModel();
            categoryAndSubCategory.subCategory = SubcatFromDb;
            categoryAndSubCategory.categoryList = _db.Category;
            List<SubCategory> SubCatlist = _db.SubCategory.AsQueryable().ToList();
            categoryAndSubCategory.subCategoryList = SubCatlist;

            return View(categoryAndSubCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryAndSubCategoryModel obj)
        {
            //_db.SubCategory.Update(obj.subCategory);
            var subcat = new SubCategory();
            subcat.Name = obj.subCategory.Name;
            subcat.CategoryId = obj.SelectedCategoryId;
            subcat.Id = obj.subCategory.Id;

            Category objCatagoryList = _db.Category.Find(obj.SelectedCategoryId);
            subcat.category = objCatagoryList;

            _db.SubCategory.Update(subcat);
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
            var catFromDb = _db.SubCategory.Find(id);
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
            var catFromDb = _db.SubCategory.Find(id);
            if (catFromDb == null)
            {
                return NotFound();
            }
            _db.SubCategory.Remove(catFromDb);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

    }
}