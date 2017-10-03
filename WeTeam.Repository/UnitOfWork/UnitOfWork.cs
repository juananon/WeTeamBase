using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTeam.Patterns.EF;
using WeTeam.Patterns.UnitOfWork;

namespace WeTeam.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IContextContainer ContextContainer { get; set; }
        private bool _disposed = false;

        public UnitOfWork(IContextContainer context)
        {
            ContextContainer = context;
        }

        public async Task Save()
        {
            await ContextContainer.Context.SaveChangesAsync();
        }

        public void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    ContextContainer.Context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
