namespace AbcYazilim.OgrenciTakip.Data.Contexts
{
    using AbcYazilim.OgrenciTakip.Data.OgrenciTakipMigration;
    using AbcYazilim.OgrenciTakip.Model.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class OgrenciTakipContext : BaseDbContext<OgrenciTakipContext,Configuration>
    {
        // Your context has been configured to use a 'OgrenciTakipContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'AbcYazilim.OgrenciTakip.Data.Contexts.OgrenciTakipContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OgrenciTakipContext' 
        // connection string in the application configuration file.
        public OgrenciTakipContext() 
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public OgrenciTakipContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Okul> Okul { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}