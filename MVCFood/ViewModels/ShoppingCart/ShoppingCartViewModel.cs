using MVCFood.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFood.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        [Required]
        public string DeliveryDate { get; set; }
        [Required]
        public string DeliveryTime { get; set; }
        public string Note { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        public DateTime GetDeliveryDate()
        {
            return DateTime.Parse(DeliveryDate + " " + DeliveryTime);
        }

        
    }
   
}