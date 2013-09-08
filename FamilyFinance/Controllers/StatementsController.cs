using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;
using FamilyFinance.Models.ViewModel;

namespace FamilyFinance.Controllers
{
    public class StatementsController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransferRepository transferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StatementsController() : this(new AccountRepository(), new TransactionRepository(), new TransferRepository())
        {
        }

        public StatementsController(IAccountRepository accountRepository, 
                                    ITransactionRepository transactionRepository, 
                                    ITransferRepository transferRepository)
        {
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
            this.transferRepository = transferRepository;
        }


        public ActionResult Statement(int accountId, int year, int month)
        {
            var account = accountRepository.Find(accountId);
            var date = GetMonthYearDate(year, month);
            
            var transactions = transactionRepository.All.Where(x => x.AccountId == accountId && x.Date.Year == year && x.Date.Month == month).ToList();
            var transfersOut = transferRepository.All.Where(x => x.AccountId == accountId && x.Date.Year == year && x.Date.Month == month).ToList();
            var transfersIn = transferRepository.All.Where(x => x.ToAccountId == accountId && x.Date.Year == year && x.Date.Month == month).ToList();

            foreach (var transfer in transfersOut)
            {
                transfer.Amount = -transfer.Amount;
            }

            var activities = new List<IAccountActivity>();
            activities.AddRange(transactions);
            activities.AddRange(transfersOut);
            activities.AddRange(transfersIn);
            activities = activities.OrderByDescending(x => x.Date).ToList();

            var viewModel = new StatementViewModel
                {
                    AccountId = accountId,
                    AccountName = account.Name,
                    StatementDate = date,
                    PreviousMonth = month == 1 ? 12 : month-1,
                    PreviousYear = month == 1 ? year-1 : year,
                    NextMonth = month == 12 ? 1 : month+1,
                    NextYear = month == 12 ? year+1: year,
                    Activities = activities,
                    MoneyIn = activities.Sum(x => x.Amount > 0 ? x.Amount : 0),
                    MoneyOut = activities.Sum(x => x.Amount < 0 ? x.Amount : 0)
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
            
            var transactions = GetTransactions(accountId);
            var transfers = GetTransfers(accountId);

            var allStatements = transactions.Concat(transfers)
                .OrderByDescending(x=>x.Date)
                .GroupBy(x => x.Year & x.Month);

            foreach (var statementCollection in allStatements)
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
                Statements = statementViewModels,
                MoneyIn = transactions.Concat(transfers).Sum(x => x.Amount > 0 ? x.Amount : 0),
                MoneyOut = transactions.Concat(transfers).Sum(x => x.Amount < 0 ? x.Amount : 0)
            };

            return View(viewModel);
        }

        private List<StatementDto> GetTransfers(int accountId)
        {
            var transfersOut = transferRepository.All.Where(x => x.AccountId == accountId);
            List<StatementDto> transfers = transfersOut
                .Select(x => new StatementDto{Amount = -x.Amount, Month = x.Date.Month, Year = x.Date.Year, Date = x.Date}).ToList();
            
            var transfersIn = transferRepository.All.Where(x => x.ToAccountId == accountId);
            transfers = transfers.Concat(transfersIn
                .Select(x => new StatementDto { Amount = x.Amount, Month = x.Date.Month, Year = x.Date.Year, Date = x.Date })).ToList();
            
            return transfers;
        }

        private List<StatementDto> GetTransactions(int accountId)
        {
            var transactionsQueryable = transactionRepository.All.Where(x => x.AccountId == accountId);
            List<StatementDto> transactions = transactionsQueryable
                .Select(x => new StatementDto{Amount = x.Amount, Month = x.Date.Month, Year = x.Date.Year, Date = x.Date}).ToList();
            return transactions;
        }

        private class StatementDto
        {
            public double Amount { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
