using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTeam.Patterns.EF;

namespace WeTeam.Patterns.UnitOfWork
{
    public interface IUnitOfWork
    {
        IContextContainer ContextContainer { get; set; }
        Task Save();
        void Dispose();
    }
}
