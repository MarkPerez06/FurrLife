using FurrLife.Data;
using FurrLife.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurrLife.Static;
using static FurrLife.Controllers.DashboardController;

namespace FurrLife.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DashboardController> _logger;
        private readonly ApplicationDbContext _context;
        public DashboardController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<DashboardController> logger, ApplicationDbContext context)
        {

            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }
        public class SeriesData
        {
            public string name { get; set; }
            public List<int> data { get; set; }
        }

        public class SeriesDataMonthlyEarning
        {
            public string name { get; set; }
            public List<decimal> data { get; set; }
        }

        public class SeriesDataForecasting
        {
            public string name { get; set; }
            public List<decimal> data { get; set; }
        }

        public class SeriesDataDouble
        {
            public string name { get; set; }
            public List<double> data { get; set; }
        }

        public class Top5Products
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Count { get; set; }
        }

        public class YearList
        {
            public int Year { get; set; }
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var OrdersModel = _context.Orders.Where(m => m.IsPaid == false).ToList();
                var TransactionsModel = _context.Orders.Where(m => m.IsPaid == true).ToList();
                decimal TransactionAmount = TransactionsModel.Sum(m => m.TotalAmount - (m.TotalAmount * Convert.ToDecimal(m.Discounts) / 100));

                var TransactionsMonthly = _context.Orders.Where(m => m.DateCreated.Month == DateTime.Now.Month && m.DateCreated.Year == DateTime.Now.Year && m.IsPaid == true).ToList();
                decimal TransactionsMonthlyAmount = TransactionsMonthly.Sum(m => m.TotalAmount - (m.TotalAmount * Convert.ToDecimal(m.Discounts) / 100));

                ViewBag.TransactionAmount = TransactionAmount;
                ViewBag.TransactionsMonthlyAmount = TransactionsMonthlyAmount;
                ViewBag.OrdersCount = OrdersModel.Count;
                ViewBag.TransactionsCount = TransactionsModel.Count;
                var Year = _context.Orders.Where(m => m.IsPaid == true).Select(m => new { Year = m.DateCreated.Year }).Distinct().OrderByDescending(m => m.Year).ToList();
                ViewBag.Year = new SelectList(Year, "Year", "Year");

                var model = _context.Orders.ToList();
                return View(model);
            }
            else
            {
                return Redirect("~/Identity/Account/Login");
            }
        }

        public ActionResult FilterYear(string Year)
        {
            if (Year == null || Year == "")
            {
                TempData["SelectedYear"] = null;
            }
            else
            {
                TempData["SelectedYear"] = Year;
            }

            return RedirectToAction("Index");
        }
        public ActionResult LoadPetHealthRecordChartWithPredictions()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }

            var Series = new List<SeriesDataDouble>();

            // Fetch top 5 conditions based on record counts for all years
            var topPetConditionsData = (from pr in _context.PetHealthRecord
                                        where pr.CreatedDate.Year <= Year
                                        group pr by pr.ExistingConditions into g
                                        orderby g.Count() descending
                                        select new
                                        {
                                            Condition = g.Key,
                                            RecordCount = g.Count()
                                        }).Take(5).ToList();

            // Fetch monthly records grouped by condition and year
            var monthlyRecordsData = (from pr in _context.PetHealthRecord
                                      where pr.CreatedDate.Year <= Year
                                      group pr by new { pr.ExistingConditions, pr.CreatedDate.Year, pr.CreatedDate.Month } into g
                                      select new
                                      {
                                          Condition = g.Key.ExistingConditions,
                                          Year = g.Key.Year,
                                          Month = g.Key.Month,
                                          RecordCount = g.Count()
                                      }).ToList();

            // Build series data with predictions
            foreach (var condition in topPetConditionsData)
            {
                // Initialize an array for storing average monthly data
                var averageMonthlyCounts = new double[12];

                // Calculate monthly averages across historical years
                for (int month = 1; month <= 12; month++)
                {
                    // Get all records for the given condition and month
                    var monthlyData = monthlyRecordsData
                        .Where(m => m.Condition == condition.Condition && m.Month == month)
                        .Select(m => m.RecordCount);

                    // Compute the average for this month
                    if (monthlyData.Any())
                    {
                        averageMonthlyCounts[month - 1] = monthlyData.Average();
                    }
                }

                // Predict next year's data using the monthly averages
                var nextYearPredictions = new List<double>(averageMonthlyCounts);

                // Add the series for historical data and predictions
                Series.Add(new SeriesDataDouble
                {
                    name = condition.Condition,
                    data = nextYearPredictions // Use the averaged data for the full next year
                });
            }

            var Result = new { Series, Year = Year + 1 }; // Return the predicted year
            return Json(Result);
        }

        public ActionResult LoadAppointmentsChartWithPredictions()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }

            var Series = new List<SeriesDataDouble>();

            // Retrieve historical data grouped by year and month
            var historicalAppointments = (from a in _context.Appointments
                                          where a.Start.Year <= Year
                                          group a by new { a.Start.Year, a.Start.Month } into g
                                          select new
                                          {
                                              Year = g.Key.Year,
                                              Month = g.Key.Month,
                                              TotalAppointments = g.Count()
                                          }).ToList();

            // Calculate monthly averages across historical years
            var monthlyAverages = new int[12];
            for (int month = 1; month <= 12; month++)
            {
                var monthlyData = historicalAppointments.Where(a => a.Month == month);
                if (monthlyData.Any())
                {
                    monthlyAverages[month - 1] = (int)monthlyData.Average(a => a.TotalAppointments);
                }
            }

            // Retrieve current year appointments
            var currentYearAppointments = (from a in _context.Appointments
                                           where a.Start.Year == Year
                                           group a by a.Start.Month into g
                                           select new
                                           {
                                               Month = g.Key,
                                               TotalAppointments = g.Count()
                                           }).ToList();

            // Populate current year monthly data
            var currentYearData = new double[12];
            foreach (var appointment in currentYearAppointments)
            {
                currentYearData[appointment.Month - 1] = appointment.TotalAppointments; // Month is 1-based
            }

            // Predict next year's appointments
            var predictedAppointments = new List<double>();
            for (int month = 0; month < 12; month++)
            {
                // Use historical average and trend from the current year
                double trend = currentYearData[month] - (month > 0 ? currentYearData[month - 1] : 0); // Calculate trend
                double prediction = monthlyAverages[month] + trend; // Combine average and trend
                predictedAppointments.Add(Math.Max(0, prediction)); // Ensure no negative predictions
            }

            // Add series data
            // Current year appointments
            Series.Add(new SeriesDataDouble { name = "" + Year, data = currentYearData.ToList() });

            // Predicted next year's appointments
            Series.Add(new SeriesDataDouble { name = "" + (Year + 1), data = predictedAppointments });

            var Result = new { Series, Year = "" };
            return Json(Result);
        }



        public ActionResult LoadMonthlyEarningChartWithPredictions()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }

            var Series = new List<SeriesDataMonthlyEarning>();

            // Retrieve earnings for all previous years grouped by year and month
            var historicalEarnings = (from o in _context.Orders
                                      where o.IsPaid == true && o.DateCreated.Year <= Year
                                      group o by new { o.DateCreated.Year, o.DateCreated.Month } into g
                                      select new
                                      {
                                          Year = g.Key.Year,
                                          Month = g.Key.Month,
                                          TotalEarning = g.Sum(item => item.TotalAmount - (item.TotalAmount * item.Discounts) / 100)
                                      }).ToList();

            // Calculate averages per month across historical years
            var monthlyAverages = new decimal[12];
            for (int month = 1; month <= 12; month++)
            {
                var monthlyData = historicalEarnings.Where(e => e.Month == month);
                if (monthlyData.Any())
                {
                    monthlyAverages[month - 1] = monthlyData.Average(e => e.TotalEarning);
                }
            }

            // Retrieve earnings for the current year
            var currentYearEarnings = (from o in _context.Orders
                                       where o.IsPaid == true && o.DateCreated.Year == Year
                                       group o by o.DateCreated.Month into g
                                       select new
                                       {
                                           Month = g.Key,
                                           TotalEarning = g.Sum(item => item.TotalAmount - (item.TotalAmount * item.Discounts) / 100)
                                       }).ToList();

            // Populate current year earnings
            decimal[] earnings = new decimal[12];
            foreach (var earning in currentYearEarnings)
            {
                earnings[earning.Month - 1] = earning.TotalEarning; // Month is 1-based
            }

            // Predict earnings for the next year based on historical averages
            var predictedEarnings = new List<decimal>();
            for (int month = 0; month < 12; month++)
            {
                // Use the average for the month and the trend from the current year
                decimal trend = earnings[month] - (month > 0 ? earnings[month - 1] : 0); // Current trend
                decimal prediction = monthlyAverages[month] + trend; // Combine trend with historical average
                predictedEarnings.Add(Math.Max(0, prediction)); // Ensure no negative earnings
            }

            // Actual earnings for the current year
            Series.Add(new SeriesDataMonthlyEarning { name = "" + Year, data = earnings.ToList() });

            // Predicted earnings for the next year
            Series.Add(new SeriesDataMonthlyEarning { name = "" + (Year + 1), data = predictedEarnings });

            var Result = new { Series, Year = "" };
            return Json(Result);
        }



        public ActionResult LoadBestSellerChart()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }

            var Series = new List<SeriesData>();

            // Fetch all relevant orders and group by product
            var topProductsData = (from o in _context.Orders
                                   join op in _context.OrderProducts on o.Id equals op.OrderId
                                   join p in _context.Products on op.ProductId equals p.Id
                                   where o.IsPaid == true
                                   group new { op, p } by new { p.Id, p.Name } into g
                                   orderby g.Sum(item => item.op.Quantity) descending
                                   select new
                                   {
                                       ProductId = g.Key.Id,
                                       ProductName = g.Key.Name,
                                       TotalQuantity = g.Sum(item => item.op.Quantity)
                                   }).Take(5).ToList();

            // Prepare monthly sales data for the top products
            var monthlySalesData = (from o in _context.Orders
                                    join op in _context.OrderProducts on o.Id equals op.OrderId
                                    where o.IsPaid == true && o.DateCreated.Year == Year
                                    group op by new { op.ProductId, o.DateCreated.Month } into g
                                    select new
                                    {
                                        ProductId = g.Key.ProductId,
                                        Month = g.Key.Month,
                                        Quantity = g.Sum(item => item.Quantity)
                                    }).ToList();

            // Build the series data
            foreach (var product in topProductsData)
            {
                var monthlyQuantities = new int[12];

                // Fill in the monthly quantities
                foreach (var monthlyData in monthlySalesData.Where(m => m.ProductId == product.ProductId))
                {
                    monthlyQuantities[monthlyData.Month - 1] = monthlyData.Quantity; // Month is 1-based
                }

                Series.Add(new SeriesData
                {
                    name = product.ProductName,
                    data = monthlyQuantities.ToList()
                });
            }

            var Result = new { Series, Year };
            return Json(Result);
        }

        public ActionResult LoadLeastSellerChart()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }

            var Series = new List<SeriesData>();

            // Fetch least sold products in a single query
            var topProductsData = (from o in _context.Orders
                                   join op in _context.OrderProducts on o.Id equals op.OrderId
                                   join p in _context.Products on op.ProductId equals p.Id
                                   where o.IsPaid == true
                                   group new { op, p } by new { p.Id, p.Name } into g
                                   orderby g.Sum(item => item.op.Quantity) ascending
                                   select new
                                   {
                                       ProductId = g.Key.Id,
                                       ProductName = g.Key.Name,
                                       TotalQuantity = g.Sum(item => item.op.Quantity)
                                   }).Take(5).ToList();

            // Fetch monthly sales data for all products in the selected year
            var monthlySalesData = (from o in _context.Orders
                                    join op in _context.OrderProducts on o.Id equals op.OrderId
                                    where o.IsPaid == true && o.DateCreated.Year == Year
                                    group op by new { op.ProductId, o.DateCreated.Month } into g
                                    select new
                                    {
                                        ProductId = g.Key.ProductId,
                                        Month = g.Key.Month,
                                        Quantity = g.Sum(item => item.Quantity)
                                    }).ToList();

            // Build the series data
            foreach (var product in topProductsData)
            {
                var monthlyQuantities = new int[12];

                // Fill in the monthly quantities for the least sold products
                foreach (var monthlyData in monthlySalesData.Where(m => m.ProductId == product.ProductId))
                {
                    monthlyQuantities[monthlyData.Month - 1] = monthlyData.Quantity; // Month is 1-based
                }

                Series.Add(new SeriesData
                {
                    name = product.ProductName,
                    data = monthlyQuantities.ToList()
                });
            }

            var Result = new { Series, Year };
            return Json(Result);
        }

        public ActionResult LoadForcastingChart()
        {
            var Series = new List<SeriesDataForecasting>();

            // Retrieve the distinct years where orders were paid
            var YearList = _context.Orders
                                   .Where(m => m.IsPaid == true)
                                   .Select(m => m.DateCreated.Year)
                                   .Distinct()
                                   .ToList();

            // Retrieve top 10 products based on total quantity sold
            var topProducts = (from o in _context.Orders
                               join op in _context.OrderProducts on o.Id equals op.OrderId
                               join p in _context.Products on op.ProductId equals p.Id
                               where o.IsPaid == true
                               group op by new { p.Id, p.Name } into g
                               orderby g.Sum(item => item.Quantity) descending
                               select new
                               {
                                   Id = g.Key.Id,
                                   Name = g.Key.Name,
                                   TotalQuantity = g.Sum(item => item.Quantity)
                               }).Take(10).ToList();

            // Retrieve forecasting data for the top products in a single query
            var forecastingData = (from o in _context.Orders
                                   join op in _context.OrderProducts on o.Id equals op.OrderId
                                   where o.IsPaid == true && YearList.Contains(o.DateCreated.Year)
                                   group new { o.DateCreated.Month, op.Quantity } by new { op.ProductId, o.DateCreated.Month } into g
                                   select new
                                   {
                                       ProductId = g.Key.ProductId,
                                       Month = g.Key.Month,
                                       TotalQuantity = g.Sum(x => x.Quantity)
                                   }).ToList();

            // Prepare series data for each top product
            foreach (var product in topProducts)
            {
                var monthlyData = new decimal[12];

                // Fill the monthly data for each product
                foreach (var data in forecastingData.Where(f => f.ProductId == product.Id))
                {
                    monthlyData[data.Month - 1] = data.TotalQuantity;
                }

                // Add the series data
                Series.Add(new SeriesDataForecasting { name = product.Name, data = monthlyData.ToList() });
            }

            return Json(Series);
        }

        public ActionResult LoadMonthlyEarningChart()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }

            var Series = new List<SeriesDataMonthlyEarning>();

            // Retrieve all paid orders for the selected year at once
            var monthlyEarnings = (from o in _context.Orders
                                   where o.IsPaid == true && o.DateCreated.Year == Year
                                   group o by o.DateCreated.Month into g
                                   select new
                                   {
                                       Month = g.Key,
                                       TotalEarning = g.Sum(item => item.TotalAmount - (item.TotalAmount * item.Discounts) / 100)
                                   }).ToList();


            var user = _context.Users.Where(m => m.UserName == User.Identity.Name && m.SecurityStamp == UserRoles.Customer.Id).FirstOrDefault();
            if (user != null)
            {
                monthlyEarnings = (from o in _context.Orders
                                   where o.IsPaid == true && o.DateCreated.Year == Year && o.UserId == user.Id
                                   group o by o.DateCreated.Month into g
                                   select new
                                   {
                                       Month = g.Key,
                                       TotalEarning = g.Sum(item => item.TotalAmount - (item.TotalAmount * item.Discounts) / 100)
                                   }).ToList();
            }
            // Create an array to hold earnings for each month (Jan to Dec)
            decimal[] earnings = new decimal[12];

            // Populate the earnings array
            foreach (var earning in monthlyEarnings)
            {
                earnings[earning.Month - 1] = earning.TotalEarning; // Month is 1-based
            }

            // Add the data to the series
            Series.Add(new SeriesDataMonthlyEarning { name = "Monthly Expenses " + Year, data = earnings.ToList() });

            var Result = new { Series, Year };
            return Json(Result);
        }



        public ActionResult ConvertToeWallet()
        {
            var model = _context.Persons.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
            var Result = new { Title = "Error!", Text = "Oppss, something went wrong!", Icon = "warning" };
            if (model != null)
            {
                var FullName = model.FirstName + " " + model.MiddleName + " " + model.LastName;
                var Email = model.Email;
                decimal WalletAmount = model.Wallet;
                decimal RewardPoints = Convert.ToDecimal(model.Salary);

                decimal NewWalletAmount = WalletAmount + RewardPoints;
                model.Wallet = NewWalletAmount;
                model.Salary = 0;
                _context.Persons.Update(model);
                _context.SaveChanges();
                Result = new { Title = "Success", Text = "Card Holder: " + FullName + " was successfully converted the Reward Points " + string.Format(new CultureInfo("en-PH", false), "{0:C}", RewardPoints) + " to your e Wallet, the new total Wallet Balance is " + string.Format(new CultureInfo("en-PH", false), "{0:C}", NewWalletAmount), Icon = "success" };
            }
            return Json(Result);
        }

        public ActionResult SearchCardNumber(string CardNumber)
        {
            var model = _context.Persons.Where(m => m.CardNumber == CardNumber && m.IsMember == true).FirstOrDefault();
            var Result = new { Title = "", Text = "", Icon = "", FullName = "", Email = "", Wallet = "" };
            if (model != null)
            {
                var FullName = model.FirstName + " " + model.MiddleName + " " + model.LastName;
                var Email = model.Email;
                var Wallet = model.Wallet.ToString();
                Result = new { Title = "Success", Text = "Card Holder: " + FullName, Icon = "success", FullName, Email, Wallet };
            }
            else
            {
                Result = new { Title = "Not Found!", Text = "Card number does not exists!", Icon = "info", FullName = "", Email = "", Wallet = "" };
            }
            return Json(Result);
        }

        public ActionResult LoadCardWallet(string CardNumber, string PIN, decimal WalletAmount)
        {
            var model = _context.Persons.Where(m => m.CardNumber == CardNumber && m.PIN == PIN).FirstOrDefault();
            var Result = new { Title = "", Text = "", Icon = "", FullName = "", Email = "" };
            if (model != null)
            {
                var FullName = model.FirstName + " " + model.MiddleName + " " + model.LastName;
                var Email = model.Email;
                var NewWalletAmount = model.Wallet + WalletAmount;
                model.Wallet = NewWalletAmount;
                _context.Persons.Update(model);
                _context.SaveChanges();
                Result = new { Title = "Success", Text = "Card Holder: " + FullName + " was successfully loaded " + string.Format(new CultureInfo("en-PH", false), "{0:C}", WalletAmount) + ", the new total wallet amount is " + string.Format(new CultureInfo("en-PH", false), "{0:C}", NewWalletAmount), Icon = "success", FullName, Email };
            }
            else
            {
                Result = new { Title = "Not Found!", Text = "Card PIN does not exists!", Icon = "info", FullName = "", Email = "" };
            }
            return Json(Result);
        }

        public IActionResult _OrderDetails(int OrderId)
        {
            var model = from op in _context.OrderProducts
                        join p in _context.Products
                        on op.ProductId equals p.Id
                        join m in _context.Menu
                        on p.MenuId equals m.Id
                        join u in _context.Units
                        on p.UnitId equals u.Id
                        where op.OrderId == OrderId
                        select new OrderProductsView
                        {
                            OrderId = op.Id,
                            OrderQuantity = op.Quantity,
                            OrderPrice = op.Price,
                            OrderDiscounts = op.Discounts,
                            OrderUserId = op.UserId,
                            MenuName = m.Name,
                            MenuDescription = m.Description,
                            ProductId = p.Id,
                            ProductName = p.Name,
                            ProductDescription = p.Description,
                            ProductImageURL = p.ImageURL,
                            ProductRating = p.ProductRating,
                            UnitName = u.Name,
                            UnitCode = u.Code
                        };
            ViewBag.OrderDetails = _context.Orders.Where(m => m.Id == OrderId).FirstOrDefault();
            return PartialView("_OrderDetails", model);
        }

        public IActionResult GetIDs(string CardNumber, string PIN)
        {
            var Person = _context.Persons.Where(m => m.CardNumber == CardNumber && m.PIN == PIN && m.IsMember == true).FirstOrDefault();
            return Json(Person);
        }

        public IActionResult ProceedPayment(int OrderId, string PaymentMethod, string CardNumber, string PIN, string SeniorCitizenID, string PWDID, int Discount)
        {
            var Result = new { Title = "", Text = "", Icon = "" };

            var Orders = _context.Orders.Where(m => m.Id == OrderId).FirstOrDefault();

            if (PaymentMethod == "Membership Card")
            {
                var Person = _context.Persons.Where(m => m.CardNumber == CardNumber && m.PIN == PIN && m.IsMember == true).FirstOrDefault();
                if (Person != null)
                {
                    decimal TotalAmount = 0;
                    var CP = _context.OrderProducts.Where(m => m.OrderId == OrderId).ToList();
                    foreach (var item in CP)
                    {
                        var DiscountedPrice = (item.Price - (item.Price * item.Discounts) / 100) * item.Quantity;
                        TotalAmount = TotalAmount + Convert.ToDecimal(DiscountedPrice);
                    }

                    decimal WalletTotal = Person.Wallet;
                    if (Person.Wallet >= TotalAmount)
                    {
                        if (SeniorCitizenID != null && SeniorCitizenID != "")
                        {
                            Person.IsSeniorCitizen = true;
                        }
                        else
                        {
                            Person.IsSeniorCitizen = false;
                        }
                        if (PWDID != null && PWDID != "")
                        {
                            Person.IsPWD = true;
                        }
                        else
                        {
                            Person.IsPWD = false;
                        }
                        Person.SeniorCitizenID = SeniorCitizenID;
                        Person.PWDID = PWDID;
                        decimal TotalDiscounted = (TotalAmount - (TotalAmount * Discount) / 100);
                        Person.Wallet = (WalletTotal - TotalDiscounted);

                        decimal GetRewards = Convert.ToDecimal(Person.Salary);
                        Person.Salary = GetRewards + (TotalDiscounted * 1) / 100;

                        _context.Persons.Update(Person);
                        _context.SaveChanges();

                        Orders.IsPaid = true;
                        Orders.SeniorCitizenID = SeniorCitizenID;
                        Orders.PWDID = PWDID;
                        Orders.TotalAmount = TotalAmount;
                        Orders.Discounts = Discount;
                        Orders.UserId = Person.UserId;
                        _context.Orders.Update(Orders);
                        _context.SaveChanges();

                        Result = new { Title = "Success!", Text = "Order was successful! \n Order ID: " + Orders.ReferenceNo + "\n Total Amount: " + string.Format(new CultureInfo("en-PH", false), "{0:C}", Orders.TotalAmount - ((Orders.TotalAmount * Orders.Discounts) / 100)), Icon = "success" };
                    }
                    else
                    {
                        Result = new { Title = "Not Enough Wallet Cash!", Text = "Your wallet cash is less than the order amount, please reload your card to proceed the payment!", Icon = "info" };
                    }
                }
                else
                {
                    Result = new { Title = "Not Found!", Text = "Card number or PIN does not exists!", Icon = "info" };
                }

            }

            if (PaymentMethod == "Cash")
            {
                decimal TotalAmount = 0;
                var CP = _context.OrderProducts.Where(m => m.OrderId == OrderId).ToList();
                foreach (var item in CP)
                {
                    var DiscountedPrice = (item.Price - (item.Price * item.Discounts) / 100) * item.Quantity;
                    TotalAmount = TotalAmount + Convert.ToDecimal(DiscountedPrice);
                }
                Orders.IsPaid = true;
                Orders.SeniorCitizenID = SeniorCitizenID;
                Orders.PWDID = PWDID;
                Orders.TotalAmount = TotalAmount;
                Orders.Discounts = Discount;
                _context.Orders.Update(Orders);
                _context.SaveChanges();

                Result = new { Title = "Success!", Text = "Order was successful! \n Order ID: " + Orders.ReferenceNo + "\n Total Amount: " + string.Format(new CultureInfo("en-PH", false), "{0:C}", Orders.TotalAmount - ((Orders.TotalAmount * Orders.Discounts) / 100)), Icon = "success" };
            }
            return Json(Result);
        }

        [HttpPost]
        public ActionResult Save(int Id, int Quantity)
        {
            var model = _context.OrderProducts.Where(m => m.Id == Id).FirstOrDefault();
            model.Quantity = Quantity;
            _context.OrderProducts.Update(model);
            _context.SaveChanges();
            return Json(model);
        }

        public ActionResult Delete(OrderProducts model)
        {
            _context.OrderProducts.Remove(model);
            _context.SaveChanges();
            return Json(model);
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