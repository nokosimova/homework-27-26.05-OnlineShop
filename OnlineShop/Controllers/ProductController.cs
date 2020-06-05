using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        DataContext data { get; set; }
        static List<Product> orderList { get; set; }

        public ProductController(DataContext context)
        {
            data = context;
        }
        
        public IActionResult AddToOrder(int? id)
        {
            Product newProduct = data.Products.FirstOrDefault(i => i.ProductId == id);
            if (newProduct == null)
                return RedirectToAction("Error", new { error = "No product with such Id" });
            Startup.orderList.Add(newProduct);
            return RedirectToAction("ShowList", new { Id = newProduct.CategoryId });
        }

        public IActionResult DeleteFromOrder(int? Id)
        {            
            if (Id == null || Startup.orderList.FirstOrDefault(i => i.ProductId == Id) == null)
                return RedirectToAction("Error", new { error = "No such product in order" });
            Product newProduct = Startup.orderList.FirstOrDefault(i => i.ProductId == Id);
            Startup.orderList.Remove(newProduct);
            return RedirectToAction("Buy");
        }

        public IActionResult Index()
        {
            return View(data.Categories.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(data.Categories.ToList());            
        }

        public IActionResult Buy()
        {
            return View(Startup.orderList);
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            data.Products.Add(product);
            data.SaveChanges();
            return RedirectToAction("Add");
        }
        [HttpGet]
        public IActionResult Change(int? Id)
        {
            if (Id == null || data.Products.FirstOrDefault(i => i.ProductId == Id) == null)
                return RedirectToAction("Error", new { error = "No product with such Id"});  
            Product product = data.Products.FirstOrDefault(i => i.ProductId == Id);
            ProductCategoryModel model = new ProductCategoryModel
            {
                product = product,
                Categories = data.Categories,
                Products = data.Products
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Change(Product product)
        {
            if (data.Products.FirstOrDefault(i => i.ProductId == product.ProductId) == null)
                return RedirectToAction("Error", new { error = "No product to change" });
            data.Products.First(i => i.ProductId == product.ProductId).ProductName = product.ProductName;
            data.Products.First(i => i.ProductId == product.ProductId).CategoryId = product.CategoryId;
            data.Products.First(i => i.ProductId == product.ProductId).Price = product.Price;
            data.SaveChanges();
            return RedirectToAction("ShowList", new{Id = product.CategoryId});
        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || data.Products.FirstOrDefault(i => i.ProductId == Id) == null)
                return RedirectToAction("Error", new { error = "No product with such Id"});
            Product product = data.Products.FirstOrDefault(i => i.ProductId == Id);
            data.Products.Remove(product);
            data.SaveChanges();
            return RedirectToAction("ShowList", new { Id = product.CategoryId });
        }

        public string Error(string error)
        {
            return $"ERROR MESSAGE:{error} ";
        }

        [HttpGet]
        public IActionResult ShowList(int? Id)
        {
            ViewBag.Order = orderList;
            ViewBag.Id = 0;
            if (Id == null) return View(new ProductCategoryModel());
            var list = (Id == 1) ? data.Products.ToList() : data.Products.Where(i => i.CategoryId == Id).ToList();
            Product product = data.Products.FirstOrDefault(i => i.ProductId == Id);
            ProductCategoryModel model = new ProductCategoryModel
            {
                product = product,
                Categories = data.Categories,
                Products = list
            };
            return View(model);
                  
        }
     }
}