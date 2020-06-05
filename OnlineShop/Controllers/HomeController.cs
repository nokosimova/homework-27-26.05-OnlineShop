using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Db;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        DataContext data;
        public static List<Product> orderList { get; set; }
        public HomeController(DataContext context)
        {
            data = context;
            orderList = new List<Product>();
        }

        public IActionResult Index()
        {
            ViewBag.CategoryId = 0;
            return View(data.Categories.ToList());
        }      
    }
}
