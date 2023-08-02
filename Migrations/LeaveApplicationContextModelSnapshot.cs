﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Practice.Models;

#nullable disable

namespace Practice.Migrations
{
    [DbContext(typeof(LeaveApplicationContext))]
    partial class LeaveApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Practice.Models.Document", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Emplid")
                        .HasColumnType("int")
                        .HasColumnName("emplid");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("documentfile");

                    b.Property<string>("documentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("documentname");

                    b.HasKey("id");

                    b.ToTable("EmployeeData", (string)null);
                });

            modelBuilder.Entity("Practice.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Companyname")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("companyname")
                        .IsFixedLength();

                    b.Property<string>("Department")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("department")
                        .IsFixedLength();

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("email")
                        .IsFixedLength();

                    b.Property<string>("Firstname")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("firstname")
                        .IsFixedLength();

                    b.Property<string>("Lastname")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("lastname")
                        .IsFixedLength();

                    b.Property<int?>("Manager")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("password")
                        .IsFixedLength();

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Manager");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("Practice.Models.LeaveQuotum", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("Emplid")
                        .HasColumnType("int")
                        .HasColumnName("emplid");

                    b.Property<int>("Remainingleave")
                        .HasColumnType("int")
                        .HasColumnName("remainingleave");

                    b.Property<int>("Totalleave")
                        .HasColumnType("int")
                        .HasColumnName("totalleave");

                    b.Property<int>("Usedleave")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Emplid");

                    b.ToTable("LeaveQuota");
                });

            modelBuilder.Entity("Practice.Models.LeaveStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Emplid")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date")
                        .HasColumnName("from_date");

                    b.Property<int>("Leaveid")
                        .HasColumnType("int")
                        .HasColumnName("leaveid");

                    b.Property<string>("Leavetype")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("leavetype");

                    b.Property<int>("Manager")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("reason");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("status");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("date")
                        .HasColumnName("to_date");

                    b.HasKey("Id");

                    b.HasIndex("Leaveid");

                    b.ToTable("LeaveStatus", (string)null);
                });

            modelBuilder.Entity("Practice.Models.LeaveTable", b =>
                {
                    b.Property<int>("Leaveid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("leaveid");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Leaveid"));

                    b.Property<int>("Employeeid")
                        .HasColumnType("int")
                        .HasColumnName("employeeid");

                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.HasKey("Leaveid");

                    b.ToTable("LeaveTable", (string)null);
                });

            modelBuilder.Entity("Practice.Models.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Companyname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("companyname")
                        .IsFixedLength();

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("department")
                        .IsFixedLength();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("email")
                        .IsFixedLength();

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("firstname")
                        .IsFixedLength();

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("lastname")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("password")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Manager", (string)null);
                });

            modelBuilder.Entity("Practice.Models.MessageModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Message");

                    b.Property<string>("ReadorNot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ReadorNot");

                    b.Property<int>("receiverid")
                        .HasColumnType("int")
                        .HasColumnName("receiverid");

                    b.Property<int>("senderid")
                        .HasColumnType("int")
                        .HasColumnName("senderid");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("timestamp");

                    b.HasKey("id");

                    b.ToTable("Message", (string)null);
                });

            modelBuilder.Entity("Practice.Models.Profile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Emplid")
                        .HasColumnType("int")
                        .HasColumnName("emplid");

                    b.Property<byte[]>("img")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("profile");

                    b.HasKey("id");

                    b.ToTable("EmployeeProfile", (string)null);
                });

            modelBuilder.Entity("Practice.Models.UserIdentity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ConnectionId");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("Userid");

                    b.HasKey("id");

                    b.ToTable("Connections", (string)null);
                });

            modelBuilder.Entity("Practice.Models.Employee", b =>
                {
                    b.HasOne("Practice.Models.Manager", "ManagerNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("Manager")
                        .HasConstraintName("FK_Employee_Manager");

                    b.Navigation("ManagerNavigation");
                });

            modelBuilder.Entity("Practice.Models.LeaveQuotum", b =>
                {
                    b.HasOne("Practice.Models.Employee", "Empl")
                        .WithMany("LeaveQuota")
                        .HasForeignKey("Emplid")
                        .IsRequired()
                        .HasConstraintName("FK_LeaveQuota_Employee");

                    b.Navigation("Empl");
                });

            modelBuilder.Entity("Practice.Models.LeaveStatus", b =>
                {
                    b.HasOne("Practice.Models.LeaveTable", "Leave")
                        .WithMany()
                        .HasForeignKey("Leaveid")
                        .IsRequired()
                        .HasConstraintName("FK_LeaveStatus_LeaveTable");

                    b.Navigation("Leave");
                });

            modelBuilder.Entity("Practice.Models.Employee", b =>
                {
                    b.Navigation("LeaveQuota");
                });

            modelBuilder.Entity("Practice.Models.Manager", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
