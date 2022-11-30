using System.Collections.Generic;

namespace FastBite.Models.Viewmodels
{
    public class CategoryAndSubCategoryModel
    {
        public IEnumerable<Category> categoryList{get;set;}
        public int SelectedCategoryId { get; set; }
        public SubCategory subCategory{get;set;}
        public List<SubCategory> subCategoryList{get;set;}
        public string errorMessage{get;set;}
    }
}