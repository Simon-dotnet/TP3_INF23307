using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;
using Nordik_Aventure.Objects.ViewModels;
using Nordik_Aventure.Services;

namespace Nordik_Aventure.Controllers
{
    [Route("finance")]
    public class FinanceController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly OrderService _orderService;

        public FinanceController(TransactionService transactionService, OrderService orderService)
        {
            _transactionService = transactionService;
            _orderService = orderService;
        }

        [Route("")]
        public IActionResult Index()
        {
            var lastTxResponse = _transactionService.GetLastTransactions(5);
            var allTxResponse = _transactionService.GetAllTransactions();
            var allOrdersResponse = _orderService.GetAllOrders();

            var allTx = allTxResponse.Data ?? new List<Transaction>();
            var lastTx = lastTxResponse.Data ?? new List<Transaction>();
            var orders = allOrdersResponse.Data ?? new List<Order>();

            var now = DateTime.Now;
            var today = now.Date;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);

            var mondayOffset = (int)now.DayOfWeek - (int)DayOfWeek.Monday;
            if (mondayOffset < 0) mondayOffset += 7;
            var startOfWeek = today.AddDays(-mondayOffset);

            var weekSales = allTx.Where(t => t.Type == "sale" && t.Date >= startOfWeek).Sum(t => t.AmountTotal);
            var weekPurchases = allTx.Where(t => t.Type == "purchase" && t.Date >= startOfWeek).Sum(t => t.AmountTotal);

            var monthSales = allTx.Where(t => t.Type == "sale" && t.Date >= startOfMonth).Sum(t => t.AmountTotal);
            var monthPurchases = allTx.Where(t => t.Type == "purchase" && t.Date >= startOfMonth).Sum(t => t.AmountTotal);

            var yearSales = allTx.Where(t => t.Type == "sale" && t.Date.Year == now.Year).Sum(t => t.AmountTotal);
            var yearPurchases = allTx.Where(t => t.Type == "purchase" && t.Date.Year == now.Year).Sum(t => t.AmountTotal);

            var monthlyRevenue = Enumerable.Range(1, 12)
                .Select(m => allTx.Where(t => t.Type == "sale" && t.Date.Year == now.Year && t.Date.Month == m)
                                  .Sum(t => t.AmountTotal))
                .ToList();

            var monthlyExpenses = Enumerable.Range(1, 12)
                .Select(m => allTx.Where(t => t.Type == "purchase" && t.Date.Year == now.Year && t.Date.Month == m)
                                  .Sum(t => t.AmountTotal))
                .ToList();

            var lateOrders = orders.Where(o => o.DateOfDelivery.Date < today).ToList();
            var todayOrders = orders.Where(o => o.DateOfDelivery.Date == today).ToList();
            var upcomingOrders = orders.Where(o => o.DateOfDelivery.Date > today).ToList();

            var recentOrders = orders
                .OrderBy(o => o.DateOfDelivery)
                .Take(5)
                .Select(o => new OrderStatusItemViewModel
                {
                    OrderId = o.OrderId,
                    DateOfOrdering = o.DateOfOrdering,
                    DateOfDelivery = o.DateOfDelivery,
                    TotalPrice = o.TotalPrice,
                    SupplierName = o.OrderSupplierProducts.FirstOrDefault()?.Supplier?.Name ?? "-",
                    Status = o.DateOfDelivery.Date < today
                        ? "En retard"
                        : o.DateOfDelivery.Date == today
                            ? "Aujourd'hui"
                            : "À venir"
                })
                .ToList();

            var vm = new FinanceDashboardViewModel
            {
                WeekSales = weekSales,
                WeekPurchases = weekPurchases,
                WeekProfit = weekSales - weekPurchases,
                MonthSales = monthSales,
                MonthPurchases = monthPurchases,
                MonthProfit = monthSales - monthPurchases,
                YearSales = yearSales,
                YearPurchases = yearPurchases,
                YearProfit = yearSales - yearPurchases,
                MonthlyRevenue = monthlyRevenue,
                MonthlyExpenses = monthlyExpenses,
                LastTransactions = lastTx.OrderByDescending(t => t.Date).ToList(),
                TotalOrders = orders.Count,
                LateOrders = lateOrders.Count,
                TodayOrders = todayOrders.Count,
                UpcomingOrders = upcomingOrders.Count,
                RecentOrders = recentOrders
            };

            return View("../ModuleFinance/HomepageFinance", vm);
        }

        [HttpGet("transactions")]
        public IActionResult TransactionHistory()
        {
            var all = _transactionService.GetAllTransactions();
            return View("../ModuleFinance/TransactionHistory", all.Data ?? new List<Transaction>());
        }

        [HttpGet("receipt/{transactionId}")]
        public IActionResult RedirectToReceipt(int transactionId)
        {
            var all = _transactionService.GetAllTransactions();
            var transaction = all.Data?.FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null)
                return NotFound();

            if (transaction.Type == "purchase")
                return Redirect($"/finance/purchase/receipt/{transactionId}");

            if (transaction.Type == "sale")
                return Redirect($"/finance/sale/receipt/{transactionId}");

            return NotFound();
        }
    }
}
