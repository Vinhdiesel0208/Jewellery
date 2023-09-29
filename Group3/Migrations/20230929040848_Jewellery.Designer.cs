﻿// <auto-generated />
using System;
using Group3.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Group3.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230929040848_Jewellery")]
    partial class Jewellery
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lib.Blogs", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDay")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Lib.BrandMst", b =>
                {
                    b.Property<string>("Brand_ID")
                        .HasColumnType("nchar(10)");

                    b.Property<string>("Brand_Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Brand_ID");

                    b.ToTable("BrandMst");
                });

            modelBuilder.Entity("Lib.CatMst", b =>
                {
                    b.Property<string>("Cat_ID")
                        .HasColumnType("nchar(10)");

                    b.Property<string>("Cat_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Cat_ID");

                    b.ToTable("CatMst");
                });

            modelBuilder.Entity("Lib.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("DateSent")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ContactDetails");
                });

            modelBuilder.Entity("Lib.Gender", b =>
                {
                    b.Property<string>("Gender_Id")
                        .HasColumnType("nchar(10)");

                    b.Property<string>("Gender_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Gender_Id");

                    b.ToTable("Gender");
                });

            modelBuilder.Entity("Lib.ProdMst", b =>
                {
                    b.Property<string>("Prod_ID")
                        .HasColumnType("nchar(10)");

                    b.Property<string>("Brand_ID")
                        .IsRequired()
                        .HasColumnType("nchar(10)");

                    b.Property<string>("Cat_ID")
                        .IsRequired()
                        .HasColumnType("nchar(10)");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender_Id")
                        .IsRequired()
                        .HasColumnType("nchar(10)");

                    b.Property<int>("Image_id")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Prod_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Type_Id")
                        .IsRequired()
                        .HasColumnType("nchar(10)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Prod_ID");

                    b.HasIndex("Brand_ID");

                    b.HasIndex("Cat_ID");

                    b.HasIndex("Gender_Id");

                    b.HasIndex("Image_id");

                    b.HasIndex("Type_Id");

                    b.ToTable("ProdMst");
                });

            modelBuilder.Entity("Lib.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prod_ID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductProd_ID")
                        .HasColumnType("nchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("ProductProd_ID");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Lib.TypeMst", b =>
                {
                    b.Property<string>("Type_Id")
                        .HasColumnType("nchar(10)");

                    b.Property<string>("Type_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Type_Id");

                    b.ToTable("tbType");
                });

            modelBuilder.Entity("Lib.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Role")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "",
                            Email = "admin@gmail.com",
                            FullName = "admin",
                            Password = "123",
                            PhoneNumber = "",
                            Role = true
                        },
                        new
                        {
                            Id = 2,
                            Address = "",
                            Email = "test@gmail.com",
                            FullName = "test",
                            Password = "123",
                            PhoneNumber = "",
                            Role = false
                        });
                });

            modelBuilder.Entity("Lib.ProdMst", b =>
                {
                    b.HasOne("Lib.BrandMst", "BrandMst")
                        .WithMany()
                        .HasForeignKey("Brand_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lib.CatMst", "CatMst")
                        .WithMany()
                        .HasForeignKey("Cat_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lib.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("Gender_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lib.ProductImage", "ProductImage")
                        .WithMany()
                        .HasForeignKey("Image_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lib.TypeMst", "TypeMst")
                        .WithMany()
                        .HasForeignKey("Type_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BrandMst");

                    b.Navigation("CatMst");

                    b.Navigation("Gender");

                    b.Navigation("ProductImage");

                    b.Navigation("TypeMst");
                });

            modelBuilder.Entity("Lib.ProductImage", b =>
                {
                    b.HasOne("Lib.ProdMst", "Product")
                        .WithMany()
                        .HasForeignKey("ProductProd_ID");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
