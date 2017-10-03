using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTeam.Entities.Test;
using WeTeam.Patterns.Repository;

namespace WeTeam.Repository.Test
{
    public interface IDummyRepository : IBaseRepository<Dummy>
    {
    }
}
