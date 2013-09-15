using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FamilyFinance.Models.Domain;
using FamilyFinance.Models.Repository;
using FamilyFinance.Models.ViewModel;
using FamilyFinance.Models.service;

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
            var date = DateService.GetMonthYearDate(year, month);
            
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
            var accountActivitiesVM = activities.OrderByDescending(x => x.Date).Select(x => new AccountActivitiesViewModel(x)).ToList();

            var viewModel = new StatementViewModel
                {
                    AccountId = accountId,
                    AccountName = account.Name,
                    StatementDate = date,
                    PreviousMonth = DateService.PreviousMonth(year, month),
                    PreviousYear = DateService.PreviousYear(year, month),
                    NextMonth = DateService.NextMonth(year, month),
                    NextYear = DateService.NextYear(year, month),
                    Activities = accountActivitiesVM,
                    MoneyIn = activities.Sum(x => x.Amount > 0 ? x.Amount : 0),
                    MoneyOut = activities.Sum(x => x.Amount < 0 ? x.Amount : 0)
                };
            return View(viewModel);
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
                var @in = statementCollection.Where(x => x.Amount > 0).Sum(x=>x.Amount) ;
                var @out = statementCollection.Where(x => x.Amount < 0).Sum(x=>x.Amount) ;
                var sum = @in + @out;
                var year = statementCollection.First().Year;
                var month = statementCollection.First().Month;
                var date = statementCollection.First().Date;

                var statementOverviewViewModel = new StatementOverviewViewModel() 
                {
                    In = @in,
                    Out = @out,
                    Amount = sum, 
                    StatementDate = DateService.GetMonthYearDate(year, month),
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
