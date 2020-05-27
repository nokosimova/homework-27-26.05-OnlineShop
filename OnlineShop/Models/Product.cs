using System;
using Microsoft.EntityFrameworkCore;


namespace OnlineShop.Models
{
    public class Product
    {
        public int ProductId{get; set;}
        public string ProductName {get; set;}
        public int CategoryId {get; set;}
        public decimal Price {get; set;}    
        
        public virtual Category Category { get; set; }
    }
}