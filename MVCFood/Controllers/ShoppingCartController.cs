using Microsoft.AspNet.Identity;
using MVCFood.DTO;
using MVCFood.Models;
using MVCFood.ViewModels.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFood.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _db;
        public ShoppingCartController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Payment()
        {           
                return View();          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishPayment(ShoppingCartViewModel req)
        {
            Customer customer; 
            List<CartItem> list = (List<CartItem>)Session["CartShop"];
            CartItem temp = new CartItem();
            var check = _db.Customers.SingleOrDefault(x => x.Email == req.Email);
            if (check == null)
            {
                customer =  new Customer()
                {
                    Name = User.Identity.Name,
                    Email = User.Identity.GetUserName(),
                    Sex = req.Sex,
                    Address = req.Address,
                    Phone = req.Phone,
                };
                _db.Customers.Add(customer);
            }
            else
            {
                customer = check;
            }


            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = req.GetDeliveryDate(),
                TotalPay = temp.TotalPayCart(list),
                State = false,
                Note=req.Note,
                CustomerId = customer.Id
            };
            _db.Orders.Add(order);

            foreach (CartItem item in list)
            {
                int price = (item.Product.PromotionPrice == 0) ? item.Product.UnitPrice : item.Product.PromotionPrice;
                OrderDetail detail = new OrderDetail()
                {
                    Quantity = item.Quantity,
                    Price = price,
                    ProductId = item.Product.Id,
                    OrderId = order.Id
                };
                _db.OrderDetails.Add(detail);
            }
            Session["CartShop"] = null;
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult PaymentNew()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishNewPayment(ShoppingCartViewModel req)
        {
            Customer customer;
            List<CartItem> list = (List<CartItem>)Session["CartShop"];
            CartItem temp = new CartItem();
            var check = _db.Customers.SingleOrDefault(x => x.Email == req.Email);
            if (check == null)
            {
                customer = new Customer()
                {
                    Name = req.Name,
                    Email =req.Email,
                    Sex = req.Sex,
                    Address = req.Address,
                    Phone = req.Phone,
                };
                _db.Customers.Add(customer);
            }
            else
            {
                customer = check;
            }
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                DeliveryDate = req.GetDeliveryDate(),
                TotalPay = temp.TotalPayCart(list),
                State = false,
                Note=req.Note,
                CustomerId = customer.Id
            };
            _db.Orders.Add(order);

            foreach (CartItem item in list)
            {
                int price = (item.Product.PromotionPrice == 0) ? item.Product.UnitPrice : item.Product.PromotionPrice;
                OrderDetail detail = new OrderDetail()
                {
                    Quantity = item.Quantity,
                    Price = price,
                    ProductId = item.Product.Id,
                    OrderId = order.Id
                };
                _db.OrderDetails.Add(detail);
            }
            Session["CartShop"] = null;
            _db.SaveChanges();
            return RedirectToAction("Index","Home");

        }
        public ActionResult ListCart()
        {
            List<CartItem> list;
            CartItem temp = new CartItem();
            if (Session["CartShop"] == null)
            {
                list = new List<CartItem>();
            }
            else
            {
                list = (List<CartItem>)Session["CartShop"];
            }
            
            Session["CartShop"] = list;            
            return View(list);
        }
        public ActionResult RemoveCartItem(int id)
        {
            List<CartItem> list;
            if (Session["CartShop"] == null)
            {
                list = new List<CartItem>();
            }
            else
            {
                list = (List<CartItem>)Session["CartShop"];
            }
            for(int i=0;i<list.Count;i++)
            {
                if (list[i].Product.Id == id)
                {
                    if(list.Count==1) Session["CartShop"] = null;
                    else
                    {
                        list.Remove(list[i]);
                        Session["CartShop"] = list;
                    }
                }
                
            }
            return RedirectToAction("ListCart");
        }
        public JsonResult GetListCart()
        {
            List<CartItem> list;           
            if (Session["CartShop"] == null)
            {
                list = new List<CartItem>();

            }
            else
            {
                list = (List<CartItem>)Session["CartShop"];
            }
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddToCart(int id)
        {
            List<CartItem> list;
            CartItem temp = new CartItem();
            Product product = _db.Products.Find(id);
            if (Session["CartShop"] == null)
            {
                list = new List<CartItem>();

            }
            else
            {
                list = (List<CartItem>)Session["CartShop"];
            }
            list = temp.UpdateOneCartItem(product, list);
            Session["CartShop"] = list;
            int cartcount = temp.CountProductOnCart(list);
            long totalpay = temp.TotalPayCart(list);
            return Json(new { CartCount = cartcount, TotalPay = totalpay });
        }
        public JsonResult AddManyCart(int id,int num)
        {
            List<CartItem> list;
            CartItem temp = new CartItem();
            Product product = _db.Products.Find(id);
            if (Session["CartShop"] == null)
            {
                list = new List<CartItem>();

            }
            else
            {
                list = (List<CartItem>)Session["CartShop"];
            }
            list = temp.UpdateCartItem(product, num, list);
            Session["CartShop"] = list;
            int cartcount = temp.CountProductOnCart(list);
            long totalpay = temp.TotalPayCart(list);
            return Json(new { CartCount = cartcount, TotalPay = totalpay });
        }
        public JsonResult AddManyCartPlush(int id, int num)
        {
            List<CartItem> list;
            CartItem temp = new CartItem();
            Product product = _db.Products.Find(id);
            if (Session["CartShop"] == null)
            {
                list = new List<CartItem>();

            }
            else
            {
                list = (List<CartItem>)Session["CartShop"];
            }
            list = temp.UpdatePlushCart(product, num, list);
            Session["CartShop"] = list;
            int cartcount = temp.CountProductOnCart(list);
            long totalpay = temp.TotalPayCart(list);
            return Json(new { CartCount = cartcount, TotalPay = totalpay });
        }
        public ActionResult GetCartShop()
        {
            List<CartItem> list = (List<CartItem>)Session["CartShop"];
            CartItem temp = new CartItem();
            ViewBag.CartCount = temp.CountProductOnCart(list);
            ViewBag.TotalPay = temp.TotalPayCart(list);
            return PartialView("_CartShop");
        }
    }
}