using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Test2DbContextWithShareData.DbContexts;

namespace Test2DbContextWithShareData.Migrations.Entity2Db
{
    [DbContext(typeof(Entity2DbContext))]
    [Migration("20170206151713_addEntity2")]
    partial class addEntity2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("two")
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Test2DbContextWithShareData.Entities.Entity1", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Entities1","one");
                });

            modelBuilder.Entity("Test2DbContextWithShareData.Entities.Entity2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Entity1Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Entity1Id");

                    b.ToTable("Entities2");
                });

            modelBuilder.Entity("Test2DbContextWithShareData.Entities.Entity2", b =>
                {
                    b.HasOne("Test2DbContextWithShareData.Entities.Entity1", "Entity1")
                        .WithMany("Entities2")
                        .HasForeignKey("Entity1Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
