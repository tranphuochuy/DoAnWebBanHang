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
        public ActionResult AddCategoryNew()
        {
            List<TypeProduct> viewmodel = _db.TypeProducts.ToList();
            var viewmodels = new AdminViewModels
            {
                AllTypeProduct = viewmodel,
            };
            return View(viewmodels);
        }
    }
}