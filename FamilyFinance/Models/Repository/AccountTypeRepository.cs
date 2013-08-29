using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.Repository
{ 
    public class AccountTypeRepository : IAccountTypeRepository
    {
        FamilyFinanceContext context = new FamilyFinanceContext();

        public IQueryable<AccountType> All
        {
            get { return context.AccountTypes; }
        }

        public IQueryable<AccountType> AllIncluding(params Expression<Func<AccountType, object>>[] includeProperties)
        {
            IQueryable<AccountType> query = context.AccountTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AccountType Find(int id)
        {
            return context.AccountTypes.Find(id);
        }

        public void InsertOrUpdate(AccountType accounttype)
        {
            if (accounttype.Id == default(int)) {
                // New entity
                context.AccountTypes.Add(accounttype);
            } else {
                // Existing entity
                context.Entry(accounttype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var accounttype = context.AccountTypes.Find(id);
            context.AccountTypes.Remove(accounttype);
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

    public interface IAccountTypeRepository : IDisposable
    {
        IQueryable<AccountType> All { get; }
        IQueryable<AccountType> AllIncluding(params Expression<Func<AccountType, object>>[] includeProperties);
        AccountType Find(int id);
        void InsertOrUpdate(AccountType accounttype);
        void Delete(int id);
        void Save();
    }
}