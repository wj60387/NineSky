﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ninesky.Web;
using Ninesky.Models;

namespace Ninesky.Web.Migrations
{
    [DbContext(typeof(NineskyDbContext))]
    [Migration("20161228135427_Module")]
    partial class Module
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ninesky.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Order");

                    b.Property<int>("ParentId");

                    b.Property<string>("ParentPath");

                    b.Property<int>("Target");

                    b.Property<int>("Type");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Ninesky.Models.CategoryGeneral", b =>
                {
                    b.Property<int>("GeneralId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int?>("ContentOrder")
                        .HasMaxLength(200);

                    b.Property<string>("ContentView")
                        .HasMaxLength(200);

                    b.Property<string>("Module")
                        .HasMaxLength(50);

                    b.Property<string>("View")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("GeneralId");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("CategoryGeneral");
                });

            modelBuilder.Entity("Ninesky.Models.CategoryLink", b =>
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

            modelBuilder.Entity("Ninesky.Models.CategoryPage", b =>
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

            modelBuilder.Entity("Ninesky.Models.CategoryGeneral", b =>
                {
                    b.HasOne("Ninesky.Models.Category", "Category")
                        .WithOne("General")
                        .HasForeignKey("Ninesky.Models.CategoryGeneral", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ninesky.Models.CategoryLink", b =>
                {
                    b.HasOne("Ninesky.Models.Category")
                        .WithOne("Link")
                        .HasForeignKey("Ninesky.Models.CategoryLink", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ninesky.Models.CategoryPage", b =>
                {
                    b.HasOne("Ninesky.Models.Category", "Category")
                        .WithOne("Page")
                        .HasForeignKey("Ninesky.Models.CategoryPage", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
