﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFood.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitPrice { get; set; }
        public int PromotionPrice { get; set; }
        public string Image { get; set; }
        public DateTime DateEntered { get; set; }
        public int Quantum { get; set; }
        public TypeProduct TypeProduct { get; set; }
        public int TypeProductId { get; set; }
    }
}