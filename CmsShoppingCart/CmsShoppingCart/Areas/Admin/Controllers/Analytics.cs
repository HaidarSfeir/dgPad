using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CmsShoppingCart.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CmsShoppingCart.Models.ViewModels;

namespace CmsShoppingCart.Areas.Admin.Controllers;
 [Area("Admin")]
public class Analytics : Controller
{
    private readonly CmsShoppingCartContext _context;

    public Analytics(CmsShoppingCartContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<DataPoint> dataPoints = new List<DataPoint>();

        var orders = await _context.Orders.ToListAsync();
        decimal sum = 0;
        dataPoints.Add(new DataPoint(0, 0));

        foreach (var order in orders)
        {
            sum += order.TotalAmount;
            dataPoints.Add(new DataPoint((int)(order.Id), sum));
        }

        ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints);


        var products = await _context.Products.ToListAsync();


        var uniqueProductIds = _context.cartItems
            .Select(ci => ci.ProductId)
            .Distinct()
            .ToList();

        decimal maxTotalSum = 0;
        int numberOfProductSold = 0;
        long productIdWithMaxTotalSum = 0;

        foreach (var productId in uniqueProductIds)
        {
            // Step 2: Retrieve cart items for the productId
            var cartItemsForProductId = _context.cartItems
                .Where(ci => ci.ProductId == productId)
                .ToList();

            // Step 3: Calculate the sum of Total in-memory
            decimal totalSum = cartItemsForProductId.Sum(ci => ci.Total);

            if (totalSum > maxTotalSum)
            {
                maxTotalSum = totalSum;
                productIdWithMaxTotalSum = productId;
                numberOfProductSold = cartItemsForProductId.Sum(ci => ci.Quantity);
            }
        }


        var ordersWithcartItems = await _context.Orders
            .Include(order => order.OrderItemsIds)
            .ToListAsync(); // Fetch the data into memory

        var userWithMaxTotalSpending = ordersWithcartItems
            .GroupBy(order => order.userId)
            .Select(group => new
            {
                UserId = group.Key,
                TotalSpending = group.Sum(order => order.OrderItemsIds.Sum(cartItem => cartItem.Total))
            }).MaxBy(result => result.TotalSpending);

        var user = new AppUser();
        var winningProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == productIdWithMaxTotalSum);
        if (userWithMaxTotalSpending != null)
        {
            user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userWithMaxTotalSpending.UserId);
        }

        var vm = new AdminViewModel();

        if (userWithMaxTotalSpending == null)
        {
            vm = new AdminViewModel()
            {
                WinningProduct = winningProduct,
                NumberOfProductSold = numberOfProductSold,
                TotalRevenue = sum,
                LoyalCustomer = user,
                MaxSpent = 0
            };
        }
        else
        {
            vm = new AdminViewModel()
            {
                WinningProduct = winningProduct,
                NumberOfProductSold = numberOfProductSold,
                TotalRevenue = sum,
                LoyalCustomer = user,
                MaxSpent = userWithMaxTotalSpending.TotalSpending
            };
        }

        return View(vm);
    }
}