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
        public HomeController(DataContext context)
        {
            data = context;
        }

        public IActionResult Index()
        {
            return View(data.Categories.ToList());
        }      
    }
}
