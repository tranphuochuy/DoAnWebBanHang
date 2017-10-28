using MVCFood.DTO;
using MVCFood.Models;
using MVCFood.ViewModels.Home;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVCFood.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        public HomeController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Index(FormCollection req)
        {

            var _allTypeProduct = _db.TypeProducts.ToList();
            var _newProduct = _db.Products.OrderByDescending(x => x.DateEntered).Take(5).ToList();
            string search = req["search"];
            
            if (search!=null && search != "")
            {
                var _allProduct = _db.Products
                .Include(x => x.TypeProduct)
                .Where(x => x.Name.Contains(search))
                .ToList();
                
                
                var model = new HomeViewModel()
                {
                    AllProduct = _allProduct,
                    AllTypeProduct = _allTypeProduct,
                    NewProduct=_newProduct
                };
                ViewBag.Message = "Kết quả tìm kiếm từ khóa: '" +search +"'";
                return View(model);
            }
            else
            {
                var _allProduct = _db.Products
                .Include(x => x.TypeProduct)
                .ToList();
     
                var model = new HomeViewModel()
                {
                    AllProduct = _allProduct,
                    AllTypeProduct = _allTypeProduct,
                    NewProduct=_newProduct
                };
                ViewBag.Message = "Danh mục sản phẩm";
                return View(model);
            }
        }
        public ActionResult Detail(int id)
        {           
            var model = _db.Products.Include(x => x.TypeProduct).SingleOrDefault(x => x.Id == id);
            return View(model);
        }
        public ActionResult SaleOff()
        {
            return View();
        }
    }
}