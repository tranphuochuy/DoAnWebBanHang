using MVCFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFood.DTO
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public List<CartItem> UpdatePlushCart(Product product, int num, List<CartItem> list)
        {
            foreach (CartItem item in list)
            {
                if (item.Product.Id == product.Id)
                {
                    item.Quantity += num;
                    return list;
                }
            }
            list.Add(new CartItem()
            {
                Quantity = num,
                Product = product
            });
            return list;
        }
        public List<CartItem> UpdateOneCartItem(Product product, List<CartItem> list)
        {
            foreach (CartItem item in list)
            {
                if (item.Product.Id == product.Id)
                {
                    item.Quantity ++;
                    return list;
                }
            }
            list.Add(new CartItem()
            {
                Quantity = 1,
                Product = product
            });
            return list;
        }
        public List<CartItem> UpdateCartItem(Product product,int num,List<CartItem> list)
        {
            foreach(CartItem item in list)
            {
                if (item.Product.Id == product.Id)
                {
                    item.Quantity=num;
                    return list;
                }
            }
            list.Add(new CartItem()
            {
                Quantity = num,
                Product = product
            });
            return list;
        }
        public int CountProductOnCart(List<CartItem> list)
        {
            if (list==null) return 0;
            int res = 0;
            foreach(CartItem item in list)
            {
                res += item.Quantity;
            }
            return res;
        }
        public long TotalPayCart(List<CartItem> list)
        {
            if (list == null) return 0;
            long res = 0;
            foreach (CartItem item in list)
                {
                    int price = (item.Product.PromotionPrice == 0) ? item.Product.UnitPrice : item.Product.PromotionPrice;
                    res += item.Quantity*price;
                }
            return res;
        }
    }
}