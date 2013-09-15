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
    public class AccountActivityController : Controller
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransferRepository _transferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AccountActivityController() : this(new TransactionRepository(), new TransferRepository())
        {
        }

        public AccountActivityController(ITransactionRepository transactionRepository, ITransferRepository transferRepository)
        {
            this._transactionRepository = transactionRepository;
            this._transferRepository = transferRepository;
        }

        public ActionResult MonthlyActivity(int year, int month)
        {
            var transactionActivities = GetTransactionActivities();
            var transferActivities = GetTransferActivities();
            var allActivities = transactionActivities.Concat(transferActivities)
                .Where(x=>x.Date.Month==month && x.Date.Year==year).OrderBy(x => x.Date);

            var accountActivityOverViewVeiwModel = CreateViewModel(allActivities, year, month);

            return View(accountActivityOverViewVeiwModel);
        }

        public ActionResult Index()
        {
            var transactionActivities = GetTransactionActivities();
            var transferActivities = GetTransferActivities();
            var allActivities = transactionActivities.Concat(transferActivities).OrderBy(x => x.Date);

            var accountActivityOverViewVeiwModel = CreateViewModel(allActivities, DateTime.Now.Year, DateTime.Now.Month);

            return View(accountActivityOverViewVeiwModel);
        }

        private static AccountActivityOverViewVeiwModel CreateViewModel(IOrderedEnumerable<AccountActivitiesViewModel> allActivities, int year, int month)
        {
            var runningTotal = 0d;
            foreach (var activity in allActivities)
            {
                if (!activity.IsTransfer)
                {
                    runningTotal += activity.Amount;
                }
                activity.RunningTotal = runningTotal;
            }

            var allActivitiesDateDesending = allActivities.Reverse().ToList();

            var accountActivityOverViewVeiwModel = new AccountActivityOverViewVeiwModel(allActivitiesDateDesending)
                {
                    MoneyIn = allActivities.Where(x => !x.IsTransfer).Sum(x => x.Amount > 0 ? x.Amount : 0),
                    MoneyOut = allActivities.Where(x => !x.IsTransfer).Sum(x => x.Amount < 0 ? x.Amount : 0),
                    PreviousMonth = DateService.PreviousMonth(year, month),
                    PreviousYear = DateService.PreviousYear(year, month),
                    NextMonth = DateService.NextMonth(year, month),
                    NextYear = DateService.NextYear(year, month),
                    StatementDate = DateService.GetMonthYearDate(year, month)
                };
            return accountActivityOverViewVeiwModel;
        }

        private IEnumerable<AccountActivitiesViewModel> GetTransferActivities()
        {
            var transfers = _transferRepository.All;
            var transferActivities = new List<AccountActivitiesViewModel>();
            foreach (var transfer in transfers)
            {
                transferActivities.Add(mapToAccountActivity(transfer));
            }
            return transferActivities;
        }

        private IEnumerable<AccountActivitiesViewModel> GetTransactionActivities()
        {
            var transactions = _transactionRepository.All;
            var transactionActivities = new List<AccountActivitiesViewModel>();
            foreach (var transaction in transactions)
            {
                transactionActivities.Add(mapToAccountActivity(transaction));
            }
            return transactionActivities;
        }

        private AccountActivitiesViewModel mapToAccountActivity(IAccountActivity accountActivity)
        {
            return new AccountActivitiesViewModel(accountActivity);
        }
    }
}
