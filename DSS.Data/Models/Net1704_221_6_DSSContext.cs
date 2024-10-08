﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DSS.Data.Models;

public partial class Net1704_221_6_DSSContext : DbContext
{
    public Net1704_221_6_DSSContext(DbContextOptions<Net1704_221_6_DSSContext> options)
        : base(options)
    {
    }
    public Net1704_221_6_DSSContext()
    {
        
    }
    public virtual DbSet<CompanyInformation> CompanyInformations { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DiamondShell> DiamondShells { get; set; }

    public virtual DbSet<ExtraDiamond> ExtraDiamonds { get; set; }

    public virtual DbSet<MainDiamond> MainDiamonds { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyInformation>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_Company_Information_1");

            entity.ToTable("Company_Information");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Hotline)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("hotline");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_id_primary");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<DiamondShell>(entity =>
        {
            entity.HasKey(e => e.DiamondShellId).HasName("diamond_shell_id_primary");

            entity.ToTable("Diamond_Shell");

            entity.Property(e => e.DiamondShellId).HasColumnName("diamondShell_id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Origin)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("origin");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<ExtraDiamond>(entity =>
        {
            entity.HasKey(e => e.ExtraDiamondId).HasName("extra_diamond_id_primary");

            entity.ToTable("Extra_Diamond");

            entity.Property(e => e.ExtraDiamondId).HasColumnName("extraDiamond_id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("title");
        });

        modelBuilder.Entity<MainDiamond>(entity =>
        {
            entity.HasKey(e => e.MainDiamondId).HasName("main_diamonds_id_primary");

            entity.ToTable("Main_Diamond");

            entity.Property(e => e.MainDiamondId).HasColumnName("mainDiamond_id");
            entity.Property(e => e.CaraWeight).HasColumnName("cara_weight");
            entity.Property(e => e.Clarity)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("clarity");
            entity.Property(e => e.Color)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("color");
            entity.Property(e => e.Cut).HasColumnName("cut");
            entity.Property(e => e.Describe)
                .HasColumnType("text")
                .HasColumnName("describe");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Origin)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("origin");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_id_primary");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.PaymentMethod)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("payment_status");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("total_amount");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("order_details_id_primary");

            entity.ToTable("Order_Detail");

            entity.Property(e => e.OrderDetailId).HasColumnName("orderDetail_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_id_primary");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.DiamondShellId).HasColumnName("diamond_shell_id");
            entity.Property(e => e.ExtraDiamondId).HasColumnName("extra_diamond_id");
            entity.Property(e => e.Fee).HasColumnName("fee");
            entity.Property(e => e.MainDiamondId).HasColumnName("main_diamond_id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.NumberExDiamond).HasColumnName("number_ex_diamond");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}