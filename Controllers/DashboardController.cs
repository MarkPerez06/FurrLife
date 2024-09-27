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


        public ActionResult LoadBestSellerChart()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }
            var Series = new List<SeriesData>();


            var TopProducts = from o in _context.Orders
                              join op in _context.OrderProducts on o.Id equals op.OrderId
                              join p in _context.Products on op.ProductId equals p.Id
                              where o.IsPaid == true
                              group new { op, p } by new { p.Id, p.Name } into g
                              orderby g.Sum(item => item.op.Quantity) descending
                              select new Top5Products
                              {
                                  Id = g.Key.Id,
                                  Name = g.Key.Name,
                                  Count = g.Sum(item => item.op.Quantity)
                              };
            var top5 = TopProducts.Take(5).ToList();

            foreach (var item in top5)
            {
                int Jan = GetBestSellerSeries(item.Id, Year, 1);
                int Feb = GetBestSellerSeries(item.Id, Year, 2);
                int Mar = GetBestSellerSeries(item.Id, Year, 3);
                int Apr = GetBestSellerSeries(item.Id, Year, 4);
                int May = GetBestSellerSeries(item.Id, Year, 5);
                int Jun = GetBestSellerSeries(item.Id, Year, 6);
                int Jul = GetBestSellerSeries(item.Id, Year, 7);
                int Aug = GetBestSellerSeries(item.Id, Year, 8);
                int Sep = GetBestSellerSeries(item.Id, Year, 9);
                int Oct = GetBestSellerSeries(item.Id, Year, 10);
                int Nov = GetBestSellerSeries(item.Id, Year, 11);
                int Dec = GetBestSellerSeries(item.Id, Year, 12);

                Series.Add(new SeriesData { name = item.Name, data = new List<int> { Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec } });
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


            var TopProducts = from o in _context.Orders
                              join op in _context.OrderProducts on o.Id equals op.OrderId
                              join p in _context.Products on op.ProductId equals p.Id
                              where o.IsPaid == true
                              group new { op, p } by new { p.Id, p.Name } into g
                              orderby g.Sum(item => item.op.Quantity) ascending
                              select new Top5Products
                              {
                                  Id = g.Key.Id,
                                  Name = g.Key.Name,
                                  Count = g.Sum(item => item.op.Quantity)
                              };
            var top5 = TopProducts.Take(5).ToList();

            foreach (var item in top5)
            {
                int Jan = GetBestLeastSeries(item.Id, Year, 1);
                int Feb = GetBestLeastSeries(item.Id, Year, 2);
                int Mar = GetBestLeastSeries(item.Id, Year, 3);
                int Apr = GetBestLeastSeries(item.Id, Year, 4);
                int May = GetBestLeastSeries(item.Id, Year, 5);
                int Jun = GetBestLeastSeries(item.Id, Year, 6);
                int Jul = GetBestLeastSeries(item.Id, Year, 7);
                int Aug = GetBestLeastSeries(item.Id, Year, 8);
                int Sep = GetBestLeastSeries(item.Id, Year, 9);
                int Oct = GetBestLeastSeries(item.Id, Year, 10);
                int Nov = GetBestLeastSeries(item.Id, Year, 11);
                int Dec = GetBestLeastSeries(item.Id, Year, 12);

                Series.Add(new SeriesData { name = item.Name, data = new List<int> { Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec } });
            }

            var Result = new { Series, Year };
            return Json(Result);
        }


        public ActionResult LoadForcastingChart()
        {
            var Series = new List<SeriesDataForecasting>();

            var YearList = _context.Orders.Where(m => m.IsPaid == true).Select(m => new YearList { Year = m.DateCreated.Year }).Distinct().ToList();

            var TopProducts = from o in _context.Orders
                              join op in _context.OrderProducts on o.Id equals op.OrderId
                              join p in _context.Products on op.ProductId equals p.Id
                              where o.IsPaid == true
                              group new { op, p } by new { p.Id, p.Name } into g
                              orderby g.Sum(item => item.op.Quantity) descending
                              select new Top5Products
                              {
                                  Id = g.Key.Id,
                                  Name = g.Key.Name,
                                  Count = g.Sum(item => item.op.Quantity)
                              };

            var top5 = TopProducts.Take(5).ToList();

            foreach (var item in top5)
            {
                decimal Jan = GetForecastingSeries(item.Id, YearList, 1);
                decimal Feb = GetForecastingSeries(item.Id, YearList, 2);
                decimal Mar = GetForecastingSeries(item.Id, YearList, 3);
                decimal Apr = GetForecastingSeries(item.Id, YearList, 4);
                decimal May = GetForecastingSeries(item.Id, YearList, 5);
                decimal Jun = GetForecastingSeries(item.Id, YearList, 6);
                decimal Jul = GetForecastingSeries(item.Id, YearList, 7);
                decimal Aug = GetForecastingSeries(item.Id, YearList, 8);
                decimal Sep = GetForecastingSeries(item.Id, YearList, 9);
                decimal Oct = GetForecastingSeries(item.Id, YearList, 10);
                decimal Nov = GetForecastingSeries(item.Id, YearList, 11);
                decimal Dec = GetForecastingSeries(item.Id, YearList, 12);

                Series.Add(new SeriesDataForecasting { name = item.Name, data = new List<decimal> { Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec } });
            }
            return Json(Series);
        }

        public int GetBestSellerSeries(int Id, int Year, int Month)
        {
            int ProductCount = 0;
            var TopProducts = from o in _context.Orders
                              join op in _context.OrderProducts on o.Id equals op.OrderId
                              join p in _context.Products on op.ProductId equals p.Id
                              where o.IsPaid == true && o.DateCreated.Year == Year && o.DateCreated.Month == Month
                              group new { op, p } by new { p.Id, p.Name } into g
                              orderby g.Sum(item => item.op.Quantity) descending
                              select new Top5Products
                              {
                                  Id = g.Key.Id,
                                  Name = g.Key.Name,
                                  Count = g.Sum(item => item.op.Quantity)
                              };

            var TP = TopProducts.Where(m => m.Id == Id).FirstOrDefault();
            if (TP != null)
            {
                ProductCount = TP.Count;
            }
            return ProductCount;
        }

        public int GetBestLeastSeries(int Id, int Year, int Month)
        {
            int ProductCount = 0;
            var TopProducts = from o in _context.Orders
                              join op in _context.OrderProducts on o.Id equals op.OrderId
                              join p in _context.Products on op.ProductId equals p.Id
                              where o.IsPaid == true && o.DateCreated.Year == Year && o.DateCreated.Month == Month
                              group new { op, p } by new { p.Id, p.Name } into g
                              orderby g.Sum(item => item.op.Quantity) ascending
                              select new Top5Products
                              {
                                  Id = g.Key.Id,
                                  Name = g.Key.Name,
                                  Count = g.Sum(item => item.op.Quantity)
                              };

            var TP = TopProducts.Where(m => m.Id == Id).FirstOrDefault();
            if (TP != null)
            {
                ProductCount = TP.Count;
            }
            return ProductCount;
        }

        public decimal GetForecastingSeries(int Id, List<YearList> YearList, int Month)
        {
            int ProductCount = 0;
            foreach (var item in YearList)
            {
                var TopProducts = from o in _context.Orders
                                  join op in _context.OrderProducts on o.Id equals op.OrderId
                                  join p in _context.Products on op.ProductId equals p.Id
                                  where o.IsPaid == true && o.DateCreated.Year == item.Year && o.DateCreated.Month == Month
                                  group new { op, p } by new { p.Id, p.Name } into g
                                  orderby g.Sum(item => item.op.Quantity) descending
                                  select new Top5Products
                                  {
                                      Id = g.Key.Id,
                                      Name = g.Key.Name,
                                      Count = g.Sum(item => item.op.Quantity)
                                  };

                var TP = TopProducts.Where(m => m.Id == Id).FirstOrDefault();
                if (TP != null)
                {
                    ProductCount = ProductCount + TP.Count;
                }
            }
            return ProductCount / YearList.Count;
        }

        public ActionResult LoadMonthlyEarningChart()
        {
            int Year = Convert.ToInt32(DateTime.Now.Year);
            if (TempData["SelectedYear"] != null)
            {
                Year = Convert.ToInt32(TempData["SelectedYear"]);
            }

            var Series = new List<SeriesDataMonthlyEarning>();

            decimal Jan = GetMonthlyEarningSeries(Year, 1);
            decimal Feb = GetMonthlyEarningSeries(Year, 2);
            decimal Mar = GetMonthlyEarningSeries(Year, 3);
            decimal Apr = GetMonthlyEarningSeries(Year, 4);
            decimal May = GetMonthlyEarningSeries(Year, 5);
            decimal Jun = GetMonthlyEarningSeries(Year, 6);
            decimal Jul = GetMonthlyEarningSeries(Year, 7);
            decimal Aug = GetMonthlyEarningSeries(Year, 8);
            decimal Sep = GetMonthlyEarningSeries(Year, 9);
            decimal Oct = GetMonthlyEarningSeries(Year, 10);
            decimal Nov = GetMonthlyEarningSeries(Year, 11);
            decimal Dec = GetMonthlyEarningSeries(Year, 12);

            Series.Add(new SeriesDataMonthlyEarning { name = "Monthly Earning " + Year, data = new List<decimal> { Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec } });

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

        public decimal GetMonthlyEarningSeries(int Year, int Month)
        {
            decimal MonthlyEarning = 0;
            var model = _context.Orders.Where(m => m.IsPaid == true && m.DateCreated.Year == Year && m.DateCreated.Month == Month).ToList();

            if (model != null)
            {
                foreach (var item in model)
                {
                    MonthlyEarning = MonthlyEarning + (item.TotalAmount - (item.TotalAmount * item.Discounts) / 100);
                }

            }
            return MonthlyEarning;
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