using System.Linq.Expressions;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Helpers;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Models.Entities;

namespace PersonalFinanceTracker.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly JwtHelper jwtHelper;

        public TransactionController(ApplicationDbContext applicationDbContext, JwtHelper jwtHelper):base(jwtHelper)
        {
            this.applicationDbContext = applicationDbContext;
            this.jwtHelper = jwtHelper;
        }

        public IActionResult Index()
        {
            var userIdStr = jwtHelper.GetUserIdFromCookie();
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdStr);

            var transactions = applicationDbContext.Transactions
                .Where(t => t.UserId == userId)
                .ToList();

            return View(transactions);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(CreateTransactionDto createTransactionDto)
        {
            var userIdString = jwtHelper.GetUserIdFromCookie();
            if (!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }
            var date = createTransactionDto.Date == default(DateTime) ? DateTime.Now : createTransactionDto.Date;

            var transactionEntity = new Transactions()
            {
                UserId = userId,
                Amount = createTransactionDto.Amount,
                Category = createTransactionDto.Category,
                TransactionType = createTransactionDto.TransactionType, // Fixed typo
                Date = date
            };

            applicationDbContext.Transactions.Add(transactionEntity);
            ViewBag.message = "Added...";
            applicationDbContext.SaveChanges();

            return RedirectToAction("create");
        }

        public IActionResult TransactionReport()
        {
            var data_inc = applicationDbContext.Transactions
        .Where(t => t.TransactionType == "Income")  // Filter for "Income"
        .GroupBy(t => t.Category)  // Group by Category
        .Select(g => new TransactionCategoryReportViewModel
        {
            Category = g.Key,
            TotalAmount = g.Sum(t => t.Amount)
        })
        .ToList();  // Execute the query and get the results

            var data_exp = applicationDbContext.Transactions
                .Where(t => t.TransactionType == "Expense")  // Filter for "Expense"
                .GroupBy(t => t.Category)  // Group by Category
                .Select(g => new TransactionCategoryReportViewModel
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToList();  // Execute the query and get the results

            var viewModel = new TransactionReportViewModel
            {
                IncomeData = data_inc,
                ExpenseData = data_exp
            };

            return View(viewModel);
        }









    }
}
