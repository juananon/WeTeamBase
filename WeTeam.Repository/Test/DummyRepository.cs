using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTeam.Entities.Test;
using WeTeam.Patterns.Repository;
using WeTeam.Patterns.UnitOfWork;

namespace WeTeam.Repository.Test
{
    public class DummyRepository : BaseRepository<Dummy>, IDummyRepository
    {
        public DummyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        
    }
}
