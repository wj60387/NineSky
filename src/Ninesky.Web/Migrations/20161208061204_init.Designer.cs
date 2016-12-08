using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ninesky.Web;
using Ninesky.Base;

namespace Ninesky.Web.Migrations
{
    [DbContext(typeof(NineskyDbContext))]
    [Migration("20161208061204_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ninesky.Base.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Order");

                    b.Property<int>("ParentId");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Type");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ninesky.Base.CategoryGeneral", b =>
                {
                    b.Property<int>("GeneralId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int?>("ContentOrder")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ContentView")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("View")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("GeneralId");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("CategoryGeneral");
                });

            modelBuilder.Entity("Ninesky.Base.CategoryLink", b =>
                {
                    b.Property<int>("LinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("LinkId");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("CategoryLink");
                });

            modelBuilder.Entity("Ninesky.Base.CategoryPage", b =>
                {
                    b.Property<int>("PageId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<string>("View")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("PageId");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("CategoryPage");
                });

            modelBuilder.Entity("Ninesky.Base.CategoryGeneral", b =>
                {
                    b.HasOne("Ninesky.Base.Category", "Category")
                        .WithOne("General")
                        .HasForeignKey("Ninesky.Base.CategoryGeneral", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ninesky.Base.CategoryLink", b =>
                {
                    b.HasOne("Ninesky.Base.Category")
                        .WithOne("Link")
                        .HasForeignKey("Ninesky.Base.CategoryLink", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ninesky.Base.CategoryPage", b =>
                {
                    b.HasOne("Ninesky.Base.Category", "Category")
                        .WithOne("Page")
                        .HasForeignKey("Ninesky.Base.CategoryPage", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
