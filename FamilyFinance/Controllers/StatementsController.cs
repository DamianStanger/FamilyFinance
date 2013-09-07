using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FamilyFinance.Models.Repository;
using FamilyFinance.Models.ViewModel;

namespace FamilyFinance.Controllers
{
    public class StatementsController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StatementsController() : this(new AccountRepository(), new TransactionRepository())
        {
        }

        public StatementsController(IAccountRepository accountRepository, TransactionRepository transactionRepository)
        {
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }


        public ActionResult Statement(int accountId, int year, int month)
        {
            var account = accountRepository.Find(accountId);
            var date = GetMonthYearDate(year, month);
            var viewModel = new StatementViewModel
                {
                    AccountId = accountId,
                    AccountName = account.Name,
                    StatementDate = date,
                    PreviousMonth = month == 1 ? 12 : month-1,
                    PreviousYear = month == 1 ? year-1 : year,
                    NextMonth = month == 12 ? 1 : month+1,
                    NextYear = month == 12 ? year+1: year,
                    Transactions =
                        transactionRepository.All.Where(
                            x => x.AccountId == accountId && x.Date.Year == year && x.Date.Month == month)
                };
            return View(viewModel);
        }

        private static string GetMonthYearDate(int year, int month)
        {
            return new DateTime(year, month, 1).ToLongDateString().Replace("01 ", "");
        }

        public ActionResult Statements(int accountId)
        {
            var statementViewModels = new List<StatementOverviewViewModel>();
            
            var transactions = transactionRepository.All.Where(x => x.AccountId == accountId)
                .Select(x => new {x.Amount, x.Date.Month, x.Date.Year, x.Date})
                .GroupBy(x => x.Year & x.Month);

            foreach (var statementCollection in transactions)
            {
                var sum = statementCollection.Sum(x => x.Amount);
                var year = statementCollection.First().Year;
                var month = statementCollection.First().Month;
                var date = statementCollection.First().Date;

                var statementOverviewViewModel = new StatementOverviewViewModel() 
                {
                    Amount = sum, 
                    StatementDate = GetMonthYearDate(year, month),
                    Date = date,
                    AccountId = accountId
                };

                statementViewModels.Add(statementOverviewViewModel);
            }

            var account = accountRepository.Find(accountId);
            var viewModel = new StatementsViewModel
            {
                AccountName = account.Name,
                Statements = statementViewModels
            };

            return View(viewModel);
        }
    }

    public class StatementsViewModel
    {
        public string AccountName { get; set; }
        public List<StatementOverviewViewModel> Statements { get; set; }
    }

    public class StatementOverviewViewModel
    {
        public int AccountId { get; set; }
        public double Amount { get; set; }
        public string StatementDate { get; set; }
        public DateTime Date { get; set; }
    }
}
