using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeTeam.Entities.FittingRooms;
using WeTeam.Entities.Test;
using WeTeam.Patterns.EF;

namespace WeTeam.Repository
{
    public class Context : DbContext, IDbContext
    {
        

        public DbSet<Dummy> Dummy { get; set; }
        public DbSet<FittingRoomBooking> FittingRoomBooking { get; set; }
        public DbSet<Entities.FittingRooms.FittingRoom> FittingRoom { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Context>(null);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
