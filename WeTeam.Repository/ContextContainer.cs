using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTeam.Patterns.EF;

namespace WeTeam.Repository
{
    public class ContextContainer : IContextContainer
    {
        public IDbContext Context { get; set; }
        
        public ContextContainer(IDbContext context)
        {
            this.Context = context;
        }
    }
}
