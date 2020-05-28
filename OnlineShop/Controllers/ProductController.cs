﻿using System;
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
            ViewBag.CategoryName = "";
            return View(data.Categories.ToList());            
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            data.Products.Add(product);
            data.SaveChanges();
            return Redirect("ShowList");
        }
        [HttpGet]
        public IActionResult Change(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");
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
        public string Change(Product product)
        {
            data.Products.First(i => i.ProductId == product.ProductId).ProductName = product.ProductName;
            data.Products.First(i => i.ProductId == product.ProductId).CategoryId = product.CategoryId;
            data.Products.First(i => i.ProductId == product.ProductId).Price = product.Price;
            data.SaveChanges();
            return "Данные изменены";
        }
        [HttpGet]
        public IActionResult ShowList(int? Id)
        {
            if (Id == null) return View(new List<Product>());
            var list = (Id == 1) ? data.Products.ToList() : data.Products.Where(i => i.CategoryId == Id).ToList();
            return View(list);
             
                  
        }

       //     return View(data.Products.Where(i => i.CategoryId = )ToList());
        
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}