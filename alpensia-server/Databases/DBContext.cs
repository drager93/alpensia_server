using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace alpensia_server.Databases
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Board> Board { get; set; }

        //startup.cs 파일에서 부르고 있기 때문에 주석
        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseMySql("Server=jinjoosoft.io;Port=43306;User=root;Password=naverscv123;Database=alpensia");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>(entity =>
            {
                entity.HasKey(e => e.BrdIndex)
                    .HasName("PRIMARY");

                entity.Property(e => e.BrdIndex)
                    .HasColumnName("BRD_Index")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BrdDetailImg)
                    .IsRequired()
                    .HasColumnName("BRD_DetailImg")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.BrdEndDate)
                    .HasColumnName("BRD_EndDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.BrdIsMain)
                    .HasColumnName("BRD_IsMain")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.BrdMainImg)
                    .IsRequired()
                    .HasColumnName("BRD_MainImg")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.BrdStartDate)
                    .HasColumnName("BRD_StartDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.BrdTitle)
                    .IsRequired()
                    .HasColumnName("BRD_Title")
                    .HasColumnType("varchar(45)");

                //entity.Property(e => e.BrdViewCount)
                //    .HasColumnName("BRD_ViewCount")
                //    .HasColumnType("int(11)");
            });
        }
    }
}