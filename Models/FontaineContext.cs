using Microsoft.EntityFrameworkCore;

namespace FontaineVerificationProject.Models
{
    public partial class FontaineContext : DbContext
    {
        public FontaineContext()
        {
        }

        public FontaineContext(DbContextOptions<FontaineContext> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Verification> Verification { get; set; }
        public virtual DbSet<SorDetail> SorDetail {get; set;}
        public virtual DbSet<vGetChassisNumbers> vGetChassisNumbers {get; set;}      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=sage200-2016\\sql2014;Database=Fontaine;User Id=dash;Password=Chatburn441977;");
                optionsBuilder.UseSqlServer("Server=SYSPRO1;Database=VerificationDash;User Id=sa;Password=mbl1175;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Verification>(entity =>
            {
                entity.HasKey(e => e.VerificationID);
                entity.Property(e => e.VerificationID).HasColumnName("VerificationID");

                entity.Property(e => e.ChassisNo);

                entity.Property(e => e.V1UserName);             

                entity.Property(e => e.V1DateTime);
                entity.Property(e => e.V1Passed);
                entity.Property(e => e.V2UserName);
                entity.Property(e => e.V2DateTime);
                entity.Property(e => e.V2Passed);
                entity.Property(e => e.SalesOrder);
                entity.Property(e => e.CustomerStockCode);
                entity.Property(e => e.StockDescription);
                entity.Property(e => e.DispatchDate);
                entity.Property(e => e.StockCode);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(128);

                entity.Property(e => e.Salt).HasMaxLength(128);
            });

            modelBuilder.Entity<SorDetail>(entity =>
            {
                entity.HasKey(u => new { u.SalesOrder, u.SalesOrderLine });   
                entity.Property(e => e.SalesOrder).HasColumnName("SalesOrder");
                entity.Property(e => e.SalesOrderLine).HasColumnName("SalesOrderLine");
                
                entity.Property(e => e.MStockCode); 
                entity.Property(e => e.MCusSupStkCode);
                entity.Property(e => e.NComment);
                entity.Property(e => e.MStockDes);                  
                entity.Property(e => e.MLineShipDate);
                entity.Property(e => e.MWarehouse);
                entity.Property(e => e.NCommentFromLin);             
            });

            modelBuilder.Entity<vGetChassisNumbers>(entity =>
            {
                entity.HasKey(u => new { u.SalesOrder, u.SalesOrderLine });   
                entity.Property(e => e.SalesOrder).HasColumnName("SalesOrder");
                entity.Property(e => e.SalesOrderLine).HasColumnName("SalesOrderLine"); 

                entity.Property(e => e.MStockCode); 
                entity.Property(e => e.MCusSupStkCode);
                entity.Property(e => e.MStockDes);
                entity.Property(e => e.MLineShipDate);
                entity.Property(e => e.ChassisNumber);
            });

        }
    }
}

