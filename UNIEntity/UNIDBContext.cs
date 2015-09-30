using System.Data.Entity;

namespace UNIEntity
{
    public class UNIDBContext : DbContext
    {
        public UNIDBContext()
        {
            Database.SetInitializer<UNIDBContext>(new CreateDatabaseIfNotExists<UNIDBContext>());

            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        // masters
        public DbSet<CandidateDetail> CandidateDetails { get; set; }

        // views
        //public DbSet<VmPurchaseOrders> VmPurchaseOrderses { get; set; }

      
    }
}