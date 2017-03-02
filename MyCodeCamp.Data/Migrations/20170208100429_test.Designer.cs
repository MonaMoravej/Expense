using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyCodeCamp.Data;

namespace MyCodeCamp.Data.Migrations
{
    [DbContext(typeof(testDb))]
    [Migration("20170208100429_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyCodeCamp.Data.Entities.test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("test_Id");

                    b.HasKey("Id");

                    b.HasIndex("test_Id");

                    b.ToTable("tests");
                });

            modelBuilder.Entity("MyCodeCamp.Data.Entities.test2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("test2s");
                });

            modelBuilder.Entity("MyCodeCamp.Data.Entities.test", b =>
                {
                    b.HasOne("MyCodeCamp.Data.Entities.test2", "t2")
                        .WithMany()
                        .HasForeignKey("test_Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
