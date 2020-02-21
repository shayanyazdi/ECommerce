using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        // localhost:5000
        [HttpGet("")]
        public IActionResult Dashboard()
        {

            ViewBag.AllProducts = dbContext.Products.ToList();
            ViewBag.AllCustomers = dbContext.Customers.ToList();
            var PCreatedAt = dbContext.Products
            .Include(p => p.Purchases)
            .ThenInclude(p => p.CreatedAt);
            // ViewBag.WhenOrdered = DateTime.Now - PCreatedAt.CreatedAt;

            return View();
        }

        // localhost:5000/product
        [HttpGet("product")]
        public IActionResult Product()
        {

            ViewBag.AllProducts = dbContext.Products.ToList();
            ViewBag.AllCustomers = dbContext.Customers.ToList();

            return View();
        }

        // localhost:5000/newproduct
        [HttpPost("newproduct")]
        public IActionResult AddProduct(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newProduct);
                dbContext.SaveChanges();

                return RedirectToAction("Product");
            }
            return View();
        }

        // localhost:5000/order
        [HttpGet("order")]
        public IActionResult Order()
        {

            ViewBag.AllProducts = dbContext.Products.ToList();
            ViewBag.AllCustomers = dbContext.Customers.ToList();

            ViewBag.AllProdPer = dbContext.Purchases
            .Include(p => p.Product)
            .Include(p => p.Customer)
            .ToList();

            return View();
        }

        // localhost:5000/createorder
        [HttpPost("createorder")]
        public IActionResult AddPurchase(Purchase newPurchase)
        {
            if (ModelState.IsValid)
            {
                var ProductOrdered = dbContext.Products.FirstOrDefault(p => p.ProductId == newPurchase.ProductId);
                if (ProductOrdered.Amount >= newPurchase.Quantity)
                {
                    ProductOrdered.Amount -= newPurchase.Quantity;

                    dbContext.Add(newPurchase);
                    dbContext.SaveChanges();

                    return RedirectToAction("Order");
                }
            }
            ModelState.AddModelError("Quantity", "Inventory is low, can't order that many items");
            ViewBag.AllProducts = dbContext.Products.ToList();
            ViewBag.AllCustomers = dbContext.Customers.ToList();

            return View("Order");
        }

        // localhost:5000/customer
        [HttpGet("customer")]
        public IActionResult Customer()
        {

            ViewBag.AllCustomers = dbContext.Customers.ToList();

            return View();
        }

        // localhost:5000/newcustomer
        [HttpPost("newcustomer")]
        public IActionResult AddCustomer(Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                var please = dbContext.Customers.FirstOrDefault(p => p.FirstName == newCustomer.FirstName && p.LastName == newCustomer.LastName);
                if (please != null)
                {

                    if (newCustomer.FirstName == please.FirstName && newCustomer.LastName == please.LastName)
                    {
                        ModelState.AddModelError("FirstName", "Don't FUCKING impersonate!!!! Bitch");
                        ViewBag.AllCustomers = dbContext.Customers.ToList();
                        return View("Customer");
                    }
                    else
                    {
                        dbContext.Add(newCustomer);
                        dbContext.SaveChanges();

                        return RedirectToAction("Customer");
                    }
                }
                dbContext.Add(newCustomer);
                dbContext.SaveChanges();

                return RedirectToAction("Customer");
            }
            ViewBag.AllCustomers = dbContext.Customers.ToList();
            return View("Customer");
        }

        // localhost:5000/deletecustomer
        [HttpGet("delete/{Id}")]
        public IActionResult DeleteCustomer(int Id)
        {
            var userToDelete = dbContext.Customers.FirstOrDefault(p => p.CustomerId == Id);
            dbContext.Remove(userToDelete);
            dbContext.SaveChanges();

            return RedirectToAction("Customer");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
