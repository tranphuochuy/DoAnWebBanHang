using MVCFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFood.Areas.Admin
{
    public class AdminViewModels
    {
        public ApplicationUser Users { get; set; }
        public IEnumerable<Product> AllProduct { get; set; }
        public List<TypeProduct> AllTypeProduct { get; set; }
    }
}