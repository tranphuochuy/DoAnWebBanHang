    using MVCFood.DTO;
using MVCFood.Models;
using MVCFood.ViewModels.Home;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVCFood.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        public ProductController()
        {
            _db = new ApplicationDbContext();
        }
        public JsonResult Detail(int id)
        {
            var model = _db.Products.Include(x => x.TypeProduct).SingleOrDefault(x => x.Id == id);
            return Json(model,JsonRequestBehavior.AllowGet);
        }
    }
}