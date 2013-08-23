using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FamilyFinance.Areas.Admin.Models.Finance;

namespace FamilyFinance.Areas.Admin.Models
{ 
    public class TransactionRepository : ITransactionRepository
    {
        FamilyFinanceContext context = new FamilyFinanceContext();

        public IQueryable<Transaction> All
        {
            get { return context.Transactions; }
        }

        public IQueryable<Transaction> AllIncluding(params Expression<Func<Transaction, object>>[] includeProperties)
        {
            IQueryable<Transaction> query = context.Transactions;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Transaction Find(int id)
        {
            return context.Transactions.Find(id);
        }

        public void InsertOrUpdate(Transaction transaction)
        {
            if (transaction.Id == default(int)) {
                // New entity
                context.Transactions.Add(transaction);
            } else {
                // Existing entity
                context.Entry(transaction).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var transaction = context.Transactions.Find(id);
            context.Transactions.Remove(transaction);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface ITransactionRepository : IDisposable
    {
        IQueryable<Transaction> All { get; }
        IQueryable<Transaction> AllIncluding(params Expression<Func<Transaction, object>>[] includeProperties);
        Transaction Find(int id);
        void InsertOrUpdate(Transaction transaction);
        void Delete(int id);
        void Save();
    }
}