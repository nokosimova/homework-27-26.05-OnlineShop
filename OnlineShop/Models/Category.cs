using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace OnlineShop.Models
{
    public class Category
    {
        public int? CategoryId{get; set;}
        public string CategoryName {get; set;}
        public virtual ICollection<Product> Products { get; set; }

    }
}