using System;
using System.Collections.Generic;
using Nordik_Aventure.Objects.Models;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Objects.ViewModels
{
    public class FinanceDashboardViewModel
    {
        public double WeekSales { get; set; }
        public double WeekPurchases { get; set; }
        public double WeekProfit { get; set; }

        public double MonthSales { get; set; }
        public double MonthPurchases { get; set; }
        public double MonthProfit { get; set; }

        public double YearSales { get; set; }
        public double YearPurchases { get; set; }
        public double YearProfit { get; set; }

        public List<double> MonthlyRevenue { get; set; } = new();
        public List<double> MonthlyExpenses { get; set; } = new();

        public double MonthDonutSales => MonthSales;
        public double MonthDonutPurchases => MonthPurchases;

        public double WeekBarSales => WeekSales;
        public double WeekBarPurchases => WeekPurchases;

        public List<Transaction> LastTransactions { get; set; } = new();

        public int TotalOrders { get; set; }
        public int LateOrders { get; set; }
        public int TodayOrders { get; set; }
        public int UpcomingOrders { get; set; }

        public List<OrderStatusItemViewModel> RecentOrders { get; set; } = new();
    }

    public class OrderStatusItemViewModel
    {
        public int OrderId { get; set; }
        public DateTime DateOfOrdering { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public double TotalPrice { get; set; }
        public string SupplierName { get; set; }
        public string Status { get; set; }
    }
}