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
        public ProductController(DataContext context)
        {
            data = context;
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
            if (Id == null) RedirectToAction("Index");
            if (data.Products.FirstOrDefault(i => i.ProductId == Id) == null) RedirectToAction("Index");
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
            if (data.Products.FirstOrDefault(i => i.ProductId == product.ProductId) == null) RedirectToAction("Index");
            data.Products.First(i => i.ProductId == product.ProductId).ProductName = product.ProductName;
            data.Products.First(i => i.ProductId == product.ProductId).CategoryId = product.CategoryId;
            data.Products.First(i => i.ProductId == product.ProductId).Price = product.Price;
            data.SaveChanges();
            return RedirectToAction("ShowList", new{Id = product.CategoryId});
        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null) RedirectToAction("Index");
            if (data.Products.FirstOrDefault(i => i.ProductId == Id) == null) return RedirectToAction("Error", new { error = "No product with such Id"});
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
        // GET: Product/Details/5
        [HttpPost]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
     }
}