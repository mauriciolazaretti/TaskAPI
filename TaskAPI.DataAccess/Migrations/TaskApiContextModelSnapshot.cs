﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskAPI.DataAccess;

#nullable disable

namespace TaskAPI.DataAccess.Migrations
{
    [DbContext(typeof(TaskApiContext))]
    partial class TaskApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskAPI.DataAccess.Entity.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskAPI.DataAccess.Entity.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskAPI.DataAccess.Entity.TaskHistoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FieldFrom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FieldModified")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FieldTo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("TaskEntityId")
                        .HasColumnType("integer");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskEntityId");

                    b.ToTable("TaskHistory");
                });

            modelBuilder.Entity("TaskAPI.DataAccess.Entity.TaskEntity", b =>
                {
                    b.HasOne("TaskAPI.DataAccess.Entity.ProjectEntity", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskAPI.DataAccess.Entity.TaskHistoryEntity", b =>
                {
                    b.HasOne("TaskAPI.DataAccess.Entity.TaskEntity", "TaskEntity")
                        .WithMany()
                        .HasForeignKey("TaskEntityId");

                    b.Navigation("TaskEntity");
                });

            modelBuilder.Entity("TaskAPI.DataAccess.Entity.ProjectEntity", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
