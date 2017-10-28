using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCFood.Models;

namespace MVCFood.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<Product>  AllProduct { get; set; }
        public List<TypeProduct> AllTypeProduct { get; set; }
        public IEnumerable<Product> NewProduct { get; set; }

    }
}