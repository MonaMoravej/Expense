using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MyCodeCamp.Data;

namespace MyCodeCamp.Data.Migrations
{
    [DbContext(typeof(testDb))]
    partial class testDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyCodeCamp.Data.Entities.test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("ProgramId");

                    b.HasKey("Id");

                    b.HasIndex("ProgramId");

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
                    b.HasOne("MyCodeCamp.Data.Entities.test2", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
