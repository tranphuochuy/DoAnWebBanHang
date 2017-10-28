using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCFood.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Security.Principal;
using System.IO;

namespace MVCFood.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _db;
        public AdminController()
        {
            _db = new ApplicationDbContext();
        }
        // GET: Admin/Admin
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var users = _db.Users.Where(c => c.Id == userid).Single();
            var viewmodel = new AdminViewModels
            {
                Users = users,
                AllTypeProduct = _db.TypeProducts.ToList()
        };
            return View(viewmodel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Products(int id)
        {
            var userid = User.Identity.GetUserId();
            var users = _db.Users.Where(c => c.Id == userid).Single();
            var viewmodel = new AdminViewModels
            {
                Users = users,
                AllTypeProduct = _db.TypeProducts.ToList(),
                AllProduct = _db.Products.Where(c => c.TypeProductId == id).ToList()
            };
            return View(viewmodel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddCategoryNew()
        {
            var userid = User.Identity.GetUserId();
            var users = _db.Users.Where(c => c.Id == userid).Single();
            List<TypeProduct> viewmodel = _db.TypeProducts.ToList();
            var viewmodels = new AdminViewModels
            {
                Users = users,
                AllTypeProduct = viewmodel
            };
            return View(viewmodels);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateCategory()
        {
           
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateCategory(TypeProduct typeProduct)
        {
            var categories = new TypeProduct
            {
                Name = typeProduct.Name
            };
            _db.TypeProducts.Add(categories);
            _db.SaveChanges();
            return RedirectToAction("AddCategoryNew");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditCategory(int id)
        {
            var categories = _db.TypeProducts.Single(c => c.Id == id);
            var category = new TypeProduct
            {
                Id = categories.Id,
                Description=categories.Description,
                Name = categories.Name
            };
            return View("CreateCategory", category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditCategory(TypeProduct typeProduct)
        {
            var categories = _db.TypeProducts.Single(c => c.Id == typeProduct.Id);
            categories.Id = typeProduct.Id;
            categories.Description = typeProduct.Description;
            categories.Name = typeProduct.Name;
            _db.SaveChanges();
            return RedirectToAction("AddCategoryNew");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCategory(int id)
        {
            var categories = _db.TypeProducts.Single(c => c.Id == id);
            _db.TypeProducts.Remove(categories);
            _db.SaveChanges();
            return RedirectToAction("AddCategoryNew");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateProducts()
        {
            var viewmodel = new ViewModelProduct
            {
                AllTypeProduct = _db.TypeProducts.ToList()
            };
            return View(viewmodel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateProducts(ViewModelProduct viewmodel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Assets/Personal/image/"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            var product = new Product
            {
                Name=viewmodel.Name,
                Description=viewmodel.Description,
                UnitPrice=viewmodel.UnitPrice,
                PromotionPrice=viewmodel.PromotionPrice,
                Image=viewmodel.Image,
                DateEntered=viewmodel.DateEntered,
                Quantum=viewmodel.Quantum,
                TypeProductId=viewmodel.TypeProductId
            };
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Products", new { id = viewmodel.TypeProductId });
        }
    }
}