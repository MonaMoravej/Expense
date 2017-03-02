using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Data.DbContexts;

namespace Data.Migrations.ExpenseDb
{
    [DbContext(typeof(Data.DbContexts.ExpenseDb))]
    [Migration("20170222064443_InitialExpenseDbAfterIdentityInitial")]
    partial class InitialExpenseDbAfterIdentityInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.Expense.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Color");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("OpenDate");

                    b.Property<decimal>("StartBalance");

                    b.Property<int>("Type");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts","Expense");
                });

            modelBuilder.Entity("Data.Entities.Expense.Budget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid>("CategoryId");

                    b.Property<Guid>("UserId");

                    b.Property<DateTime>("YearMonth");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets","Expense");
                });

            modelBuilder.Entity("Data.Entities.Expense.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("IconId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid?>("ParentId");

                    b.Property<int>("Type");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.HasIndex("ParentId");

                    b.HasIndex("UserId");

                    b.ToTable("Categories","Expense");
                });

            modelBuilder.Entity("Data.Entities.Expense.Icon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.ToTable("Icons","Expense");
                });

            modelBuilder.Entity("Data.Entities.Expense.Payee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Payees","Expense");
                });

            modelBuilder.Entity("Data.Entities.Expense.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountId");

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("CategoryId");

                    b.Property<DateTime>("Date");

                    b.Property<Guid?>("FromAccountId");

                    b.Property<Guid?>("PayeeId");

                    b.Property<Guid?>("ToAccountId");

                    b.Property<int>("Type");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("PayeeId");

                    b.HasIndex("ToAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions","Expense");
                });

           

            modelBuilder.Entity("Data.Entities.Expense.Account", b =>
                {
                    b.HasOne("Data.Entities.Identity.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.Expense.Budget", b =>
                {
                    b.HasOne("Data.Entities.Expense.Category", "Category")
                        .WithMany("Budgets")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entities.Identity.User", "User")
                        .WithMany("Budgets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.Expense.Category", b =>
                {
                    b.HasOne("Data.Entities.Expense.Icon", "Icon")
                        .WithMany("Categories")
                        .HasForeignKey("IconId");

                    b.HasOne("Data.Entities.Expense.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("Data.Entities.Identity.User", "User")
                        .WithMany("Categories")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Data.Entities.Expense.Payee", b =>
                {
                    b.HasOne("Data.Entities.Expense.Category", "Category")
                        .WithMany("Payees")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Entities.Identity.User", "User")
                        .WithMany("Payees")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Entities.Expense.Transaction", b =>
                {
                    b.HasOne("Data.Entities.Expense.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");

                    b.HasOne("Data.Entities.Expense.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Data.Entities.Expense.Account", "FromAccount")
                        .WithMany("FromTransactions")
                        .HasForeignKey("FromAccountId");

                    b.HasOne("Data.Entities.Expense.Payee", "Payee")
                        .WithMany("Transactions")
                        .HasForeignKey("PayeeId");

                    b.HasOne("Data.Entities.Expense.Account", "ToAccount")
                        .WithMany("ToTransactions")
                        .HasForeignKey("ToAccountId");

                    b.HasOne("Data.Entities.Identity.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
