using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FamilyFinance.Models.Domain;

namespace FamilyFinance.Models.Repository
{ 
    public class TransferRepository : ITransferRepository
    {
        FamilyFinanceContext context = new FamilyFinanceContext();

        public IQueryable<Transfer> All
        {
            get { return context.Transfers; }
        }

        public IQueryable<Transfer> AllIncluding(params Expression<Func<Transfer, object>>[] includeProperties)
        {
            IQueryable<Transfer> query = context.Transfers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Transfer Find(int id)
        {
            return context.Transfers.Find(id);
        }

        public void InsertOrUpdate(Transfer transfer)
        {
            if (transfer.Id == default(int)) {
                // New entity
                context.Transfers.Add(transfer);
            } else {
                // Existing entity
                context.Entry(transfer).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var transfer = context.Transfers.Find(id);
            context.Transfers.Remove(transfer);
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

    public interface ITransferRepository : IDisposable
    {
        IQueryable<Transfer> All { get; }
        IQueryable<Transfer> AllIncluding(params Expression<Func<Transfer, object>>[] includeProperties);
        Transfer Find(int id);
        void InsertOrUpdate(Transfer transfer);
        void Delete(int id);
        void Save();
    }
}