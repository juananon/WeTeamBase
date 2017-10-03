using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeTeam.Patterns.EF
{
    public interface IContextContainer
    {
        IDbContext Context { get; set; }
    }
}
