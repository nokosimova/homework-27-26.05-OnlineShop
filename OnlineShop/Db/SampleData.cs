using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Models;

namespace OnlineShop.Db
{
    public static class SampleData
    {
        public static void InitializeCategory(DataContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category {CategoryName = "Все" },
                    new Category {CategoryName = "Овощи"},
                    new Category {CategoryName = "Фрукты"},
                    new Category {CategoryName = "Мясные продукты" },
                    new Category {CategoryName = "Молочные продукты"},
                    new Category {CategoryName = "Мучные издения"}
                    );
                context.SaveChanges();
            }
        }
        public static void InitializeProduct(DataContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product {ProductName = "Хлеб Маколли", CategoryId = 6, Price = 5},
                    new Product {ProductName = "Картошка русская", CategoryId = 2, Price = 8 },
                    new Product {ProductName = "Кефир Саодат", CategoryId = 5, Price = 6 }
                    );
                context.SaveChanges();
            }
        }
    }
}
