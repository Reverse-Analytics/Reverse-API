﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReverseAnalytics.Infrastructure.Persistence;

#nullable disable

namespace ReverseAnalytics.Infrastructure.Persistence.Migrations.SQLite
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("money")
                        .HasDefaultValue(0m);

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<double>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("REAL")
                        .HasDefaultValue(0.0);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(17)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<double>("QuantityInStock")
                        .HasColumnType("REAL");

                    b.Property<decimal>("SalePrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.Property<decimal?>("SupplyPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.Property<int>("UnitOfMeasurement")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Volume")
                        .HasPrecision(18, 2)
                        .HasColumnType("REAL");

                    b.Property<double?>("Weight")
                        .HasPrecision(18, 2)
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Product_Category", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Refund", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceivedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RefundDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SaleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.ToTable("Refund", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.RefundDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RefundId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("RefundId");

                    b.ToTable("Refund_Detail", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("Currency")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Receipt")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("SaleType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(3);

                    b.Property<string>("SoldBy")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalDiscount")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.Property<decimal>("TotalDue")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.Property<decimal>("TotalPaid")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Sale", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SaleDebt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ClosedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DebtDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("SaleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<decimal>("TotalDue")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.Property<decimal>("TotalPaid")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SaleId")
                        .IsUnique();

                    b.ToTable("Sale_Debt", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SaleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SaleId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleId");

                    b.ToTable("Sale_Detail", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("money")
                        .HasDefaultValue(0m);

                    b.Property<string>("Company")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(17)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Supplier", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReceivedBy")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SupplyDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalDue")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.Property<decimal>("TotalPaid")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Supply", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplyDebt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ClosedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<int>("SupplyId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalDue")
                        .HasPrecision(18, 2)
                        .HasColumnType("money");

                    b.Property<decimal>("TotalPaid")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SupplyId");

                    b.ToTable("Supply_Debt", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplyDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SupplyId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SupplyId");

                    b.ToTable("Supply_Detail", (string)null);
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Product", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Refund", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Sale", "Sale")
                        .WithMany("Refunds")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.RefundDetail", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Product", "Product")
                        .WithMany("RefundDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReverseAnalytics.Domain.Entities.Refund", "Refund")
                        .WithMany("RefundDetails")
                        .HasForeignKey("RefundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Refund");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Sale", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Customer", "Customer")
                        .WithMany("Sales")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SaleDebt", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Sale", "Sale")
                        .WithOne("SaleDebt")
                        .HasForeignKey("ReverseAnalytics.Domain.Entities.SaleDebt", "SaleId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SaleDetail", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Product", "Product")
                        .WithMany("SaleDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReverseAnalytics.Domain.Entities.Sale", "Sale")
                        .WithMany("SaleDetails")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supply", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Supplier", "Supplier")
                        .WithMany("Supplies")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplyDebt", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Supply", "Supply")
                        .WithMany("SupplyDebts")
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supply");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.SupplyDetail", b =>
                {
                    b.HasOne("ReverseAnalytics.Domain.Entities.Product", "Product")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ReverseAnalytics.Domain.Entities.Supply", "Supply")
                        .WithMany("SupplyDetails")
                        .HasForeignKey("SupplyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supply");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Product", b =>
                {
                    b.Navigation("PurchaseDetails");

                    b.Navigation("RefundDetails");

                    b.Navigation("SaleDetails");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Refund", b =>
                {
                    b.Navigation("RefundDetails");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Sale", b =>
                {
                    b.Navigation("Refunds");

                    b.Navigation("SaleDebt")
                        .IsRequired();

                    b.Navigation("SaleDetails");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Supplies");
                });

            modelBuilder.Entity("ReverseAnalytics.Domain.Entities.Supply", b =>
                {
                    b.Navigation("SupplyDebts");

                    b.Navigation("SupplyDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
