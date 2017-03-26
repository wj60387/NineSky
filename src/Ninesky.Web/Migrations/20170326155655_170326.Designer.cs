using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ninesky.Web;
using Ninesky.Models;

namespace Ninesky.Web.Migrations
{
    [DbContext(typeof(NineskyDbContext))]
    [Migration("20170326155655_170326")]
    partial class _170326
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ninesky.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChildNum");

                    b.Property<int>("ContentModuleId");

                    b.Property<int>("ContentOrder");

                    b.Property<string>("ContentView")
                        .HasMaxLength(200);

                    b.Property<string>("Creater")
                        .HasMaxLength(255);

                    b.Property<int>("Depth");

                    b.Property<string>("Description");

                    b.Property<string>("LinkUrl")
                        .HasMaxLength(500);

                    b.Property<string>("Meta_Description")
                        .HasMaxLength(255);

                    b.Property<string>("Meta_Keywords")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Order");

                    b.Property<int>("PageSize");

                    b.Property<int>("ParentId");

                    b.Property<string>("ParentPath");

                    b.Property<string>("PicUrl")
                        .HasMaxLength(255);

                    b.Property<bool>("ShowOnMenu")
                        .HasMaxLength(255);

                    b.Property<int>("Target");

                    b.Property<int>("Type");

                    b.Property<string>("View")
                        .HasMaxLength(255);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ninesky.Models.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Controller")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<bool>("Enabled");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ModuleId");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Ninesky.Models.ModuleOrder", b =>
                {
                    b.Property<int>("ModuleOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ModuleId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Order");

                    b.HasKey("ModuleOrderId");

                    b.HasIndex("ModuleId");

                    b.ToTable("ModuleOrder");
                });

            modelBuilder.Entity("Ninesky.Models.ModuleOrder", b =>
                {
                    b.HasOne("Ninesky.Models.Module", "Module")
                        .WithMany("ModuleOrders")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
