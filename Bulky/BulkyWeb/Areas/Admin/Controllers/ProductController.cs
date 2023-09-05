using Bulky.DataAccess;
using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var objProductList = _unitOfWork.productRepository.GetAll(includeProperties: "Category").ToList();
            
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            //ViewBag.CategoryList = CategoryList;
            //ViewData[nameof(CategoryList)] = CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                // create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.productRepository.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    
                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the older image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.productRepository.Add(productVM.Product);
                    TempData["success"] = "Product created successfully";
                }
                else
                {
                    _unitOfWork.productRepository.Update(productVM.Product);
                    TempData["success"] = "Product updated successfully";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
            
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.productRepository.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.productRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var objCategoryList = _unitOfWork.productRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objCategoryList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productToBeDeleted = _unitOfWork.productRepository.Get(u => u.Id == id);
            if(productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.productRepository.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { Success = true, message = "Deleted Successfully." });
        }
        #endregion
    }
}
