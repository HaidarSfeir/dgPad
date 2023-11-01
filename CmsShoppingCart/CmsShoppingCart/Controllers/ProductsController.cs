using CmsShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace CmsShoppingCart.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly CmsShoppingCartContext context;

        public ProductsController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        //GET /products
        public async Task<IActionResult> Index(int p = 1)
        {

            int pageSize = 3;
            var products = context.Products.
                OrderByDescending(x => x.Id)
                .Skip((p - 1) * pageSize)
                .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Products
                .Count() / pageSize);

            return View(await products.ToListAsync());
        }


        //GET /products/ category
        public async Task<IActionResult> ProductsByCategory(string categorySlug,int p = 1)
        {
            Category category = await context.Categories.
                Where(x => x.Slug == categorySlug).FirstOrDefaultAsync();

            if (category == null) 
            {
               return RedirectToAction("Index");
            }

            int pageSize = 3;
            var products = context.Products.
                OrderByDescending(x => x.Id)
                .Where(x => x.CategoryId == category.Id)
                .Skip((p - 1) * pageSize)
                .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.
                Ceiling((decimal)context.Products
                .Where(x => x.CategoryId == category.Id)
                .Count() / pageSize);
           /* ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = category.Slug;*/

            return View(await products.ToListAsync());
        }

        public IActionResult OrderByPrice(string sortOrder)
        {
            IQueryable<Product> products;

            if (sortOrder == "ascending")
            {
                products = context.Products.OrderBy(p => p.Price);
            }
            else
            {
                products = context.Products.OrderByDescending(p => p.Price);
            }

            return View("Index", products.ToList());
        }


        [Route("/products/details")]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            return View(product);
        }


        public IActionResult OrderByPriceCategory(string sortOrder, string categorySlug)
        {
            int pageSize = 3;
            ViewBag.PageNumber = 1; // Initialize the page number for sorting
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = categorySlug;

            IQueryable<Product> products;

            if (string.IsNullOrEmpty(categorySlug))
            {
                // No category specified, return an empty list or handle it as needed.
                return View("ProductsByCategory", new List<Product>());
            }

            Category category = context.Categories.SingleOrDefault(c => c.Slug == categorySlug);

            if (category == null)
            {
                // Category not found, handle it as needed (e.g., show an error message).
                return View("ProductsByCategory", new List<Product>());
            }

            if (sortOrder == "ascending")
            {
                products = context.Products
                    .Where(p => p.CategoryId == category.Id)
                    .OrderBy(p => p.Price);
            }
            else
            {
                products = context.Products
                    .Where(p => p.CategoryId == category.Id)
                    .OrderByDescending(p => p.Price);
            }

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)products.Count() / pageSize);

            return View("ProductsByCategory", products.ToList());
        }





        // Search method for the "Index" view
        public async Task<IActionResult> Search(string searchTerm, int p = 1)
        {
            int pageSize = 6;

            var query = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            var products = query
                .OrderByDescending(x => x.Id)
                .Skip((p - 1) * pageSize)
                .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)query.Count() / pageSize);

            return View("Index", await products.ToListAsync());
        }

        // Search method for the "ProductsByCategory" view
        public async Task<IActionResult> SearchByCategory(string categorySlug, string searchTerm, int p = 1)
        {
            Category category = await context.Categories
                .Where(x => x.Slug == categorySlug)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return RedirectToAction("Index");
            }

            int pageSize = 6;

            var query = context.Products
                .Where(x => x.CategoryId == category.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            var products = query
                .OrderByDescending(x => x.Id)
                .Skip((p - 1) * pageSize)
                .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)query.Count() / pageSize);
            ViewBag.CategoryName = category.Name;
            ViewBag.CategorySlug = category.Slug;

            return View("ProductsByCategory", await products.ToListAsync());
        }


        [Route("/products/Rate")]
        public async Task<IActionResult> Rate(Product product)
        {
            var temp = await context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            var oldRating = temp.Rating;

            if (oldRating == 0)
            {
                temp.Rating = product.Rating;
            }
            else
            {
                var newRating = (oldRating + product.Rating) / 2;

                temp.Rating = newRating;
            }

            temp.RatingCount += 1;


            context.Update(temp);
            var res = await context.SaveChangesAsync() > 0;

            if (res)
            {
                TempData["Success"] = "Rating Submitted!";
                return RedirectToAction("Index", "Products");
            }

            return BadRequest("Bad");
        }

        [Route("/products/Review")]
        public async Task<IActionResult> Review(Product product)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var productId = product.Id;

            var body = product.body;

            await context.Reviews.AddAsync(new Review()
            {
                UserId = userId,
                ProductId = productId,
                body = body
            });

            var res = await context.SaveChangesAsync() > 0;

            if (res)
            {
                TempData["Success"] = "The Review has been submitted!";
            }

            return RedirectToAction("Index");
        }
        

        public async Task<IActionResult> ReadReviews(long id)
        {
            var reviews = await context.Reviews
                .Include(r => r.AppUser).Where(r => r.ProductId == id)
                .ToListAsync();
          
            return View(reviews);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(long id)
        {
            var review = await context.Reviews.FindAsync(id);

            if (review == null)
            {
                TempData["Error"] = "The review does not exist";
            }
            else
            {
                context.Reviews.Remove(review);
                await context.SaveChangesAsync();
                TempData["Success"] = "The review has been deleted";
            }

            return RedirectToAction("Index");
        }






    }
}
